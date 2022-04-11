using System;

namespace Lexico3
{
    class Program
    {
        static void Main(string[] args)
        {
            Lexico a = new Lexico();
           
            while(!a.FinArchivo())
            {
                a.NextToken();
            }
            a.cerrar();
        }
    }
}