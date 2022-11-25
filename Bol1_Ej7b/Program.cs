using System.Threading;

namespace Bol1_Ej7b
{
    class Program
    {
        static readonly object l = new object();
        static Random r = new Random();
        static int random;
        static int randomSleep;
        static int contador = 0;
        static int pos = 0;
        static int i = 0;
        static bool arranca;
        static bool finished;
        static string ganador = "";

        static void Main(string[] args)
        {
            arranca = true;
            finished = false;

            Thread thread1 = new Thread(player1);
            Thread thread2 = new Thread(player2);
            thread1.IsBackground = true;
            thread2.IsBackground = true;
            Thread thread3 = new Thread(display);
            thread1.Start();
            thread2.Start();
            thread3.Start();
        }

        static void player1()
        {
            while (!finished)
            {
                lock (l)
                {
                    if (!finished)
                    {
                        random = r.Next(1, 11);

                        Console.SetCursorPosition(0, 3);
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Número:{0,3}", random);
                        //Console.WriteLine(random + " Cont: " + contador + " ");
                        Console.ResetColor();

                        if (random == 5 || random == 7)
                        {
                            if (arranca)
                            {
                                contador++;
                                arranca = false;
                            }
                            else
                            {
                                contador += 5;
                            }
                        }

                        if (contador >= 10)
                        {
                            finished = true;
                            ganador = "Player 1";
                        }

                        //random = r.Next(100, 100 * random);
                        randomSleep = r.Next(100, 100 * random);
                    }
                }
                Thread.Sleep(randomSleep);
            }
        }

        static void player2()
        {
            while (!finished)
            {
                lock (l)
                {
                    if (!finished)
                    {
                        random = r.Next(1, 11);

                        Console.SetCursorPosition(0, 5);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Número:{0,3}", random);
                        //Console.WriteLine(random + " Cont: " + contador + " ");
                        Console.ResetColor();

                        if (random == 5 || random == 7)
                        {
                            if (!arranca)
                            {
                                contador--;
                                arranca = true;
                                Monitor.Pulse(l);
                            }
                            else
                            {
                                contador -= 5;
                            }
                        }

                        if (contador <= -10)
                        {
                            finished = true;
                            ganador = "Player 2";

                        }

                        //random = r.Next(100, 100 * random);
                        randomSleep = r.Next(100, 100 * random);
                    }
                }
                Thread.Sleep(randomSleep);
            }
        }

        static void display()
        {
            char[] caracteres = { '|', '/', '-', '\\' };

            if (!arranca)
            {
                lock (l)
                {
                    Monitor.Wait(l);
                }
            }

            while (!finished)
            {
                lock (l)
                {
                    if (!finished)
                    {
                        //if (!arranca)
                        //{
                        //    Monitor.Wait(l);
                        //}

                        Console.SetCursorPosition(0, 0);
                        Console.WriteLine("Caracter:  " + caracteres[pos]);
                        if (pos != 3)
                        {
                            pos++;
                        }
                        else
                        {
                            pos = 0;
                        }
                        i++;
                    }
                }
                Thread.Sleep(200);
            }

            Console.SetCursorPosition(0, 7);
            Console.WriteLine("El ganador ha sido: " +ganador+" Contador: "+contador);
                
        }
    }
}
