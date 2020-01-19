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
        List<Color> colorOptions = new List<Color>();
        Tool pencilTool;
        Tool fillTool;

        public frmPaint()
        {
            InitializeComponent();
            //Tools
            pencilTool = new PencilTool(pnlPaint);
            fillTool = new FillTool(pnlPaint);
            pnlPaint.activeTool = pencilTool;
            //pnlColorOptions
            this.pnlColorOptions.PixelSize = 20;
            this.pnlColorOptions.defaultPaintingEnabled = false;
            this.pnlColorOptions.onPixelClicked += setDrawingColor;
            this.pnlColorOptions.pixelGrid.Size = new Vector(10, 2);
            colorOptions.Add(Color.Black);
            colorOptions.Add(Color.Gray);
            colorOptions.Add(Color.White);
            colorOptions.Add(Color.Red);
            colorOptions.Add(Color.Orange);
            colorOptions.Add(Color.Yellow);
            colorOptions.Add(Color.Green);
            colorOptions.Add(Color.Blue);
            colorOptions.Add(Color.Purple);
            colorOptions.Add(Color.Brown);
            colorOptions.Add(Color.RosyBrown);
            colorOptions.Add(Color.SaddleBrown);
            colorOptions.Add(Color.SandyBrown);
            this.pnlColorOptions.setColorsFromList(colorOptions);
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

        private void btnToolPencil_Click(object sender, EventArgs e)
        {
            pnlPaint.activeTool = pencilTool;
        }

        private void btnToolFill_Click(object sender, EventArgs e)
        {
            pnlPaint.activeTool = fillTool;
        }
    }
}
