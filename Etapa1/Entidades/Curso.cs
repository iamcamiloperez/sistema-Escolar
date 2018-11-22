using System;
using System.Collections.Generic;

namespace ColegioCore.Entidades
{
    public class Curso : ObjetoColegioBase
    {
        public TiposJornada Jornada { get; set; }

        public List<Asignatura> Asignaturas { get; set; }

        public List<Alumno> Alumnos { get; set; }
        
    }
}