using System.Collections.Generic;

namespace API.Entities
{
    public class Customer
    {
         public long Amount { get; set; }
        public string Currency { get; set; }
        public string Source { get; set; }
        public string ReceiptEmail { get; set; }

        public string Region{ get; set; }
        public string CardNumber{ get; set; }
        public string Date{ get; set; }
        public string Cvc{ get; set; }
    }
}