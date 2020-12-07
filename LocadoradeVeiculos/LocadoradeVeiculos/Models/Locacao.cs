using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LocadoradeVeiculos.Models
{
    [Table("Locacao")]
    public class Locacao
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdLocacao { get; set; }
        [DataType(DataType.Date)]
        public DateTime DataReservaInicio { get; set; }
        public DateTime DataReservaFim { get; set; }
        [Required]
        public int IdCliente { get; set; }

        [ForeignKey("IdCliente")]
        public virtual Cliente Cliente { get; set; }
        [Required]
        public int IdEstoque{ get; set; }
        [ForeignKey("IdEstoque")]
        public virtual Estoque Estoque { get; set; }
    }
}
