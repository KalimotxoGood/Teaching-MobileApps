//Rextester.Program.Main is the entry point for your code. Don't change it.
//Compiler version 4.0.30319.17929 for Microsoft (R) .NET Framework 4.5

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;

namespace Rextester
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Your code goes here
            /*
            List<int> list = new List<int>();
            int value;
            list.Add(1);
            list.Add(5);
            list.Add(9);
            List<int>.Enumerator e=list.GetEnumerator();
            //t b = 3;
            //Console.WriteLine(list[0]);
           
            //List<string> expr = myString.ToList();
            //List<string> expr = myString.Select(c => c.ToString()).ToList();
            
            */
            
            string raw = "ab!cdefg!hijkl";
            string newString = "";
 //         string newExpr = "";
            string a=null;
            string b =null;
            string d = null;
            int i=0;
            raw = raw;
            var newExpr = new StringBuilder();
            foreach (var c in raw)
            {
                if (" abcdefghijklmnopqrstuvwxyz".Contains(c)) // 1. Is it a letter of alph?
                {
                    CharEnumerator chEnum = raw.GetEnumerator();  // 2. Yes, then look 2 ahead
                                                                   // Possible by creating local character enumeration of raw
                    if(i<=2 && chEnum.MoveNext()) // Look ahead one
                    {
                        
                        Console.WriteLine(chEnum.Current);   
                        i++;                           
                        a=chEnum.Current.ToString();    // set current location to a (for future two digit alphabet)
                        chEnum.MoveNext();
                    }
                    if (!(" abcdefghijklmnopqrstuvwxyz".Contains(c) && (i==1)))
                    {
                        newExpr.Append(a); // append only 'a' if second relative digit is not an alphabet.
                        break; //break out after updating newExpr
                    }
                    if(" abcdefghijklmnopqrstuvwxyz".Contains(c) && (i==1))  // permit to enter if second digit is an alphabet
                    {
                      Console.WriteLine(chEnum.Current);   // 
                      b=chEnum.Current.ToString();     //set second (relative) digit to b if next condition verifies "!"
                      Console.WriteLine("b is assinged:" +b);
                      i++;  
                    }
                    if(" !".Contains(c) && (i==2)) //permit if third relative digit is an '!'
                    {
                      Console.WriteLine(chEnum.Current);
                      d=chEnum.Current.ToString();     // If true, set third digit ("!") to d
                      Console.WriteLine("d is assinged:" +d); 
                      newExpr.Append(a+b+d);    //Append everything to the already existing if "ab!" is to be added
                      i++;  
                      break;
                    }
                 
                    //go back and start from next enumeration "c" of "raw"
       
                }
                i=0;
            
            
            }
            
            Console.WriteLine(newExpr);
            
        }
    }
}
