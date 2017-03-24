namespace Bill2Pay.GenerateIRSFile.Model
{
    /// <summary>
    /// Structure of Fields associat with IRS Fixed length file
    /// </summary>
    public class Field
    {
        /// <summary>
        /// Name of Field
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Field Position
        /// </summary>
        public string Position { get; set; }

        /// <summary>
        /// Field Length
        /// </summary>
        public int Length { get; set; }

        /// <summary>
        /// Field Defaujlt  value
        /// </summary>
        public string Default { get; set; }

        /// <summary>
        /// Field Alternate value
        /// </summary>
        public string Alternate { get; set; }

        /// <summary>
        /// Field Type
        /// </summary>
        public FieldType Type { get; set; }

        /// <summary>
        /// Date
        /// </summary>
        public string Data { get; set; }

        /// <summary>
        /// Allignment
        /// </summary>
        public Alignment Alignment { get; set; }

        /// <summary>
        /// Table Name associated with Field
        /// </summary>
        public string Table { get; set; }

        /// <summary>
        /// Pading Value
        /// </summary>
        /// <param name="value">Value</param>
        /// <returns></returns>
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
