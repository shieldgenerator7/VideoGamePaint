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
        Dictionary<Color, Brush> colorBrushes = new Dictionary<Color, Brush>();
        bool mouseDown = false;

        public frmPaint()
        {
            InitializeComponent();
        }

        private void pnlPaint_Paint(object sender, PaintEventArgs e)
        {
            Trace.WriteLine("pnlPaint called");
            Pen p = new Pen(Color.Red);
            Brush b = new SolidBrush(Color.Red);
            Graphics g = e.Graphics;
            //g.DrawRectangle(p, 10, 10, 100, 100);
            //g.FillRectangle(b, 10, 10, 100, 100);

            for (int x = 0; x < pnlPaint.pixelGrid.GRID_SIZE; x++)
            {
                for (int y = 0; y < pnlPaint.pixelGrid.GRID_SIZE; y++)
                {
                    g.FillRectangle(
                        getBrush(pnlPaint.getColor(x, y)),
                        pnlPaint.getRect(x, y)
                        );
                }
            }
        }

        Brush getBrush(Color color)
        {
            if (color == null)
            {
                color = Color.Red;
            }
            Brush brush;
            if (colorBrushes.ContainsKey(color))
            {
                brush = colorBrushes[color];
            }
            else
            {
                brush = new SolidBrush(color);
                colorBrushes.Add(color, brush);
            }
            return brush;
        }
        
        private void btnColorPicker_Click(object sender, EventArgs e)
        {
            DialogResult result = dlgColor.ShowDialog();
            btnColorPicker.BackColor = dlgColor.Color;
            pnlPaint.drawColor = dlgColor.Color;
        }
    }
}
