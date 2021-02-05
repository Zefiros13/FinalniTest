using FinalniTest.DTOs;
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
    public class JediniceController : ApiController
    {
        IOrganizacionaJedinicaRepository _repository { get; set; }

        public JediniceController(IOrganizacionaJedinicaRepository repository)
        {
            _repository = repository;
        }

        //GET api/jedinice
        public IEnumerable<OrganizacionaJedinica> GetAll()
        {
            return _repository.GetAll();
        }

        //GET api/jedinice/1
        public IHttpActionResult GetById(int id)
        {
            var jedinica = _repository.GetById(id);

            if (jedinica == null)
            {
                return NotFound();
            }

            return Ok(jedinica);
        }

        //POST api/jedinice
        public IHttpActionResult Post(OrganizacionaJedinica jedinica)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _repository.Create(jedinica);

            return CreatedAtRoute("DefaultApi", new { id = jedinica.Id }, jedinica);
        }


        //PUT api/jedinice/1
        public IHttpActionResult Put(int id, OrganizacionaJedinica jedinica)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != jedinica.Id)
            {
                return BadRequest();
            }

            try
            {
                _repository.Update(jedinica);
            }
            catch
            {
                return BadRequest();
            }

            return Ok(jedinica);
        }

        //DELETE api/jedinice/1
        public IHttpActionResult Delete(int id)
        {
            var jedinica = _repository.GetById(id);

            if (jedinica == null)
            {
                return NotFound();
            }

            _repository.Delete(jedinica);

            return Ok();
        }

        //GET api/tradicija
        [Route("api/tradicija")]
        public IEnumerable<OrganizacionaJedinica> GetTradicija()
        {
            return _repository.Tradicija();
        }

        //GET api/brojnost
        [Route("api/brojnost")]
        public IEnumerable<BrojZaposlenihPoJediniciDTO> GetBrojnost()
        {
            return _repository.Brojnost();
        }

        //POST api/plate
        [Route("api/plate")]
        public IEnumerable<ProsecnePlatePoJediniciDTO> PostPlate(int granica)
        {
            return _repository.Plate(granica);
        }

    }
}
