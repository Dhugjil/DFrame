using DFrame.DAL;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {
            //List<int> list = new List<int>();
            //list.Select(x=>x>3);
            Assembly assembly = Assembly.Load("Models");
            SQLFactory.Create(SQLFactory.DatabaseType.MySQL, "test1", assembly);
            //Action call = delegate { SQLFactory.Create(DatabaseType.MySQL, "test1", assembly); };
            ////call += delegate { SQLFactory.Create(DatabaseType.MSSQLServer, "test1", assembly); };
            ////call.Invoke();
            //Task.Run(call);
            //ActionList model = new ActionList();
            Console.ReadKey();
        }
    }
}
