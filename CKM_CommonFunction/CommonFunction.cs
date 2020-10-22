using System.Collections.Generic;
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
        public bool IsPriceKey(char c, bool AllowMinus)
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
        public bool IsNumberKey(char c, bool AllowMinus)
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

        public bool IsByteLengthOver(int maxlength, string text)
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

        /// <summary>
        /// Get All control 
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public IEnumerable<Control> GetAllControls(Control root)
        {
            foreach (Control control in root.Controls)
            {
                foreach (Control child in GetAllControls(control))
                {
                    yield return child;
                }
            }
            yield return root;
        }

        //Clear All Control within param panel
        public void Clear(Panel panel)
        {
            IEnumerable<Control> c = GetAllControls(panel);
            foreach (Control ctrl in c)
            {
                if (ctrl is TextBox)
                    ((TextBox)ctrl).Text = string.Empty;
                if (ctrl is ComboBox)
                    ((ComboBox)ctrl).SelectedValue = "-1";
                if (ctrl is CheckBox)
                    ((CheckBox)ctrl).Checked = false;
                if (ctrl is DataGridView)
                {
                    if (((DataGridView)ctrl).DataSource is DataTable dtGrid)
                        dtGrid.Rows.Clear();
                }
            }
        }
        public void DisablePanel(Panel panel)
        {
            foreach (Control ctrl in panel.Controls)
            {
                ctrl.Enabled = false;
            }
        }
    }
}
