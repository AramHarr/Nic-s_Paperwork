using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Nic_s_Paperwork
{
    class Program
    {
        static void Main(string[] args)
        {
            bool run = true;


            Intro();
            Console.WriteLine();


            while (run)
            {
                string filePath = @"C:\Users\aramh\source\repos\Nic's_Paperwork\Nic's_Paperwork\bin\Debug\net5.0\DB\database.txt";
                string filePathAdditional = @"C:\Users\aramh\source\repos\Nic's_Paperwork\Nic's_Paperwork\bin\Debug\net5.0\DB\additionalDB.txt";
                string filePathRecBin = @"C:\Users\aramh\source\repos\Nic's_Paperwork\Nic's_Paperwork\bin\Debug\net5.0\DB\RecBin.txt";

                List<string> db = File.ReadAllLines(filePath).ToList();
                List<string> dbAdd = File.ReadAllLines(filePathAdditional).ToList();
                List<string> recycleBin = File.ReadAllLines(filePathRecBin).ToList();


                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("<Teacher/>Aram--> ");
                Console.ForegroundColor = ConsoleColor.Cyan;
                string command = Console.ReadLine();
                Console.ForegroundColor = ConsoleColor.Yellow;

                string[] commands = command.Split(' ');
                bool found = false;


                try
                {
                    switch (commands[0])
                    {


                        // >> ADDITIONAL <<

                        case "additional":

                            if (commands[1] == "new")
                            {
                                dbAdd = Additional.AddNewInfo(dbAdd);
                                File.WriteAllLines(filePathAdditional, dbAdd);
                            }
                            else if (commands[1] == "show")
                            {
                                Additional.ShowAll(dbAdd);
                            }
                            else
                            {
                                Console.WriteLine($"Command {commands[1]} not found.");
                            }

                            break;



                        // >> ADD <<

                        case "add":

                            Student newStudent = new Student();

                            if (commands[1] == "prog")
                            {
                                db.Add(newStudent.AddStudent("programming"));
                            }
                            else if (commands[1] == "math")
                            {
                                db.Add(newStudent.AddStudent("math"));
                            }
                            else if (commands[1] == "both")
                            {
                                string info = newStudent.AddStudent("both");
                                string mathInfo = "math" + info.Substring(4);
                                string progInfo = "programming" + info.Substring(4);
                                db.Add(mathInfo);
                                db.Add(progInfo);
                            }
                            else
                            {
                                Console.WriteLine($"command {commands[1]} not found");
                            }

                            File.WriteAllLines(filePath, db);

                            break;



                        // >> REMOVE <<

                        case "remove":

                            foreach (string line in db)
                            {
                                Student student = new Student(line);

                                if (commands[1] == student.subject && commands[2] == student.name)
                                {
                                    db.Remove(line);
                                    recycleBin.Add(line);
                                    break;
                                }
                            }

                            File.WriteAllLines(filePath, db);
                            File.WriteAllLines(filePathRecBin, recycleBin);

                            break;


                        case "recycle": // recycle bin

                            foreach (string line in recycleBin)
                            {
                                Student student = new Student(line);
                                student.ShowProfile();
                                for (int i = 1; i <= 12; i++)
                                {
                                    student.ShowStatus(Convert.ToString(i));
                                }
                                Console.WriteLine();
                            }

                            break;


                        // >> SHOW <<

                        case "show":

                            found = false;
                            int num = 1;
                            db.Sort();

                            foreach (string info in db)
                            {
                                Student student = new Student(info);

                                if (commands[1] == "all")
                                {
                                    found = true;
                                    student.ShowStudent(num);
                                    num++;
                                }
                                else if (student.subject == commands[2] && student.name == commands[3])
                                {
                                    found = true;

                                    if (commands[1] == "profile")
                                    {
                                        student.ShowProfile();
                                    }
                                    else if (commands[1] == "status")
                                    {
                                        student.ShowStatus(commands[4]);
                                    }
                                    else
                                    {
                                        Console.WriteLine($"Command {commands[1]} not found");
                                    }

                                }

                            }

                            if (!found)
                            {
                                Console.WriteLine("Student not found");
                            }

                            break;


                        // >> PAY <<

                        case "pay":

                            found = false;

                            foreach (string info in db)
                            {
                                Student student = new Student(info);

                                if (student.subject == commands[1] && student.name == commands[2])
                                {
                                    found = true;

                                    db.Add(student.DoPayment(info, commands[3]));
                                    db.Remove(info);
                                    break;

                                }

                            }

                            if (!found)
                            {
                                Console.WriteLine("Student not found");
                            }

                            File.WriteAllLines(filePath, db);

                            break;


                        // >> EDIT <<

                        case "edit":

                            foreach (string line in db)
                            {
                                Student student = new Student(line);

                                if (commands[1] == student.subject && commands[2] == student.name)
                                {
                                    db.Remove(line);
                                    db.Add(student.EditProfile(line, commands[3], commands[4]));
                                    break;
                                }
                            }

                            File.WriteAllLines(filePath, db);

                            break;


                        // >> PRESENCE/ABSENCE <<

                        case "present":

                            found = false;

                            foreach (string info in db)
                            {
                                Student student = new Student(info);

                                if (student.subject == commands[1] && student.name == commands[2])
                                {
                                    found = true;

                                    db.Add(student.DidCome(info, commands[3], true));
                                    db.Remove(info);
                                    break;

                                }

                            }

                            if (!found)
                            {
                                Console.WriteLine("Student not found");
                            }

                            File.WriteAllLines(filePath, db);

                            break;


                        case "absent":

                            found = false;

                            foreach (string info in db)
                            {
                                Student student = new Student(info);

                                if (student.subject == commands[1] && student.name == commands[2])
                                {
                                    found = true;

                                    db.Add(student.DidCome(info, commands[3], false));
                                    db.Remove(info);
                                    break;

                                }

                            }

                            if (!found)
                            {
                                Console.WriteLine("Student not found");
                            }

                            File.WriteAllLines(filePath, db);

                            break;


                        // >> HELP <<

                        case "help":

                            Student.Help();

                            break;


                        case "clean":

                            Console.Clear();
                            Intro();

                            break;


                        // >> END <<

                        case "end":

                            run = false;

                            break;


                        default:

                            Console.WriteLine($"Command {commands[0]} not found! Try help for all commands.");

                            break;
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    Console.WriteLine("Command expected! Try help for all commands.");
                }


                Console.WriteLine();
            }

        }


        public static void Intro()
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;


            string company = @"

███╗   ████╗█████████████╗    ██╗  ██████████╗███████╗
████╗  ██████╔════██╔════╝    ██║ ██╔████╔══████╔════╝
██╔██╗ ██████║    ███████╗    █████╔╝████║  █████████╗
██║╚██╗██████║    ╚════██║    ██╔═██╗████║  ██╚════██║
██║ ╚██████╚█████████████║    ██║  ██████████╔███████║
╚═╝  ╚═══╚═╝╚═════╚══════╝    ╚═╝  ╚═╚═╚═════╝╚══════╝                                                      
";

            Console.WriteLine(company);
            Console.ResetColor();
        }
    }
}
// subject:name:year:phoneNumber:phoneNumberParent#01>0/0$0|02>0/0$0|03>0/0$0|04>0/0$0|05>0/0$0|06>0/0$0|07>0/0$0|08>0/0$0|09>0/0$0|10>0/0$0|11>0/0$0|12>0/0$0