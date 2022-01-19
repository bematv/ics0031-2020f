using System;

namespace Crypto
{
    public class Validation
    {
        
        protected static ulong Coprime(ulong m)
        {
            ulong e = 2;
            while (GCD(m, e) != 1)
            {
                e++;
            }
    
            return e;
        }
        protected static ulong GCD(ulong e, ulong z)
        {
            if (e == 0)
                return z;
            return GCD(z % e, e);
        }
        protected static ulong ModularInverse(ulong m, ulong e)
        {
            ulong k = 1;
            while (true)
            {
                var x = 1 + (k * m);
                if (x % e == 0)
                    return x / e;
                k++;
            }
        }
        protected static ulong modPow(ulong bse, ulong exponent, ulong modulus)
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

        protected static int roundUpToNextPowerOfTwo(int v)
        {
            v--;
            v |= v >> 1;
            v |= v >> 2;
            v |= v >> 4;
            v |= v >> 8;
            v |= v >> 16;
            v++;
            return v;
        }
    }
}