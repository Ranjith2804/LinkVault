using LinkVault.Models;
using System.Collections;
using System.Collections.Concurrent;
using System.Formats.Tar;
using System.Reflection.Metadata.Ecma335;
namespace LinkVault.Services
{
    public class Linkservice
    {
        public readonly ConcurrentDictionary<String, Linkentry> store = new();
        public Linkentry create(string orginalurl)
        {
            var code = GenerateCode();
            var entry = new Linkentry
            {
                code = code,
                orginalurl = orginalurl,
                CreatedAt = DateTime.UtcNow,
                hit = 0

            };
            store[code]= entry;
            return entry;
            
        }
        public Linkentry? getbycode(string code)
        {
            if (store.TryGetValue(code,out var entry))
            {
                Interlocked.Increment(ref entry.hit);   
                return entry;
            }
            return null;

        }
        public IEnumerable<Linkentry> getall() => store.Values;
        private static string GenerateCode()
        {
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Range(0, 6)
                .Select(_ => chars[Random.Shared.Next(chars.Length)])
                .ToArray());
        }
    }
}
