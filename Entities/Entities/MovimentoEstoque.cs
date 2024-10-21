using Entities.Notification;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Entities
{
    [Table("movimentacaoestoque")] 
    public class MovimentoEstoque : Notificacao
    {
        [Key] // Define a chave primária
        [Column("CodMovimentacao")] 
        [Display(Name = "Id")]
        public int CodMovimentacao { get; set; }

        [Column("DataMovimentacao")] 
        [Display(Name = "Data")]
        [Required(ErrorMessage = "Obrigatória")]
        public DateTime DataMovimentacao { get; set; }

        [Column("ValorUnitario")] 
        [Display(Name = "Valor do Produto")]
        public decimal? ValorUnitario { get; set; } 

        [Column("Quantidade")] 
        [Display(Name = "Quantidade do Produto")]
        [Required(ErrorMessage = "A quantidade é obrigatória")]
        public int Quantidade { get; set; }

        [Column("FkFornecedor")] 
        [Required(ErrorMessage = "O fornecedor é obrigatório")]
        public int FkFornecedor { get; set; }

        [Column("FkProduto")] 
        [Required(ErrorMessage = "O produto é obrigatório")]
        public int FkProduto { get; set; }

        [Column("FkTipoMov")] 
        [Required(ErrorMessage = "O tipo de movimentação é obrigatório")]
        public int Fk_tipomovimentacao { get; set; }

     
        public Fornecedor Fornecedor { get; set; }
        public Produto Produto { get; set; }
        public tipomovimentacao tipomovimentacao { get; set; }

        public MovimentoEstoque()
        {

        }
    }
}
