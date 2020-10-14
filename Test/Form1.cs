using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CKM_CommonFunction;
using CKM_DataLayer;

namespace Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
            FileFunction ff = new FileFunction();
            Dictionary<string, string> dicConfig = ff.ReadConfig(@"C:\\DBConfig\\DBConfig.ini", "DataBase", "Shinyoh");

            CKMDL c = new CKMDL();
        }
    }
}
