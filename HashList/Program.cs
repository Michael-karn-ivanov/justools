using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HashList
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0 || args.Length == 1)
            {
                Console.WriteLine("Usage: HashList <path to list of ids> <shard count>");
                return;
            }

            string line;
            using (System.IO.StreamReader file = new System.IO.StreamReader(args[0]))
            {
                while ((line = file.ReadLine()) != null)
                {
                    var hash = Hash.Program.Sha384Base64(line);
                    var route = Math.Abs(Shard.Program.StableStringHashCode(hash) % int.Parse(args[1]));
                    Console.WriteLine("{0}\t{1}\t{2}", line, hash, route);
                    Clipboard
                }
            }
        }
    }
}
