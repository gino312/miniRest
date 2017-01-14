using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json.Linq;

namespace ServicioRest.Controllers
{
    public class wordController : ApiController
    {
        //El Json debe seguir la siguiente sintaxis
        //{ "data" : "hola" }
        [HttpPost]
        public JObject JsonPost(JObject palabraJson)
        {
            int max_caract = 0; //Si max caracter cambia a 1, es porque hay problemas con el largo del string
            try
            {
                //Se valida que sea un string de 4 caracteres
                if (palabraJson["data"].ToString().Length == 4)
                {
                    string data = palabraJson["data"].ToString();
                    dynamic jsonWord = new JObject();

                    jsonWord.code = "00";
                    jsonWord.description = "OK";
                    //Se pasan los caracteres del string a mayuscula
                    jsonWord.data = data.ToUpper();

                    return jsonWord;
                }
                else {//Si no es de largo 4, se cambia el valor de max_caract para poder enviar un error 400
                    max_caract = 1;
                    throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "C"));
                }
            }
            catch
            {
                if (max_caract == 1)//Se entrega el error 400 con el mensaje explicando lo que podría generarlo
                    throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Cantidad de caracteres no soportados (Maximo de caracteres = 4)"));
                //Pudiendo ser cualquier otro error lo manejamos con error 500
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Ocurrio un error inesperado (Revise sintaxis del json)"));
            }
        }
    }
}
