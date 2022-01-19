using System;

namespace MenuSystem
{
    public class KeyExchange : Validation
    {
        private ulong _pKeyFirst;
        private ulong _pKeySecond;
        private ulong _pSecretFirst;
        private ulong _pSecretSecond;


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

        public void keyExchange()
        {
            Console.WriteLine("Input first public key (has to be a prime number)");
            _pKeyFirst = KeyValidation();
            Console.WriteLine("Input second public key (has to be a prime number)");
            _pKeySecond = KeyValidation();

            ulong x;
            ulong pFirst;
            ulong pSecond;
            Console.WriteLine("Input first secret key (has to be a prime number)");
            String p1 = Console.ReadLine();

            while (!ulong.TryParse(p1, out x))
            {
                Console.WriteLine("Not a valid number, try again.");
                p1 = Console.ReadLine();
            }

            Console.WriteLine("Input second secret key (has to be a prime number)");
            String p2 = Console.ReadLine();

            while (!ulong.TryParse(p2, out x))
            {
                Console.WriteLine("Not a valid number, try again.");
                p2 = Console.ReadLine();
            }

            pFirst = Convert.ToUInt64(p1);
            pSecond = Convert.ToUInt64(p2);

            _pSecretFirst = KeyGenerator(_pKeyFirst, pFirst, _pKeySecond);
            _pSecretSecond = KeyGenerator(_pKeyFirst, pSecond, _pKeySecond);
            Console.WriteLine("First common key is - " +
                              KeyGenerator(_pSecretSecond, pFirst, _pKeySecond));
            Console.WriteLine("Second common key is - " +
                              KeyGenerator(_pSecretFirst, pSecond, _pKeySecond));
        }
    }
}