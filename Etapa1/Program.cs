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
            Printer.EscribirTitulo(engine.Colegio.Nombre);            
            imprimirCursos(engine.Colegio);
            
            var diccObj = engine.GetDiccionarioObjetos();

            engine.imprimirDiccionario(diccObj);
            /*
            //Consulta por interface
            var listaILugar = from obj in listaObjetos
                            where obj is iLugar
                            select (iLugar) obj;
             */                            
            //engine.Colegio.LimpiarLugar();
            var reportes = new Reportes(diccObj);
            var evalList = reportes.GetListaEvaluaciones();
            var listaAsg = reportes.GetListaAsignaturas();
            var listaEvalPorAsig = reportes.GetDiccEvalPorMateria();
            var prom = reportes.GetPromedioAlumnosPorAsignatura();   

            Printer.EscribirTitulo("Captura de evaluación por consola");
            var newEval = new Evaluacion();
            string nombre;
            string nota;

            WriteLine("Ingrese el nombre de la evaluación");
            nombre = Console.ReadLine();
            if(string.IsNullOrWhiteSpace(nombre)){
                WriteLine("Valor inválido");
                WriteLine("Saliendo");
            }else{
                newEval.Nombre = nombre.ToLower();
                WriteLine("Nombre capturado");
            }

            WriteLine("Ingrese el valor de la nota");
            nota = Console.ReadLine();
            if(string.IsNullOrWhiteSpace(nota)){
                WriteLine("Valor inválido");
                WriteLine("Saliendo");
            }else{
                try{
                    newEval.Nota = float.Parse(nota);
                    if(newEval.Nota < 0 || newEval.Nota>5){
                        throw new ArgumentOutOfRangeException("el valor debe estar entre 0.0 y 5.0");    
                    }
                    WriteLine("Nota capturada");
                }catch(ArgumentOutOfRangeException ex){
                    WriteLine($"Valor inválido: {ex.Message}");
                }
                catch(Exception e)
                {
                    WriteLine($"Valor inválido: {e.Message}");
                    WriteLine("Saliendo");
                }                
            }


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
