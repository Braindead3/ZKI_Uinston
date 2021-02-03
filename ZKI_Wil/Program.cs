using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZKI_Wil
{
    class Program
    {
        static void Main(string[] args)
        {
            char[] alphabet = { 'А', 'Б', 'В', 'Г', 'Д', 'Е', 'Ё', 'Ж', 'З', 'И', 'Й', 'К', 'Л', 'М', 'Н', 'О', 'П', 'Р', 'С', 'Т', 'У', 'Ф', 'Х', 'Ц', 'Ч', 'Ш', 'Щ', 'Ъ', 'Ы', 'Ь', 'Э', 'Ю', 'Я', ' ', ',', '.' };
            char[,] kv1 = new char[6, 6];
            char[,] kv2 = new char[6, 6];
            char[,] kv3 = new char[6, 12];

            Console.WriteLine("Kluch1:");
            string kluch1 = Console.ReadLine();

            kluch1 = kluch1.ToUpper();
            var kl1 = kluch1.Distinct();
            char[] kl1Dis = kl1.ToArray();

            Console.WriteLine("Kluch2:");
            string kluch2 = Console.ReadLine();

            kluch2 = kluch2.ToUpper();
            var kl2 = kluch2.Distinct();
            char[] kl2Dis = kl2.ToArray();

            var alpPoKluchu1 = NewAlp(alphabet, kv1, kl1Dis);
            var alpPoKluchu2 = NewAlp(alphabet, kv2, kl2Dis);

            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    kv3[i, j] = alpPoKluchu1[i, j];
                }
            }

            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    kv3[i, j + 6] = alpPoKluchu2[i, j];
                }
            }

            AlpOutput(kv3);

            Console.WriteLine("Введите сообщение:");
            string mes = Console.ReadLine();
            mes = mes.ToUpper();
            if (mes.Count() % 2 != 0)
            {
                mes += " ";
            }
            
            string cryptText = "";

            cryptText=Crypt(kv3, mes);
            string decryptText = Decrypt(kv3, cryptText);
            Console.WriteLine(mes);
            Console.WriteLine(cryptText);
            Console.WriteLine(decryptText);

            Console.ReadLine();
        }

        private static string  Crypt(char[,] kv3, string mes)
        {
            string cryptText="";
            int rowBuk1 = 0, colBuk1 = 0, rowBuk2 = 0, colBuk2 = 0;
            for (int i = 0; i < mes.Length - 1; i += 2)
            {
                for (int r = 0; r < 6; r++)
                {
                    for (int c = 0; c < 6; c++)
                    {
                        if (mes[i] == kv3[r, c])
                        {
                            rowBuk1 = r;
                            colBuk1 = c;
                        }
                    }
                }

                for (int r = 0; r < kv3.GetLength(0); r++)
                {
                    for (int c = 6; c < kv3.GetLength(1); c++)
                    {
                        if (mes[i + 1] == kv3[r, c])
                        {
                            rowBuk2 = r;
                            colBuk2 = c;
                        }
                    }
                }

                if (rowBuk1 == rowBuk2)
                {
                    if (colBuk1 == 5)
                    {
                        cryptText += kv3[rowBuk1, colBuk1+1];
                    }
                    if (colBuk2 == 11)
                    {
                        cryptText += kv3[rowBuk2, 0];
                    }
                    else
                    {
                        cryptText += kv3[rowBuk1, colBuk1 + 1];
                        cryptText += kv3[rowBuk2, colBuk2 + 1];
                    }
                }
                else
                {
                    cryptText += kv3[rowBuk1, colBuk2];
                    cryptText += kv3[rowBuk2, colBuk1];
                }
            }
            return cryptText;
        }

        private static string Decrypt(char[,] kv3, string cryptText)
        {
            string mes = cryptText;
            cryptText = "";
            int rowBuk1 = 0, colBuk1 = 0, rowBuk2 = 0, colBuk2 = 0;
            for (int i = 0; i < mes.Length - 1; i += 2)
            {
                for (int r = 0; r < kv3.GetLength(0); r++)
                {
                    for (int c = 6; c < kv3.GetLength(1); c++)
                    {
                        if (mes[i] == kv3[r, c])
                        {
                            rowBuk1 = r;
                            colBuk1 = c;
                        }
                    }
                }

                for (int r = 0; r < 6; r++)
                {
                    for (int c = 0; c < 6; c++)
                    {
                        if (mes[i + 1] == kv3[r, c])
                        {
                            rowBuk2 = r;
                            colBuk2 = c;
                        }
                    }
                }

                if (rowBuk1 == rowBuk2)
                {
                    if (colBuk1 == 6)
                    {
                        cryptText += kv3[rowBuk1, colBuk1-1];
                    }
                    if (colBuk2 == 0)
                    {
                        cryptText += kv3[rowBuk2, 11];
                    }
                    else
                    {
                        cryptText += kv3[rowBuk1, colBuk2 - 1];
                        cryptText += kv3[rowBuk2, colBuk1 - 1];
                    }

                }
                else
                {
                    cryptText += kv3[rowBuk1, colBuk2];
                    cryptText += kv3[rowBuk2, colBuk1];
                }
            }
            return cryptText;
        }

        private static void AlpOutput(char[,] kv1)
        {
            for (int i = 0; i < kv1.GetLength(0); i++)
            {
                for (int j = 0; j < kv1.GetLength(1); j++)
                {
                    Console.Write(kv1[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        private static char[,] NewAlp(char[] alphabet, char[,] kv1, char[] kl1Dis)
        {
            int rows = 0, columns = 0;

            for (int i = 0; i < kl1Dis.Length; i++)
            {
                kv1[rows, columns] = kl1Dis[i];
                columns++;
                if (columns == kv1.GetLength(1))
                {
                    columns = 0;
                    rows++;
                }
            }

            char[] alpDis1 = alphabet.Except(kl1Dis).ToArray();
            int counter = 0;

            for (int i = rows; i < kv1.GetLength(0); i++)
            {
                for (int j = columns; j < kv1.GetLength(1); j++)
                {
                    kv1[i, j] = alpDis1[counter];
                    counter++;
                    columns = 0;
                }
            }
            return kv1;
        }
    }
}
