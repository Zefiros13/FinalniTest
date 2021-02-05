using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinalniTest.Models
{
    public class OrganizacionaJedinica
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Ime { get; set; }

        [Range(2010,2020)]
        public int GodinaOsnivanja { get; set; }
    }
}