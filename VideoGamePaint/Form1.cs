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

namespace VideoGamePaint
{
    public partial class frmPaint : Form
    {       

        public frmPaint()
        {
            InitializeComponent();
        }
        
        private void btnColorPicker_Click(object sender, EventArgs e)
        {
            DialogResult result = dlgColor.ShowDialog();
            btnColorPicker.BackColor = dlgColor.Color;
            pnlPaint.drawColor = dlgColor.Color;
        }
    }
}
