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
            char[,] kv3 = new char[12, 12];

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

            var alpPoKluchu1=NewAlp(alphabet, kv1, kl1Dis);
            var alpPoKluchu2=NewAlp(alphabet, kv2, kl2Dis);

            Console.ReadLine();
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


            for (int i = 0; i < kv1.GetLength(0); i++)
            {
                for (int j = 0; j < kv1.GetLength(1); j++)
                {
                    Console.Write(kv1[i, j] + " ");
                }
                Console.WriteLine();
            }
            return kv1;
        }
    }
}
