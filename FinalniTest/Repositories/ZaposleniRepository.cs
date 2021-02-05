using FinalniTest.Interfaces;
using FinalniTest.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace FinalniTest.Repositories
{
    public class ZaposleniRepository : IDisposable, IZaposleniRepository
    {
        private ApplicationDbContext _context = new ApplicationDbContext();

        public IEnumerable<Zaposleni> GetAll()
        {
            return _context.Zaposleni.Include(z => z.Jedinica).OrderBy(z => z.GodinaZaposlenja);
        }

        public Zaposleni GetById(int id)
        {
            return _context.Zaposleni.SingleOrDefault(k => k.Id == id);
        }

        public void Create(Zaposleni zaposleni)
        {
            _context.Zaposleni.Add(zaposleni);
            _context.SaveChanges();
        }

        public void Update(Zaposleni zaposleni)
        {
            _context.Entry(zaposleni).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }
        public void Delete(Zaposleni zaposleni)
        {
            _context.Zaposleni.Remove(zaposleni);
            _context.SaveChanges();
        }


        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_context != null)
                {
                    _context.Dispose();
                    _context = null;
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public IEnumerable<Zaposleni> GetZaposleniPoDatumuRodjenja(int rodjenje)
        {
            return _context.Zaposleni.Where(z => z.GodinaRodjenja > rodjenje).OrderBy(z => z.GodinaRodjenja);
        }

        public IEnumerable<Zaposleni> GetZaposleniPoPlati(decimal najmanje, decimal najvise)
        {
            return _context.Zaposleni.Where(z => z.Plata >= najmanje && z.Plata <= najvise).OrderByDescending(z => z.Plata).Include(z => z.Jedinica);
        }
    }
}