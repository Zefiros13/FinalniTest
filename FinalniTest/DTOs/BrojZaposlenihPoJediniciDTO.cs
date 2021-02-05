using FinalniTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalniTest.DTOs
{
    public class BrojZaposlenihPoJediniciDTO
    {
        public int Id { get; set; }

        public OrganizacionaJedinica Jedinica { get; set; }

        public int BrojZapolsenih { get; set; }
    }
}