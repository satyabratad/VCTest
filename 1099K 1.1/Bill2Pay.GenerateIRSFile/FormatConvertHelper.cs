using System;
using System.Globalization;

namespace Bill2Pay.GenerateIRSFile
{
    /// <summary>
    /// Format Convert Helper
    /// </summary>
    public static class FormatConvertHelper
    {
        /// <summary>
        /// Converting to String
        /// </summary>
        /// <param name="value">Object</param>
        /// <returns>String</returns>
        public static string ToString(object value)
        {
            if (value == null)
            {
                return string.Empty;
            }

            return Convert.ToString(value, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Converting To Int
        /// </summary>
        /// <param name="value">Object</param>
        /// <returns>Integer</returns>
        public static int ToInt(object value)
        {

            int integer = 0;
            if (value == null)
            {
                return integer;
            }
            var text = FormatConvertHelper.ToString(value).Trim();
            if (int.TryParse(text, out integer))
            {
                return integer;
            }

            return integer;
        }

        /// <summary>
        /// Converting Nullable Integer
        /// </summary>
        /// <param name="value">Object</param>
        /// <returns>Interger/Null</returns>
        public static int? ToIntNull(object value)
        {

            int integer = 0;
            if (value == null)
            {
                return null;
            }
            var text = FormatConvertHelper.ToString(value).Trim();
            if (string.IsNullOrEmpty(text))
            {
                return null;
            }
            int.TryParse(text, out integer);

            return (int?)integer;
        }
        /// <summary>
        /// Converting To double
        /// </summary>
        /// <param name="value">Object</param>
        /// <returns>Double</returns>
        public static double ToDouble(object value)
        {
            double output = default(double);
            if (value == null)
            {
                return output;
            }
            var text = FormatConvertHelper.ToString(value).Trim();
            if (string.IsNullOrEmpty(text))
            {
                return output;
            }
            double.TryParse(text, out output);
            return output;
        }
        /// <summary>
        /// Converting Nullable Double
        /// </summary>
        /// <param name="value">Object</param>
        /// <returns>Double/Null</returns>
        public static double? ToDoubleNull(object value)
        {
            double output = 0.0d;
            if (value == null)
            {
                return null;
            }
            var text = FormatConvertHelper.ToString(value).Trim();
            if (string.IsNullOrEmpty(text))
            {
                return null;
            }
            double.TryParse(text, out output);
            return (double?)output;
        }

        /// <summary>
        /// Converting To double
        /// </summary>
        /// <param name="value">Object</param>
        /// <returns>Double</returns>
        public static decimal ToDecimal(object value)
        {
            decimal output = default(decimal);
            if (value == null)
            {
                return output;
            }
            var text = FormatConvertHelper.ToString(value).Trim();
            if (string.IsNullOrEmpty(text))
            {
                return output;
            }
            decimal.TryParse(text, out output);
            return output;
        }
        /// <summary>
        /// Converting Nullable Double
        /// </summary>
        /// <param name="value">Object</param>
        /// <returns>Double/Null</returns>
        public static decimal? ToDecimalNull(object value)
        {
            decimal output = default(decimal);
            if (value == null)
            {
                return null;
            }
            var text = FormatConvertHelper.ToString(value).Trim();
            if (string.IsNullOrEmpty(text))
            {
                return null;
            }
            decimal.TryParse(text, out output);
            return (decimal?)output;
        }

        /// <summary>
        /// Converting to DateTime
        /// </summary>
        /// <param name="value">Object</param>
        /// <param name="format">DateFormat</param>
        /// <returns>DateTime</returns>
        public static DateTime ToDateTime(object value, DateFormats format)
        {
            DateTime date = DateTime.MinValue;

            if (value == null)
            {
                return DateTime.MinValue;
            }

            if (DateTime.TryParseExact(Convert.ToString(value, CultureInfo.InvariantCulture), format.ToString(),
                CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
            {
                return date;
            }

            return date;
        }
        /// <summary>
        /// ToEnum
        /// </summary>
        /// <typeparam name="T">T</typeparam>
        /// <param name="value">object</param>
        /// <returns>ToEnum</returns>
        public static T ToEnum<T>(object value) where T : struct
        {
            if (value == null)
            {
                return default(T);
            }
            var text = FormatConvertHelper.ToString(value).Trim();
            if (string.IsNullOrEmpty(text))
            {
                return default(T);
            }

            T result = default(T);
            if (Enum.TryParse(text, true, out result))
            {
                return result;
            }
            else
            {
                return default(T);
            }
        }
    }
}
