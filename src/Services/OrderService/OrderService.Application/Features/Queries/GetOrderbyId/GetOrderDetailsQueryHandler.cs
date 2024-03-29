﻿using AutoMapper;
using MediatR;
using OrderService.Application.Interfaces.Repostory;
using OrderService.Application.ViewModels;

namespace OrderService.Application.Features.Queries.GetOrderbyId
{
    public class GetOrderDetailsQueryHandler : IRequestHandler<GetOrderDetailsQuery, OrderDatialViewModel>
    {
        private IOrderRepsotory orderRepsotory;
        private readonly IMapper mapper;

        public GetOrderDetailsQueryHandler(IOrderRepsotory orderRepsotory, IMapper mapper)
        {
            this.orderRepsotory = orderRepsotory ?? throw new NotImplementedException(nameof(orderRepsotory));
            this.mapper = mapper;
        }

        public async Task<OrderDatialViewModel> Handle(GetOrderDetailsQuery request, CancellationToken cancellationToken)
        {
            var order = await orderRepsotory.GetByIdAsyc(request.OrderId, i => i.OrderItems);
            var result = mapper.Map<OrderDatialViewModel>(order);
            return result;
        }
    }
}