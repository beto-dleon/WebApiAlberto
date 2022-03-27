using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApiAlberto.Validaciones;

namespace WebApiAlberto.Entitys
{
    public class Computadoras: IValidatableObject
    {

        public int Id { get; set; }

        [Required(ErrorMessage = " Es necesario llenar el campo de {0}")]
        [StringLength(maximumLength:15, ErrorMessage = "El campo de {0} solo puede tener hasta 15 caracteres")]
        [PrimeraLetraMayuscula]
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

        [NotMapped]
        public int menor { get; set; }
        [NotMapped]
        public int mayor { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {

            if (!string.IsNullOrEmpty(Marca))
            {
                var primeraLetra = Marca[0].ToString();

                if (primeraLetra != primeraLetra.ToUpper())
                {
                    yield return new ValidationResult("La primera letra debe ser mayuscula",
                        new String[] { nameof(Marca) });
                }
            }

            if (menor > mayor)
            {
                yield return new ValidationResult("Este valor no puede ser mas grande que el valor del campo mayor!",
                    new String[] { nameof(menor) });
            }

        }

    }
}
