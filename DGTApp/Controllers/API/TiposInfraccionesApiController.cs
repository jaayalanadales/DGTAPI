using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DGTApp.Controllers.API
{
    [Route("infracciones/tipos")]
    public class TiposInfraccionesApiController : ApiController
    {
        [HttpPost]
        
        public IHttpActionResult Post(JObject data)
        {
            using (Model1Container context = new Model1Container())
            {
                string descripcion = data["Descripcion"].ToString();
                int puntosDescontar = data["Puntos"].ToObject<int>();

                context.TiposInfraccion.Add(new TipoInfraccion() { Descripcion = descripcion, Puntos = puntosDescontar });

                context.SaveChanges();

                return Ok("Tipo de infracción añadido satisfactoriamente");
            }


        }
    }
}
