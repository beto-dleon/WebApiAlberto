namespace WebApiAlberto.Entitys
{
    public class Computadoras
    {

        public int Id { get; set; }

        public String Marca { get; set; }

        public List<Complementos> complementos  { get; set; }

    }
}
