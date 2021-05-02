using AutoMapper;
using DefaultAPI.Application.Interfaces;
using DefaultAPI.Controllers.Base;
using DefaultAPI.Domain;
using DefaultAPI.Domain.Entities;
using DefaultAPI.Domain.Models;
using DefaultAPI.Infra.CrossCutting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DefaultAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class GeneralController : BaseController
    {
        private readonly ICepService _cepsService;
        private readonly IStatesService _statesService;
        private readonly IRegionService _regionService;

        public GeneralController(IMapper mapper, IGeneralService generalService, ICepService cepsService, IStatesService statesService, IRegionService regionService) : base(mapper, generalService)
        {
            _cepsService = cepsService;
            _statesService = statesService;
            _regionService = regionService;
        }

        [HttpGet("v1/export2Zip/{directory}/{typeFile}")]
        public async Task<IActionResult> Export2Zip(string directory, int typeFile = 2)
        {
            ExtensionMethods extensionMethods = new ExtensionMethods();
            MemoryStream memoryStream = await _generalService.Export2Zip(directory, typeFile);
            return File(await Task.FromResult(memoryStream.ToArray()), extensionMethods.GetMemoryStreamType(typeFile), $"Archive.{extensionMethods.GetMemoryStreamExtension(typeFile)}");
        }

        [HttpGet("v1/backup/{directory}")]
        public async Task<IActionResult> Backup(string directory)
        {
            ResultReturned result = await _generalService.RunSqlBackup(directory);

            if (result.Result)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("v1/getCep/{cep}/{refreshCep}")]
        public async Task<IActionResult> GetCep(string cep, bool refreshCep)
        {
            try
            {
                Ceps modelCep = new Ceps();
                if (refreshCep && !string.IsNullOrWhiteSpace(cep))
                {
                    modelCep = await _cepsService.GetByCep(cep);
                    RequestData requestData = await _generalService.RequestDataToExternalAPI(string.Format("{0}{1}{2}", Constants.urlToGetCep, cep, "/json/"));
                    if (requestData.IsSuccess)
                    {
                        Ceps modelCepAPI = JsonConvert.DeserializeObject<Ceps>(requestData.Data);
                        if (modelCepAPI != null)
                        {
                            modelCepAPI.IdEstado = await _statesService.GetStateByInitials(modelCepAPI.Uf);
                            await _cepsService.RefreshCep(cep, modelCep, modelCepAPI);
                            modelCep = await _cepsService.GetByCep(cep);
                        }
                    }
                }
                else
                {
                    modelCep = await _cepsService.GetByCep(cep);
                }
                return Ok(modelCep);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultReturned(false, string.Format(Constants.ExceptionRequestAPI, Constants.urlToGetCep)));
            }
        }

        [HttpGet("v1/getStates/{refreshStates}")]
        public async Task<IActionResult> RrefreshStates(bool refreshEstados)
        {
            try
            {
                List<Region> listRegion = await _regionService.GetAllRegion();
                List<States> listStates = await _statesService.GetAllStates();
                if (refreshEstados)
                {
                    if (listStates != null && listStates.Count > 0)
                    {
                        RequestData requestData = await _generalService.RequestDataToExternalAPI(Constants.urlToGetStates);
                        if (requestData.IsSuccess)
                        {
                            List<States> listStatesAPI = JsonConvert.DeserializeObject<List<States>>(requestData.Data);
                            if (listStatesAPI != null && listStatesAPI.Count > 0)
                            {
                                await _regionService.RefreshRegion(listStatesAPI);
                                await _statesService.RefreshStates(listStates, listStatesAPI, listRegion);
                                listStates = await _statesService.GetAllStates();
                            }
                        }
                    }
                }
                return Ok(listStates);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultReturned(false, string.Format(Constants.ExceptionRequestAPI, Constants.urlToGetStates)));
            }
        }

        [HttpGet("v1/addRegions")]
        public async Task<IActionResult> AddRegions()
        {
            try
            {
                List<Region> list = new List<Region>();
                RequestData requestData = await _generalService.RequestDataToExternalAPI(Constants.urlToGetStates);
                if (requestData.IsSuccess)
                {
                    List<States> listStatesAPI = JsonConvert.DeserializeObject<List<States>>(requestData.Data);
                    if (listStatesAPI != null && listStatesAPI.Count > 0)
                    {
                        list = listStatesAPI.Select(x => new
                        {
                            Nome = x.Regiao.Nome,
                            Sigla = x.Regiao.Sigla,
                        }).Distinct().Select(z => new Region()
                        {
                            Nome = z.Nome,
                            IsActive = true,
                            Sigla = z.Sigla,
                            CreatedTime = DateTime.Now
                        }).ToList();

                        await _regionService.AddRegions(list);
                    }
                }

                return Ok(list);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultReturned(false, string.Format(Constants.ExceptionRequestAPI, Constants.urlToGetStates)));
            }
        }

        [HttpGet("v1/addStates")]
        public async Task<IActionResult> AddStates()
        {
            try
            {
                List<Region> listRegion = await _regionService.GetAllRegion();
                List<States> listStates = new List<States>();
                RequestData requestData = await _generalService.RequestDataToExternalAPI(Constants.urlToGetStates);
                if (requestData.IsSuccess)
                {
                    List<States> listStatesAPI = JsonConvert.DeserializeObject<List<States>>(requestData.Data);
                    if (listStatesAPI != null && listStatesAPI.Count > 0 && listRegion != null && listRegion.Count > 0)
                    {
                        listStates = listStatesAPI.Select(x => new States()
                        {
                            CreatedTime = DateTime.Now,
                            IsActive = true,
                            Nome = x.Nome,
                            Sigla = x.Sigla,
                            IdRegiao = listRegion.FirstOrDefault(z => z.Sigla == x.Regiao.Sigla).Id.Value
                        }).ToList();

                        await _statesService.AddStates(listStates);
                    }
                }

                return Ok(listStates);
            }
            catch (Exception ex)
            {
                return BadRequest(new ResultReturned(false, string.Format(Constants.ExceptionRequestAPI, Constants.urlToGetStates)));
            }
        }
    }
}
