using System;
using System.Collections.Generic;
using ColegioCore.Util;

namespace ColegioCore.Entidades
{
    public class Curso : ObjetoColegioBase , iLugar
    {
        public TiposJornada Jornada { get; set; }

        public List<Asignatura> Asignaturas { get; set; }

        public List<Alumno> Alumnos { get; set; }
        public string Direccion { get ; set; }

        public void LimpiarLugar()
        {
            Printer.DibujarLinea();
            Console.WriteLine($"Limpiando curso {Nombre}");
        }
    }
}