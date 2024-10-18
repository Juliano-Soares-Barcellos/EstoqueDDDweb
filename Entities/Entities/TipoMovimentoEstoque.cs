using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entities
{
   public class tipomovimentacao
    {
        [Display(Name = "tipo Movimento")]
        public int CodMov { get; set; }
        [Display(Name = "Entrada ou Saida de Materiais")]
        public string Descricao { get; set; }

     
    }
}
