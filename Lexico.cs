namespace Lexico3
{
    //requerimiento 1: los caracteres entrecomillados tambien son caracteres ejemplo: 'a','#',')' etc
    //                 expresion regular: caracter -> 'c'
    //requerimiento 2: los caracteres con # tambien son caracteres ejemplo: #10,#64,#192,#32
    //                 expresion regular: caracter -> #D(D)?(D)?

    //1.-Agregar los dos automatas del caracter(entrecomillados o #) en el JFlap
    //2.-Agregar los dos automatas en la matriz de transiciones: excel,csv,C#

    public class Lexico : Token
    {
        StreamReader archivo;
        StreamWriter Log;
        const int F = -1;
        const int E = -2;

        int[,] TRAND = new int[,]
        {
            //WS,EF,EL,L, D, .,	E, +, -, =,	:, ;, &, |,	!, >, <, *,	%, /, ", ?,La, ', #
            { 0, 0, 0, 1, 2,33, 1,21,22, 8,10,12,13,14,15,18,19,24,24,29,26,28,33,34,37}, //estado 0
            { F, F, F, 1, 1, F, 1, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F}, //estado 1
            { F, F, F, F, 2, 3, 5, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F}, //estado 2
            { E, E, E, E, 4, E, E, E, E, E, E, E, E, E, E, E, E, E, E, E, E, E, E, E, E}, //estado 3
            { F, F, F, F, 4, F, 5, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F}, //estado 4
            { E, E, E, E, 7, E, E, 6, 6, E, E, E, E, E, E, E, E, E, E, E, E, E, E, E, E}, //estado 5
            { E, E, E, E, 7, E, E, E, E, E, E, E, E, E, E, E, E, E, E, E, E, E, E, E, E}, //estado 6
            { F, F, F, F, 7, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F}, //estado 7
            { F, F, F, F, F, F, F, F, F, 9, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F}, //estado 8
            { F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F}, //estado 9
            { F, F, F, F, F, F, F, F, F,11, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F}, //estado 10
            { F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F}, //estado 11
            { F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F}, //estado 12
            { F, F, F, F, F, F, F, F, F, F, F, F,16, F, F, F, F, F, F, F, F, F, F, F, F}, //estado 13
            { F, F, F, F, F, F, F, F, F, F, F, F, F,16, F, F, F, F, F, F, F, F, F, F, F}, //estado 14
            { F, F, F, F, F, F, F, F, F,17, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F}, //estado 15
            { F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F}, //estado 16
            { F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F}, //estado 17
            { F, F, F, F, F, F, F, F, F,20, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F}, //estado 18
            { F, F, F, F, F, F, F, F, F,20, F, F, F, F, F,20, F, F, F, F, F, F, F, F, F}, //estado 19
            { F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F}, //estado 20
            { F, F, F, F, F, F, F,23, F,23, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F}, //estado 21
            { F, F, F, F, F, F, F, F,23,23, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F}, //estado 22
            { F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F}, //estado 23
            { F, F, F, F, F, F, F, F, F,25, F, F, F, F, F, F, F, F, F, F, F, F ,F, F, F}, //estado 24
            { F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F}, //estado 25
            {26, E,26,26,26,26,26,26,26,26,26,26,26,26,26,26,26,26,26,26,27,26,26,26,26}, //estado 26
            { F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F}, //estado 27
            { F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F}, //estado 28
            { F, F, F, F, F, F, F, F, F,25, F, F, F, F, F, F, F,31, F,30, F, F, F, F, F}, //estado 29
            {30, 0, 0,30,30,30,30,30,30,30,30,30,30,30,30,30,30,30,30,30,30,30,30,30,30}, //estado 30
            {31, E,31,31,31,31,31,31,31,31,31,31,31,31,31,31,31,32,31,31,31,31,31,31,31}, //estado 31
            {31, E,31,31,31,31,31,31,31,31,31,31,31,31,31,31,31,32,31, 0,31,31,31,31,31}, //estado 32
            { F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F}, //estado 33
            {35, E, E,35,35,35,35,35,35,35,35,35,35,35,35,35,35,35,35,35,35,35,35,35,35}, //estado 34
            { E, E, E, E, E, E, E, E, E, E, E, E, E, E, E, E, E, E, E, E, E, E, E,36, E}, //estado 35
            { F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F}, //estado 36
            { F, F, F, F,38, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F}, //estado 37
            { F, F, F, F,39, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F}, //estado 38
            { F, F, F, F,40, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F}, //estado 39
            { F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F, F}, //estado 40
            
            //WS,EF,EL,L, D, .,	E, +, -, =,	:, ;, &, |,	!, >, <, *,	%, /, ", ?,La, ', #
        };
        public Lexico()
        {
            archivo = new StreamReader("C:\\Users\\marco\\OneDrive\\Escritorio\\Lenguajes y automatas 1\\Lexico3\\prueba.cpp");
            Log     = new StreamWriter("C:\\Users\\marco\\OneDrive\\Escritorio\\Lenguajes y automatas 1\\Lexico3\\prueba.Log"); 
            Log.AutoFlush = true;
        }
        public void cerrar()
        {
            archivo.Close();
            Log.Close();
        }       

        private void clasifica(int estado)
        {
            switch(estado)
            {
                case 1: 
                    setClasificacion(Tipos.Identificador);
                    break;
                case 2:
                    setClasificacion(Tipos.Numero);
                    break;
                case 8:
                    setClasificacion(Tipos.Asignacion);
                    break;
                case 9:
                case 17:
                case 18:
                case 19:
                    setClasificacion(Tipos.OperadorRelacional);
                    break;
                case 10:
                case 13:
                case 14:
                    setClasificacion(Tipos.Caracter);
                    break;
                case 11:
                    setClasificacion(Tipos.Inicializacion);
                    break;
                case 12:
                    setClasificacion(Tipos.FinSentencia);
                    break;
                case 15:
                case 16:
                    setClasificacion(Tipos.OperadorLogico);
                    break;
                case 21:
                case 22:
                    setClasificacion(Tipos.OperadorTermino);
                    break;
                case 23:
                    setClasificacion(Tipos.IncrementoTermino);
                    break;   
                case 24:
                case 29:
                    setClasificacion(Tipos.OperadorFactor);
                    break;
                case 25:
                    setClasificacion(Tipos.IncrementoFactor);
                    break;
                case 26:
                    setClasificacion(Tipos.Cadena);
                    break;
                case 28:
                    setClasificacion(Tipos.OperadorTernario);
                    break;
                case 33:
                case 34:
                case 36:
                case 37:
                case 38:
                    setClasificacion(Tipos.Caracter);
                    break;
            } 
        }
        private int columna(char c)
        {
            //WS,EF,EL,L, D, .,	E, +, -, =,	:, ;, &, |,	!, >, <, *,	%, /, ", ?,La
            if(FinArchivo())
            {
                return 1;
            }
            else if(c == '\n')
            {
                return 2;
            }
            else if(char.IsWhiteSpace(c))
            {
                return 0;
            }
            else if(char.ToUpper(c) == 'E')
            {
                return 6;
            }
            else if(char.IsLetter(c))
            {
                return 3;
            }
            else if(char.IsDigit(c))
            {
                return 4;
            }
            else if(c == '.')
            {
                return 5;
            }
            else if(c == '+')
            {
                return 7;
            }
            else if(c == '-')
            {
                return 8;
            }
            else if(c == '=')
            {
                return 9;
            }
            else if(c == ':')
            {
                return 10;
            }
            else if(c == ';')
            {
                return 11;
            }
            else if(c == '&')
            {
                return 12;
            }
            else if(c == '|')
            {
                return 13;
            }
            else if(c == '!')
            {
                return 14;
            }
            else if(c == '>')
            {
                return 15;
            }
            else if(c == '<')
            {
                return 16;
            }
            else if(c == '*')
            {
                return 17;
            }
            else if(c == '%')
            {
                return 18;
            }
            else if(c == '/')
            {
                return 19;
            }
            else if(c == '"')
            {
                return 20;
            }
            else if(c == '?')
            {
                return 21;
            }
            else if(c == 39)
            {
                return 23;
            }
            else if(c == '#')
            {
                return 24;
            }
            return 22;
        }
        //WS,EF,EL,L, D, .,	E, +, -, =,	:, ;, &, |,	!, >, <, *,	%, /, ", ?,La, ', #
        public void NextToken() 
        {
            string buffer = "";           
            char c;      
            int estado = 0;


            while(estado >= 0)
            {
                c = (char)archivo.Peek(); //Funcion de transicion
                estado = TRAND[estado,columna(c)];
                clasifica(estado);
                if (estado >= 0)
                {
                    archivo.Read();
                    if (estado >0)
                    {
                        buffer += c;
                    }
                    else
                    {
                        buffer = "";
                    }
                }
            }
            if(estado == E)
            {
                Console.WriteLine("Error lexico");
                Log.WriteLine("Error lexico");
            }
            setContenido(buffer); 
            switch(buffer)
            {
                case "char":
                case "int":
                case "float":
                        setClasificacion(Tipos.TipoDato);
                        break;
                case "private":
                case "protected":
                case "public":
                        setClasificacion(Tipos.Zona);
                        break;
                case "if":
                case "else":
                case "switch":
                        setClasificacion(Tipos.Condicion);
                        break;
                case "while":
                case "for":
                case "do":
                        setClasificacion(Tipos.Ciclo);
                        break;
            }
            Log.WriteLine(getContenido() + " | " + getClasificacion());
        }

        public bool FinArchivo()
        {
            return archivo.EndOfStream;
        }
    }
}