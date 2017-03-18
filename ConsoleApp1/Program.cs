using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Application
{
    public class Record : IEquatable<Record>
    {
        public int id { get; set; }
        public string name { get; set; }



        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            Record m = obj as Record; 
            if (m as Record == null)
                return false;

            return m.id == this.id;
        }

        public bool Equals(Record other)
        {
            if (other == null) return false;
            return (this.id.Equals(other.id));
        }

        public override int GetHashCode()
        {
            return id;
        }
        public override string ToString()
        {
            return "ID: " + id + "   Name: " + name;
        }


    }


    public class Control
    {
        
        
        public void FileMenu()
        {
            Console.WriteLine("Планировщик задач");
            Console.WriteLine(@"
1 Создать новый список
2 Открыть список из файла ( не работает )

Введите цифру:");
            int i = Convert.ToInt32(Console.ReadLine());
            if (i == 1)
            {
                Program.NewFile();
            }
            else if (i == 2)
            {
                Program.OpenFile();
            }
        }
        public void EditMenu()
        {
            int id; string str;
            Console.WriteLine(@"
1 Добавить запись
2 Удалить запись
3 Посмотреть записи
4 Изменить запись
				
Введите цифру:
	        ");
            int i = Convert.ToInt32(Console.ReadLine());
            switch (i)
            {
                case 1:
                    Console.Write("Введите id:");
                    id = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Введите name:");
                    str = Convert.ToString(Console.ReadLine()).Trim();
                    AddRec(id, str);
                    
                    


                    break;
                case 2:
                    Console.WriteLine("Введите ID удаляемой записи");
                    id = Convert.ToInt32(Console.ReadLine());

                    DelRec(id);
                    break;
                case 3:
                    ShowRecs();
                    break;
                case 4:
                    Console.Write("Введите id изменяемой записи:");
                    id = Convert.ToInt32(Console.ReadLine());

                    ChangeRec(id);
                    break;

            }
        }
        public void AddRec(int id, string name)
        {
            Program.list.Add(new Record()
            {
                id = id,
                name = name
            });
            
            Console.WriteLine("Запись добавлена");
            
            ShowRecs();
        }
        public void DelRec(int id)
        {
            Record rec = new Record()
            {
                id = id,
                name = ""
            };
            Program.list.Remove(rec);
            Console.WriteLine("Запись удалена");
            ShowRecs();
        }
        public void ChangeRec(int id)
        {
            int index = Program.list.FindIndex(item => item.id == id);

            if (index >= 0)
            {
                Console.WriteLine();
                Console.WriteLine("Введите новое значение name:");
                string newName = Convert.ToString(Console.ReadLine());
                Program.list[index].name = newName;
                Console.WriteLine("Запись изменена");
                
                ShowRecs();
                
            } else
                Console.WriteLine("Заданной записи не существует."); 
            }


        

        public static void ShowRecs()
        {
            List<Record> l = Program.list;
            Console.WriteLine("Список записей:");
            for (int i = 0; i < l.Count; i++)
            {
                Console.WriteLine(l[i].ToString());
            }

        }


        

    public class IO
    {
            public void Save()
            {
                Console.WriteLine("Сохранение в файл");
                Console.WriteLine("Файл сохранения save.json");
                string path = Convert.ToString(Console.ReadLine());
                using (System.IO.StreamWriter file = File.CreateText(@"save.json"))
                {
              
                        //file.WriteLine(rec);
                        JsonSerializer serializer = new JsonSerializer();
                        serializer.Serialize(file, Program.list);

                }
            }
            public static List<Record> Open()
            {
                List<Record> list = new List<Record>();
                Console.WriteLine("Путь файла: save.json");
                string path = Convert.ToString(Console.ReadLine());
               

                try
                {
                    using (System.IO.StreamReader file = File.OpenText(@"save.json"))
                    
                    {
                        string fileStr = file.ReadToEnd();
                        Console.WriteLine(fileStr);
                        List<string> jsons = new List<string>();
                        
                        JsonSerializer serializer = new JsonSerializer();
                        //не получается десериализовать!!!
                       // List<Record> input = serializer.Deserialize<List<Record>>(file);
                        //List<Record> input = JsonConvert.SerializeObject(items);
                        
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Этот файл не может быть прочитан");
                    Console.WriteLine(e.Message);
                }

                
                return list;
            }
            

        }

    public class Program
    {
        public static List<Record> list;
            public static void NewFile()
            {
                list = new List<Record>();
            }
            public static void OpenFile()
            {
                list = IO.Open();
            }
            public static void Main()
            {

                Control ctrl = new Control();
                IO io = new IO();
                ctrl.FileMenu();
                string ans;
                do
                {
                    ctrl.EditMenu();
                    Console.Write("Завершить работу? y/n ");
                    ans = Console.ReadLine();

                    if (ans == "y") break;

                } while (true);
                Console.WriteLine("Сохранить файл? y/n");
                if ( Console.ReadLine() == "y")
                {
                    io.Save();
                }
            }
        }
    }
}
    


