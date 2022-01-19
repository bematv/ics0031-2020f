using System;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Text.Unicode;

namespace shift_ciphers
{
    class Program
    { 
        static void Main(string[] args) 
        { 
            Console.WriteLine("Benedek Matveev - 201840IVSB"); 
            Console.WriteLine("\n");
            
            // Main Menu
            // support any skip -255 to 255
            // support utf-8, use base64 encoding to go to 7 bit characters
            var userChoice = "";
            do
            {
                // Set global alphabet
                // The base-64 digits in ascending order from zero are
                // the uppercase characters "A" to "Z",
                // the lowercase characters "a" to "z",
                // the numerals "0" to "9",
                // and the symbols "+" and "/".
                // The valueless character "=" is used for trailing padding.
                var base64Alphabet =
                    "ABCDEFGHIJKLMNOPQRSTUVWXYZ" +
                    "abcdefghijklmnopqrstuvwxyz" +
                    "0123456789" +
                    "+/="; //"=" as filler
                Console.WriteLine("" +
                                  "   _____ __    _ ______     _______       __                  \n" +
                                  "  / ___// /_  (_) __/ /_   / ____(_)___  / /_  ___  __________\n" +
                                  "  \\__ \\/ __ \\/ / /_/ __/  / /   / / __ \\/ __ \\/ _ \\/ ___/ ___/\n" +
                                  " ___/ / / / / / __/ /_   / /___/ / /_/ / / / /  __/ /  (__  )\n" +
                                  "/____/_/ /_/_/_/  \\__/   \\____/_/ .___/_/ /_/\\___/_/  /____/\n" +
                                  "                               /_/                               " +
                                  "");
                Console.WriteLine("                   The Incredible Shift Ciphers                        ");
                Console.WriteLine("" +
                                  "   _____ __  __________________   ______________  __  ____________  _____\n" +
                                  "  / ___// / / /  _/ ____/_  __/  / ____/  _/ __ \\/ / / / ____/ __ \\/ ___/\n" +
                                  "  \\__ \\/ /_/ // // /_    / /    / /    / // /_/ / /_/ / __/ / /_/ /\\__ \\ \n" +
                                  " ___/ / __  // // __/   / /    / /____/ // ____/ __  / /___/ _, _/___/ / \n" +
                                  "/____/_/ /_/___/_/     /_/     \\____/___/_/   /_/ /_/_____/_/ |_|/____/  \n" +
                                  "                                                              by bematv../\n");
                
                Console.WriteLine("C) Caesar");
                Console.WriteLine("V) Vigenere");
                Console.WriteLine("E) Exit");
                Console.WriteLine("-----------------");
                Console.Write("Your choice: ");

                // Read the input value
                userChoice = Console.ReadLine();
                userChoice = userChoice?.Trim();

                // Loop for cipher selection
                switch (userChoice.ToUpper())
                {
                    case "C":
                        Console.WriteLine("\n");
                        DoCaesar(base64Alphabet);
                        break;

                    case "C)":
                        Console.WriteLine("\n");
                        DoCaesar(base64Alphabet);
                        break;

                    case "V":
                        Console.WriteLine("\n");
                        DoVigenere(base64Alphabet);
                        break;

                    case "V)":
                        Console.WriteLine("\n");
                        DoVigenere(base64Alphabet);
                        break;

                    case "E":
                        Console.WriteLine("\n");
                        Environment.Exit(0);
                        break;

                    case "E)":
                        Console.WriteLine("\n");
                        break;

                    default:
                        Console.WriteLine($"This '{userChoice}' is not and valid input! ;)");
                        Console.WriteLine("\n");
                        break;
                }
            } while (userChoice != "E");
        }




        // Caesar cypher menu
        static void DoCaesar(String base64Alphabet)
        {
            bool inputIsValid = false;
            do
            {
                Console.WriteLine("========== Caesar ===========");
                Console.WriteLine("E) Encrypt");
                Console.WriteLine("D) Decrypt");
                Console.WriteLine("B) Back");
                Console.WriteLine("-----------------");
                Console.Write("Your choice: ");
            
            var userChoiceCrypt = Console.ReadLine();
                userChoiceCrypt = userChoiceCrypt?.Trim();

                // Loop for cryptography selection
                switch (userChoiceCrypt.ToUpper())
                {
                    case "E":
                        inputIsValid = true;
                        Console.WriteLine("\n");
                        DoCaesarEncrypt(base64Alphabet);
                        break;

                    case "E)":
                        inputIsValid = true;
                        Console.WriteLine("\n");
                        DoCaesarEncrypt(base64Alphabet);
                        break;

                    case "D":
                        inputIsValid = true;
                        Console.WriteLine("\n");
                        DoCaesarDecrypt(base64Alphabet);
                        break;

                    case "D)":
                        inputIsValid = true;
                        Console.WriteLine("\n");
                        DoCaesarDecrypt(base64Alphabet);
                        break;

                    case "B":
                        inputIsValid = true;
                        Console.WriteLine("\n");
                        break;

                    case "B)":
                        inputIsValid = true;
                        Console.WriteLine("\n");
                        break;

                    default:
                        inputIsValid = false;
                        Console.WriteLine($"This '{userChoiceCrypt}' is not and valid input!");
                        Console.WriteLine("\n");
                        break;
                }
            } while (!inputIsValid);
            // Read the input value
            




            // Caesar cipher encryption
            static void DoCaesarEncrypt(String base64Alphabet)
            {
                Console.WriteLine("========== Caesar Enccryption ===========");


                // Get user input: shift amount
                bool inputIsValid = false;
                int shiftAmount = 0;

                do
                {
                    // Get shift amount
                    Console.Write("Please input shift amount (-65 - 65) (enter C to cancel): ");
                    var shiftString = Console.ReadLine()?.Trim();

                    // Cancel
                    if (shiftString.ToUpper() == "C")
                    {
                        inputIsValid = true;
                        return;
                    }

                    // Valid
                    inputIsValid = int.TryParse(shiftString, out shiftAmount);

                    // Invalid
                    if (!inputIsValid)
                    {
                        Console.WriteLine($"This '{shiftString}' is not and valid input!");
                    }
                } while (!inputIsValid);

                shiftAmount = shiftAmount % base64Alphabet.Length;
                Console.WriteLine($"Caesar shift: {shiftAmount}");


                // Get user input: plain text
                var plainText = "";
                do
                {
                    Console.Write("Please enter plain text: (enter C to cancel): ");
                    plainText = Console.ReadLine();
                    if (plainText.ToUpper() == "C")
                    {
                        inputIsValid = true;
                        return;
                    }
                    else if (plainText == "")
                    {
                        inputIsValid = false;
                        Console.WriteLine($"This '{plainText}' is not and valid input! Please write something!");
                    }
                    else
                    {
                        inputIsValid = true;
                    }
                } while (!inputIsValid);


                // Convert input to utf8
                var utf8 = new UTF8Encoding();
                var utf8Bytes = utf8.GetBytes(plainText);
                var base64Char = System.Convert.ToBase64String(utf8Bytes);

                // Convert utf8 to base64
                Console.WriteLine();
                Console.WriteLine("========== Convert plain text to base64 ===========");

                /*
                // !!! SYSTEM CRASH with too much foreign characters
                // Loop for convert
                String lastThreeDigits = "";
                Console.Write("Convert: ");
                 
                for (var i = 0; i < utf8Bytes.Length; i++)
                {
                    Console.Write($"{plainText[i]} -> {base64Char[i]}, ");
                    lastThreeDigits = base64Char.Substring((base64Char.Length - 3), 3);
                }

                Console.Write($"-> {lastThreeDigits}");
                Console.WriteLine();
                Console.WriteLine("========== Base64 converted: ===========");
                */
                 

                // Convert utf8 to base64
                var base64Str = System.Convert.ToBase64String(utf8Bytes);
                Console.WriteLine($"Base64 converted: {base64Str}");
                Console.WriteLine();


                /*
                // Encryption for base64
                Console.WriteLine("========== Base64 encryption: ===========");
                Console.Write("Encrypt: ");
                */
                String base64Encr = "";
                for (int i = 0; i < base64Str.Length; i++)
                {
                    var b64Char = base64Str[i];
                    var chrIndex = base64Alphabet.IndexOf(b64Char);
                    chrIndex = chrIndex + shiftAmount;

                    if (chrIndex < 0)
                    {
                        chrIndex = base64Alphabet.Length + chrIndex;
                    }
                    else if (chrIndex > (base64Alphabet.Length - 1))
                    {
                        chrIndex = chrIndex - (base64Alphabet.Length);
                    }

                    /*Console.Write($"{b64Char} -> {base64Alphabet[chrIndex]}, ");*/
                    base64Encr = base64Encr + base64Alphabet[chrIndex];
                }

                Console.WriteLine();
                Console.WriteLine("========== Base64 encrypted: ===========");
                Console.WriteLine($"Base64 encrypted: {base64Encr}");
                Console.WriteLine("\n");
            }




            // Caesar cipher Decryption
            static void DoCaesarDecrypt(String base64Alphabet)
            {
                Console.WriteLine("========== Caesar Decryption ===========");

                // Get user input: shift amount
                bool inputIsValid = false;
                int shiftAmount = 0;

                do
                {
                    // Shift amount
                    Console.Write("Please input shift amount (-65 - 65) (enter C to cancel): ");
                    var shiftString = Console.ReadLine()?.Trim();

                    // Cancel
                    if (shiftString.ToUpper() == "C")
                    {
                        inputIsValid = true;
                        return;
                    }

                    // Valid
                    inputIsValid = int.TryParse(shiftString, out shiftAmount);

                    // Invalid
                    if (!inputIsValid)
                    {
                        Console.WriteLine($"This '{shiftString}' is not and valid input!");
                    }
                } while (!inputIsValid);

                shiftAmount = shiftAmount % base64Alphabet.Length;
                Console.WriteLine($"Caesar shift: {shiftAmount}");



                // Get user input: plain text
                var cipherText = "";
                do
                {
                    Console.Write("Please enter cipher text: (enter C to cancel): ");
                    cipherText = Console.ReadLine();

                    // Not bae64 alphabet filter
                    Regex rgx = new Regex("[^A-Za-z0-9+/=]");
                    bool containsSpecialCharacter = rgx.IsMatch(cipherText);

                    // Too short base64 filter
                    int minLength = 4;

                    if (cipherText.ToUpper() == "C")
                    {
                        inputIsValid = true;
                        return;
                    }
                    else if (cipherText == "")
                    {
                        inputIsValid = false;
                        Console.WriteLine($"This '{cipherText}' is not and valid input! Please write something!");
                    }
                    else if (containsSpecialCharacter)
                    {
                        inputIsValid = false;
                        Console.WriteLine(
                            $"This '{cipherText}' is not and valid input! Input contains invalid character!");
                    }
                    else if (cipherText.Length < minLength)
                    {
                        inputIsValid = false;
                        Console.WriteLine($"This '{cipherText}' is not and valid input! Input is too short!");
                    }
                    else
                    {
                        inputIsValid = true;
                    }
                } while (!inputIsValid);

                var base64Encr = cipherText;

                /*
                // Decrypting:
                Console.WriteLine();
                Console.WriteLine("========== Base64 decryption: ===========");
                Console.Write("Decrypt: ");
                */

                shiftAmount = shiftAmount * -1;
                String base64Decr = "";

                for (int i = 0; i < base64Encr.Length; i++)
                {
                    var b64Char = base64Encr[i];
                    var charIndex = base64Alphabet.IndexOf(b64Char);
                    charIndex = charIndex + shiftAmount;
                    if (charIndex < 0)
                    {
                        charIndex = base64Alphabet.Length + charIndex;
                    }
                    else if (charIndex > (base64Alphabet.Length - 1))
                    {
                        charIndex = charIndex - base64Alphabet.Length;
                    }

                    base64Decr += base64Alphabet[charIndex];
                    /*Console.Write($"{b64Char} -> {base64Alphabet[charIndex]}, ");*/
                }

                Console.WriteLine();
                Console.WriteLine("========== Base64 decrypted: ===========");
                Console.WriteLine($"Base64 encrypted: {base64Decr}");

                var decrPlainText = System.Convert.FromBase64String(base64Decr);
                
                /*
                // !!! SYSTEM CRASH with too much foreign characters
                //De-convert
                Console.WriteLine();
                Console.WriteLine("========== Base64 de-convert: ===========");
                Console.Write("De-convert: ");
                for (var i = 0; i < decrPlainText.Length; i++)
                {
                    Console.Write($"{base64Decr[i]} -> {Encoding.UTF8.GetString(decrPlainText)[i]}, ");
                }
                */

                Console.WriteLine();
                Console.WriteLine("========== Base64 de-converted: ===========");
                Console.WriteLine($"Base64 de-converted: {Encoding.UTF8.GetString(decrPlainText)}");
                Console.WriteLine("\n");
            }
        }




        // Vigenere menu
        static void DoVigenere(string base64Alphabet)
        {
            bool inputIsValid = false;
            do
            {
                Console.WriteLine("========== Vigenere ===========");
                Console.WriteLine("E) Encrypt");
                Console.WriteLine("D) Decrypt");
                Console.WriteLine("B) Back");
                Console.WriteLine("-----------------");
                Console.Write("Your choice: ");

                // Read the input value
                var userChoiceCrypt = Console.ReadLine();
                userChoiceCrypt = userChoiceCrypt?.Trim();

                // Loop for encryption selection
                switch (userChoiceCrypt.ToUpper())
                {
                    case "E":
                        inputIsValid = true;
                        Console.WriteLine("\n");
                        DoVigenereEncrypt(base64Alphabet);
                        break;

                    case "E)":
                        inputIsValid = true;
                        Console.WriteLine("\n");
                        DoVigenereEncrypt(base64Alphabet);
                        break;

                    case "D":
                        inputIsValid = true;
                        Console.WriteLine("\n");
                        DoVigenereDecrypt(base64Alphabet);
                        break;

                    case "D)":
                        inputIsValid = true;
                        Console.WriteLine("\n");
                        DoVigenereDecrypt(base64Alphabet);
                        break;

                    case "B":
                        inputIsValid = true;
                        Console.WriteLine("\n");
                        break;

                    case "B)":
                        inputIsValid = true;
                        Console.WriteLine("\n");
                        break;

                    default:
                        inputIsValid = false;
                        Console.WriteLine($"This '{userChoiceCrypt}' is not and valid input!");
                        Console.WriteLine("\n");
                        break;
                }
            } while (!inputIsValid);
            
            


            // Vigenere encryption function
            static void DoVigenereEncrypt(String base64Alphabet)
            {
                Console.WriteLine("========== Vigenere Encryption ===========");


                // Set global variables
                bool inputIsValid = false;
                String plainText = "";
                String keyText = "";
                var utf8 = new UTF8Encoding();
                byte[] plainUtf8Bytes;
                byte[] keyUtf8Bytes;


                // Get plain text
                do
                {
                    // Get plain text
                    Console.Write("Please enter plain text: (enter C to cancel): ");
                    plainText = Console.ReadLine();

                    // Cancel
                    if (plainText.ToUpper() == "C")
                    {
                        inputIsValid = true;
                        return;
                    }
                    // Validate
                    else if (plainText != "")
                    {
                        inputIsValid = true;
                    }
                    else
                    {
                        Console.WriteLine($"'{plainText}' is an invalid plain text. Write something.\n");
                    }
                    
                } while (!inputIsValid);
                // Convert plain text to utf 8
                plainUtf8Bytes = utf8.GetBytes(plainText);

                
                // Get key text
                do
                {
                    // Get key text
                    Console.Write("Please enter key text: (enter C to cancel): ");
                    keyText = Console.ReadLine();
                    
                    if (keyText.ToUpper() == "C")
                    {
                        inputIsValid = true;
                        return;
                    }
                    else if (keyText != "")
                    {
                        inputIsValid = true;
                        int keyLength = 0;
                        //making sure key length == plain text length
                        if (keyText.Length < plainText.Length)
                        {
                            for (int i = 0; i < plainText.Length; i++)
                            {
                                if (keyText.Length <= i)
                                {
                                    keyText += keyText[keyLength];
                                    keyLength++;
                                }
                            }
                        }
                    }
                    else
                    {
                        inputIsValid = false;
                        Console.WriteLine($"'{keyText}' is an invalid plain text. Write something.");
                    }
                } while (!inputIsValid);
                // Convert key text to utf8                                   
                keyUtf8Bytes = utf8.GetBytes(keyText);
                Console.WriteLine();
                

                // Encode to base64
                String base64StrEncr = "";
                String base64StrPlain = System.Convert.ToBase64String(plainUtf8Bytes);
                String base64StrKey = System.Convert.ToBase64String(keyUtf8Bytes);
                Console.WriteLine("========== Base64 plain converted: ===========");
                Console.WriteLine($"Base64 plain converted: {base64StrPlain}");
                Console.WriteLine();
                Console.WriteLine("========== Base64 key converted: ===========");
                Console.WriteLine($"Base64 key converted: {base64StrKey}");
                Console.WriteLine();


                // Encrypt base64
                /*Console.WriteLine("========== Encrypting: ===========");*/
                for (int i = 0; i < base64StrPlain.Length; i++)
                {
                    int charIndexPlain = base64Alphabet.IndexOf(base64StrPlain[i]);
                    int charIndexKey = base64Alphabet.IndexOf(base64StrKey[i]);
                    int charIndexEncr = charIndexKey + charIndexPlain;
                    if (charIndexEncr > (base64Alphabet.Length - 1))
                    {
                        charIndexEncr = charIndexEncr - base64Alphabet.Length;
                    }

                    /* // !!! SYSTEM CRASH with too much foreign characters
                    Console.Write($"{base64StrPlain[i]} + {base64StrKey[i]} -> {base64Alphabet[charIndexEncr]}, ");*/
                    base64StrEncr += base64Alphabet[charIndexEncr];
                }

                /*Console.WriteLine();*/
                Console.WriteLine("========== Base64 encrypted: ===========");
                Console.WriteLine($"Base64 encrypted: {base64StrEncr}");
                Console.WriteLine("\n");
            }




            // Vigenere decryption function 
            static void DoVigenereDecrypt(String base64Alphabet)
            {
                Console.WriteLine("========== Vigenere Decryption ===========");


                // Set global variables
                bool inputIsValid = false;
                String cipherText = "";
                String keyText = "";

                
                // Get plain text
                do
                {

                    // Get cipher text
                    Console.Write("Please enter cipher text: (enter C to cancel): ");
                    cipherText = Console.ReadLine();
                    

                    // Not bae64 alphabet filter
                    Regex rgx = new Regex("[^A-Za-z0-9+/=]");
                    bool containsSpecialCharacter = rgx.IsMatch(cipherText);

                    // Too short base64 filter
                    int minLength = 4;

                    if (cipherText.ToUpper() == "C")
                    {
                        inputIsValid = true;
                        return;
                    }
                    else if (cipherText == "")
                    {
                        inputIsValid = false;
                        Console.WriteLine($"This '{cipherText}' is not and valid input! Please write something!");
                    }
                    else if (containsSpecialCharacter)
                    {
                        inputIsValid = false;
                        Console.WriteLine(
                            $"This '{cipherText}' is not and valid input! Input contains invalid character!");
                    }
                    else if (cipherText.Length < minLength)
                    {
                        inputIsValid = false;
                        Console.WriteLine($"This '{cipherText}' is not and valid input! Input is too short!");
                    }
                    else
                    {
                        inputIsValid = true;
                    }
                } while (!inputIsValid);
                
                // cancel key text
                if (cipherText.ToUpper() == "C")
                {
                    Console.WriteLine("\n");
                    return;
                }
                
                // Get key text
                do
                {
                    // Get key text
                    Console.Write("Please enter key cipher text: (enter C to cancel): ");
                    keyText = Console.ReadLine();
                    // Not bae64 alphabet filter
                    Regex rgx = new Regex("[^A-Za-z0-9+/=]");
                    bool containsSpecialCharacter = rgx.IsMatch(keyText);

                    // Too short base64 filter
                    int minLength = 4;
                    
                    if (keyText.ToUpper() == "C")
                    {
                        inputIsValid = true;
                        return;
                    }
                    else if (keyText == "")
                    {
                        inputIsValid = false;
                        Console.WriteLine($"This '{keyText}' is not and valid input! Please write something!");
                    }
                    else if (containsSpecialCharacter)
                    {
                        inputIsValid = false;
                        Console.WriteLine(
                            $"This '{keyText}' is not and valid input! Input contains invalid character!");
                    }
                    else if (keyText.Length < minLength)
                    {
                        inputIsValid = false;
                        Console.WriteLine($"This '{keyText}' is not and valid input! Input is too short!");
                    }
                    else
                    {
                        inputIsValid = true;
                    }
                } while (!inputIsValid);
                Console.WriteLine();


                // Decrypting base64
                var base64StrEncr = cipherText;
                var base64StrKey = keyText;
                String base64StrDecr = "";
                
                
                /*Console.WriteLine("========== Decrypting: ===========");*/

                for (int i = 0; i < base64StrEncr.Length; i++)
                {
                    var charIndexKey = base64Alphabet.IndexOf(base64StrKey[i]);
                    var charIndexText = base64Alphabet.IndexOf(base64StrEncr[i]);
                    var charIndexEncr = charIndexText - charIndexKey;
                    if (charIndexEncr < 0)
                    {
                        charIndexEncr = charIndexEncr + base64Alphabet.Length;
                    }

                   /* // !!! SYSTEM CRASH with too much foreign characters
                    Console.Write($"{base64StrEncr[i]} - {base64StrKey[i]} -> {base64Alphabet[charIndexEncr]}, ");*/
                    base64StrDecr = base64StrDecr + base64Alphabet[charIndexEncr];
                }

                /*Console.WriteLine();*/
                Console.WriteLine("========== Base64 decrypted: ===========");
                Console.WriteLine($"Base64 decrypted: {base64StrDecr}");
                Console.WriteLine();


                // Print results
                var plainUtf8Bytes = Convert.FromBase64String(base64StrDecr);
                String decrPlainText = Encoding.UTF8.GetString(plainUtf8Bytes);
                Console.WriteLine("========== Base64 de-converted: ===========");
                Console.WriteLine($"Base64 plain text de-converted: {decrPlainText}");

                var keyUtf8Bytes = Convert.FromBase64String(base64StrKey);
                String decrPlainKey = Encoding.UTF8.GetString(keyUtf8Bytes);
                Console.WriteLine($"Base64 key de-converted: {decrPlainKey}");
                Console.WriteLine("\n");
            }
        }
    }
}