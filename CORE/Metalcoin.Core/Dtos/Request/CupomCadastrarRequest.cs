using Metalcoin.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Metalcoin.Core.Dtos.Request
{
    public class CupomCadastrarRequest
    {
        [Required(ErrorMessage = "Código do cupom é obrigátorio")]
        [MaxLength(25, ErrorMessage = "Categoria pode ter no máximo 15 letras")]
        [MinLength(3, ErrorMessage = "Categoria deve ter no mínimo 5 letras")]
        public string Codigo { get; set; }

        [Required(ErrorMessage = "Descrição do cupom é obrigátorio")]
        [MaxLength(25, ErrorMessage = "Categoria pode ter no máximo 25 letras")]
        [MinLength(3, ErrorMessage = "Categoria deve ter no mínimo 5 letras")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "Valor do cupom é obrigátorio")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O valor do desconto deve ser maior que zero")]
        public decimal ValorDesconto { get; set; }

        [Required(ErrorMessage = "Tipo de desconto do cupom é obrigátorio")]
        public TipoDesconto TipoDesconto { get; set; }

        [Required(ErrorMessage = "Data de validade do cupom é obrigátorio")]
        public DateTime DataValidade { get; set; }

        [Required(ErrorMessage = "Quantidade liberada de cupom é obrigátorio")]
        [Range(1, int.MaxValue, ErrorMessage = "A quantidade liberada deve ser maior que zero")]
        public int QuantidadeLiberada { get; set; }

    }
}
