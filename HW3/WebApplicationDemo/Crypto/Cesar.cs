using System;
using System.Text;

namespace Crypto
{
    public static class Cesar
    {
        
        public static byte[] Encrypt(string input, byte shiftAmount, Encoding encoding)
        {
            var inputBytes = encoding.GetBytes(input);
            return Enc(inputBytes, shiftAmount);
        }

        public static byte[] Enc(byte[] input, byte shiftAmount)
        {
            var result = new byte[input.Length];

            
            if (shiftAmount == 0)
            {
                for (var i = 0; i < input.Length; i++)
                {
                    result[i] = input[i];
                }
            }
            else
            {
                for (int i = 0; i < input.Length; i++)
                {
                    var newCharValue = (input[i] + shiftAmount);
                    
                    if (newCharValue > byte.MaxValue)
                    {
                        newCharValue = newCharValue - byte.MaxValue;
                    }

                    result[i] = (byte)newCharValue;
                }
            }

            
            return result;
        }
        
    }
}