namespace LoopDrawingAcadUI
{
    partial class LoopDrawingUI
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
            this.lblSourceFile = new System.Windows.Forms.Label();
            this.btnLoadLoopDrawingsSourceFile = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnCreateDrawings = new System.Windows.Forms.Button();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblSourceFile
            // 
            this.lblSourceFile.AutoSize = true;
            this.lblSourceFile.Location = new System.Drawing.Point(141, 29);
            this.lblSourceFile.Name = "lblSourceFile";
            this.lblSourceFile.Size = new System.Drawing.Size(85, 13);
            this.lblSourceFile.TabIndex = 20;
            this.lblSourceFile.Text = "Source File Path";
            // 
            // btnLoadLoopDrawingsSourceFile
            // 
            this.btnLoadLoopDrawingsSourceFile.Location = new System.Drawing.Point(15, 16);
            this.btnLoadLoopDrawingsSourceFile.Name = "btnLoadLoopDrawingsSourceFile";
            this.btnLoadLoopDrawingsSourceFile.Size = new System.Drawing.Size(109, 39);
            this.btnLoadLoopDrawingsSourceFile.TabIndex = 19;
            this.btnLoadLoopDrawingsSourceFile.Text = "Load Source File";
            this.btnLoadLoopDrawingsSourceFile.UseVisualStyleBackColor = true;
            this.btnLoadLoopDrawingsSourceFile.Click += new System.EventHandler(this.btnLoadLoopDrawingsSourceFile_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnCreateDrawings);
            this.panel2.Controls.Add(this.btnLoadLoopDrawingsSourceFile);
            this.panel2.Controls.Add(this.lblSourceFile);
            this.panel2.Location = new System.Drawing.Point(23, 30);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(816, 171);
            this.panel2.TabIndex = 21;
            // 
            // btnCreateDrawings
            // 
            this.btnCreateDrawings.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreateDrawings.Location = new System.Drawing.Point(15, 72);
            this.btnCreateDrawings.Name = "btnCreateDrawings";
            this.btnCreateDrawings.Size = new System.Drawing.Size(782, 78);
            this.btnCreateDrawings.TabIndex = 23;
            this.btnCreateDrawings.Text = "Create Drawings";
            this.btnCreateDrawings.UseVisualStyleBackColor = true;
            this.btnCreateDrawings.Click += new System.EventHandler(this.btnCreateDrawings_Click);
            // 
            // LoopDrawingUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(847, 209);
            this.Controls.Add(this.panel2);
            this.Name = "LoopDrawingUI";
            this.Text = "LoopDrawingUI";
            this.Load += new System.EventHandler(this.LoopDrawingUI_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label lblSourceFile;
        private System.Windows.Forms.Button btnLoadLoopDrawingsSourceFile;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnCreateDrawings;
    }
}