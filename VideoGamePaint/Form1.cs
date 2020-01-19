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
            //pnlColorOptions
            this.pnlColorOptions.pixelSize = 20;
            this.pnlColorOptions.defaultPaintingEnabled = false;
            this.pnlColorOptions.onPixelClicked += setDrawingColor;
        }
        
        private void btnColorPicker_Click(object sender, EventArgs e)
        {
            DialogResult result = dlgColor.ShowDialog();
            setDrawingColor(dlgColor.Color);
        }

        private void setDrawingColor(Color color)
        {
            dlgColor.Color = color;
            btnColorPicker.BackColor = color;
            pnlPaint.drawColor = color;
        }
    }
}
