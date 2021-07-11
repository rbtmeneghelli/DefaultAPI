using DefaultAPI.Application.Interfaces;
using DefaultAPI.Domain.Command;
using DefaultAPI.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DefaultAPI.Application.Handlers
{
    public class RegionCommandsHandler : IRequestHandler<RegionCreateCommand, ResultReturned>, IRequestHandler<RegionUpdateCommand, ResultReturned>, IRequestHandler<RegionDeleteCommand, ResultReturned>
    {
        private IRegionService _regionService;

        public RegionCommandsHandler(IRegionService regionService)
        {
            _regionService = regionService;
        }

        public Task<ResultReturned> Handle(RegionCreateCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<ResultReturned> Handle(RegionUpdateCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<ResultReturned> Handle(RegionDeleteCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
