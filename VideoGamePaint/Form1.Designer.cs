﻿namespace VideoGamePaint
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
            this.pnlColors.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlPaint
            // 
            this.pnlPaint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPaint.Location = new System.Drawing.Point(0, 0);
            this.pnlPaint.Name = "pnlPaint";
            this.pnlPaint.Size = new System.Drawing.Size(800, 450);
            this.pnlPaint.TabIndex = 0;
            // 
            // pnlColors
            // 
            this.pnlColors.Controls.Add(this.pnlColorOptions);
            this.pnlColors.Controls.Add(this.btnColorPicker);
            this.pnlColors.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlColors.Location = new System.Drawing.Point(0, 350);
            this.pnlColors.Name = "pnlColors";
            this.pnlColors.Padding = new System.Windows.Forms.Padding(5);
            this.pnlColors.Size = new System.Drawing.Size(800, 100);
            this.pnlColors.TabIndex = 1;
            // 
            // pnlColorOptions
            // 
            this.pnlColorOptions.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlColorOptions.Location = new System.Drawing.Point(95, 5);
            this.pnlColorOptions.Name = "pnlColorOptions";
            this.pnlColorOptions.Size = new System.Drawing.Size(400, 90);
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
            this.btnColorPicker.Size = new System.Drawing.Size(90, 90);
            this.btnColorPicker.TabIndex = 0;
            this.btnColorPicker.UseVisualStyleBackColor = false;
            this.btnColorPicker.Click += new System.EventHandler(this.btnColorPicker_Click);
            // 
            // dlgColor
            // 
            this.dlgColor.SolidColorOnly = true;
            // 
            // frmPaint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pnlColors);
            this.Controls.Add(this.pnlPaint);
            this.Name = "frmPaint";
            this.Text = "Video Game Paint";
            this.pnlColors.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private PixelGridPanel pnlPaint;
        private System.Windows.Forms.Panel pnlColors;
        private System.Windows.Forms.ColorDialog dlgColor;
        private System.Windows.Forms.Button btnColorPicker;
        private PixelGridPanel pnlColorOptions;
    }
}

