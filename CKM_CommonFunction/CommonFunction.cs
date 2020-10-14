using System.Data;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace CKM_CommonFunction
{
    public class CommonFunction
    {
        /// <summary>
        /// Check UserInput Key is Valid for Price
        /// </summary>
        /// <param name="c">Type Char</param>
        /// <param name="AllowMinus">If price allow minus sign then true otherwise false</param>
        /// <returns></returns>
        public bool IsPriceKey(char c,bool AllowMinus)
        {
            if (char.IsDigit(c) || (((c == (char)Keys.Back) || c == ',') || c == '.') || (c == '\u0016' || c == '\u0001' || c == '\u0003' || c == '\u0018') || (c == '-' && AllowMinus) || (c == (char)Keys.Enter))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Check UserInput Key is Valid for Price
        /// </summary>
        /// <param name="c">Type Char</param>
        /// <param name="AllowMinus">If price allow minus sign then true otherwise false</param>
        /// <returns></returns>
        public bool IsNumberKey(char c,bool AllowMinus)
        {
            if (char.IsDigit(c) || (((c == (char)Keys.Back)) || (c == '-' && AllowMinus)) || (c == '\u0016' || c == '\u0001' || c == '\u0003' || c == '\u0018') || (c == (char)Keys.Enter))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Check UserInput Key is Valid for YYYYMM
        /// </summary>
        /// <param name="c">Type Char</param>
        /// <param name="AllowMinus">If price allow minus sign then true otherwise false</param>
        /// <returns></returns>
        public bool IsYYYYMMKey(char c)
        {
            if (char.IsDigit(c) || (((c == (char)Keys.Back))) || (c == '\u0016' || c == '\u0001' || c == '\u0003' || c == '\u0018') || (c == (char)Keys.Enter))
            {
                return true;
            }

            return false;
        }

        public bool IsByteLengthOver(int maxlength,string text)
        {
            int byteCount = Encoding.GetEncoding("Shift_JIS").GetByteCount(text);
            if (byteCount > maxlength)
                return true;
            return false;
        }

        /// <summary>
        /// Convert Datatable to Json
        /// </summary>
        /// <param name="datatable"></param>
        /// <returns></returns>
        public string DataTableToJSONWithJSONNet(DataTable table)
        {
            return JsonConvert.SerializeObject(table);
        }
    }
}
