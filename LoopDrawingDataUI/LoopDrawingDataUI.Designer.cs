namespace LoopDrawingDataUI
{
    partial class frmLoopUI
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnConfigFile = new System.Windows.Forms.Button();
            this.lblConfigFile = new System.Windows.Forms.Label();
            this.btnBuildObjects = new System.Windows.Forms.Button();
            this.lblTemplatePath = new System.Windows.Forms.Label();
            this.btnTemplatePath = new System.Windows.Forms.Button();
            this.lblDrawingOutputPath = new System.Windows.Forms.Label();
            this.btnOutputPath = new System.Windows.Forms.Button();
            this.lblSiteID = new System.Windows.Forms.Label();
            this.lblSiteIDHardCoded = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblExcelFile = new System.Windows.Forms.Label();
            this.btnExcelFile = new System.Windows.Forms.Button();
            this.lblResultOutputPath = new System.Windows.Forms.Label();
            this.btnResultOutputPath = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnConfigFile
            // 
            this.btnConfigFile.Location = new System.Drawing.Point(4, 12);
            this.btnConfigFile.Name = "btnConfigFile";
            this.btnConfigFile.Size = new System.Drawing.Size(125, 31);
            this.btnConfigFile.TabIndex = 8;
            this.btnConfigFile.Text = "Config File";
            this.btnConfigFile.UseVisualStyleBackColor = true;
            this.btnConfigFile.Click += new System.EventHandler(this.btnConfigFile_Click);
            // 
            // lblConfigFile
            // 
            this.lblConfigFile.AutoSize = true;
            this.lblConfigFile.Location = new System.Drawing.Point(143, 20);
            this.lblConfigFile.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblConfigFile.Name = "lblConfigFile";
            this.lblConfigFile.Size = new System.Drawing.Size(74, 15);
            this.lblConfigFile.TabIndex = 9;
            this.lblConfigFile.Text = "lblConfigFile";
            // 
            // btnBuildObjects
            // 
            this.btnBuildObjects.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnBuildObjects.Location = new System.Drawing.Point(5, 14);
            this.btnBuildObjects.Name = "btnBuildObjects";
            this.btnBuildObjects.Size = new System.Drawing.Size(739, 58);
            this.btnBuildObjects.TabIndex = 12;
            this.btnBuildObjects.Text = "Generate Drawing Data";
            this.btnBuildObjects.UseVisualStyleBackColor = true;
            this.btnBuildObjects.Click += new System.EventHandler(this.btnBuildObjects_Click);
            // 
            // lblTemplatePath
            // 
            this.lblTemplatePath.AutoSize = true;
            this.lblTemplatePath.Location = new System.Drawing.Point(143, 94);
            this.lblTemplatePath.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTemplatePath.Name = "lblTemplatePath";
            this.lblTemplatePath.Size = new System.Drawing.Size(92, 15);
            this.lblTemplatePath.TabIndex = 14;
            this.lblTemplatePath.Text = "lblTemplatePath";
            // 
            // btnTemplatePath
            // 
            this.btnTemplatePath.Location = new System.Drawing.Point(4, 86);
            this.btnTemplatePath.Name = "btnTemplatePath";
            this.btnTemplatePath.Size = new System.Drawing.Size(125, 31);
            this.btnTemplatePath.TabIndex = 13;
            this.btnTemplatePath.Text = "Template Path";
            this.btnTemplatePath.UseVisualStyleBackColor = true;
            this.btnTemplatePath.Click += new System.EventHandler(this.btnTemplatePath_Click);
            // 
            // lblDrawingOutputPath
            // 
            this.lblDrawingOutputPath.AutoSize = true;
            this.lblDrawingOutputPath.Location = new System.Drawing.Point(143, 137);
            this.lblDrawingOutputPath.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDrawingOutputPath.Name = "lblDrawingOutputPath";
            this.lblDrawingOutputPath.Size = new System.Drawing.Size(126, 15);
            this.lblDrawingOutputPath.TabIndex = 16;
            this.lblDrawingOutputPath.Text = "lblDrawingOutputPath";
            // 
            // btnOutputPath
            // 
            this.btnOutputPath.Location = new System.Drawing.Point(4, 123);
            this.btnOutputPath.Name = "btnOutputPath";
            this.btnOutputPath.Size = new System.Drawing.Size(125, 43);
            this.btnOutputPath.TabIndex = 15;
            this.btnOutputPath.Text = "Drawing Output Path";
            this.btnOutputPath.UseVisualStyleBackColor = true;
            this.btnOutputPath.Click += new System.EventHandler(this.btnOutputPath_Click);
            // 
            // lblSiteID
            // 
            this.lblSiteID.AutoSize = true;
            this.lblSiteID.Location = new System.Drawing.Point(143, 222);
            this.lblSiteID.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSiteID.Name = "lblSiteID";
            this.lblSiteID.Size = new System.Drawing.Size(19, 15);
            this.lblSiteID.TabIndex = 17;
            this.lblSiteID.Text = "94";
            // 
            // lblSiteIDHardCoded
            // 
            this.lblSiteIDHardCoded.AutoSize = true;
            this.lblSiteIDHardCoded.Location = new System.Drawing.Point(82, 222);
            this.lblSiteIDHardCoded.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSiteIDHardCoded.Name = "lblSiteIDHardCoded";
            this.lblSiteIDHardCoded.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblSiteIDHardCoded.Size = new System.Drawing.Size(40, 15);
            this.lblSiteIDHardCoded.TabIndex = 18;
            this.lblSiteIDHardCoded.Text = "Site ID";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lblExcelFile);
            this.panel2.Controls.Add(this.btnExcelFile);
            this.panel2.Controls.Add(this.lblResultOutputPath);
            this.panel2.Controls.Add(this.btnResultOutputPath);
            this.panel2.Controls.Add(this.lblSiteIDHardCoded);
            this.panel2.Controls.Add(this.lblSiteID);
            this.panel2.Controls.Add(this.lblDrawingOutputPath);
            this.panel2.Controls.Add(this.btnOutputPath);
            this.panel2.Controls.Add(this.lblTemplatePath);
            this.panel2.Controls.Add(this.btnTemplatePath);
            this.panel2.Controls.Add(this.lblConfigFile);
            this.panel2.Controls.Add(this.btnConfigFile);
            this.panel2.Location = new System.Drawing.Point(2, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(755, 249);
            this.panel2.TabIndex = 20;
            // 
            // lblExcelFile
            // 
            this.lblExcelFile.AutoSize = true;
            this.lblExcelFile.Location = new System.Drawing.Point(143, 57);
            this.lblExcelFile.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblExcelFile.Name = "lblExcelFile";
            this.lblExcelFile.Size = new System.Drawing.Size(65, 15);
            this.lblExcelFile.TabIndex = 22;
            this.lblExcelFile.Text = "lblExcelFile";
            // 
            // btnExcelFile
            // 
            this.btnExcelFile.Location = new System.Drawing.Point(3, 49);
            this.btnExcelFile.Name = "btnExcelFile";
            this.btnExcelFile.Size = new System.Drawing.Size(125, 31);
            this.btnExcelFile.TabIndex = 21;
            this.btnExcelFile.Text = "Excel File";
            this.btnExcelFile.UseVisualStyleBackColor = true;
            this.btnExcelFile.Click += new System.EventHandler(this.btnExcelFile_Click);
            // 
            // lblResultOutputPath
            // 
            this.lblResultOutputPath.AutoSize = true;
            this.lblResultOutputPath.Location = new System.Drawing.Point(142, 186);
            this.lblResultOutputPath.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblResultOutputPath.Name = "lblResultOutputPath";
            this.lblResultOutputPath.Size = new System.Drawing.Size(114, 15);
            this.lblResultOutputPath.TabIndex = 20;
            this.lblResultOutputPath.Text = "lblResultOutputPath";
            // 
            // btnResultOutputPath
            // 
            this.btnResultOutputPath.Location = new System.Drawing.Point(3, 172);
            this.btnResultOutputPath.Name = "btnResultOutputPath";
            this.btnResultOutputPath.Size = new System.Drawing.Size(125, 43);
            this.btnResultOutputPath.TabIndex = 19;
            this.btnResultOutputPath.Text = "Result Output Path";
            this.btnResultOutputPath.UseVisualStyleBackColor = true;
            this.btnResultOutputPath.Click += new System.EventHandler(this.btnResultOutputPath_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnBuildObjects);
            this.panel3.Location = new System.Drawing.Point(2, 267);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(755, 75);
            this.panel3.TabIndex = 21;
            // 
            // frmLoopUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 356);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Name = "frmLoopUI";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.frmLoopUI_Load);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private Button btnConfigFile;
        private Label lblConfigFile;
        private Button btnBuildObjects;
        private Label lblTemplatePath;
        private Button btnTemplatePath;
        private Label lblDrawingOutputPath;
        private Button btnOutputPath;
        private Label lblSiteID;
        private Label lblSiteIDHardCoded;
        private Panel panel2;
        private Label lblResultOutputPath;
        private Button btnResultOutputPath;
        private Label lblExcelFile;
        private Button btnExcelFile;
        private Panel panel3;
    }
}