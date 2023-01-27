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
            this.btnReadBlocks = new System.Windows.Forms.Button();
            this.btnSelectDrawing = new System.Windows.Forms.Button();
            this.lblDwgPath = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtAttributes = new System.Windows.Forms.TextBox();
            this.txtBlocks = new System.Windows.Forms.TextBox();
            this.lblBlockNames = new System.Windows.Forms.Label();
            this.lblAttributeList = new System.Windows.Forms.Label();
            this.lblSourceFile = new System.Windows.Forms.Label();
            this.btnLoadLoopDrawingsSourceFile = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnCreateDrawings = new System.Windows.Forms.Button();
            this.btnTemplatePath = new System.Windows.Forms.Button();
            this.lblTemplatePath = new System.Windows.Forms.Label();
            this.btnOutputPath = new System.Windows.Forms.Button();
            this.lblOutputPath = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnReadBlocks
            // 
            this.btnReadBlocks.Location = new System.Drawing.Point(14, 137);
            this.btnReadBlocks.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnReadBlocks.Name = "btnReadBlocks";
            this.btnReadBlocks.Size = new System.Drawing.Size(218, 75);
            this.btnReadBlocks.TabIndex = 10;
            this.btnReadBlocks.Text = "Read Blocks";
            this.btnReadBlocks.UseVisualStyleBackColor = true;
            this.btnReadBlocks.Click += new System.EventHandler(this.btnReadBlocks_Click);
            // 
            // btnSelectDrawing
            // 
            this.btnSelectDrawing.Location = new System.Drawing.Point(14, 33);
            this.btnSelectDrawing.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnSelectDrawing.Name = "btnSelectDrawing";
            this.btnSelectDrawing.Size = new System.Drawing.Size(218, 75);
            this.btnSelectDrawing.TabIndex = 14;
            this.btnSelectDrawing.Text = "Select Drawing";
            this.btnSelectDrawing.UseVisualStyleBackColor = true;
            this.btnSelectDrawing.Click += new System.EventHandler(this.btnSelectDrawing_Click);
            // 
            // lblDwgPath
            // 
            this.lblDwgPath.AutoSize = true;
            this.lblDwgPath.Location = new System.Drawing.Point(282, 44);
            this.lblDwgPath.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblDwgPath.Name = "lblDwgPath";
            this.lblDwgPath.Size = new System.Drawing.Size(134, 25);
            this.lblDwgPath.TabIndex = 15;
            this.lblDwgPath.Text = "DrawingPath";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtAttributes);
            this.panel1.Controls.Add(this.txtBlocks);
            this.panel1.Controls.Add(this.lblBlockNames);
            this.panel1.Controls.Add(this.lblDwgPath);
            this.panel1.Controls.Add(this.lblAttributeList);
            this.panel1.Controls.Add(this.btnSelectDrawing);
            this.panel1.Controls.Add(this.btnReadBlocks);
            this.panel1.Location = new System.Drawing.Point(46, 420);
            this.panel1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1632, 754);
            this.panel1.TabIndex = 18;
            // 
            // txtAttributes
            // 
            this.txtAttributes.Location = new System.Drawing.Point(936, 137);
            this.txtAttributes.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtAttributes.Multiline = true;
            this.txtAttributes.Name = "txtAttributes";
            this.txtAttributes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtAttributes.Size = new System.Drawing.Size(632, 598);
            this.txtAttributes.TabIndex = 7;
            // 
            // txtBlocks
            // 
            this.txtBlocks.Location = new System.Drawing.Point(288, 137);
            this.txtBlocks.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.txtBlocks.Multiline = true;
            this.txtBlocks.Name = "txtBlocks";
            this.txtBlocks.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtBlocks.Size = new System.Drawing.Size(632, 598);
            this.txtBlocks.TabIndex = 6;
            // 
            // lblBlockNames
            // 
            this.lblBlockNames.AutoSize = true;
            this.lblBlockNames.Location = new System.Drawing.Point(282, 106);
            this.lblBlockNames.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblBlockNames.Name = "lblBlockNames";
            this.lblBlockNames.Size = new System.Drawing.Size(280, 25);
            this.lblBlockNames.TabIndex = 8;
            this.lblBlockNames.Text = "Block Names in Block Table";
            // 
            // lblAttributeList
            // 
            this.lblAttributeList.AutoSize = true;
            this.lblAttributeList.Location = new System.Drawing.Point(940, 106);
            this.lblAttributeList.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblAttributeList.Name = "lblAttributeList";
            this.lblAttributeList.Size = new System.Drawing.Size(384, 25);
            this.lblAttributeList.TabIndex = 9;
            this.lblAttributeList.Text = "Attribute Tag / Value (?) for Loop Block";
            // 
            // lblSourceFile
            // 
            this.lblSourceFile.AutoSize = true;
            this.lblSourceFile.Location = new System.Drawing.Point(282, 56);
            this.lblSourceFile.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblSourceFile.Name = "lblSourceFile";
            this.lblSourceFile.Size = new System.Drawing.Size(171, 25);
            this.lblSourceFile.TabIndex = 20;
            this.lblSourceFile.Text = "Source File Path";
            // 
            // btnLoadLoopDrawingsSourceFile
            // 
            this.btnLoadLoopDrawingsSourceFile.Location = new System.Drawing.Point(30, 31);
            this.btnLoadLoopDrawingsSourceFile.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnLoadLoopDrawingsSourceFile.Name = "btnLoadLoopDrawingsSourceFile";
            this.btnLoadLoopDrawingsSourceFile.Size = new System.Drawing.Size(218, 75);
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
            this.panel2.Location = new System.Drawing.Point(46, 58);
            this.panel2.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1632, 328);
            this.panel2.TabIndex = 21;
            // 
            // btnCreateDrawings
            // 
            this.btnCreateDrawings.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCreateDrawings.Location = new System.Drawing.Point(30, 138);
            this.btnCreateDrawings.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.btnCreateDrawings.Name = "btnCreateDrawings";
            this.btnCreateDrawings.Size = new System.Drawing.Size(548, 150);
            this.btnCreateDrawings.TabIndex = 23;
            this.btnCreateDrawings.Text = "Create Drawings";
            this.btnCreateDrawings.UseVisualStyleBackColor = true;
            this.btnCreateDrawings.Click += new System.EventHandler(this.btnCreateDrawings_Click);
            // 
            // btnTemplatePath
            // 
            this.btnTemplatePath.Location = new System.Drawing.Point(114, 1202);
            this.btnTemplatePath.Margin = new System.Windows.Forms.Padding(6);
            this.btnTemplatePath.Name = "btnTemplatePath";
            this.btnTemplatePath.Size = new System.Drawing.Size(218, 75);
            this.btnTemplatePath.TabIndex = 25;
            this.btnTemplatePath.Text = "Template Path";
            this.btnTemplatePath.UseVisualStyleBackColor = true;
            // 
            // lblTemplatePath
            // 
            this.lblTemplatePath.AutoSize = true;
            this.lblTemplatePath.Location = new System.Drawing.Point(366, 1227);
            this.lblTemplatePath.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblTemplatePath.Name = "lblTemplatePath";
            this.lblTemplatePath.Size = new System.Drawing.Size(149, 25);
            this.lblTemplatePath.TabIndex = 26;
            this.lblTemplatePath.Text = "Template path";
            // 
            // btnOutputPath
            // 
            this.btnOutputPath.Location = new System.Drawing.Point(114, 1291);
            this.btnOutputPath.Margin = new System.Windows.Forms.Padding(6);
            this.btnOutputPath.Name = "btnOutputPath";
            this.btnOutputPath.Size = new System.Drawing.Size(218, 75);
            this.btnOutputPath.TabIndex = 23;
            this.btnOutputPath.Text = "Output Path";
            this.btnOutputPath.UseVisualStyleBackColor = true;
            // 
            // lblOutputPath
            // 
            this.lblOutputPath.AutoSize = true;
            this.lblOutputPath.Location = new System.Drawing.Point(366, 1316);
            this.lblOutputPath.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblOutputPath.Name = "lblOutputPath";
            this.lblOutputPath.Size = new System.Drawing.Size(124, 25);
            this.lblOutputPath.TabIndex = 24;
            this.lblOutputPath.Text = "Output path";
            // 
            // LoopDrawingUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1735, 1527);
            this.Controls.Add(this.btnTemplatePath);
            this.Controls.Add(this.lblTemplatePath);
            this.Controls.Add(this.btnOutputPath);
            this.Controls.Add(this.lblOutputPath);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.Name = "LoopDrawingUI";
            this.Text = "LoopDrawingUI";
            this.Load += new System.EventHandler(this.LoopDrawingUI_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnReadBlocks;
        private System.Windows.Forms.Button btnSelectDrawing;
        private System.Windows.Forms.Label lblDwgPath;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtAttributes;
        private System.Windows.Forms.TextBox txtBlocks;
        private System.Windows.Forms.Label lblBlockNames;
        private System.Windows.Forms.Label lblAttributeList;
        private System.Windows.Forms.Label lblSourceFile;
        private System.Windows.Forms.Button btnLoadLoopDrawingsSourceFile;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button btnCreateDrawings;
        private System.Windows.Forms.Button btnTemplatePath;
        private System.Windows.Forms.Label lblTemplatePath;
        private System.Windows.Forms.Button btnOutputPath;
        private System.Windows.Forms.Label lblOutputPath;
    }
}