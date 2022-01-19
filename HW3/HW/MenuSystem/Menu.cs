using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace MenuSystem
{
    public class Menu
    {
        private readonly int _menuLevel;
        private const string MenuCommandExit = "X";
        private const string MenuCommandReturnToMain = "M";
        private Dictionary<string, MenuItem> _menuItemsDictionary = new Dictionary<string, MenuItem>();

        public Menu(int menuLevel = 0)
        {
            _menuLevel = menuLevel;
        }

        public string Title { get; set; }

        public Dictionary<string, MenuItem> MenuItemsDictionary
        {
            get => _menuItemsDictionary;
            set
            {
                _menuItemsDictionary = value;
                if (_menuLevel >= 1)
                {
                    _menuItemsDictionary.Add(MenuCommandReturnToMain,
                        new MenuItem() { Title = "Return to Main MenuSystem" });
                }
                _menuItemsDictionary.Add(MenuCommandExit,
                    new MenuItem() { Title = "Exit" });
            }
        }
        


        public string Run()
        {
            var command = "";
            do
            {
                //Console.Clear();
                Console.WriteLine(Title);
                Console.WriteLine("========================");

                foreach (var menuItem in MenuItemsDictionary)
                {
                    Console.Write(menuItem.Key);
                    Console.Write(" ");
                    Console.WriteLine(menuItem.Value);
                }

                Console.WriteLine("----------");
                Console.Write(">");

                command = Console.ReadLine()?.Trim().ToUpper() ?? "";
                
                var returnCommand = "";
                
                if (MenuItemsDictionary.ContainsKey(command))
                {
                    
                    var menuItem = MenuItemsDictionary[command];
                    
                    returnCommand = menuItem.CommandToExecute != null ? menuItem.CommandToExecute() : returnCommand;
                    

                if (returnCommand == MenuCommandExit)
                {
                    command = MenuCommandExit;
                }
                

                if (returnCommand == MenuCommandReturnToMain && _menuLevel != 0)
                {
                    command = MenuCommandReturnToMain;
                }
                
                if (menuItem.Title != null && command != MenuCommandReturnToMain &&
                    command != MenuCommandExit)
                {
                    switch (_menuLevel)
                    {
                        case 1:
                            Caesar(menuItem.Encrypt);
                            break;
                        case 2:
                            Vigenere();
                            break;
                        case 3:
                            KeyExchange keys = new KeyExchange();
                            keys.keyExchange();
                            break;
                        case 4:
                            RSA.RsaBruteforce();
                            break;
                        case 5:
                            RSA.RsaImplemantation(menuItem.Title);
                            break;
                    }
                    
                }
                }

            } while (command != MenuCommandExit &&
                     command != MenuCommandReturnToMain);
            
            return command;
        }

        static string UserInput()
        {
            var input = "";
            Console.WriteLine("To exit press Q");
            do
            {
                Console.WriteLine("Enter plain text:");
                input = Console.ReadLine();
            } while (input == "");

            if (input != "q") return input;
            return "";
        }

        static int Key(int keyLimit)
        {
            var key = 0;
            var inputKey = "";
            do
            {
                Console.WriteLine("Enter shift number");
                inputKey = Console.ReadLine()?.ToLower().Trim();
                
                if (inputKey != "q")
                {
                    if (int.TryParse(inputKey, out var userValue))
                    {
                        key = userValue % keyLimit;

                        if (key == 0)
                        {
                            Console.WriteLine($"'multiples of {keyLimit}' is not a cipher");
                        }
                        else
                        {
                            Console.WriteLine($"key is {key}");
                        }
                    }
                }

            } while (inputKey != "q" && key == 0);

            return key;
        }
        

        static void Caesar(bool encrypt)
        {
            var input = UserInput();
            if (input == "")
                return;
            var key = Key(255);
            
            var output = "";
            foreach (char ch in input)
                output += encrypt ? (char) (ch + key) : (char) (ch - key);
            Console.Clear();
            Console.WriteLine(output);
        }

        static void Vigenere()
        {
            var lowerBound = 97;
            var upperBound = 122;
            var input = UserInput();
            if (input == "")
                return;
            var inputKey = "";
            var output = "";
            var resultOutput = "";
            bool capitalLetter = false;

            inputKey = VigereneValidation(input);

            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] >= lowerBound && input[i] <= upperBound)
                {
                    capitalLetter = Char.IsUpper(input[i]);
                    var charIndex = Char.ToLower(input[i]) + inputKey[i] - lowerBound;
                    output += charIndex > upperBound
                        ? (char) (lowerBound + charIndex - upperBound - 1)
                        : (char) charIndex;
                    resultOutput += capitalLetter ? Char.ToUpper(output[i]) : output[i];
                }
                else resultOutput += input[i];
            }
            Console.Clear();
            Console.WriteLine(resultOutput);
        }

        static string VigereneValidation(string str)
        {
            var keyLength = 0;
            var validKey = true;
            foreach (char ch in str)
                if ((ch >= 'a' && ch <= 'z') || (ch >= 'A' && ch <= 'Z'))
                    keyLength++;
            var inputKey = "";
            do
            {
                Console.WriteLine($"input at least two English letters");
                inputKey = Console.ReadLine();
                if (inputKey != null)
                {
                    inputKey = inputKey.ToLower();
                    foreach (char ch in inputKey)
                        if (ch < 'a' && ch > 'z')
                            validKey = false;
                }
            } while (inputKey != "q" && !validKey);

            if (inputKey.Length < str.Length)
            {
                var keyLengthDifference = str.Length / inputKey.Length + 1;
                for (var i = 0; i < keyLengthDifference; i++)
                {
                    inputKey += inputKey;
                }
            }

            return inputKey;
        }

        

     


      

        
        


        



        static (int result, bool wasCanceled) GetUserIntInput(string prompt, int min, int max = 1000, int? cancelIntValue = null, string cancelStrValue = "")
        {
            var userXint = 0;
            var userCanceled = false;
            do
            {
                Console.WriteLine(prompt);
                if (cancelIntValue.HasValue || !string.IsNullOrWhiteSpace(cancelStrValue))
                {
                    Console.WriteLine($"To cancel input enter: {cancelIntValue}" +
                                      $"{ (cancelIntValue.HasValue && !string.IsNullOrWhiteSpace(cancelStrValue) ? " or " : "") }" +
                                      $"{cancelStrValue}");
                }

                Console.Write(">");
                var consoleLine = Console.ReadLine();
               

                if (consoleLine == cancelStrValue) return (0, true);

                if (int.TryParse(consoleLine, out var userInt))
                {
                    return userInt == cancelIntValue ? (userInt, true) : (userInt, false);
                }

                Console.WriteLine($"'{consoleLine}' cant be converted to int value!");
            } while (true);

            return (0, true);
        }

    }
}
