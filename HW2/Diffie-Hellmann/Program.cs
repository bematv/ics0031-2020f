using System;
using System.Runtime.InteropServices;

namespace Diffie_Hellman
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Prime: 
            Console.WriteLine("Please input a prime: ");
            var prime = Console.ReadLine();
            var primeInt = checkInputUlong(prime);
            if(primeInt < 0) goto Prime;
            if (CheckPrime.CheckingPrime(primeInt)) Console.WriteLine("Thank you for entering a prime number!");
            else
            {
                Console.WriteLine("It seem like you have not entered a prime, please try again.");
                goto Prime;
            }
            Console.WriteLine("Please enter a generator value:");
            Generator:
            var generator = Console.ReadLine();
            var generatorInt = checkInputUlong(generator);
            if(generatorInt < 0) goto Generator;
            Participant1:
            Console.WriteLine("Please enter a number for participant 1:");
            var par1 = Console.ReadLine();
            var par1Int = checkInputUlong(par1);
            if(par1Int < 0) goto Participant1;
            Participant2:
            Console.WriteLine("Please enter a number for participant 2:");
            var par2 = Console.ReadLine();
            var par2Int = checkInputUlong(par2);
            if(par2Int < 0) goto Participant2;
            var par1Pub = CalculatePublic(primeInt, generatorInt, par1Int);
            var par2Pub = CalculatePublic(primeInt, generatorInt, par2Int);
            Console.WriteLine("The public values for participant 1 is " + par1Pub);
            Console.WriteLine("The public values for participant 2 is " + par2Pub);
            Console.WriteLine("Par1 and Par2 will now exchange keys, metaphorically.");
            var par1Priv = CalculatePrivate(par1Pub, par2Int, primeInt);
            var par2Priv = CalculatePrivate(par2Pub, par1Int, primeInt);
            Console.WriteLine("Participant 1 and Participant 2 calculate their respective keys");
            Console.WriteLine("participant 1: " + par1Priv + ", Participant 2: " + par2Priv);
            if(par1Priv == par2Priv) Console.WriteLine("Seems like it all went well.");
            else Console.WriteLine("Yikes...");
        }

        static ulong checkInputUlong(string a)
        {
            try
            {
                var attempt = UInt64.Parse(a);
                return attempt;
            }
            catch (Exception e)
            {
                Console.WriteLine("Seems like you just tried to input something that is not a/an (positive) integer. Try again!");
                return Convert.ToUInt64(-1);
            }
        }
        
        static ulong CalculatePublic(ulong p, ulong g, ulong x)
        {
            ulong result = 0;
            result = Convert.ToUInt64(Math.Pow(g, x) % p);
            return result;
        }

        static ulong CalculatePrivate(ulong y, ulong a, ulong p)
        {
            ulong result = 0;
            result = Convert.ToUInt64(Math.Pow(y, a) % p);
            return result;
        }
    }
}
