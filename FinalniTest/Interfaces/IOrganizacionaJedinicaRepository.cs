using FinalniTest.DTOs;
using FinalniTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalniTest.Interfaces
{
    public interface IOrganizacionaJedinicaRepository
    {
        IEnumerable<OrganizacionaJedinica> GetAll();
        OrganizacionaJedinica GetById(int id);
        void Create(OrganizacionaJedinica jedinica);
        void Update(OrganizacionaJedinica jedinica);
        void Delete(OrganizacionaJedinica jedinica);
        IEnumerable<OrganizacionaJedinica> Tradicija();
        IEnumerable<BrojZaposlenihPoJediniciDTO> Brojnost();
        IEnumerable<ProsecnePlatePoJediniciDTO> Plate(int granica);
    }
}
