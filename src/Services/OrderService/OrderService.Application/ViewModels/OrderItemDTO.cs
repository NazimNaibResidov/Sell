﻿namespace OrderService.Application.ViewModels
{
    public class OrderItemDTO
    {
        public string ProductName { get; set; }

        public decimal Unitprice { get; set; }
        public int Untis { get; set; }

        public string PictureUrl { get; set; }
    }
}