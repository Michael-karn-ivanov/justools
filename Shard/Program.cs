using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Shard
{
    public class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0 || args.Length == 1)
            {
                Console.WriteLine("Usage: Shard <key> <shards amount>");
                return;
            }

            Console.WriteLine(Math.Abs(StableStringHashCode(args[0]) % int.Parse(args[1])));
            Console.WriteLine("Query:");
            Console.WriteLine("PartitionKey eq '" + HttpUtility.UrlEncode(args[0]) + "'");
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
