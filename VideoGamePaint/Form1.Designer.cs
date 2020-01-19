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
            this.pnlPaint = new VideoGamePaint.PixelGridPanel();
            this.pnlColors = new System.Windows.Forms.Panel();
            this.pnlColorOptions = new VideoGamePaint.PixelGridPanel();
            this.btnColorPicker = new System.Windows.Forms.Button();
            this.dlgColor = new System.Windows.Forms.ColorDialog();
            this.pnlTools = new System.Windows.Forms.Panel();
            this.spltColors = new System.Windows.Forms.SplitContainer();
            this.spltTools = new System.Windows.Forms.SplitContainer();
            this.pnlColors.SuspendLayout();
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
            // pnlPaint
            // 
            this.pnlPaint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPaint.Location = new System.Drawing.Point(0, 0);
            this.pnlPaint.Name = "pnlPaint";
            this.pnlPaint.Size = new System.Drawing.Size(699, 400);
            this.pnlPaint.TabIndex = 0;
            // 
            // pnlColors
            // 
            this.pnlColors.Controls.Add(this.pnlColorOptions);
            this.pnlColors.Controls.Add(this.btnColorPicker);
            this.pnlColors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlColors.Location = new System.Drawing.Point(0, 0);
            this.pnlColors.Name = "pnlColors";
            this.pnlColors.Padding = new System.Windows.Forms.Padding(5);
            this.pnlColors.Size = new System.Drawing.Size(800, 49);
            this.pnlColors.TabIndex = 1;
            // 
            // pnlColorOptions
            // 
            this.pnlColorOptions.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlColorOptions.Location = new System.Drawing.Point(95, 5);
            this.pnlColorOptions.Name = "pnlColorOptions";
            this.pnlColorOptions.Size = new System.Drawing.Size(400, 39);
            this.pnlColorOptions.TabIndex = 1;
            // 
            // btnColorPicker
            // 
            this.btnColorPicker.BackColor = System.Drawing.Color.Black;
            this.btnColorPicker.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnColorPicker.FlatAppearance.BorderSize = 0;
            this.btnColorPicker.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnColorPicker.Location = new System.Drawing.Point(5, 5);
            this.btnColorPicker.Name = "btnColorPicker";
            this.btnColorPicker.Size = new System.Drawing.Size(90, 39);
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
            this.pnlTools.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTools.Location = new System.Drawing.Point(0, 0);
            this.pnlTools.Name = "pnlTools";
            this.pnlTools.Size = new System.Drawing.Size(100, 400);
            this.pnlTools.TabIndex = 2;
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
            this.spltColors.SplitterDistance = 400;
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
            this.spltTools.Size = new System.Drawing.Size(800, 400);
            this.spltTools.SplitterDistance = 100;
            this.spltTools.SplitterWidth = 1;
            this.spltTools.TabIndex = 4;
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
    }
}

