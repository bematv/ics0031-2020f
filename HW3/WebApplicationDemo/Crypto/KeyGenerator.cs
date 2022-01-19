using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;

namespace Crypto
{
    public class KeyGenerator
    {
        public static byte[] CreateKey(string password, int keyBytes = 32)
        {
            const int Iterations = 300;
            var keyGenerator = new Rfc2898DeriveBytes(password, Salt, Iterations);
            return keyGenerator.GetBytes(keyBytes);
        }
        public static readonly byte[] Salt = 
            new byte[] { 10, 20, 30 , 40, 50, 60, 70, 80};

        public static (ulong x, ulong y) GeneratePrimesNaive(int bitSize = 16)
        {
            List<long> primes = new List<long>();
            primes.Add(2);
            long nextPrime = 3;
            while (Convert.ToString(nextPrime, toBase: 2).Length < bitSize)
            {
                int sqrt = (int) Math.Sqrt(nextPrime);
                bool isPrime = true;
                for (int i = 0; (int) primes[i] <= sqrt; i++)
                {
                    if (nextPrime % primes[i] == 0)
                    {
                        isPrime = false;
                        break;
                    }
                }

                if (isPrime)
                {
                    primes.Add(nextPrime);
                }

                nextPrime += 2;
            }
            Random rnd = new Random();
            var r = rnd.Next(3, primes.Count/2-1);
            var p = primes[r];
            r = rnd.Next(primes.Count/2, primes.Count);
            long q = primes[r];

            return ((ulong)p, (ulong)q);
        }

        public static string messageEncrypt(byte[] messageBytes, byte[] passWBytes)
        {
            var result = new byte[passWBytes.Length];
            int cypher = 0;
            for (int i = 0; i < messageBytes.Length; i++)
            {
                cypher = (passWBytes[i] + messageBytes[i]) % (byte.MaxValue);
                   
                result[i] = (byte) cypher;
            }

            for (int i = messageBytes.Length; i < passWBytes.Length; i++)
            {
                cypher = passWBytes[i];
                result[i] = (byte) cypher;
            }
            
            return Convert.ToBase64String(result);
        }
        
        public static string messageDecrypt(byte[] messageBytes, byte[] passWBytes)
        {
            var result = new byte[passWBytes.Length];
            int cypher = 0;
            for (int i = 0; i < messageBytes.Length; i++)
            {
                cypher = messageBytes[i] - passWBytes[i] < 0
                    ? byte.MaxValue - Math.Abs(messageBytes[i] - passWBytes[i])
                    : messageBytes[i] - passWBytes[i];
                
                result[i] = (byte) cypher;
            }
            
            
            return Encoding.Default.GetString(result);
        }
    }
}