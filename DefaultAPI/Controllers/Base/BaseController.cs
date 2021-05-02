using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DefaultAPI.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DefaultAPI.Controllers.Base
{
    [Produces("application/json")]
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        protected readonly IMapper _mapper;
        protected readonly IGeneralService _generalService;

        protected BaseController(IMapper mapper, IGeneralService generalService)
        {
            _mapper = mapper;
            _generalService = generalService;
        }
    }
}
