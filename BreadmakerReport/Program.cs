using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using RatingAdjustment.Services;
using BreadmakerReport.Models;

namespace BreadmakerReport
{
    class Program
    {
        static string dbfile = @".\data\breadmakers.db";
        static RatingAdjustmentService ratingAdjustmentService = new RatingAdjustmentService();

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Bread World");
            var BreadmakerDb = new BreadMakerSqliteContext(dbfile);
            var BMList = BreadmakerDb.Breadmakers;
            var tom =BMList.Select(s => s.BreadmakerId);
            System.Collections.Generic.List<string> Bid = new System.Collections.Generic.List<string>();
            System.Collections.Generic.List<Item> Alldata = new System.Collections.Generic.List<Item>();
            System.Collections.Generic.List<Item> pp = new System.Collections.Generic.List<Item>();
            string[,] tandid;
            var test1= BMList.Select(s=>s);
            foreach (var id in tom)
            {
                Bid.Add(id);
            }
               
            foreach (var name in Bid)
            {

                var tok = BMList.Where(s => s.BreadmakerId == name)
               .Select(s => s.Reviews);
    

                foreach (var name1 in tok)
                {
                    double m = 0;
                    double k = 0;
                    for (int i = 0; i < name1.Count; i++)
                    {
                        k=k+name1[i].stars;
                        m++;
                    }
                    string t_t = "";
                    foreach (var kk in test1)
                    {
                        if(name == kk.BreadmakerId)
                        {
                            t_t = kk.title;
                        }
                        
                    }

                  
                    Item p = new Item();
                    p.Reviews = m;
                    p.Average = Math.Round(k / m, 2);
                    p.Adjust = ratingAdjustmentService.Adjust(Math.Round(k / m, 2), m);
                    p.title = t_t;
                    pp.Add(p);
                }

                
            }
            pp = pp
                   .OrderByDescending(o => o.Adjust)
                   .ToList();

            // TODO: add LINQ logic ...
            //       ...
            //.ToList();
            

            Console.WriteLine("[#]  Reviews Average  Adjust    Description");
            for (var j = 0; j < 3; j++)
           {
                var i = pp[j];
                // TODO: add output
                 Console.WriteLine( j+1 + "\t"+i.Reviews+ "\t" +i.Average+ "\t"+i.Adjust+ "\t" + i.title );
            }
        }
    }
}
