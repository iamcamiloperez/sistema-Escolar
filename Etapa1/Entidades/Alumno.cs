using System;
using System.Collections.Generic;

namespace ColegioCore.Entidades
{
    public class Alumno: ObjetoColegioBase
    {        
        public List<Evaluacion> Evaluaciones { get; set; } = new List<Evaluacion>();
    }
}