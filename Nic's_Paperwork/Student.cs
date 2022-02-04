using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nic_s_Paperwork
{
    public class Student
    {
        public string subject;
        public string name;
        protected string birthYear;
        protected string phoneNumber;
        protected string phoneNumberParent;
        protected string[] stats;

        // Constructors

        public Student()
        {

        }

        public Student(string line)
        {
            // Profile

            string infos = line.Substring(0, line.IndexOf('#'));

            string[] info = infos.Split(':');

            if (info[0] == "programming")
            {
                subject = "prog";
            }
            else
            {
                subject = info[0];
            }

            name = info[1];
            birthYear = info[2];
            phoneNumber = info[3];
            phoneNumberParent = info[4];



            // Status

            string statInfos = line.Substring(line.IndexOf('#') + 1);

            stats = statInfos.Split('|');
        }


        // Methods

        public string AddStudent(string subject)
        {
            Console.WriteLine($"Subject: {subject}");
            this.subject = subject;

            Console.Write("Name: ");
            name = Console.ReadLine();
            Console.Write("Birth year: ");
            birthYear = Console.ReadLine();
            Console.Write("Phone number (student): ");
            phoneNumber = Console.ReadLine();
            Console.Write("Phone number (parent): ");
            phoneNumberParent = Console.ReadLine();

            return $"{subject}:{name}:{birthYear}:{phoneNumber}:{phoneNumberParent}#01>0/0$0|02>0/0$0|03>0/0$0|04>0/0$0|05>0/0$0|06>0/0$0|07>0/0$0|08>0/0$0|09>0/0$0|10>0/0$0|11>0/0$0|12>0/0$0";
        }

        public void ShowStatus(string strMonth)
        {
            int month = 0;

            try
            {
                month = Convert.ToInt32(strMonth);

                string stat = stats[month - 1];

                string came = stat.Substring(3, stat.IndexOf('/') - 3);
                string didntCome = stat.Substring(stat.IndexOf('/') + 1, stat.IndexOf('$') - stat.IndexOf('/') - 1);
                bool isPaid = false;

                if (stat.Substring(stat.IndexOf('$') + 1) == "1")
                {
                    isPaid = true;
                }

                Console.Write("Presence: ");
                Console.WriteLine(came);
                Console.Write("Absence: ");
                Console.WriteLine(didntCome);
                Console.Write("Payment done: ");
                Console.WriteLine(isPaid);
            }
            catch (FormatException)
            {
                Console.WriteLine("Invali number for month.");
                
            }
            
        }

        public void ShowProfile()
        {
            Console.Write("Name: ");
            Console.WriteLine(name);
            Console.Write("Birth year: ");
            Console.WriteLine(birthYear);
            Console.Write("Phone number: ");
            Console.WriteLine(phoneNumber);
            Console.Write("Phone number(parent): ");
            Console.WriteLine(phoneNumberParent);
        }

        public string DoPayment(string line, string strMonth)
        {
            int month = Convert.ToInt32(strMonth);

            string changed = line.Substring(0, line.IndexOf('#') + 1);

            for (int i = 0; i < 12; i++)
            {
                if (month - 1 == i)
                {
                    string monthStat = stats[i].Substring(0, stats[i].IndexOf('$') + 1);
                    changed += monthStat + '1';
                }
                else
                {
                    changed += stats[i];
                }

                if (i != 11)
                {
                    changed += '|';
                }
            }

            Console.WriteLine("Payment succeded.");

            return changed;
        }

        public string DidCome(string line, string strMonth, bool came)
        {
            int month = Convert.ToInt32(strMonth);

            string changed = line.Substring(0, line.IndexOf('#') + 1);

            for (int i = 0; i < 12; i++)
            {
                if (month - 1 == i && came == true)
                {
                    string monthStat = stats[i].Substring(0, stats[i].IndexOf('>') + 1);
                    string unChangedPart = stats[i].Substring(stats[i].IndexOf('/'));
                    int times = Convert.ToInt32(stats[i].Substring(stats[i].IndexOf('>') + 1, stats[i].IndexOf('/') - stats[i].IndexOf('>') - 1)) + 1;
                    changed += monthStat + times + unChangedPart;
                    Console.WriteLine("Nice.");
                }
                else if (month - 1 == i && came == false)
                {
                    string monthStat = stats[i].Substring(0, stats[i].IndexOf('/') + 1);
                    string unChangedPart = stats[i].Substring(stats[i].IndexOf('$'));
                    int times = Convert.ToInt32(stats[i].Substring(stats[i].IndexOf('/') + 1, stats[i].IndexOf('$') - stats[i].IndexOf('/') - 1)) + 1;
                    changed += monthStat + times + unChangedPart;
                    Console.WriteLine("That's bad.");
                }
                else
                {
                    changed += stats[i];
                }

                if (i != 11)
                {
                    changed += '|';
                }
            }

            return changed;
        }

        public string EditProfile(string line, string field, string val)
        {
            string unchanged = line.Substring(line.IndexOf('#'));

            string changed = "";

            switch (field)
            {
                case "name":
                    name = val;
                    break;

                case "year":
                    birthYear = val;
                    break;

                case "phonenumber":
                    phoneNumber = val;
                    break;

                case "phonenumberparent":
                    phoneNumberParent = val;
                    break;
            }

            changed = $"{subject}:{name}:{birthYear}:{phoneNumber}:{phoneNumberParent}" + unchanged;

            return changed;
        }

        public void ShowStudent(int num)
        {
            Console.WriteLine($"{num}. {subject} - {name}");
        }

        public static void Help()
        {
            Console.WriteLine("add [subject]");
            Console.WriteLine("show status [subject] [name] [month]");
            Console.WriteLine("show profile [subject] [name]");
            Console.WriteLine("show all");
            Console.WriteLine("pay [subject] [name] [month]");
            Console.WriteLine("present [subject] [name] [month]");
            Console.WriteLine("absent [subject] [name] [month]");
            Console.WriteLine("edit [subject] [name] [field] [value]");
            Console.WriteLine("remove [subject] [name]");
            Console.WriteLine("recycle bin");
            Console.WriteLine("additional new");
            Console.WriteLine("additional show");
            Console.WriteLine("clean");
            Console.WriteLine("end");
        }

    }
}
