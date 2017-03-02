using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bill2Pay.GenerateIRSFile.Model
{
    public class Field
    {
        public string Name { get; set; }
        public string Position { get; set; }
        public int Length { get; set; }
        public string Default { get; set; }
        public string Alternate { get; set; }
        public FieldType Type { get; set; }
        public string Data { get; set; }
        public Alignment Alignment { get; set; }
        public string Table { get; set; }
        public string PadValue(string value)
        {
            string newValue = string.Empty;
            if(Type==FieldType.Alphanumeric)
            {
                if(Alignment == Model.Alignment.Left)
                {
                    newValue = value.PadRight(Length,' ');
                }
                else
                {
                    newValue = value.PadLeft(Length, ' ');
                }
            }
            else if (Type == FieldType.Numeric)
            {
                if (Alignment == Model.Alignment.Left)
                {
                    newValue = value.PadRight(Length, '0');
                }
                else
                {
                    newValue = value.PadLeft(Length, '0');
                }
            }

            return newValue;
        }
    }
}
