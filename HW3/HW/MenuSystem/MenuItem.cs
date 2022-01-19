using System;

namespace MenuSystem
{
    public class MenuItem
    {
        private string _title;

        public string Title
        {
            get => _title;
            set => _title = Validate(value, 1, 100, false);
        }
        
        public Func<string> CommandToExecute { get; set; }
        public bool Reverse { get; set; }
        public bool Encrypt { get; set; }

        private static string Validate(string item, int minLength, int maxLength, bool toUpper)
        {
            item = item.Trim();
            if (toUpper)
            {
                item = item.ToUpper();
            }
            if (item.Length < minLength || item.Length > maxLength)
            {
                throw new ArgumentException(
                    $"String is not correct length (" +
                    $"{minLength}-{maxLength})! Got " +
                    $"{item.Length} characters.");
            }

            return item;
        }

        public override string ToString()
        {
            return Title;
        }
    }
}