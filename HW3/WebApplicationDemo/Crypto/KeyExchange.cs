using System;
using System.Security.Cryptography.X509Certificates;
using Domain;

namespace Crypto
{
    public class KeyExchange
    {
        public static bool PrimesValidation(ulong prime)
        {
            if (prime < 2)
                return false;
            for (ulong i = prime/2; i > 1; i--)
            {
                if (prime % i == 0)
                    return false;
            }
            return true;
        }

        static ulong KeyGenerator(ulong publicKeyA, ulong secretPart, ulong publicKeyB)
        {
            
            ulong powerOfKeyValue = 1;
            ulong remainingPart = publicKeyA % publicKeyB;
            for (ulong i = secretPart; i > 0; i--)
            {
                
                if (powerOfKeyValue < publicKeyB && i != secretPart)
                {
                    powerOfKeyValue = powerOfKeyValue * publicKeyA;
                    if (powerOfKeyValue > publicKeyB)
                    {
                        remainingPart = powerOfKeyValue % publicKeyB;
                        powerOfKeyValue = 1;
                    }
                }

                if (remainingPart != 0)
                {
                    powerOfKeyValue = remainingPart * powerOfKeyValue;
                    remainingPart = 1;
                }
            }

            return powerOfKeyValue;
        }
        public static void KeyExchangeImplementation(ExchangeKey key)
        {

            
            var p1 = key.ASecret;

            var p2 = key.BSecret;

            

            // pFirst = Convert.ToUInt64(p1);
            // pSecond = Convert.ToUInt64(p2);

            p1 = KeyGenerator(key.P, key.ASecret, key.G);
            p2 = KeyGenerator(key.P, key.BSecret, key.G);
            
            key.CommonSecret = KeyGenerator(p2, p1, key.G);
            //KeyGenerator(key.ASecret, key.BSecret, key.G);
        }
    }
}