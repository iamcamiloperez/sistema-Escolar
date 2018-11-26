using System;
using System.Collections.Generic;

namespace ColegioCore.Entidades
{
    public class Evaluacion : ObjetoColegioBase
    {
        
        public float Nota { get; set; }
        public Alumno Estudiante {get; set; }
        public Asignatura Materia { get; set; }

        public Evaluacion(){
            
        }
        public Evaluacion(string nombre, float nota, Alumno estudiante, Asignatura materia) => (Nombre, Nota, Estudiante, Materia ) = (nombre, nota, estudiante, materia);

        public override string ToString()
        {
            return $"Nota: {Nota} - Alumno: {Estudiante.Nombre} - Asignatura: {Materia.Nombre}";
        }

    }
}