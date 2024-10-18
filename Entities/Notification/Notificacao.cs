using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Notification
{
    public class Notificacao
    {
        [Display(Name = "Notificações")]
        public List<Notificacao> notificacoes;

        [Display(Name = "Nome da Propriedade")]
        public string nomePropriedade { get; set; }
        [Display(Name = "Mensagem")]
        public string mensagem { get; set; }

        public Notificacao()
        {
            notificacoes = new List<Notificacao>();
        }

         public bool ValidaPropiedadeString(string valor, string nomePropriedade)
        {
            if(string.IsNullOrWhiteSpace(valor) || string.IsNullOrWhiteSpace( nomePropriedade))
            {
                notificacoes.Add(new Notificacao
                {
                    nomePropriedade = nomePropriedade,
                    mensagem = "Campo Obrigatorio"
                });
                return false;
            }
            return true;
        }

        public bool ValidaPropiedadeInt(int valor, string nomePropriedade)
        {
            if (valor < 1 || string.IsNullOrWhiteSpace(nomePropriedade))
            {
                notificacoes.Add(new Notificacao
                {
                    mensagem = "Valor deve ser Maior que zero",
                    nomePropriedade = nomePropriedade
                });
                return false;
            }
            return true;
        }

        public bool ValidaPropiedadeDecimal(decimal ? valor, string nomePropriedade)
        {
            if (valor < 1 || string.IsNullOrWhiteSpace(nomePropriedade))
            {
                notificacoes.Add(new Notificacao
                {
                    mensagem = "Valor deve ser Maior que zero",
                    nomePropriedade = nomePropriedade
                });
                return false;
            }
            return true;
        }
        public bool validaValorVazioOuNulo(string valor, string nomePropriedade, int  valorInt)
        {

            if (ValidaPropiedadeString(valor, nomePropriedade) && ValidaPropiedadeInt(valorInt, nomePropriedade))
            {
                return true;
            }
            return false;
        }
    }
}
