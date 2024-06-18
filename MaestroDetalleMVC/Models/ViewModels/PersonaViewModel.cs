using System;
using System.Collections.Generic;

namespace MaestroDetalleMVC.Models.ViewModels
{
    public class PersonaViewModel
    {
        public List<Persona> personas { get; set; }

        // Agregar las propiedades necesarias para el formulario
        public int Cedula { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Direccion { get; set; }

        public PersonaViewModel()
        {
            personas = new List<Persona>();
        }
    }

    public class Persona
    {
        public int Cedula { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Direccion { get; set; }
    }
}
