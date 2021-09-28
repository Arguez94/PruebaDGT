using PruebaInnovation.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PruebaInnovation.Classes
{
    public class OperacionesInfraccion : IOperacionesInfraccion
    {
        private DgtContext db;
        public OperacionesInfraccion(DgtContext db)
        {
            this.db = db != null ? db : new DgtContext();
        }
        //Método para agregar un tipo de Infracción. Podría haberse agregado a una clase aparte para los T.Infracción pero lo he hecho así por simplificar
        //Es necesario agregar tipos de Infracciones para poder registrar infracciones a posteriori
        public void AgregarTipoInfraccion(string identificador, string descripcion, int puntosDescuento)
        {
            if (!db.TipoInfraccion.Any(a => a.Identificador == identificador))
            {
                try
                {
                    TipoInfraccion nuevoTipoInfraccion = new TipoInfraccion();
                    nuevoTipoInfraccion.Identificador = identificador;
                    nuevoTipoInfraccion.Descripcion = descripcion;
                    nuevoTipoInfraccion.PuntosDescuento = puntosDescuento;

                    if(!ValidarModeloTipoInfraccion(nuevoTipoInfraccion)) throw new Exception("El modelo introducido no es válido");

                    db.TipoInfraccion.Add(nuevoTipoInfraccion);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    throw new Exception("Se ha producido el siguiente error al insertar el tipo de infracción: " + e.Message);
                }
            }
            else
            {
                throw new Exception("El tipo infracción con identificador: " + identificador + " ya existe");
            }
        }

        //Método implementado de la interfaz
        public void RegistrarInfraccion(string identificador, string identificadorTipoInfraccion, DateTime fechaYHora, string matricula, string dniConductor)
        {
            //Anotacion: Para registrar las infracciones se tiene en cuenta que no es necesario que el conductor sea habitual del vehiculo
            //Si fuese necesario por requerimientos se podría implementar una nueva condición para comprobarlo
            try
            {
                //Primero se comprueba que el conductor existe
                if (!Utils.ExisteConductor(dniConductor, db))
                {
                    throw new Exception("No se ha podido registrar la infracción porque el conductor no existe");
                }

                //Luego que existe el tipo de infracción indicado
                if (!Utils.ExisteTipoInfraccion(identificadorTipoInfraccion, db))
                {
                    throw new Exception("No se ha podido registrar la infracción porque el tipo de infracción no existe");
                }

                //Luego que existe el vehiculo indicado
                if (!Utils.ExisteVehiculo(matricula, db))
                {
                    throw new Exception("No se ha podido registrar la infracción porque el vehículo no existe");
                }

                //Por último, que la infracción con ese ID ya exista
                if (!Utils.ExisteInfraccion(identificador, db))
                {
                    //Se obtenen el tipo de inf. y el conductor y se genera la infracción con esos datos
                    TipoInfraccion tipoInfraccion = db.TipoInfraccion.FirstOrDefault(f => f.Identificador == identificadorTipoInfraccion);
                    Conductor conductorInfractor = db.Conductor.FirstOrDefault(f => f.DNI == dniConductor);
                    Infraccion nuevaInfraccion = new Infraccion();
                    nuevaInfraccion.Identificador = identificador;
                    nuevaInfraccion.TipoInfraccion = tipoInfraccion;
                    nuevaInfraccion.Conductor = conductorInfractor;
                    nuevaInfraccion.Vehiculo = db.Vehiculo.FirstOrDefault(f => f.Matricula == matricula);
                    nuevaInfraccion.FechaYHora = fechaYHora;

                    if(!ValidarModeloInfraccion(nuevaInfraccion)) throw new Exception("El modelo introducido no es válido");

                    //Por último, se restan los puntos al conductor y se le asocia la infraccion
                    SancionarConductor(conductorInfractor, nuevaInfraccion);

                    db.Infraccion.Add(nuevaInfraccion);
                    db.SaveChanges();
                }
                else
                {
                    throw new Exception("La infracción con identificador: " + identificador + " ya existe");
                }
            }
            catch (Exception e)
            {
                throw new Exception("Se ha producido un error al registrar la infraccion: " + e.Message);
            }

        }

        //Se implementa el método para obtener infracciones de un conductor, de la interfaz
        public List<Infraccion> ObtenerInfraccionesConductor(string dni)
        {
            return db.Infraccion.Where(w => w.Conductor.DNI == dni).ToList();
        }

        //Se implementa el método para obtener top 5 infracciones mas cometidas, de la interfaz
        public List<string> ObtenerTop5Infracciones()
        {
            return db.Infraccion.GroupBy(g => g.TipoInfraccion).OrderByDescending(g => g.Count()).Select(s => s.Key.Descripcion).Take(5).ToList();
        }

        //Función para asociar infracción a un conductor y restarle los puntos
        public void SancionarConductor(Conductor conductorInfractor, Infraccion infraccion)
        {
            conductorInfractor.Puntos -= infraccion.TipoInfraccion?.PuntosDescuento ?? 0;
            conductorInfractor.Infraccion.Add(infraccion);
        }

        //Función para comprobar que el modelo es correcto. En este caso los puntos siempre tendrán valor
        public bool ValidarModeloTipoInfraccion(TipoInfraccion tipoInfraccion)
        {
            return Utils.ComprobarAtributoString(tipoInfraccion.Identificador)
                && Utils.ComprobarAtributoString(tipoInfraccion.Descripcion);
        }

        //Función para comprobar que el modelo es correcto. 
        //Como la funcion de agregar infraccion ya tiene suficientes comprobaciones en este caso solo se comprueba el identificador
        public bool ValidarModeloInfraccion(Infraccion infraccion)
        {
            return Utils.ComprobarAtributoString(infraccion.Identificador);
        }
    }
}
