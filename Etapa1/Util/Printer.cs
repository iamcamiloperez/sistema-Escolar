using static System.Console;
namespace ColegioCore.Util
{
    public static class Printer
    {
        public static void DibujarLinea(int tamaño = 10){
            var linea = "".PadLeft(tamaño,'_');            

            WriteLine(linea+"\n");
        }

        public static void EscribirTitulo(string titulo){   
            DibujarLinea(titulo.Length+6);
            WriteLine($"|  {titulo}  |");
            DibujarLinea(titulo.Length+6);
        }

        public static void EscribirLinea(string linea){   
            WriteLine($"|  {linea}  |");
        }

        public static void Timbrar(int hz = 1000, int tiempo=500, int veces = 1){
            while(veces>0){
                Beep(hz,tiempo);
                veces--;
            }
        }
    }
}
