using Entities.Notification;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Entities.Entities
{
    public class Produto : Notificacao
    {
        [Display(Name = "Código")]
        public int CodProduto { get; set; }

        [Display(Name = "Produto")]
        [MaxLength(255)]
        public string NomeProduto { get; set; }

        [Display(Name = "Quantidade")]
        public int Estoque { get; set; }
    }
}
