using DefaultAPI.Domain.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace DefaultAPI.Domain.Entities
{
    public class Ceps : BaseEntity
    {
        public string Cep { get; set; }
        public string Logradouro { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Localidade { get; set; }
        public string Uf { get; set; }
        public string Ibge { get; set; }
        public string Gia { get; set; }
        public string Ddd { get; set; }
        public string Siafi { get; set; }
        public States Estado { get; set; }
        public long IdEstado { get; set; }

        public Ceps()
        {

        }

        public Ceps(long id, string cep, Ceps modelCepAPI, long idEstado, DateTime? createdTime)
        {
            Id = id;
            Bairro = modelCepAPI.Bairro;
            Cep = cep;
            Complemento = modelCepAPI.Complemento;
            Ddd = modelCepAPI.Ddd;
            Gia = modelCepAPI.Gia;
            UpdateTime = DateTime.Now;
            Ibge = modelCepAPI.Ibge;
            Localidade = modelCepAPI.Localidade;
            Logradouro = modelCepAPI.Logradouro;
            Siafi = modelCepAPI.Siafi;
            Uf = modelCepAPI.Uf;
            IdEstado = idEstado;
            CreatedTime = createdTime;
        }

        public Ceps(string cep, Ceps modelCepAPI)
        {
            Bairro = modelCepAPI.Bairro;
            Cep = cep;
            Complemento = modelCepAPI.Complemento;
            Ddd = modelCepAPI.Ddd;
            Gia = modelCepAPI.Gia;
            UpdateTime = DateTime.Now;
            Ibge = modelCepAPI.Ibge;
            Localidade = modelCepAPI.Localidade;
            Logradouro = modelCepAPI.Logradouro;
            Siafi = modelCepAPI.Siafi;
            Uf = modelCepAPI.Uf;
            IsActive = true;
            CreatedTime = DateTime.Now;
            IdEstado = modelCepAPI.IdEstado;
        }
    }
}
