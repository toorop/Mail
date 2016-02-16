using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test
{
    public partial class Form1 : Form
    {
        private List<string> subjectL;

        public Form1()
        {
            InitializeComponent();
        }


        private void Test_Click(object sender, EventArgs e)
        {
            Mail.Email mail = new Mail.Email(textBox1.Text);
            string pmHeader = mail.GetHeader("received");
            Debug.WriteLine("subject:" + pmHeader);




        }
    }
}
