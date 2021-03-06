﻿using Models;
using System;
using DFrame.Model;
using System.Collections.Generic;
using System.Reflection;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {
            //Assembly ass = Assembly.Load("Models");
            //DFrame.DAL.SQLFactory.Create(DFrame.DAL.SQLFactory.DatabaseType.MSSQLServer, "test1", ass);

            DateTime d = DateTime.Now.AddDays(2);

            string name = "sdfs\"dfsd";
            long? id = null;
            Person model = new Person()
            {
                PersonID = 3333,
                State = true
            };

            long count = DBModel
                .Select<Person>()
                .Where<Person>(x => x.State == false && x.Text == name || x.State == false && x.PersonID == id && x.PersonID == model.PersonID)
                .OrderBy<Person>(x => new Person { Text = x.Text, Age = x.Age }).Count();

            //List<result> list = DBModel
            //    .Select<Person>(x => new Person { State = x.State, Age = x.Age })
            //    .Where<Person>(x => x.Text.Contains("啊") || x.Age >= 46)
            //    .OrderBy<Person>(x => new Person { Text = x.Text, Age = x.Age })
            //    .OrderByDescending<Person>(x => x.CreateTime)
            //    .ToList<result>();

            //int t = DBModel
            //     .Select<Person>(x => x.State)
            //     .Where<Person>(x => x.Text.Contains("啊") || x.Age >= 46)
            //     .OrderBy<Person>(x => new Person { Text = x.Text, Age = x.Age })
            //     .OrderByDescending<Person>(x => x.CreateTime)
            //     .Delete();

            Console.ReadKey();
        }
    }
    public class result
    {
        public int? Age { get; set; }

        public bool? State { get; set; }
    }
}
