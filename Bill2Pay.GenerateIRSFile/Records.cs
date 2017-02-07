using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bill2Pay.GenerateIRSFile.Model;

namespace Bill2Pay.GenerateIRSFile
{
    public class Records
    {
        public string RecordType { get; set; }
        public List<Field> Fields { get; set; }
    }
}
