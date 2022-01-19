using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using MenuSystem;

namespace Console_application
{
    class Program
    {

        static void Main(string[] args)
        {
            Encryption();
            
        }
        
        
        
        
        

        static void Encryption()
        {
            var rsaOption = new Menu(5)
            {
                Title = "Choose action",
                MenuItemsDictionary = new Dictionary<string, MenuItem>()
                {
                    {
                        "1", new MenuItem()
                        {
                            Title = "Encrypt",
                            CommandToExecute = null
                        }
                    },
                    {
                        "2", new MenuItem()
                        {
                            Title = "Decrypt",
                            CommandToExecute = null
                        }
                    },
                }
            };
            
            Console.Clear();
            
            var rsa = new Menu(4)
            {
                Title = "Choose action",
                MenuItemsDictionary = new Dictionary<string, MenuItem>()
                {
                    {
                        "1", new MenuItem()
                        {
                            Title = "RSA",
                            CommandToExecute = rsaOption.Run
                        }
                    },
                    {
                        "2", new MenuItem()
                        {
                            Title = "RSA bruteforce",
                            CommandToExecute = null
                        }
                    },
                }
            };

            var keyExchange = new Menu(3)
            {
                Title = "Choose action",
                MenuItemsDictionary = new Dictionary<string, MenuItem>()
                {
                    {
                        "1", new MenuItem()
                        {
                            Title = "Diffie-Hellman key exchange",
                            CommandToExecute = null
                        }
                    },
                }
            };
            
            var vigenere = new Menu(2)
            {
                Title = "Choose action",
                MenuItemsDictionary = new Dictionary<string, MenuItem>()
                {
                    {
                        "1", new MenuItem()
                        {
                            Title = "Vigenere encryption/decryption",
                            CommandToExecute = null
                        }
                    },
                }
            };
            
            var caesar = new Menu(1)
            {
                Title = "Choose action",
                MenuItemsDictionary = new Dictionary<string, MenuItem>()
                {
                    {
                        "1", new MenuItem()
                        {
                            Title = "Encrypt",
                            Encrypt = true,
                            CommandToExecute = null
                        }
                    },
                    {
                        "2", new MenuItem()
                        {
                            Title = "Decrypt",
                            Encrypt = false,
                            CommandToExecute = null
                        }
                    },
                }
            };


            var menu0 = new Menu(0)
            { 
                Title = "Pick cipher",
                MenuItemsDictionary = new Dictionary<string, MenuItem>()
                {
                    {
                        "1", new MenuItem()
                        {
                            Title = "Caesar",
                            Reverse = true,
                            CommandToExecute = caesar.Run
                        }
                    },
                    {
                        "2", new MenuItem()
                        {
                            Title = "Vigenere",
                            CommandToExecute = vigenere.Run
                        }
                    },
                    {
                        "3", new MenuItem()
                        {
                            Title = "Key exchange",
                            CommandToExecute = keyExchange.Run
                        }
                    },
                    {
                        "4", new MenuItem()
                        {
                            Title = "RSA",
                            CommandToExecute = rsa.Run
                        }
                    }

                }
            };

            menu0.Run();
        }

    }
}
