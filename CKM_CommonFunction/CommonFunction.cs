using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace CKM_CommonFunction
{
    public class CommonFunction
    {
        /// <summary>
        /// change datatable to xml format
        /// </summary>
        /// <param name="dt"></param>
        /// <returns>return xml format string</returns>
        public String DataTableToXml(DataTable dt)
        {
            dt.TableName = "test";
            System.IO.StringWriter writer = new System.IO.StringWriter();
            dt.WriteXml(writer, XmlWriteMode.WriteSchema, false);
            string result = writer.ToString();
            return result;
        }
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
                if(ctrl is DataGridView)
                {
                    ((DataGridView)ctrl).ReadOnly = true;
                }
                else if(!(ctrl is Label))
                    ctrl.Enabled = false;
            }

            if(panel.Name.Equals("PanelTitle"))
            {
                Control controls = panel.Parent as Control;
                if (controls.Controls.Find("cboMode", true).Length > 0)
                {
                    Control ctrl1 = controls.Controls.Find("cboMode", true)[0];
                    ctrl1.Enabled = false;
                }
            }            
        }
        public void EnablePanel(Panel panel)
        {
            foreach (Control ctrl in panel.Controls)
            {
                ctrl.Enabled = true;
            }

            if(panel.Name.Equals("PanelTitle"))
            {
                Control controls = panel.Parent as Control;
                if (controls.Controls.Find("cboMode", true).Length > 0)
                {
                    Control ctrl1 = controls.Controls.Find("cboMode", true)[0];
                    ctrl1.Enabled = true;
                }
            }           
        }

        /// <summary>
        /// Check Date
        /// </summary>
        public bool DateCheck(TextBox textBox)
        {
            if (!string.IsNullOrWhiteSpace(textBox.Text))
            {
                if (IsInteger(textBox.Text.Replace("/", "").Replace("-", "")))
                {
                    string day = string.Empty, month = string.Empty, year = string.Empty;
                    if (textBox.Text.Contains("/"))
                    {
                        string[] date = textBox.Text.Split('/');
                        day = date[date.Length - 1].PadLeft(2, '0');
                        month = date[date.Length - 2].PadLeft(2, '0');

                        if (date.Length > 2)
                            year = date[date.Length - 3];

                        textBox.Text = year + month + day;//  this.Text.Replace("/", "");
                    }
                    else if (textBox.Text.Contains("-"))
                    {
                        string[] date = textBox.Text.Split('-');
                        day = date[date.Length - 1].PadLeft(2, '0');
                        month = date[date.Length - 2].PadLeft(2, '0');

                        if (date.Length > 2)
                            year = date[date.Length - 3];

                        textBox.Text = year + month + day;//  this.Text.Replace("-", "");
                    }

                    string text = textBox.Text;
                    text = text.PadLeft(8, '0');
                    day = text.Substring(text.Length - 2);
                    month = text.Substring(text.Length - 4).Substring(0, 2);
                    year = Convert.ToInt32(text.Substring(0, text.Length - 4)).ToString();

                    if (month == "00")
                    {
                        month = string.Empty;
                    }
                    if (year == "0")
                    {
                        year = string.Empty;
                    }

                    if (string.IsNullOrWhiteSpace(month))
                        month = DateTime.Now.Month.ToString().PadLeft(2, '0');//if user doesn't input for month,set current month

                    if (string.IsNullOrWhiteSpace(year))
                    {
                        year = DateTime.Now.Year.ToString();//if user doesn't input for year,set current year
                    }
                    else
                    {
                        if (year.Length == 1)
                            year = "200" + year;
                        else if (year.Length == 2)
                            year = "20" + year;
                    }

                    //string strdate = year + "-" + month + "-" + day;  2019.6.11 chg
                    string strdate = year + "/" + month + "/" + day;
                    if (CheckDateValue(strdate))
                    {
                        textBox.Text = strdate;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// check date is correct
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool CheckDateValue(string value)
        {
            return DateTime.TryParseExact(value,
                       "yyyy/MM/dd",
                       System.Globalization.CultureInfo.InvariantCulture,
                       DateTimeStyles.None,
                       out DateTime d);
        }

        /// <summary>
        /// Check Int value or not
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool IsInteger(string value)
        {
            value = value.Replace("-", "");
            if (Int64.TryParse(value, out Int64 Num))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
