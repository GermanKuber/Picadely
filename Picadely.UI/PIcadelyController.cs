using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Picadely.Entities;
using Picadely.Services;

namespace Picadely.UI
{
    public class CompraPicadaDto
    {
        public int PicadaId { get; set; }
        public int UsuarioId { get; set; }
    }
    public class PicadelyController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public IHttpActionResult Post([FromBody]CompraPicadaDto compraPicada)
        {

            new ComprasServices().Comprar(new LoginService().GetUsuarioById(compraPicada.UsuarioId),
                new PicadasServices().GetPicadas().FirstOrDefault(x => x.Id == compraPicada.PicadaId));
            return Content(HttpStatusCode.Created, "");
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}