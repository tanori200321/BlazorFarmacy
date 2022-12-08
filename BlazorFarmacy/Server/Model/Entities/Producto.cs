﻿using System.ComponentModel.DataAnnotations;

namespace BlazorFarmacy.Server.Model.Entities
{
    public class Producto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }
    }
}
