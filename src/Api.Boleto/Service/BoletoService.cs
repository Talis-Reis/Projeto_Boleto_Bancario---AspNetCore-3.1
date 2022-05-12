using System;
using System.Globalization;
using Api.Boleto.Domain.Entities;
using Api.Boleto.Domain.Interfaces;

namespace Api.Boleto.Service
{
    public class BoletoService : IBoletoService
    {
        public BoletoEntity GetInformacoesBoleto(string linhaDigitavel)
        {
            try
            {
                if (linhaDigitavel.Length == 47)
                {
                    // Posição de 01 a 03: identificação do banco;-
                    // Posição 04: código de moeda;-
                    // Posição 05 a 09: cinco primeiras posições do campo livre (posições 20 a 24 do código de barras);-
                    // Posição 10: dígito verificador do primeiro campo;-
                    // Posição 11 a 20: 6ª a 15ª posições do campo livre (posições 25 a 34 do código de barras);-
                    // Posição 21: dígito verificador do segundo campo;-
                    // Posição 22 a 31: 16ª a 25ª posições do campo livre (posições 35 a 44 do código de barras);-
                    // Posição 32: dígito verificador do terceiro campo;-
                    // Posição 33: dígito verificador geral (posição 5 do código de barras);-
                    // Posição 34 a 47: fator de vencimento (posições 6 a 9 do código de barras);-
                    // Posição 38 a 47: valor nominal do boleto (posições 10 a 19 do código de barras).-

                    // LINHA DIGITAVEL = 21290001192110001210904475617405975870000002000

                    //212
                    //9
                    //00011
                    //9
                    //2110001210
                    //9
                    //0447561740
                    //5
                    //9
                    //7587
                    //0000002000

                    Int64 position1 = Convert.ToInt64(linhaDigitavel.Substring(0, 3));
                    Int64 position2 = Convert.ToInt64(linhaDigitavel.Substring(3, 1));
                    Int64 position3 = Convert.ToInt64(linhaDigitavel.Substring(4, 5));
                    Int64 position4 = Convert.ToInt64(linhaDigitavel.Substring(9, 1));
                    Int64 position5 = Convert.ToInt64(linhaDigitavel.Substring(10, 10));
                    Int64 position6 = Convert.ToInt64(linhaDigitavel.Substring(20, 1));
                    Int64 position7 = Convert.ToInt64(linhaDigitavel.Substring(21, 10));
                    Int64 position8 = Convert.ToInt64(linhaDigitavel.Substring(31, 1));
                    Int64 position9 = Convert.ToInt64(linhaDigitavel.Substring(32, 1));
                    Int64 position10 = Convert.ToInt64(linhaDigitavel.Substring(33, 4));
                    Int64 position11 = Convert.ToInt64(linhaDigitavel.Substring(37, 8));
                    Int64 position12 = Convert.ToInt64(linhaDigitavel.Substring(45, 2));


                    string barCodeSemDigitoVerificador = (linhaDigitavel.Substring(0, 3) + linhaDigitavel.Substring(3, 1) + linhaDigitavel.Substring(33, 4) + linhaDigitavel.Substring(37, 8) + linhaDigitavel.Substring(45, 2) + linhaDigitavel.Substring(4, 5) + linhaDigitavel.Substring(10, 10) + linhaDigitavel.Substring(21, 10));

                    int soma = 0;
                    int mod = -1;
                    int dv = -1;
                    int peso = 2;
                    int DvGeral = -1;

                    for (int i = barCodeSemDigitoVerificador.Length - 1; i != -1; i--)
                    {
                        int ch = Convert.ToInt32(barCodeSemDigitoVerificador[i].ToString());
                        soma += ch * peso;
                        if (peso < 9)
                        {
                            peso += 1;
                        }
                        else
                        {
                            peso = 2;
                        }
                    }

                    mod = 11 - (soma % 11);
                    if (mod == 0 || mod == 1 || mod > 9)
                    {
                        dv = 1;
                    }
                    else
                    {
                        dv = mod;
                    }
                    DvGeral = dv;

                    string barCode = barCodeSemDigitoVerificador.Substring(0, 4) + DvGeral.ToString() + barCodeSemDigitoVerificador.Substring(4, 39);

                    DateTime dataBase = new DateTime(1997, 10, 07);
                    DateTime dataVenctoBoleto = dataBase.AddDays(position10);

                    string valor = linhaDigitavel.Substring(37, 8) + "," + linhaDigitavel.Substring(45, 2);
                    double valorcorrigido = double.Parse(valor);
                    BoletoEntity boletoInfos = new BoletoEntity();

                    boletoInfos.linhaDigitavel = linhaDigitavel;
                    boletoInfos.barCode = barCode;
                    boletoInfos.valorBoleto = valorcorrigido;
                    boletoInfos.vencimentoBoleto = dataVenctoBoleto;

                    return boletoInfos;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
