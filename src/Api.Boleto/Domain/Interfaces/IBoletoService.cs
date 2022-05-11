using Api.Boleto.Domain.Entities;

namespace Api.Boleto.Domain.Interfaces
{
    public interface IBoletoService
    {
        BoletoEntity GetInformacoesBoleto(string linhaDigitavel);
    }
}
