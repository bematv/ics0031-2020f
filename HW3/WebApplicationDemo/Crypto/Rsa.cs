using System;
using System.Diagnostics.Tracing;
using System.Linq.Expressions;
using System.Text;
using Domain;

namespace Crypto
{
    public class Rsa : Validation
    {
        public static void RsaImplemantation(RsaKey rsa)
        {
            ulong keyLength = 0, keySecret = 0;
            Random rdn = new Random();
            (rsa.PPrime, rsa.QPrime) = KeyGenerator.GeneratePrimesNaive();
            keyLength = rsa.PPrime * rsa.QPrime;
            rsa.KeyLength = Convert.ToBase64String(Encoding.Default.GetBytes(keyLength.ToString()));
            var m = (rsa.PPrime - 1) * (rsa.QPrime - 1);
            
            int messageRsa = keyLength > Int32.MaxValue ? rdn.Next(2, Int32.MaxValue) : rdn.Next(2, (int)keyLength);
            rsa.Exponent = Coprime(m);

            keySecret = ModularInverse(m, rsa.Exponent);
            rsa.KeySecret = Convert.ToBase64String(Encoding.Default.GetBytes(keySecret.ToString()));
            
            var passWCypher = Encoding.Default.GetBytes(modPow((ulong)messageRsa, rsa.Exponent, keyLength).ToString());
            rsa.RsaCypher = Convert.ToBase64String(passWCypher);

            var messageLength = Encoding.Default.GetBytes(rsa.Message).Length*8;
            var symmKeyLength = messageLength < 32 ? 32 : roundUpToNextPowerOfTwo(messageLength);
            var symmKey = KeyGenerator.CreateKey(messageRsa.ToString(), symmKeyLength);
            
            rsa.Cypher = KeyGenerator.messageEncrypt(Encoding.Default.GetBytes(rsa.Message), symmKey);
        }

        public static void RsaDecrypt(RsaKey rsa)
        {
            var cypherText = Convert.FromBase64String(rsa.Cypher);
            var cypher = Encoding.Default.GetString(Convert.FromBase64String(rsa.RsaCypher));
            var secret = Encoding.Default.GetString(Convert.FromBase64String(rsa.KeySecret));
            var length = Encoding.Default.GetString(Convert.FromBase64String(rsa.KeyLength));
            
            ulong dirivedMessage = 0;
            if (UInt64.TryParse(cypher, out var rsaCypher) && UInt64.TryParse(secret, out var keySecret) &&
                UInt64.TryParse(length, out var keyLength))
            {
                dirivedMessage = modPow(rsaCypher, keySecret, keyLength);
            }

            var symmKeyLength = Convert.FromBase64String(rsa.Cypher).Length;
            var symmKey = KeyGenerator.CreateKey(dirivedMessage.ToString(), symmKeyLength);

            rsa.Message = KeyGenerator.messageDecrypt(cypherText, symmKey);
        }
    }
}