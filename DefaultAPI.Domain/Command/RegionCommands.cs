using DefaultAPI.Domain.Dto;
using DefaultAPI.Domain.Entities;
using DefaultAPI.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DefaultAPI.Domain.Command
{
    public class RegionCreateCommand : IRequest<ResultReturned>
    {
        public Region Region { get; set; }
    }

    public class RegionUpdateCommand : IRequest<ResultReturned>
    {
        public long? Id { get; set; }
        public Region Region { get; set; }
    }

    public class RegionDeleteCommand : IRequest<ResultReturned>
    {
        public long? Id { get; set; }
        public bool IsDeletePhysical { get; set; }
    }
}
