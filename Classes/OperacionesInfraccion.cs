using PruebaInnovation.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PruebaInnovation.Classes
{
    public class OperacionesInfraccion : IOperacionesInfraccion
    {
        DgtContext db = new DgtContext();
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
                    db.TipoInfraccion.Add(nuevoTipoInfraccion);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Se ha producido el siguiente error al insertar el tipo de infracción: " + e.Message);
                }
            }
            else
            {
                Console.WriteLine("El tipo infracción con identificador: " + identificador + " ya existe");
            }
        }

        //Método implementado de la interfaz
        public void RegistrarInfraccion(string identificador, string identificadorTipoInfraccion, DateTime fechaYHora, Vehiculo vehiculo, string dniConductor)
        {
            try
            {
                //Primero se comprueba que el conductor existe
                if (!Utils.ExisteConductor(dniConductor, db))
                {
                    Console.WriteLine("No se ha podido registrar la infracción porque el conductor no existe");
                    return;
                }

                //Luego que existe el tipo de infracción indicado
                if (!Utils.ExisteTipoInfraccion(identificadorTipoInfraccion, db))
                {
                    Console.WriteLine("No se ha podido registrar la infracción porque el tipo de infracción no existe");
                    return;
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
                    nuevaInfraccion.Vehiculo = vehiculo;
                    nuevaInfraccion.FechaYHora = fechaYHora;

                    //Por último, se restan los puntos al conductor y se le asocia la infraccion
                    SancionarConductor(conductorInfractor, nuevaInfraccion);
                }
                else
                {
                    Console.WriteLine("La infracción con identificador: " + identificador + " ya existe");
                    return;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Se ha producido un error al registrar la infraccion: " + e.Message);
                return;
            }

        }

        //Se implementa el método para obtener infracciones de un conductor, de la interfaz
        public List<Infraccion> ObtenerInfraccionesConductor(string dni)
        {
            return db.Infraccion.Where(w => w.Conductor.DNI == dni).ToList();
        }

        //Se implementa el método para obtener top 5 infracciones mas cometidas, de la interfaz
        public List<TipoInfraccion> ObtenerTop5Infracciones()
        {
            return db.Infraccion.GroupBy(g => g.TipoInfraccion).OrderByDescending(g => g.Key).Select(s => s.Key).Take(5).ToList();
        }

        //Función para asociar infracción a un conductor y restarle los puntos
        public void SancionarConductor(Conductor conductorInfractor, Infraccion infraccion)
        {
            conductorInfractor.Puntos = -infraccion.TipoInfraccion?.PuntosDescuento ?? 0;
            conductorInfractor.Infraccion.Add(infraccion);
        }

    }
}
