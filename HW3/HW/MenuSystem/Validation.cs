using System;

namespace MenuSystem
{
    public class Validation
    {
        public static bool PrimesValidation(ulong inputKey)
        {
            if (inputKey < 2)
                return true;
            for (ulong i = inputKey/2; i > 1; i--)
            {
                if (inputKey % i == 0)
                    return true;
            }
            return false;
        }
        
        public static ulong KeyValidation()
        {
            ulong tKey = 0;
            var inputKey = "";
            bool wrongInput = false;
            do
            {
                if (wrongInput)
                    Console.WriteLine("Try again");
                inputKey = Console.ReadLine();
                if (ulong.TryParse(inputKey, out tKey))
                    wrongInput = PrimesValidation(tKey);
            } while (wrongInput);
            return tKey;
        }
        protected static ulong GCD(ulong e, ulong z)
        {
            if (e == 0)
                return z;
            return GCD(z % e, e);
        }
        
        public static ulong modPow(ulong bse, ulong exponent, ulong modulus)
        {
            ulong results = 1;
            bse = bse % modulus;
            while (exponent > 0)
            {
                if ((exponent & 1) == 1)
                    results = results * bse % modulus;
                
                exponent >>= 1;
                bse = bse * bse % modulus;
            }
            return results;
        }
    }
}