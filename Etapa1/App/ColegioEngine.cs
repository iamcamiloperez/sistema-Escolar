using System;
using System.Collections.Generic;
using System.Linq;
using static System.Console;

namespace ColegioCore.Entidades
{
    public sealed class ColegioEngine
    {
        public Colegio Colegio { get; set; }

        public ColegioEngine()
        {            
        }

        #region Inicializar
        public void inicializar()
        {
            Colegio = new Colegio("One School", 2012, TiposColegio.Primaria,
                pais: "Colombia",
                ciudad: "Tunja");
            
            cargarCursos();
            cargarAsignaturas();
            generarEvaluaciones();                        
        }
#endregion

        #region cargas
        private void cargarAsignaturas()
        {
            foreach(var curso in Colegio.Cursos)
            {
                var listaAsignaturas = new List<Asignatura>(){
                    new Asignatura{Nombre="Matemáticas"},
                    new Asignatura{Nombre="Educación Física"},
                    new Asignatura{Nombre="Inglés"},
                    new Asignatura{Nombre="Ciencias Naturales"}
                };
                curso.Asignaturas = listaAsignaturas;
            }
        }

        private void generarEvaluaciones()
        {            
            Random rnd = new Random();
            foreach(var curso in Colegio.Cursos)
            {
                foreach(var materia in curso.Asignaturas)
                {
                    foreach(var estudiante in curso.Alumnos)
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            float calificacion = (rnd.Next(0, 5) * 1.0f) + (rnd.Next(0, 9) / 10.0f);								
                            var ev = new Evaluacion("Corte-"+i+1+"",calificacion,estudiante,materia);                            
                            estudiante.Evaluaciones.Add(ev);
                        }                        
                    }
                }
            }
        }        

        private List<Alumno> generarAlumnosAlAzar(int cantidad)
        {
            string[] nombre = {"Mickey","Minnie","gooffy","Donald","Winnie","Igor","Box"};
            string[] Apellido = {"Mouse","Bunny","Troop","Pooh","Pother","Rico","Jackson"};
            string[] nombre2 = {"Lucas","Hugo","Paco","Luis","Deisy","Mouse","Porqui"};

            //Conbinatoria
            var listaAlumnos = from n1 in nombre
                               from n2 in nombre2
                               from a1 in Apellido
                               select new Alumno{ Nombre = $"{n1} {n2} {a1}"};
            return listaAlumnos.OrderBy((al)=>al.UniqueId).Take(cantidad).ToList();
        }

        private void cargarCursos()
        {
            //Coleccion generica de tipo Curso
            var listaCursos = new List<Curso>(){
                new Curso{Nombre="101", Jornada = TiposJornada.Mañana},
                new Curso{Nombre="201", Jornada = TiposJornada.Mañana},
                new Curso{Nombre="301", Jornada = TiposJornada.Mañana},
                new Curso{Nombre="402", Jornada = TiposJornada.Mañana },
                new Curso{Nombre="502", Jornada = TiposJornada.Mañana}
            };

            Colegio.Cursos = listaCursos;
            Random rnd = new Random();
            foreach(var c in Colegio.Cursos)
            {
                int cantidadRandom = rnd.Next(5 ,20);
                c.Alumnos = generarAlumnosAlAzar(cantidadRandom);

            }

            //Eliminar miembros de colección
            //--Remueve todos los elementos de una coleccion
            //otraColeccion.Clear();
            //--remueve ciertos elementos
            //colegio.Cursos.Remove(item);

            // comentarios ctrl kc ctrl ku
            // //Delegado
            // colegio.Cursos.RemoveAll(delegate (Curso cur)
            // {
            //     return cur.Nombre == "501";
            // });

            // //Expresion Lambda
            // colegio.Cursos.RemoveAll((cur) => cur.Nombre == "502");
        }
#endregion
        
        #region listas

        public Dictionary<LlaveDiccionario, IEnumerable<ObjetoColegioBase>> GetDiccionarioObjetos(){
            
            var dicc = new Dictionary<LlaveDiccionario, IEnumerable<ObjetoColegioBase>>();
            
            dicc.Add(LlaveDiccionario.COLEGIO, new[]{Colegio});
            dicc.Add(LlaveDiccionario.CURSO, Colegio.Cursos.Cast<ObjetoColegioBase>());            

            return dicc;
            
        }

        public IReadOnlyList<ObjetoColegioBase> GetObjetosColegio()
        {
            var listaObj = new List<ObjetoColegioBase>();
            listaObj.Add(Colegio);
            listaObj.AddRange(Colegio.Cursos);
            foreach(var curso in Colegio.Cursos)
            {
                listaObj.AddRange(curso.Asignaturas);
                listaObj.AddRange(curso.Alumnos);

                foreach(var est in curso.Alumnos)
                {
                    listaObj.AddRange(est.Evaluaciones);
                }
            }
            return listaObj.AsReadOnly();
        }


        public IReadOnlyList<ObjetoColegioBase> GetObjetosColegio(
            bool traeEvaluaciones = true,
            bool traeAlumnos = true,
            bool traerAsignaturas = true,
            bool traerCursos = true
            ){
                return GetObjetosColegio(out int dummy,out dummy,out dummy,out dummy );
            }
        

        public IReadOnlyList<ObjetoColegioBase> GetObjetosColegio(
            out int conteoEval,
            out int conteoCur,
            out int conteoAsig,
            out int conteoAlum,
            bool traeEvaluaciones = true,
            bool traeAlumnos = true,
            bool traerAsignaturas = true,
            bool traerCursos = true
            )
        {
            conteoEval = conteoCur = conteoAsig = conteoAlum = 0;
            var listaObj = new List<ObjetoColegioBase>();
            listaObj.Add(Colegio);
            if(traerCursos){
                listaObj.AddRange(Colegio.Cursos);                
            }
            conteoCur += Colegio.Cursos.Count;
            foreach(var curso in Colegio.Cursos)
            {
                conteoAsig += curso.Asignaturas.Count;
                conteoAlum += curso.Alumnos.Count;
                if(traerAsignaturas){
                    listaObj.AddRange(curso.Asignaturas);
                }                
                if(traeAlumnos){
                    listaObj.AddRange(curso.Alumnos);
                }
                if(traeEvaluaciones){
                    foreach(var est in curso.Alumnos)
                    {
                        listaObj.AddRange(est.Evaluaciones);
                        conteoEval += est.Evaluaciones.Count;
                    }
                }                
            }
            return listaObj.AsReadOnly();
        }

        #endregion
    }
}