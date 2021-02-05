using FinalniTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalniTest.Interfaces
{
    public interface IZaposleniRepository
    {
        IEnumerable<Zaposleni> GetAll();
        Zaposleni GetById(int id);
        void Create(Zaposleni zaposleni);
        void Update(Zaposleni zaposleni);
        void Delete(Zaposleni zaposleni);
        IEnumerable<Zaposleni> GetZaposleniPoDatumuRodjenja(int rodjenje);
        IEnumerable<Zaposleni> GetZaposleniPoPlati(decimal najmanje, decimal najvise);
    }
}
