using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace BDSA2019.Lecture03
{
    static class Notes
    {
        // public static void Hamlet()
        // {
        //     var text = File.ReadAllText("Hamlet.txt");

        //     var words = Regex.Split(text, @"\P{L}+");

        //     var histogram = from w in words
        //                     group w by w into h
        //                     let c = h.Count()
        //                     orderby c descending
        //                     select new { Word = h.Key, Count = c };

        //     ToString(histogram.Take(5));

        //     // Dictionary
        //     // LO
        // }

        // public static void ToString<T>(this T stuff)
        // {
        //     Console.WriteLine(stuff);
        // }
    }
}
