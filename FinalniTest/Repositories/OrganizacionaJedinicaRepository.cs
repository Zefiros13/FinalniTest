using FinalniTest.DTOs;
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
    public class OrganizacionaJedinicaRepository : IDisposable, IOrganizacionaJedinicaRepository
    {
        private ApplicationDbContext _context = new ApplicationDbContext();

        public IEnumerable<OrganizacionaJedinica> GetAll()
        {
            return _context.OrganizacioneJedinice;
        }

        public OrganizacionaJedinica GetById(int id)
        {
            return _context.OrganizacioneJedinice.SingleOrDefault(k => k.Id == id);
        }

        public void Create(OrganizacionaJedinica jedinica)
        {
            _context.OrganizacioneJedinice.Add(jedinica);
            _context.SaveChanges();
        }

        public void Update(OrganizacionaJedinica jedinica)
        {
            _context.Entry(jedinica).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }
        public void Delete(OrganizacionaJedinica jedinica)
        {
            _context.OrganizacioneJedinice.Remove(jedinica);
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

        public IEnumerable<OrganizacionaJedinica> Tradicija()
        {
            var sveJedinice = GetAll().OrderBy(j => j.GodinaOsnivanja);

            var retVal = new List<OrganizacionaJedinica>
            {
                sveJedinice.First(),
                sveJedinice.Last()
            };

            return retVal;
        }

        public IEnumerable<BrojZaposlenihPoJediniciDTO> Brojnost()
        {
            return _context.Zaposleni.GroupBy(
                z => z.Jedinica,
                z => z.Id,
                (jedinica, zaposleni) =>
                new BrojZaposlenihPoJediniciDTO
                {
                    Jedinica = jedinica,
                    Id = jedinica.Id,
                    BrojZapolsenih = zaposleni.Count()
                }).AsEnumerable();
        }

        public IEnumerable<ProsecnePlatePoJediniciDTO> Plate(int granica)
        {
            var query = _context.Zaposleni.GroupBy(
                z => z.Jedinica,
                z => z.Plata,
                (jedinica, plata) =>
                new ProsecnePlatePoJediniciDTO
                {
                    Id = jedinica.Id,
                    Jedinica = jedinica,
                    ProsecnaPlata = plata.Average()
                }).Where(d => d.ProsecnaPlata > granica).OrderBy(d => d.ProsecnaPlata);

            return query;
        }
    }
}