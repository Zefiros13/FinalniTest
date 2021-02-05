using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinalniTest.Models
{
    public class Zaposleni
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(70)]
        public string ImeIPrezime { get; set; }

        [Required]
        [MaxLength(50)]
        public string Rola { get; set; }

        [Range(1960, 1999)]
        public int GodinaRodjenja { get; set; }

        [Required]
        [Range(2010, 2020)]
        public int GodinaZaposlenja { get; set; }

        [Required]
        [Range(251, 99999)]
        public decimal Plata { get; set; }

        public OrganizacionaJedinica Jedinica { get; set; }

        [Required]
        public int JedinicaId { get; set; }
    }
}