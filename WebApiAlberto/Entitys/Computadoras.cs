using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiAlberto.Entitys
{
    public class Computadoras
    {

        public int Id { get; set; }

        [Required(ErrorMessage = " Es necesario llenar el campo de {0}")]
        [StringLength(maximumLength:15, ErrorMessage = "El campo de {0} solo puede tener hasta 15 caracteres")]
        public String Marca { get; set; }

        [Range(1000,10000, ErrorMessage = "El {0} no se encuentra dentro del rango establecido")]
        [NotMapped]
        public int Precio { get; set; }

        [NotMapped]
        [CreditCard]
        public string Tarjeta { get; set; }

        [NotMapped]
        [Url]
        public string Url { get; set; }

        public List<Complementos> complementos  { get; set; }

    }
}
