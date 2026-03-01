using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Driving___Vehicle_License_Department__DVLD__Project
{
    public partial class frmNotes : Form
    {
        public frmNotes()
        {
            InitializeComponent();
        }

        public frmNotes(string Notes)
        {
            InitializeComponent();

            richTextBox1.Text = Notes;
            richTextBox1.ReadOnly = true;

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
