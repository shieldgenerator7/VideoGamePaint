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
            player.rules = RuleBuilder.buildRuleSet(getComboBoxRuleSetString());
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

            this.flwCode.Controls.Add(newComboBox);
            //Move "new" button to end of list
            this.flwCode.Controls.SetChildIndex(btnAddExpression, this.flwCode.Controls.Count - 1);

            setExpressionDropDownOptions(newComboBox);

            newComboBox.Size = new System.Drawing.Size(121, 33);
        }

        private void setExpressionDropDownOptions(ComboBox cmb)
        {
            cmb.SelectedIndexChanged -= cmbExpression_SelectedIndexChanged;
            string textCMB = (cmb.SelectedIndex >= 0)
                ? "" + cmb.Items[cmb.SelectedIndex]
                : "";
            cmb.Items.Clear();
            cmb.Items.Add(" ");
            int indexCMB = cmb.Parent.Controls.IndexOf(cmb);
            //Find the options you need
            Type[] options = new Type[0];
            string strCombo = getComboBoxRuleSetString(indexCMB);
            try
            {
                RuleBuilder.buildRuleSet(strCombo);
            }
            catch (ArgumentMissingException ame)
            {
                options = ame.argTypes;
            }

            //Options found,
            //Populate the items list
            if (options.Length == 0)
            {
                cmb.Items.AddRange(new string[] { ":", ",", "." });
            }
            foreach (Expression expr in RuleBuilder.Expressions)
            {
                //2020-01-27: TODO: (to fix) this condition currently allows bools in actions and functions in conditions!!
                if (options.Length == 0
                    && (expr.isBool || expr.isFunction))
                {
                    cmb.Items.Add(expr);
                    string[] constantNames;
                    constantNames = expr.getConstantNames(typeof(bool));
                    if (constantNames != null && constantNames.Length > 0)
                    {
                        foreach (string constantName in constantNames)
                        {
                            if (!cmb.Items.Contains(constantName))
                            {
                                cmb.Items.Add(constantName);
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
                            cmb.Items.Add(expr);
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
                                if (!cmb.Items.Contains(constantName))
                                {
                                    cmb.Items.Add(constantName);
                                }
                            }
                        }
                    }
                }
            }

            //If the value can be anything (like a number or string),
            if (cmb.Items.Contains("INFINITY"))
            {
                //Make the combobox text editable
                cmb.Items.Remove("INFINITY");
                cmb.DropDownStyle = ComboBoxStyle.DropDown;
            }
            //If the value can only be certain things,
            else
            {
                //Make the combobox text non-editable
                cmb.DropDownStyle = ComboBoxStyle.DropDownList;
            }

            //Keep the previously selected item, if possible
            cmb.SelectedIndex = 0;
            if (textCMB != null && textCMB != "")
            {
                for (int i = 0; i < cmb.Items.Count; i++)
                {
                    if (textCMB == "" + cmb.Items[i])
                    {
                        cmb.SelectedIndex = i;
                        break;
                    }
                }
            }
            //Else find the first acceptable option
            if (cmb.SelectedIndex == 0 && textCMB != "" + cmb.Items[cmb.SelectedIndex])
            {
                for (int i = 0; i < cmb.Items.Count; i++)
                {
                    string option = "" + cmb.Items[i];
                    if (option.Length > 1)
                    {
                        cmb.SelectedIndex = i;
                        break;
                    }
                }
            }
            cmb.SelectedIndexChanged += cmbExpression_SelectedIndexChanged;
        }

        private void cmbExpression_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cmb = (ComboBox)sender;
            bool blank = "" + cmb.SelectedItem == "" || cmb.Text == null || cmb.Text.Trim() == "";
            bool lastCMB = flwCode.Controls[flwCode.Controls.Count - 2] == cmb;
            //Add or Edit next expression
            if (lastCMB)
            {
                if (!blank)
                {
                    addNewExpressionDropDown();
                }
            }
            else
            {
                setExpressionDropDownOptions(
                    (ComboBox)flwCode.Controls[
                        flwCode.Controls.IndexOf(cmb) + 1
                        ]
                    );
            }
            //Delete current expression
            if (blank)
            {
                flwCode.Controls.Remove(cmb);
                return;
            }
        }

        private string getComboBoxRuleSetString(int maxIndex = -1)
        {
            if (maxIndex < 0)
            {
                maxIndex = flwCode.Controls.Count - 1;
            }
            string strCombo = "";
            for (int i = 0; i < maxIndex; i++)
            {
                Control ctrl = flwCode.Controls[i];
                if (ctrl is ComboBox)
                {
                    strCombo += ctrl.Text + " ";
                }
            }
            return strCombo;
        }

        private void btnAddExpression_Click(object sender, EventArgs e)
        {
            addNewExpressionDropDown();
        }
    }
}