using System;

namespace Api.Boleto.Domain.Entities
{
    public class BoletoEntity
    {
        public string linhaDigitavel { get; set; }
        public string barCode { get; set; }
        public double valorBoleto { get; set; }
        public DateTime vencimentoBoleto { get; set; }
    }
}
