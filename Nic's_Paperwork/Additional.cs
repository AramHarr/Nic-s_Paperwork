using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nic_s_Paperwork
{
    class Additional
    {
        public static List<string> AddNewInfo(List<string> db)
        {
            Console.Write("    # ");
            string written = Console.ReadLine();
            string info = "    # " + written;

            db.Add(info);

            return db;
        }

        public static void ShowAll(List<string> db)
        {
            foreach (string line in db)
            {
                Console.WriteLine(line);
            }
        }
    }

}
