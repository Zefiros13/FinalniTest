using FinalniTest.Interfaces;
using FinalniTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FinalniTest.Controllers
{
    public class ZaposleniController : ApiController
    {
        IZaposleniRepository _repository { get; set; }

        public ZaposleniController(IZaposleniRepository repository)
        {
            _repository = repository;
        }

        //GET api/zaposleni
        public IEnumerable<Zaposleni> GetAll()
        {
            return _repository.GetAll();
        }

        //GET api/zaposleni/1
        public IHttpActionResult GetById(int id)
        {
            var zaposleni = _repository.GetById(id);

            if (zaposleni == null)
            {
                return NotFound();
            }

            return Ok(zaposleni);
        }

        //POST api/zaposleni
        public IHttpActionResult Post(Zaposleni zaposleni)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _repository.Create(zaposleni);

            return CreatedAtRoute("DefaultApi", new { id = zaposleni.Id }, zaposleni);
        }


        //PUT api/zaposleni/1
        public IHttpActionResult Put(int id, Zaposleni zaposleni)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != zaposleni.Id)
            {
                return BadRequest();
            }

            try
            {
                _repository.Update(zaposleni);
            }
            catch
            {
                return BadRequest();
            }

            return Ok(zaposleni);
        }

        //DELETE api/zaposleni/1
        public IHttpActionResult Delete(int id)
        {
            var zaposleni = _repository.GetById(id);

            if (zaposleni == null)
            {
                return NotFound();
            }

            _repository.Delete(zaposleni);

            return Ok();
        }

        //GET api/zaposleni/?rodjenje={godina}
        public IEnumerable<Zaposleni> GetZaposleniPoDatumuRodjenja(int rodjenje)
        {
            return _repository.GetZaposleniPoDatumuRodjenja(rodjenje);
        }

        //POST api/pretraga
        [Route("api/pretraga/")]
        public IEnumerable<Zaposleni> PostGetByPlata(decimal najmanja, decimal najveca)
        {
            return _repository.GetZaposleniPoPlati(najmanja, najveca);
        }

    }
}
