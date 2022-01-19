using System;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ulong x = 1500859;
            ulong y = 1304321;
            ulong z = x * y;
            for (ulong i = 1; i < z; i++)
            {
                Console.WriteLine(z/i + "- z -" + z);
                if (z / i == 0)
                {
                    Console.WriteLine(i);
                }
            }
        }
    }
}