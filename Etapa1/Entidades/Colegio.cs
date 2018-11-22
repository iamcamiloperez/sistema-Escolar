using System.Collections.Generic;
using ColegioCore.Entidades;

namespace ColegioCore.Entidades
{
    public class Colegio : ObjetoColegioBase
    {      
        public int AñoCreacion { get; set; }
        public string Pais { get; set; }

        public string Ciudad { get; set; }
        public TiposColegio TipoColegio { get; set; }        

        public TiposJornada Jornada { get; set; }
        public List<Curso> Cursos { get; set; }

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
    }
}