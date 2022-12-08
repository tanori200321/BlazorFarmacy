using System.ComponentModel.DataAnnotations;

namespace BlazorFarmacy.Server.Model.Entities
{
    public class Cliente
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }

        public string Email { get; set; }
    }
}
