using System.ComponentModel.DataAnnotations;

namespace BlazorFarmacy.Server.Model.Entities
{
    public class Empleado
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        public string Puesto { get; set; }
    }
}