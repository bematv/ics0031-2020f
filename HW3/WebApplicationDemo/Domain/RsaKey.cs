using System;
using Microsoft.AspNetCore.Identity;

namespace Domain
{
    public class RsaKey
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public string Cypher { get; set; }
        public string KeyLength { get; set; }
        public ulong PPrime { get; set; }
        public ulong QPrime { get; set; }
        public ulong Exponent { get; set; }
        
        public string RsaCypher { get; set; }
        public string KeySecret { get; set; }
        
        public string UserId { get; set; }
        public IdentityUser User { get; set; }
    }
}