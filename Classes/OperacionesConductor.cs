using PruebaInnovation.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PruebaInnovation.Classes
{
    public class OperacionesConductor : IOperacionesConductor
    {
        private DgtContext db;
        public OperacionesConductor(DgtContext db)
        {
            this.db = db != null ? db : new DgtContext();
        }
        //Se implementa el método Agregar conductor de la interfaz, con los parámetros indicados en el enunciado
        public void AgregarConductor(string dni, string nombre, string apellidos, int puntos)
        {
            try
            {
                //Únicamente se ha de comprobar si ya existe en el sistema
                if (!Utils.ExisteConductor(dni, db))
                {
                    Conductor nuevoConductor = new Conductor();
                    nuevoConductor.DNI = dni;
                    nuevoConductor.Nombre = nombre;
                    nuevoConductor.Apellidos = apellidos;
                    nuevoConductor.Puntos = puntos;

                    if (!ValidarModeloConductor(nuevoConductor)) throw new Exception("El modelo introducido no es válido");

                    db.Conductor.Add(nuevoConductor);
                    db.SaveChanges();
                }
                else
                {
                    throw new Exception("El conductor con dni: " + dni + " ya existe");
                }

            }
            catch (Exception e)
            {
                throw new Exception("Se ha producido el siguiente error al insertar el conductor: " + e.Message);
            }

        }

        public Conductor GetConductor(string dni)
        {
            return db.Conductor.FirstOrDefault(w => w.DNI == dni);
        }

        //Se implementa el método Obtener Top N conductores de la interfaz, con los parámetros indicados en el enunciado
        public List<Conductor> ObtenerTopNConductores(int n)
        {
            return db.Conductor.OrderByDescending(o => o.Puntos).Take(n).ToList();
        }

        //Función para comprobar que el modelo es correcto
        public bool ValidarModeloConductor(Conductor conductor)
       {
            return Utils.ComprobarAtributoString(conductor.DNI)
                && Utils.ComprobarAtributoString(conductor.Nombre)
                && Utils.ComprobarAtributoString(conductor.Apellidos);
       }

    }
}