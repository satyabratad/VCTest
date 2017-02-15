using Bill2Pay.GenerateIRSFile;
using Bill2Pay.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLibrary
{
    class Program
    {
        static void Main(string[] args)
        {
           
            GenerateTaxFile file = new GenerateTaxFile(true,2015,1);
            file.ReadFromSchemaFile();
            //Console.WriteLine(file.mk());
            Console.ReadKey();
        }
    }
}
