using System;
using System.Collections.Generic;
using ColegioCore.Entidades;
using ColegioCore.Util;

namespace ColegioCore.Entidades
{
    public class Colegio : ObjetoColegioBase , iLugar
    {      
        public int AñoCreacion { get; set; }
        public string Pais { get; set; }

        public string Ciudad { get; set; }

        public TiposColegio TipoColegio { get; set; }        

        public TiposJornada Jornada { get; set; }
        public List<Curso> Cursos { get; set; }
        public string Direccion { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public Colegio(string nombre, int añoCreacion) => (Nombre, AñoCreacion) = (nombre.ToUpper(), añoCreacion);

        public Colegio(string nombre, int año, TiposColegio tipo, string pais="", string ciudad=""){
            (Nombre, AñoCreacion, TipoColegio) = (nombre, año, tipo);
            Pais = pais;
            Ciudad = ciudad;
        }

        public override string ToString()
        {
            return $" Nombre: \"{Nombre}\"\n Tipo: \"{TipoColegio}\" \n Pais: \"{Pais}\" \n Ciudad: \"{Ciudad}\"";
        }

        public void LimpiarLugar(){
            Printer.DibujarLinea();
            Console.WriteLine("Limpiando el Colegio");
            foreach(var curso in Cursos)
            {
                curso.LimpiarLugar();
            }
        }
    }
}