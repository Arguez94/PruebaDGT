using PruebaInnovation.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PruebaInnovation.Classes
{
    public class OperacionesConductor : IOperacionesConductor
    {
        DgtContext db = new DgtContext();
        //Se implementa el método Agregar conductor de la interfaz, con los parámetros indicados en el enunciado
        public void AgregarConductor(string dni, string nombre, string apellidos, int? puntos)
        {
            try
            {
                //Únicamente se ha de comprobar si ya existe en el sistema
                //Si no se le indican puntos, se le ponen 8 por defecto
                if (!Utils.ExisteConductor(dni, db))
                {
                    Conductor nuevoConductor = new Conductor();
                    nuevoConductor.DNI = dni;
                    nuevoConductor.Nombre = nombre;
                    nuevoConductor.Apellidos = apellidos;
                    nuevoConductor.Puntos = puntos != null ? puntos.GetValueOrDefault() : Consts.PUNTOS_INICIALES;
                    db.Conductor.Add(nuevoConductor);
                    db.SaveChanges();
                }
                else
                {
                    Console.WriteLine("El conductor con dni: " + dni + " ya existe");
                }

            }
            catch (Exception e)
            {
                Console.WriteLine("Se ha producido el siguiente error al insertar el conductor: " + e.Message);
            }

        }

        //Se implementa el método Obtener Top N conductores de la interfaz, con los parámetros indicados en el enunciado
        public List<Conductor> ObtenerTopNConductores(int n)
        {
            return db.Conductor.OrderByDescending(o => o.Puntos).Take(n).ToList();
        }

       

    }
}