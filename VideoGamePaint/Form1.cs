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
        Tool handTool;
        Tool lineTool;
        Tool lightBulbFillTool;
        Player player;
        Keys lastKey;

        public frmPaint()
        {
            InitializeComponent();
            //Tools
            pencilTool = new PencilTool(pnlPaint);
            fillTool = new FillTool(pnlPaint);
            handTool = new HandTool(pnlPaint);
            lineTool = new LineTool(pnlPaint);
            lightBulbFillTool = new LightBulbFillTool(pnlPaint);
            pnlPaint.activeTool = pencilTool;
            //Player
            player = new Player(pnlPaint.pixelGrid);
            //pnlColorOptions
            this.pnlColorOptions.PixelSize = 20;
            this.pnlColorOptions.defaultPaintingEnabled = false;
            this.pnlColorOptions.onPixelClicked += setDrawingColor;
            this.pnlColorOptions.pixelGrid.Size.x = 10;
            this.pnlColorOptions.pixelGrid.Size.y = 2;
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

        private void btnHandTool_Click(object sender, EventArgs e)
        {
            pnlPaint.activeTool = handTool;
        }

        private void btnToolLine_Click(object sender, EventArgs e)
        {
            pnlPaint.activeTool = lineTool;
        }

        private void btnToolFillLightBulb_Click(object sender, EventArgs e)
        {
            pnlPaint.activeTool = lightBulbFillTool;
        }

        private void tmrGame_Tick(object sender, EventArgs e)
        {
            pnlPaint.entityGrid.clear(null);
            player.applyControls(pnlPaint.lastKey);
            pnlPaint.entityGrid.setPixel(
                player.pos.x,
                player.pos.y,
                PixelGridPanel.ColorToRGB(player.color)
                );
            pnlPaint.Invalidate();
        }

        private void frmPaint_KeyDown(object sender, KeyEventArgs e)
        {
            lastKey = e.KeyCode;
        }
    }
}
