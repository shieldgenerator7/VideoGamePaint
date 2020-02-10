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
        List<ComboBox> expressions = new List<ComboBox>();

        public frmPaint()
        {
            InitializeComponent();
            resizeForm();
            //Rule Builder
            RuleBuilder.buildMetas();
            addNewExpressionDropDown();
            //Tools
            pencilTool = new PencilTool(pnlPaint);
            fillTool = new FillTool(pnlPaint);
            handTool = new HandTool(pnlPaint);
            lineTool = new LineTool(pnlPaint);
            lightBulbFillTool = new LightBulbFillTool(pnlPaint);
            pnlPaint.activeTool = pencilTool;
            //Player
            player = new Player(pnlPaint.colliderGrid);
            player.setVariableNames(txtVariables.Text);
            player.rules = RuleBuilder.buildRuleSet(txtCode.Text);
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
            pnlPaint.Focus();
        }

        private void setDrawingColor(Color color)
        {
            dlgColor.Color = color;
            btnColorPicker.BackColor = color;
            pnlPaint.DrawColor = color;
        }

        private void btnToolPencil_Click(object sender, EventArgs e)
        {
            pnlPaint.activeTool = pencilTool;
            pnlPaint.Focus();
        }

        private void btnToolFill_Click(object sender, EventArgs e)
        {
            pnlPaint.activeTool = fillTool;
            pnlPaint.Focus();
        }

        private void btnHandTool_Click(object sender, EventArgs e)
        {
            pnlPaint.activeTool = handTool;
            pnlPaint.Focus();
        }

        private void btnToolLine_Click(object sender, EventArgs e)
        {
            pnlPaint.activeTool = lineTool;
            pnlPaint.Focus();
        }

        private void btnToolFillLightBulb_Click(object sender, EventArgs e)
        {
            pnlPaint.activeTool = lightBulbFillTool;
            pnlPaint.Focus();
        }

        private void tmrGame_Tick(object sender, EventArgs e)
        {
            pnlPaint.entityGrid.clear(RGB.nullRGB);
            player.inputs = pnlPaint.pressedKeys;
            player.processRules();
            player.updateMovement();
            pnlPaint.entityGrid.setPixel(
                player.pos.x,
                player.pos.y,
                PixelGridPanel.ColorToRGB(player.color)
                );
            pnlPaint.Invalidate();
        }

        private void tabPalettes_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tabPalettes.SelectedIndex)
            {
                case 0:
                    pnlPaint.ActiveGrid = pnlPaint.pixelGrid;
                    setDrawingColor(btnColorPicker.BackColor);
                    pnlPaint.checkSyncPixelGridToColliderGrid();
                    break;
                case 1:
                    pnlPaint.ActiveGrid = pnlPaint.colliderGrid;
                    break;
            }
            pnlPaint.Focus();
        }

        private void btnCodeGo_Click(object sender, EventArgs e)
        {
            player.setVariableNames(txtVariables.Text);
            player.rules.Clear();
            player.rules = RuleBuilder.buildRuleSet(txtCode.Text);
        }

        private void frmPaint_Resize(object sender, EventArgs e)
        {
            resizeForm();
        }
        private void spltCode_Resize(object sender, EventArgs e)
        {
            resizeForm();
        }
        private void splitContainer1_Panel2_Resize(object sender, EventArgs e)
        {
            resizeForm();
        }
        void resizeForm()
        {
            spltVariables.Size = new Size(
                spltCode.Panel2.Size.Width,
                spltCode.Panel2.Size.Height - btnCodeGo.Size.Height
                );
        }

        private void txtCode_TextChanged(object sender, EventArgs e)
        {
            //Point showPoint = txtCode.GetPositionFromCharIndex(txtCode.SelectionStart);
            //showPoint.Offset(0, (int)Math.Ceiling(txtCode.Font.SizeInPoints)+5);
            //ctxtExpression.Show(txtCode, showPoint);
        }

        void addNewExpressionDropDown()
        {
            ComboBox newComboBox = new ComboBox();

            newComboBox.FormattingEnabled = true;
            newComboBox.Items.Add(" ");
            //if (flwCode.Controls.Count )
            //Find the options you need
            Type[] options = new Type[0];
            string strCombo = "";
            foreach (Control ctrl in flwCode.Controls)
            {
                if (ctrl is ComboBox)
                {
                    strCombo += ctrl.Text + " ";
                }
            }
            if (!strCombo.Contains(":"))
            {
                strCombo += ": Move Player VectorUp";
            }
            try
            {
                RuleBuilder.buildRule(strCombo);
            }
            catch (ArgumentMissingException ame)
            {
                options = ame.argTypes;
            }
            catch (IndexOutOfRangeException ioore)
            {
                //do nothing
            }
            //if (flwCode.Controls.Count > 0)
            //{
            //    options = ((Expression)
            //       ((ComboBox) flwCode.Controls[flwCode.Controls.Count - 1]).SelectedItem
            //       ).getArgumentTypeOptions(0);
            //}
            foreach (Expression expr in RuleBuilder.Expressions)
            {
                //2020-01-27: TODO: (to fix) this condition currently allows bools in actions and functions in conditions!!
                if (options.Length == 0
                    && (expr.isBool || expr.isFunction))
                {
                    newComboBox.Items.Add(expr);
                    string[] constantNames;
                    constantNames = expr.getConstantNames(typeof(bool));
                    if (constantNames != null && constantNames.Length > 0)
                    {
                        foreach (string constantName in constantNames)
                        {
                            if (!newComboBox.Items.Contains(constantName))
                            {
                                newComboBox.Items.Add(constantName);
                            }
                        }
                    }
                }
                //Find a return type that matches one of the options
                else
                {
                    foreach (Type type in options)
                    {
                        if (type == null || expr.isType(type))
                        {
                            newComboBox.Items.Add(expr);
                            break;
                        }
                    }
                    string[] constantNames;
                    foreach (Type type in options)
                    {
                        constantNames = expr.getConstantNames(type);
                        if (constantNames != null && constantNames.Length > 0)
                        {
                            foreach (string constantName in constantNames)
                            {
                                if (!newComboBox.Items.Contains(constantName))
                                {
                                    newComboBox.Items.Add(constantName);
                                }
                            }
                        }
                    }
                }
            }
            if (options.Length > 0 && options[0] != null)
            {
                newComboBox.Text = options[0].Name;
            }
            else
            {
                newComboBox.Text = "Condition or Function";
            }
            //newComboBox.Items.AddRange(RuleBuilder.Expressions);
            //newComboBox.Location = new System.Drawing.Point(3, 3);
            //newComboBox.Name = "cmbExpression";
            newComboBox.Size = new System.Drawing.Size(121, 33);
            newComboBox.SelectedIndexChanged += cmbExpression_SelectedIndexChanged;

            this.flwCode.Controls.Add(newComboBox);
            //Move "new" button to end of list
            this.flwCode.Controls.SetChildIndex(btnAddExpression, this.flwCode.Controls.Count - 1);
        }

        private void cmbExpression_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cmb = (ComboBox)sender;
            if (flwCode.Controls[flwCode.Controls.Count - 2] == cmb)
            {
                if (cmb.Text != null && cmb.Text.Trim() != "")
                {
                    addNewExpressionDropDown();
                }
            }
        }

        private void btnAddExpression_Click(object sender, EventArgs e)
        {
            addNewExpressionDropDown();
        }
    }
}