using System;
using System.ComponentModel.DataAnnotations;

namespace RSA
{
    public class CheckPrime
    {
        public static bool CheckingPrime(ulong a)
        {
            if (a == 2) return true;
            if (a % 2 == 0) return false;
            var boundary = (ulong)Math.Floor(Math.Sqrt(a));
            for (ulong i = 3; i <= boundary; i+=2)
                if (a % i == 0)
                    return false;
            return true;
        }
    }
}