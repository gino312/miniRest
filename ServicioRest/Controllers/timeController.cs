using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json.Linq;

namespace ServicioRest.Controllers
{
    public class timeController : ApiController
    {
        //La URL tendra que tener la siguiente sintaxis
        ///time?value=16:30
        //Aceptara valores de hora como por ejemplo
        // 16:30
        // 16:30:00
        [HttpGet]
        public JObject formateoHora(string value)
        {
            try
            {
                //Primero se parsea el valor obtenido de la url a la hora universal
                string nueva_hora = DateTimeOffset.Parse(value).ToUniversalTime().ToString(); 
                //Se crea un JOject (Json)
                dynamic jsonTime = new JObject();
                jsonTime.code = "00";
                jsonTime.description = "OK";
                //Se le agrega la hora parseada al Json
                jsonTime.data = nueva_hora;
                return jsonTime;
            }
            catch //Si ocurre cualquier error se manejara con el estado 500
            {
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Ocurrio un error inesperado (Asegurese que la sintaxis de la hora es correcta: XX:XX o XX:XX:XX)"));
            }
        }
    }
}
