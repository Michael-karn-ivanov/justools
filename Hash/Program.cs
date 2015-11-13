using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Hash
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Usage: Hash <string to hash>");
                return;
            }

            var builder = new StringBuilder();
            for (int i = 0; i < args.Length; i++)
            {
                builder.Append(args[i]);
                if (i != args.Length - 1)
                    builder.Append(" ");
            }

            var key = Sha384Base64(builder.ToString());
            Console.WriteLine("Hash:");
            Console.WriteLine(key);
            Console.WriteLine("Query:");
            Console.WriteLine("PartitionKey eq '" + HttpUtility.UrlEncode(key) + "'");
            int hashShard = StableStringHashCode(key);
            Console.WriteLine("Shards:");
            for (int i = 2; i < 5; i++)
                Console.WriteLine(string.Format("{0}: {1}", i, Math.Abs(hashShard) % i));
        }

        public static string Sha384Base64(string text)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(text);
            var hashstring = new SHA384Cng();
            byte[] hash = hashstring.ComputeHash(bytes);
            // this is to  make the hash url-safe for use in Azure table partition and row keys for example
            return Convert.ToBase64String(hash).Replace('/', '_');
        }

        public static int StableStringHashCode(string str)
        {
            var hash1 = 5381;
            var hash2 = hash1;
            var i = 0;
            int c;
            while (i < str.Length && (c = str[i]) != 0)
            {
                hash1 = ((hash1 << 5) + hash1) ^ c;
                if (i == str.Length - 1 || (c = str[i + 1]) == 0)
                    break;
                hash2 = ((hash2 << 5) + hash2) ^ c;
                i += 2;
            }
            return hash1 + hash2 * 1566083941;
        }
    }
}
