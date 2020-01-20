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
            this.pnlColors = new System.Windows.Forms.Panel();
            this.btnColorPicker = new System.Windows.Forms.Button();
            this.dlgColor = new System.Windows.Forms.ColorDialog();
            this.pnlTools = new System.Windows.Forms.Panel();
            this.btnToolFill = new System.Windows.Forms.Button();
            this.btnToolPencil = new System.Windows.Forms.Button();
            this.spltColors = new System.Windows.Forms.SplitContainer();
            this.spltTools = new System.Windows.Forms.SplitContainer();
            this.btnHandTool = new System.Windows.Forms.Button();
            this.pnlPaint = new VideoGamePaint.PixelGridPanel();
            this.pnlColorOptions = new VideoGamePaint.PixelGridPanel();
            this.btnToolLine = new System.Windows.Forms.Button();
            this.pnlColors.SuspendLayout();
            this.pnlTools.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spltColors)).BeginInit();
            this.spltColors.Panel1.SuspendLayout();
            this.spltColors.Panel2.SuspendLayout();
            this.spltColors.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spltTools)).BeginInit();
            this.spltTools.Panel1.SuspendLayout();
            this.spltTools.Panel2.SuspendLayout();
            this.spltTools.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlColors
            // 
            this.pnlColors.Controls.Add(this.pnlColorOptions);
            this.pnlColors.Controls.Add(this.btnColorPicker);
            this.pnlColors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlColors.Location = new System.Drawing.Point(0, 0);
            this.pnlColors.Name = "pnlColors";
            this.pnlColors.Padding = new System.Windows.Forms.Padding(5);
            this.pnlColors.Size = new System.Drawing.Size(800, 46);
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
            this.btnColorPicker.Size = new System.Drawing.Size(90, 36);
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
            this.pnlTools.Controls.Add(this.btnToolLine);
            this.pnlTools.Controls.Add(this.btnHandTool);
            this.pnlTools.Controls.Add(this.btnToolFill);
            this.pnlTools.Controls.Add(this.btnToolPencil);
            this.pnlTools.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTools.Location = new System.Drawing.Point(0, 0);
            this.pnlTools.Name = "pnlTools";
            this.pnlTools.Size = new System.Drawing.Size(100, 403);
            this.pnlTools.TabIndex = 2;
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
            // spltColors
            // 
            this.spltColors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.spltColors.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.spltColors.IsSplitterFixed = true;
            this.spltColors.Location = new System.Drawing.Point(0, 0);
            this.spltColors.Name = "spltColors";
            this.spltColors.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // spltColors.Panel1
            // 
            this.spltColors.Panel1.Controls.Add(this.spltTools);
            // 
            // spltColors.Panel2
            // 
            this.spltColors.Panel2.Controls.Add(this.pnlColors);
            this.spltColors.Size = new System.Drawing.Size(800, 450);
            this.spltColors.SplitterDistance = 403;
            this.spltColors.SplitterWidth = 1;
            this.spltColors.TabIndex = 3;
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
            this.spltTools.Size = new System.Drawing.Size(800, 403);
            this.spltTools.SplitterDistance = 100;
            this.spltTools.SplitterWidth = 1;
            this.spltTools.TabIndex = 4;
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
            // pnlPaint
            // 
            this.pnlPaint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPaint.Location = new System.Drawing.Point(0, 0);
            this.pnlPaint.Name = "pnlPaint";
            this.pnlPaint.PixelSize = 8F;
            this.pnlPaint.Size = new System.Drawing.Size(699, 403);
            this.pnlPaint.TabIndex = 0;
            // 
            // pnlColorOptions
            // 
            this.pnlColorOptions.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlColorOptions.Location = new System.Drawing.Point(95, 5);
            this.pnlColorOptions.Name = "pnlColorOptions";
            this.pnlColorOptions.PixelSize = 8F;
            this.pnlColorOptions.Size = new System.Drawing.Size(400, 36);
            this.pnlColorOptions.TabIndex = 1;
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
            // frmPaint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.spltColors);
            this.Name = "frmPaint";
            this.Text = "Video Game Paint";
            this.pnlColors.ResumeLayout(false);
            this.pnlTools.ResumeLayout(false);
            this.spltColors.Panel1.ResumeLayout(false);
            this.spltColors.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spltColors)).EndInit();
            this.spltColors.ResumeLayout(false);
            this.spltTools.Panel1.ResumeLayout(false);
            this.spltTools.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spltTools)).EndInit();
            this.spltTools.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private PixelGridPanel pnlPaint;
        private System.Windows.Forms.Panel pnlColors;
        private System.Windows.Forms.ColorDialog dlgColor;
        private System.Windows.Forms.Button btnColorPicker;
        private PixelGridPanel pnlColorOptions;
        private System.Windows.Forms.Panel pnlTools;
        private System.Windows.Forms.SplitContainer spltColors;
        private System.Windows.Forms.SplitContainer spltTools;
        private System.Windows.Forms.Button btnToolFill;
        private System.Windows.Forms.Button btnToolPencil;
        private System.Windows.Forms.Button btnHandTool;
        private System.Windows.Forms.Button btnToolLine;
    }
}

