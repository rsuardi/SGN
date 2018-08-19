using System;
namespace SGN.Client.Models
{
    public class OrderFrameViewModel
    {
        
        public string Address { get; set; } = String.Empty;

        public OrderFrameViewModel(string Address)
        {
            this.Address = Address;
        }
    }
}
