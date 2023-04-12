using System;

namespace DiscountManagement.Application.Contracts.CustomerDiscountAggregate
{
    public class CustomerDiscountViewModel
    {
        public long Id { get; set; }
        public int DiscountRate { get; set; }
        public DateTime StartDate { get; set; }
        public string StartDateShamsi { get; set; }
        public DateTime EndDate { get; set; }
        public string EndDateShamsi { get; set; }
        public string Reason { get; set; }
        public long ProductId { get; set; }
        public string Product { get; set; }
        public string CreationDate { get; set; }
    }
}