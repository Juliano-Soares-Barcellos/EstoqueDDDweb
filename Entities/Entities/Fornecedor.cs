using Entities.Notification;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
    public class Fornecedor: Notificacao
    {
        [Display(Name = "Id do fornecedor")]
        public int CodFornecedor { get; set; }

        [Display(Name="Nome do fornecedor")]
        public string NomeFornecedor { get; set; }
    }
}
