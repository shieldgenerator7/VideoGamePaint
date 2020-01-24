namespace VideoGamePaint
{
    partial class frmPaint
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPaint));
            this.pnlColors = new System.Windows.Forms.Panel();
            this.btnColorPicker = new System.Windows.Forms.Button();
            this.dlgColor = new System.Windows.Forms.ColorDialog();
            this.pnlTools = new System.Windows.Forms.Panel();
            this.btnToolFillLightBulb = new System.Windows.Forms.Button();
            this.btnToolLine = new System.Windows.Forms.Button();
            this.btnHandTool = new System.Windows.Forms.Button();
            this.btnToolFill = new System.Windows.Forms.Button();
            this.btnToolPencil = new System.Windows.Forms.Button();
            this.spltPalettes = new System.Windows.Forms.SplitContainer();
            this.spltTools = new System.Windows.Forms.SplitContainer();
            this.tabPalettes = new System.Windows.Forms.TabControl();
            this.tabColors = new System.Windows.Forms.TabPage();
            this.tabColliders = new System.Windows.Forms.TabPage();
            this.tmrGame = new System.Windows.Forms.Timer(this.components);
            this.txtCode = new System.Windows.Forms.TextBox();
            this.btnCodeGo = new System.Windows.Forms.Button();
            this.pnlPaint = new VideoGamePaint.PixelGridPanel();
            this.pnlColorOptions = new VideoGamePaint.PixelGridPanel();
            this.spltCode = new System.Windows.Forms.SplitContainer();
            this.pnlColors.SuspendLayout();
            this.pnlTools.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spltPalettes)).BeginInit();
            this.spltPalettes.Panel1.SuspendLayout();
            this.spltPalettes.Panel2.SuspendLayout();
            this.spltPalettes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spltTools)).BeginInit();
            this.spltTools.Panel1.SuspendLayout();
            this.spltTools.Panel2.SuspendLayout();
            this.spltTools.SuspendLayout();
            this.tabPalettes.SuspendLayout();
            this.tabColors.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spltCode)).BeginInit();
            this.spltCode.Panel1.SuspendLayout();
            this.spltCode.Panel2.SuspendLayout();
            this.spltCode.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlColors
            // 
            this.pnlColors.Controls.Add(this.pnlColorOptions);
            this.pnlColors.Controls.Add(this.btnColorPicker);
            this.pnlColors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlColors.Location = new System.Drawing.Point(3, 3);
            this.pnlColors.Name = "pnlColors";
            this.pnlColors.Padding = new System.Windows.Forms.Padding(5);
            this.pnlColors.Size = new System.Drawing.Size(674, 31);
            this.pnlColors.TabIndex = 1;
            // 
            // btnColorPicker
            // 
            this.btnColorPicker.BackColor = System.Drawing.Color.Black;
            this.btnColorPicker.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnColorPicker.FlatAppearance.BorderSize = 0;
            this.btnColorPicker.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnColorPicker.Location = new System.Drawing.Point(5, 5);
            this.btnColorPicker.Name = "btnColorPicker";
            this.btnColorPicker.Size = new System.Drawing.Size(90, 21);
            this.btnColorPicker.TabIndex = 0;
            this.btnColorPicker.UseVisualStyleBackColor = false;
            this.btnColorPicker.Click += new System.EventHandler(this.btnColorPicker_Click);
            // 
            // dlgColor
            // 
            this.dlgColor.SolidColorOnly = true;
            // 
            // pnlTools
            // 
            this.pnlTools.Controls.Add(this.btnToolFillLightBulb);
            this.pnlTools.Controls.Add(this.btnToolLine);
            this.pnlTools.Controls.Add(this.btnHandTool);
            this.pnlTools.Controls.Add(this.btnToolFill);
            this.pnlTools.Controls.Add(this.btnToolPencil);
            this.pnlTools.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTools.Location = new System.Drawing.Point(0, 0);
            this.pnlTools.Name = "pnlTools";
            this.pnlTools.Size = new System.Drawing.Size(100, 463);
            this.pnlTools.TabIndex = 2;
            // 
            // btnToolFillLightBulb
            // 
            this.btnToolFillLightBulb.Location = new System.Drawing.Point(5, 290);
            this.btnToolFillLightBulb.Name = "btnToolFillLightBulb";
            this.btnToolFillLightBulb.Size = new System.Drawing.Size(90, 58);
            this.btnToolFillLightBulb.TabIndex = 4;
            this.btnToolFillLightBulb.Text = "LightBulb Fill";
            this.btnToolFillLightBulb.UseVisualStyleBackColor = true;
            this.btnToolFillLightBulb.Click += new System.EventHandler(this.btnToolFillLightBulb_Click);
            // 
            // btnToolLine
            // 
            this.btnToolLine.Location = new System.Drawing.Point(5, 219);
            this.btnToolLine.Name = "btnToolLine";
            this.btnToolLine.Size = new System.Drawing.Size(92, 63);
            this.btnToolLine.TabIndex = 3;
            this.btnToolLine.Text = "Line Tool";
            this.btnToolLine.UseVisualStyleBackColor = true;
            this.btnToolLine.Click += new System.EventHandler(this.btnToolLine_Click);
            // 
            // btnHandTool
            // 
            this.btnHandTool.Location = new System.Drawing.Point(5, 150);
            this.btnHandTool.Name = "btnHandTool";
            this.btnHandTool.Size = new System.Drawing.Size(90, 62);
            this.btnHandTool.TabIndex = 2;
            this.btnHandTool.Text = "Hand Tool";
            this.btnHandTool.UseVisualStyleBackColor = true;
            this.btnHandTool.Click += new System.EventHandler(this.btnHandTool_Click);
            // 
            // btnToolFill
            // 
            this.btnToolFill.Location = new System.Drawing.Point(4, 82);
            this.btnToolFill.Name = "btnToolFill";
            this.btnToolFill.Size = new System.Drawing.Size(91, 61);
            this.btnToolFill.TabIndex = 1;
            this.btnToolFill.Text = "Fill Tool";
            this.btnToolFill.UseVisualStyleBackColor = true;
            this.btnToolFill.Click += new System.EventHandler(this.btnToolFill_Click);
            // 
            // btnToolPencil
            // 
            this.btnToolPencil.Location = new System.Drawing.Point(5, 13);
            this.btnToolPencil.Name = "btnToolPencil";
            this.btnToolPencil.Size = new System.Drawing.Size(92, 62);
            this.btnToolPencil.TabIndex = 0;
            this.btnToolPencil.Text = "Pencil Tool";
            this.btnToolPencil.UseVisualStyleBackColor = true;
            this.btnToolPencil.Click += new System.EventHandler(this.btnToolPencil_Click);
            // 
            // spltPalettes
            // 
            this.spltPalettes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spltPalettes.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.spltPalettes.IsSplitterFixed = true;
            this.spltPalettes.Location = new System.Drawing.Point(0, 0);
            this.spltPalettes.Name = "spltPalettes";
            this.spltPalettes.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // spltPalettes.Panel1
            // 
            this.spltPalettes.Panel1.Controls.Add(this.spltTools);
            // 
            // spltPalettes.Panel2
            // 
            this.spltPalettes.Panel2.Controls.Add(this.tabPalettes);
            this.spltPalettes.Size = new System.Drawing.Size(696, 548);
            this.spltPalettes.SplitterDistance = 463;
            this.spltPalettes.SplitterWidth = 1;
            this.spltPalettes.TabIndex = 3;
            // 
            // spltTools
            // 
            this.spltTools.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spltTools.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.spltTools.IsSplitterFixed = true;
            this.spltTools.Location = new System.Drawing.Point(0, 0);
            this.spltTools.Name = "spltTools";
            // 
            // spltTools.Panel1
            // 
            this.spltTools.Panel1.Controls.Add(this.pnlTools);
            // 
            // spltTools.Panel2
            // 
            this.spltTools.Panel2.Controls.Add(this.pnlPaint);
            this.spltTools.Size = new System.Drawing.Size(696, 463);
            this.spltTools.SplitterDistance = 100;
            this.spltTools.SplitterWidth = 1;
            this.spltTools.TabIndex = 4;
            // 
            // tabPalettes
            // 
            this.tabPalettes.Controls.Add(this.tabColors);
            this.tabPalettes.Controls.Add(this.tabColliders);
            this.tabPalettes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabPalettes.Location = new System.Drawing.Point(0, 0);
            this.tabPalettes.Name = "tabPalettes";
            this.tabPalettes.SelectedIndex = 0;
            this.tabPalettes.Size = new System.Drawing.Size(696, 84);
            this.tabPalettes.TabIndex = 1;
            this.tabPalettes.SelectedIndexChanged += new System.EventHandler(this.tabPalettes_SelectedIndexChanged);
            // 
            // tabColors
            // 
            this.tabColors.Controls.Add(this.pnlColors);
            this.tabColors.Location = new System.Drawing.Point(8, 39);
            this.tabColors.Name = "tabColors";
            this.tabColors.Padding = new System.Windows.Forms.Padding(3);
            this.tabColors.Size = new System.Drawing.Size(680, 37);
            this.tabColors.TabIndex = 0;
            this.tabColors.Text = "Colors";
            this.tabColors.UseVisualStyleBackColor = true;
            // 
            // tabColliders
            // 
            this.tabColliders.Location = new System.Drawing.Point(8, 39);
            this.tabColliders.Name = "tabColliders";
            this.tabColliders.Padding = new System.Windows.Forms.Padding(3);
            this.tabColliders.Size = new System.Drawing.Size(680, 37);
            this.tabColliders.TabIndex = 1;
            this.tabColliders.Text = "Colliders";
            this.tabColliders.UseVisualStyleBackColor = true;
            // 
            // tmrGame
            // 
            this.tmrGame.Enabled = true;
            this.tmrGame.Interval = 50;
            this.tmrGame.Tick += new System.EventHandler(this.tmrGame_Tick);
            // 
            // txtCode
            // 
            this.txtCode.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.txtCode.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.txtCode.Font = new System.Drawing.Font("Courier New", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCode.Location = new System.Drawing.Point(0, 65);
            this.txtCode.Multiline = true;
            this.txtCode.Name = "txtCode";
            this.txtCode.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtCode.Size = new System.Drawing.Size(325, 483);
            this.txtCode.TabIndex = 0;
            this.txtCode.Text = resources.GetString("txtCode.Text");
            // 
            // btnCodeGo
            // 
            this.btnCodeGo.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnCodeGo.Location = new System.Drawing.Point(0, 0);
            this.btnCodeGo.Name = "btnCodeGo";
            this.btnCodeGo.Size = new System.Drawing.Size(96, 56);
            this.btnCodeGo.TabIndex = 1;
            this.btnCodeGo.Text = "Go";
            this.btnCodeGo.UseVisualStyleBackColor = true;
            this.btnCodeGo.Click += new System.EventHandler(this.btnCodeGo_Click);
            // 
            // pnlPaint
            // 
            this.pnlPaint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPaint.Location = new System.Drawing.Point(0, 0);
            this.pnlPaint.Name = "pnlPaint";
            this.pnlPaint.PixelSize = 8F;
            this.pnlPaint.Size = new System.Drawing.Size(595, 463);
            this.pnlPaint.TabIndex = 0;
            // 
            // pnlColorOptions
            // 
            this.pnlColorOptions.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlColorOptions.Location = new System.Drawing.Point(95, 5);
            this.pnlColorOptions.Name = "pnlColorOptions";
            this.pnlColorOptions.PixelSize = 8F;
            this.pnlColorOptions.Size = new System.Drawing.Size(400, 21);
            this.pnlColorOptions.TabIndex = 1;
            // 
            // spltCode
            // 
            this.spltCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spltCode.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.spltCode.Location = new System.Drawing.Point(0, 0);
            this.spltCode.Name = "spltCode";
            // 
            // spltCode.Panel1
            // 
            this.spltCode.Panel1.Controls.Add(this.spltPalettes);
            // 
            // spltCode.Panel2
            // 
            this.spltCode.Panel2.Controls.Add(this.txtCode);
            this.spltCode.Panel2.Controls.Add(this.btnCodeGo);
            this.spltCode.Panel2.Resize += new System.EventHandler(this.splitContainer1_Panel2_Resize);
            this.spltCode.Size = new System.Drawing.Size(800, 548);
            this.spltCode.SplitterDistance = 696;
            this.spltCode.SplitterWidth = 8;
            this.spltCode.TabIndex = 4;
            this.spltCode.Resize += new System.EventHandler(this.spltCode_Resize);
            // 
            // frmPaint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 548);
            this.Controls.Add(this.spltCode);
            this.Name = "frmPaint";
            this.Text = "Video Game Paint";
            this.Resize += new System.EventHandler(this.frmPaint_Resize);
            this.pnlColors.ResumeLayout(false);
            this.pnlTools.ResumeLayout(false);
            this.spltPalettes.Panel1.ResumeLayout(false);
            this.spltPalettes.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spltPalettes)).EndInit();
            this.spltPalettes.ResumeLayout(false);
            this.spltTools.Panel1.ResumeLayout(false);
            this.spltTools.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spltTools)).EndInit();
            this.spltTools.ResumeLayout(false);
            this.tabPalettes.ResumeLayout(false);
            this.tabColors.ResumeLayout(false);
            this.spltCode.Panel1.ResumeLayout(false);
            this.spltCode.Panel2.ResumeLayout(false);
            this.spltCode.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spltCode)).EndInit();
            this.spltCode.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private PixelGridPanel pnlPaint;
        private System.Windows.Forms.Panel pnlColors;
        private System.Windows.Forms.ColorDialog dlgColor;
        private System.Windows.Forms.Button btnColorPicker;
        private PixelGridPanel pnlColorOptions;
        private System.Windows.Forms.Panel pnlTools;
        private System.Windows.Forms.SplitContainer spltPalettes;
        private System.Windows.Forms.SplitContainer spltTools;
        private System.Windows.Forms.Button btnToolFill;
        private System.Windows.Forms.Button btnToolPencil;
        private System.Windows.Forms.Button btnHandTool;
        private System.Windows.Forms.Button btnToolLine;
        private System.Windows.Forms.Button btnToolFillLightBulb;
        private System.Windows.Forms.Timer tmrGame;
        private System.Windows.Forms.TabControl tabPalettes;
        private System.Windows.Forms.TabPage tabColors;
        private System.Windows.Forms.TabPage tabColliders;
        private System.Windows.Forms.TextBox txtCode;
        private System.Windows.Forms.Button btnCodeGo;
        private System.Windows.Forms.SplitContainer spltCode;
    }
}
