using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;

using Picadely.Entities;
using Picadely.Services;

namespace Picadely.UI
{
    public class PicadelyController : ApiController
    {

        [HttpPost]
        public IHttpActionResult Post([FromBody] CompraPicadaDto compraPicada)
        {
            new ComprasServices().Comprar(new LoginService().GetUsuarioById(compraPicada.UsuarioId),
                new PicadasServices().GetPicadas().FirstOrDefault(x => x.Id == compraPicada.PicadaId));
            return Content(HttpStatusCode.Created, "");
        }

        [HttpGet]
        public HttpResponseMessage Get()
        {
            new PicadasServices().GenerarMenuEnXml();
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK);
            string filePath = @"C:\picada\picada.xml";
            if (!File.Exists(filePath))
            {
                response.StatusCode = HttpStatusCode.NotFound;
                response.ReasonPhrase = string.Format("File not found: {0} .", "Picadas.xml");
                throw new HttpResponseException(response);
            }
            byte[] bytes = File.ReadAllBytes(filePath);
            response.Content = new ByteArrayContent(bytes);
            response.Content.Headers.ContentLength = bytes.LongLength;
            response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
            response.Content.Headers.ContentDisposition.FileName = "Picadas.xml";
            response.Content.Headers.ContentType = new MediaTypeHeaderValue(MimeMapping.GetMimeMapping("Picadas.xml"));
            return response;
        }
    }
}