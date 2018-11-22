using System;
using System.Collections.Generic;
using ColegioCore.Entidades;
using ColegioCore.Util;
using static System.Console;

namespace ColegioCore
{
    class Program
    {
        static void Main(string[] args)
        {
            var engine = new ColegioEngine();
            engine.inicializar();
            Printer.EscribirTitulo(engine.Colegio.Nombre);
            imprimirCursos(engine.Colegio);
            Printer.Timbrar(veces:1, tiempo:200);

            var listaObjetos = engine.GetObjetosColegio();

            
        }
        
        
        public static void imprimirCursos(Colegio colegio)
        {   
            try
            {
                if(colegio?.Cursos != null){
                    foreach (var item in colegio.Cursos)
                    {                        
                        WriteLine($"curso: {item.Nombre} - Id: {item.UniqueId}");
                    }                
                }else{
                    WriteLine("No hay cursos en el colegio");    
                }
            }
            catch (System.Exception)
            {                
                WriteLine("No se pudieron imprimir los cursos");
            }
        }                    
    }
}
