using Org.BouncyCastle.Bcpg;
using Org.BouncyCastle.Utilities;
using System;
using System.Text.RegularExpressions;

namespace Domain.Kernel
{
    public class KernelDomain
    {
        public decimal validaPonto(string valor)
        {
            string regexPatern = @"/^\d *\.?\d *$/";

            if (!Regex.IsMatch(valor, regexPatern))
            {
                if (decimal.TryParse(valor, out decimal resultado))
                {
                    return Convert.ToDecimal(valor.Replace(".", ","));
                   
                }
                else
                {
                    throw new ArgumentException("Você colocou um ponto ao Inves de uma virugula");
                }
            }
            return Convert.ToDecimal(valor);
        }
    }
}



