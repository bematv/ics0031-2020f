using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;


namespace MenuSystem
{
    public class RSA : Validation
    {
        private static ulong _prKey;
        private static ulong _pKey;
        private static ulong _cipher;
        private static ulong _exponent;
        private const string RsaOption = "Decrypt";

        public RSA()
        {
            _prKey = 0;
        }

        public static void RsaImplemantation(string option = "")
        {
            if (option == RsaOption)
                RsaDecrypt();
            else
            {
                Console.Write("Please Enter your text here\n");
                var txt = Console.ReadLine();
                ulong message = ulongValidation(txt);
                


                Console.WriteLine("Enter first prime number");
                var p = KeyValidation();
                Console.WriteLine("Enter second prime number");
                var q = KeyValidation();

                _pKey = p * q;
                var m = (p - 1) * (q - 1);

                _exponent = Coprime(m);

                Console.WriteLine($"Public key: n:{_pKey} exponent:{_exponent}");

                _prKey = ModularInverse(m);
                

                
                Console.WriteLine($"Private key: p*q=n : {p}*{q}={_pKey} Private key:{_prKey}");
                Console.WriteLine($"Message:{message}");
                _cipher = modPow(message, _exponent, _pKey);
                Console.WriteLine("Cipher is : " + _cipher);
                
            }
        }

       private static void RsaDecrypt()
        {
            
            Console.WriteLine("Please Enter your cipher here");
            var txt = Console.ReadLine();
            _cipher = ulongValidation(txt);

            Console.WriteLine("Enter private key");
            txt = Console.ReadLine();
            _prKey = ulongValidation(txt);
            
            Console.WriteLine("Enter public key");
            txt = Console.ReadLine();
            _pKey = ulongValidation(txt);
            
            var decryption = modPow(_cipher, _prKey, _pKey);
            Console.WriteLine("Decrypted message is : " + decryption);
        }

        public static void RsaBruteforce()
        {
            Console.WriteLine("Please Enter your cipher here");
            var txt = Console.ReadLine();
            _cipher = ulongValidation(txt);

            Console.WriteLine("Enter public key");
            txt = Console.ReadLine();
            _pKey = ulongValidation(txt);

            ulong p = 0, q;
            p = GeneratePrimesNaive(_pKey/2);
            q = _pKey / p;
            
            var m = (p - 1) * (q - 1);
            _exponent = Coprime(m);
            _prKey = ModularInverse(m);
            
            var decryption = modPow(_cipher, _prKey, _pKey);
            Console.WriteLine("Decrypted message is : " + decryption);


        }

        private static ulong ulongValidation(string txt)
        {
            ulong message;
            while (!ulong.TryParse(txt, out message))
            {
                Console.WriteLine("Not a valid number, try again.");
                txt = Console.ReadLine();
            }

            return message;
        }
        
        private static ulong GeneratePrimesNaive(ulong n)
        {
            List<ulong> primes = new List<ulong>();
            primes.Add(2);
            ulong nextPrime = 3;
            while ((ulong)primes.Count < n)
            {
                int sqrt = (int)Math.Sqrt(nextPrime);
                bool isPrime = true;
                for (int i = 0; (int)primes[i] <= sqrt; i++)
                {
                    if (nextPrime % primes[i] == 0)
                    {
                        isPrime = false;
                        break;
                    }
                }
                if (isPrime)
                {
                    if (_pKey % nextPrime == 0)
                        return nextPrime;
                    primes.Add(nextPrime);
                }
                nextPrime += 2;
            }
            return 0;
        }
        
        private static ulong Coprime(ulong m)
        {
            ulong e = 2;
            while (GCD(m, e) != 1)
            {
                e++;
            }
    
            return e;
        }

        private static ulong ModularInverse(ulong m)
        {
            ulong k = 1;
            while (true)
            {
                var x = 1 + (k * m);
                if (x % _exponent == 0)
                    return x / _exponent;
                k++;
            }
        }


    }
}