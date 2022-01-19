using System;
using System.Net.Mime;
using System.Text;

namespace RSA
{
    class Program
    {
        static ulong parseULong(string s)
        {
            try
            {
                var a = UInt64.Parse(s);
                return a;
            }
            catch (Exception e)
            {
                return 0;
            }
        }
        
        static ulong GCD(ulong a, ulong b)
        {
            while (a != 0 && b != 0)
            {
                if (a > b)
                    a %= b;
                else
                    b %= a;
            }

            return a | b;
        }
        static ulong findD(ulong e, ulong z) 
        {
            ulong i = z, v = 0, d = 1;
            while (e>0) {
                ulong t = i/e, x = e;
                e = i % x;
                i = x;
                x = d;
                d = v - t*x;
                v = x;
            }
            v %= z;
            if (v<0) v = (v+z)%z;
            return v;
        }

        static ulong powerFunc(ulong baseNum, ulong power)
        {
            //So apparently if I use primes that I have used in my crypto paper, say 1049 and 1543 with a derived
            //number 'e' 1511, ulong is not long enough. Will attach paper for clarity and how I had tested the program
            //until I reached this stage, went down to primes 7 and 3 afterwords
            ulong result = 1;
            for (ulong i = 0; i < power; i++)
            {
                result = result * baseNum;
            }
            return result;
        }
        
        static byte[] encrypt(string a, ulong e, ulong n)
        {
            var byteString = Encoding.Default.GetBytes(a);
            Console.Write("Your plaintext bytes: ");
            foreach (var c in byteString)
            {
                Console.Write(c + " ");
            }
            Console.WriteLine();
            ulong[] ulongString = new ulong[byteString.Length];
            ulong temp = new ulong();
            for (var i = 0; i < byteString.Length; i++)
            {
                temp = Convert.ToUInt64(byteString[i]);
                ulongString[i] = temp;
            }
            for (var i = 0; i < ulongString.Length; i++)
            {
                var powered = powerFunc(ulongString[i], e);
                powered = powered % n;
                ulongString[i] = powered;
            }
            for (var i = 0; i < byteString.Length; i++)
            {
                byteString[i] = Convert.ToByte(ulongString[i]);
            }
            return byteString;
        }
        
        static string dencrypt(string a, ulong d, ulong n)
        {
            var byteString = Convert.FromBase64String(a);
            ulong[] ulongString = new ulong[byteString.Length];
            ulong temp = new ulong();
            for (var i = 0; i < byteString.Length; i++)
            {
                temp = Convert.ToUInt64(byteString[i]);
                ulongString[i] = temp;
            }
            
            Console.WriteLine(a + " " + d + " " + n);
            //Upon debugging, seems like ulong cannot handle the lenght of the needed powered function even with lowest primes
            for (var i = 0; i < ulongString.Length; i++)
            {
                Console.WriteLine(ulongString[i]);
                var powered = powerFunc(ulongString[i], d);
                Console.WriteLine("Powered: " + powered);
                powered = powered % n;
                Console.WriteLine("Modded: " + powered);
                ulongString[i] = powered;
            }

            foreach (var c in ulongString)
            {
                Console.Write(c + " ");
            }
            for (var i = 0; i < byteString.Length; i++)
            {
                byteString[i] = Convert.ToByte(ulongString[i]);
            }

            var resultString = Encoding.UTF8.GetString(byteString);
            return resultString;
        }

        static int menu()
        {
            var choice = 0;
            Console.WriteLine();
            Console.WriteLine("Welcome to the menu!");
            Console.WriteLine("Encrypt: 1");
            Console.WriteLine("Decrypt: 2");
            Console.WriteLine("X to exit");
            var choosing = Console.ReadLine();
            Console.WriteLine();
            switch (choosing)
            {
                case "1":
                    return 1;
                case "2":
                    return 2;
                case "x":
                    return -1;
                default:
                    return 0;
            }
        }

        static void EncryptionInputs(ulong e, ulong n)
        {
            //Beggining of user input for decryption of a given plaintext/ciphertext
            StartOfEnDE:
            Console.WriteLine("Please input the plaintext you would like to encrypt.");
            var plainText = Console.ReadLine().Trim();
            var ciphertext = encrypt(plainText, e, n);
            Console.Write("Your cipher text is: ");
            foreach (var c in ciphertext)
            {
                Console.Write(c + " ");
            }
            Console.WriteLine();
            Console.WriteLine("Your ciphertext, but cooler: " + Convert.ToBase64String(ciphertext));
        }

        static void DecryptionInputs(ulong d, ulong n)
        {
            //Beggining of user input for decryption of a given plaintext/ciphertext
            StartOfEnDE:
            Console.WriteLine("Please input the ciphertext you would like to decrypt.");
            var ciphertext = Console.ReadLine().Trim();
            var plaintext = "";
            try
            { 
                plaintext = dencrypt(ciphertext, d, n);
            }
            catch (Exception e)
            {
                Console.WriteLine("Uh oh, not a base64 I see there...");
                goto StartOfEnDE;
            }
            Console.Write("Your plain text is: ");
            foreach (var c in plaintext)
            {
                Console.Write(c + " ");
            }
        }
        
        static void Main(string[] args)
        {
            //Beggining of RSA private/public compuation with user inputs
            //Getting First prime
            Prime1:
            Console.WriteLine("Please enter the first prime number:");
            var temp = "";
            temp = Console.ReadLine();
            var q = parseULong(temp);
            var check = CheckPrime.CheckingPrime(q);
            if (!check)
            {
                Console.WriteLine("It seems like you did not input a proper number or it is not prime. Try again!");
                goto Prime1;
            }
            //Getting Second prime
            Prime2:
            Console.WriteLine("Please enter the second prime number:");
            temp = Console.ReadLine();
            var p = parseULong(temp);
            check = CheckPrime.CheckingPrime(p);
            if(!check)
            {
                Console.WriteLine("It seems like you did not input a proper number or it is not prime. Try again!");
                goto Prime2;
            }
            //Computing n
            var n = p * q;
            Console.WriteLine("The computed n is " + n);
            //Computing z
            var z = (p - 1) * (q - 1);
            Console.WriteLine("The computed z is " + z);
            //Getting and verifying e as a correct value for coprime
            Etot:
            Console.WriteLine("Please enter 'e' which would comply with gcd(e, (p-1)(q-1)) = 1");
            temp = Console.ReadLine();
            var e = parseULong(temp);
            if (e == 0)
            {
                Console.WriteLine("Please enter a valid integer.");
                goto Etot;
            }

            if (GCD(e, (p - 1)) != 1 || GCD(e, (q - 1)) != 1)
            {
                Console.WriteLine("It seems that 'e' does not comply with the regulations.");
                goto Etot;
            }
            Console.WriteLine("It seems that " + e + " complies with regulations and will now be used as a public exponent.");
            //Getting modular inverse
            var d = findD(e, z);
            Console.WriteLine("Your private key has been calculated and it is " + d);
            
            Menu:
            var choice = menu();
            switch (choice)
            {
                case 1:
                    EncryptionInputs(e, n);
                    goto Menu;
                case 2:
                    DecryptionInputs(d, n);
                    goto Menu;
                case 0:
                    Console.WriteLine("Seems like you entered something that does not bode well for this program, try again.");
                    goto Menu;
                default:
                    Console.WriteLine("Was nice seeing you, have a great day!");
                    break;
            }
        }
    }
}