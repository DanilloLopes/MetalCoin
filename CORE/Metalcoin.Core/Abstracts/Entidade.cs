using System.ComponentModel.DataAnnotations;

namespace Metalcoin.Core.Abstracts
{
    public abstract class Entidade
    {
        [Key]
        public Guid Id { get; set; }

        protected Entidade()
        {
            Id = Guid.NewGuid();
        }
    }
}
