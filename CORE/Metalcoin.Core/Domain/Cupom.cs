using Metalcoin.Core.Abstracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Metalcoin.Core.Enums;

namespace Metalcoin.Core.Domain
{
    public class Cupom : Entidade
    {
        public string Codigo { get; set; }      
        public string Descricao { get; set; }
        public decimal ValorDesconto { get; set; }
        public TipoDesconto TipoDesconto { get; set; }
        public DateTime DataValidade { get; set; }
        public int QuantidadeLiberada { get; set; }  
        public int QuantidadeUsada { get; set; }  
        public TipoStatus Status { get; set; }
        public DateTime DataCadastro { get; set; }        
        public DateTime DataAlteracao { get; set; }
    }
}
