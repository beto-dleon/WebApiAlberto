namespace WebApiAlberto.Entitys
{
    public class Complementos
    {
        public int Id { get; set; }
        public string Producto { get; set; }

        public int ComputadorasId { get; set; }
        public Computadoras Computadoras { get; set;}

    }
}
