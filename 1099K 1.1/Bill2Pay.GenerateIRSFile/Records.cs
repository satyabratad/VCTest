using Bill2Pay.GenerateIRSFile.Model;
using System.Collections.Generic;

namespace Bill2Pay.GenerateIRSFile
{
    /// <summary>
    /// Records
    /// </summary>
    public class Records
    {
        /// <summary>
        /// Record Type
        /// </summary>
        public string RecordType { get; set; }

        /// <summary>
        /// Fields
        /// </summary>
        public List<Field> Fields { get; set; }
    }
}
