using System;
using System.Linq;
using System.Collections.Generic;
using ColegioCore.Entidades;

namespace ColegioCore.App
{
    public class Reportes
    {
        Dictionary<LlaveDiccionario, IEnumerable<ObjetoColegioBase>> _diccionario;

        //Metodo constructor de Reportes
        public Reportes(Dictionary<LlaveDiccionario, IEnumerable<ObjetoColegioBase>> dicc)
        {
            //si el diccionario es nulo genera excepción
            if (dicc == null)
            {
                throw new ArgumentNullException(nameof (dicc));
            }else{
                _diccionario = dicc;
            }
        }


        //Crea la lista de datos de evaluación para el reporte
        public IEnumerable<Evaluacion> GetListaEvaluaciones()
        {
            IEnumerable<Evaluacion> rta;
            //Obtienen la llave del colegio
            if( _diccionario.TryGetValue(LlaveDiccionario.EVALUACION,
                                                 out IEnumerable<ObjetoColegioBase> lista))
            {
                return rta = lista.Cast<Evaluacion>();
            }else{
                return new List<Evaluacion>();
                //Escribir en el log la auditoria
            }                                             
        }


        //Sobrecarga del metodo getListaAsignaturas
        public IEnumerable<string> GetListaAsignaturas()
        {
            return GetListaAsignaturas(out var dummy);
        }


        //Crea la lista de datos de asignaturas evaluadas
        public IEnumerable<string> GetListaAsignaturas(
            out IEnumerable<Evaluacion> listaEvaluaciones)
        {
            //Obtiene lista de evaluaciones
            listaEvaluaciones = GetListaEvaluaciones();

            //retorna Asignaturas que tienen una evaluación
            return (from Evaluacion ev in listaEvaluaciones
                    select ev.Materia.Nombre).Distinct();
        }


        //Crea la lista de evaluaciones por asignatura
        public Dictionary<string, IEnumerable<Evaluacion>> GetDiccEvalPorMateria()
        {
            //Creación del diccionario
            var DiccRta = new Dictionary<string, IEnumerable<Evaluacion>>();
            //Obtengo asignaturas
            var listaAsignaturas = GetListaAsignaturas(out var listaEval);

            //Ciclo para llenar un diccionario asignando nombre asig y su grupo de evaluaciones
            foreach (var asig in listaAsignaturas)
            {
                var evalsAsig = from eval in listaEval
                                where eval.Materia.Nombre == asig
                                select eval;

                DiccRta.Add(asig, evalsAsig);
            }
            return DiccRta;            
        }

        //Lista de promedio de alumnos por asignatura
        public Dictionary<string, IEnumerable<object>> GetPromedioAlumnosPorAsignatura()
        {
            //crear el diccionario
            var respuesta = new Dictionary<string, IEnumerable<object>>();
            //obtener el diccionario de evaluaciones por asignatura
            var diccEval = GetDiccEvalPorMateria();

            //Recorre la lista de Evaluaciones por materia
            foreach (var asignaEval in diccEval)
            {
                //consulta genera promedio agrupando por estudiante
                var promEst = from eval in asignaEval.Value
                            group eval by new {
                                eval.Estudiante.UniqueId,
                                eval.Estudiante.Nombre
                                }
                                into grupoEvalAlumno
                                select new AlumnoPromedio
                            {                                
                                alumnoId = grupoEvalAlumno.Key.UniqueId,
                                alumnoNombre = grupoEvalAlumno.Key.Nombre,
                                promedio = grupoEvalAlumno.Average(evaluacion=> evaluacion.Nota)
                            };
                //Agrega el promedio de un estudiante a la respuesta
                respuesta.Add(asignaEval.Key, promEst);
            }            
            return respuesta;
        }

    }
}