using PruebaInnovation.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PruebaInnovation.Classes
{
    public class OperacionesVehiculo : IOperacionesVehiculo
    {
        DgtContext db = new DgtContext();
        //Se implementa el método Agregar vehículo de la interfaz, con los parámetros indicados en el enunciado
        public void AgregarVehiculo(string matricula, string marca, string modelo, string dniConductor)
        {
            try
            {
                //Primero se comprueba si el vehículo existe o no
                if (Utils.ExisteVehiculo(matricula, db))
                {
                    Console.WriteLine("ERROR: El vehículo a agregar ya existe");
                    return;
                }

                //Luego se comprueba la existencia del conductor, utilizando el método de la clase correspondiente
                if (Utils.ExisteConductor(dniConductor, db))
                {
                    Console.WriteLine("ERROR: El conductor a agregar no existe");
                    return;
                }

                //Una vez hechas las comprobaciones, se consulta el conductor, se crea el vehículo, se asocian y se guarda en el almacén de datos
                Conductor conductor = db.Conductor.FirstOrDefault(w => w.DNI == dniConductor);
                Vehiculo nuevoVehiculo = new Vehiculo();
                nuevoVehiculo.Matricula = matricula;
                nuevoVehiculo.Marca = marca;
                nuevoVehiculo.Modelo = modelo;

                //Aquí podría crearse el vehículo directamente aunque no tuviese conductor asociado, pero dependería de los requerimientos
                //En este caso bloqueamos la creación del objeto si no hay conductor asociado
                if (AsociarConductor(nuevoVehiculo, conductor))
                {
                    db.Vehiculo.Add(nuevoVehiculo);
                    db.SaveChanges();
                }
                
            }
            catch (Exception e)
            {
                Console.WriteLine("Se ha producido el siguiente error durante la operación de creación de vehículo: " + e.Message);
            }

        }

        //Método para controlar la asociación de vehículo y conductor
        //El método el booleano para ayudarnos a controlar la operación desde la creación de vehículo
        public bool AsociarConductor(Vehiculo vehiculo, Conductor conductor)
        {
            try
            {
                //Hay que comprobar el número de vehículos asociados al conductor (deben ser menos de 10)
                //Además, se controla el caso en el que el conductor ya esté asociado al vehículo, aunque esto no debería suceder dado el flujo de información
                if (ComprobarVehiculosConductor(conductor) && !vehiculo.Conductor.Contains(conductor))
                {
                    vehiculo.Conductor.Add(conductor);
                    return true;
                }
                else
                {
                    Console.WriteLine("El conductor ya tiene 10 vehículos o mas o ya existe como conductor habitual en este vehículo");
                    return false;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Ha ocurrido un error al asociar el conductor al vehículo: " + e.Message);
                return false;
            }
        }

        //Método para comprobar si el número de vehículos del conductor es menor a 10. 
        public bool ComprobarVehiculosConductor(Conductor conductor)
        {
            return db.Vehiculo.Where(w => w.Conductor.Contains(conductor)).Count() < 10;
        }
    }
}