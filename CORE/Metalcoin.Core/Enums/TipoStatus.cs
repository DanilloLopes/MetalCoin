using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metalcoin.Core.Enums
{
    public enum TipoStatus
    {
        [Description("Cupom ativo. Pode ser utilizado.")]
        Ativo = 0,
        [Description("Cupom já atingiu ou ultrapassou a válidade. Não pode ser utilizado.")]
        Expirado = 1,
        [Description("Cupom foi desativado, não pode ser utilizado.")]
        Desativado = 2,
        [Description("Cupom atingiu a quantidade máxima de utilizações, não pode ser utilizado.")]
        TotalmenteUtilizado = 3
    }
}
