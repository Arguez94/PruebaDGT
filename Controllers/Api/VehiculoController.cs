using PruebaInnovation.Classes;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;


namespace PruebaInnovation.Controllers
{
    public class VehiculoController : ApiController
    {
        private OperacionesVehiculo operaciones = new OperacionesVehiculo(new DgtContext());

        //Función para obtener vehiculo a partir de su matricula
        [HttpGet]
        public object Get(string matricula)
        {
            try
            {
                Vehiculo vehiculo = operaciones.GetVehiculo(matricula);
                return Request.CreateResponse(HttpStatusCode.OK, vehiculo);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.InnerException);
            }
        }

        //Función para agregar un vehiculo al sistema
        [HttpPost]
        public object Post(dynamic body)
        {
            try
            {
                //TODO: Método para procesar el body
                string matricula = body.matricula ?? null;
                string marca = body.marca ?? null;
                string modelo = body.modelo ?? null;
                string dni = body.dni ?? null;

                operaciones.AgregarVehiculo(matricula, marca, modelo, dni);
                return Request.CreateResponse(HttpStatusCode.OK, "El vehiculo con matricula: " + matricula + " se ha creado correctamente");
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }


    }
}
