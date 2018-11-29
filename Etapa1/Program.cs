using System;
using System.Collections.Generic;
using System.Linq;
using ColegioCore.App;
using ColegioCore.Entidades;
using ColegioCore.Util;
using static System.Console;

namespace ColegioCore
{
    class Program
    {
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.ProcessExit += AccionDelEvento;
            var engine = new ColegioEngine();
            engine.inicializar();
            
            var diccObj = engine.GetDiccionarioObjetos();

            Printer.EscribirTitulo("Reporte general del Colegio");
            
            engine.imprimirDiccionario(diccObj);            

        }

        private static void AccionDelEvento(object sender, EventArgs e)
        {
            Printer.Timbrar(veces:1, tiempo:200);
            Printer.EscribirTitulo("Salió");
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
