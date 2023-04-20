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
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblLogFilePath = new System.Windows.Forms.Label();
            this.btnLogPath = new System.Windows.Forms.Button();
            this.lblExcelFile = new System.Windows.Forms.Label();
            this.btnExcelFile = new System.Windows.Forms.Button();
            this.lblResultOutputPath = new System.Windows.Forms.Label();
            this.btnResultOutputPath = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnLoadTestConfig = new System.Windows.Forms.Button();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnConfigFile
            // 
            this.btnConfigFile.Location = new System.Drawing.Point(7, 26);
            this.btnConfigFile.Margin = new System.Windows.Forms.Padding(6);
            this.btnConfigFile.Name = "btnConfigFile";
            this.btnConfigFile.Size = new System.Drawing.Size(266, 66);
            this.btnConfigFile.TabIndex = 8;
            this.btnConfigFile.Text = "Config File";
            this.btnConfigFile.UseVisualStyleBackColor = true;
            this.btnConfigFile.Click += new System.EventHandler(this.btnConfigFile_Click);
            // 
            // lblConfigFile
            // 
            this.lblConfigFile.AutoSize = true;
            this.lblConfigFile.Location = new System.Drawing.Point(292, 43);
            this.lblConfigFile.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblConfigFile.Name = "lblConfigFile";
            this.lblConfigFile.Size = new System.Drawing.Size(148, 32);
            this.lblConfigFile.TabIndex = 9;
            this.lblConfigFile.Text = "lblConfigFile";
            // 
            // btnBuildObjects
            // 
            this.btnBuildObjects.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnBuildObjects.Location = new System.Drawing.Point(9, 30);
            this.btnBuildObjects.Margin = new System.Windows.Forms.Padding(6);
            this.btnBuildObjects.Name = "btnBuildObjects";
            this.btnBuildObjects.Size = new System.Drawing.Size(1372, 124);
            this.btnBuildObjects.TabIndex = 12;
            this.btnBuildObjects.Text = "Generate Drawing Data";
            this.btnBuildObjects.UseVisualStyleBackColor = true;
            this.btnBuildObjects.Click += new System.EventHandler(this.btnBuildObjects_Click);
            // 
            // lblTemplatePath
            // 
            this.lblTemplatePath.AutoSize = true;
            this.lblTemplatePath.Location = new System.Drawing.Point(292, 201);
            this.lblTemplatePath.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTemplatePath.Name = "lblTemplatePath";
            this.lblTemplatePath.Size = new System.Drawing.Size(184, 32);
            this.lblTemplatePath.TabIndex = 14;
            this.lblTemplatePath.Text = "lblTemplatePath";
            // 
            // btnTemplatePath
            // 
            this.btnTemplatePath.Location = new System.Drawing.Point(7, 183);
            this.btnTemplatePath.Margin = new System.Windows.Forms.Padding(6);
            this.btnTemplatePath.Name = "btnTemplatePath";
            this.btnTemplatePath.Size = new System.Drawing.Size(266, 66);
            this.btnTemplatePath.TabIndex = 13;
            this.btnTemplatePath.Text = "Template Path";
            this.btnTemplatePath.UseVisualStyleBackColor = true;
            this.btnTemplatePath.Click += new System.EventHandler(this.btnTemplatePath_Click);
            // 
            // lblDrawingOutputPath
            // 
            this.lblDrawingOutputPath.AutoSize = true;
            this.lblDrawingOutputPath.Location = new System.Drawing.Point(292, 292);
            this.lblDrawingOutputPath.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDrawingOutputPath.Name = "lblDrawingOutputPath";
            this.lblDrawingOutputPath.Size = new System.Drawing.Size(250, 32);
            this.lblDrawingOutputPath.TabIndex = 16;
            this.lblDrawingOutputPath.Text = "lblDrawingOutputPath";
            // 
            // btnOutputPath
            // 
            this.btnOutputPath.Location = new System.Drawing.Point(7, 262);
            this.btnOutputPath.Margin = new System.Windows.Forms.Padding(6);
            this.btnOutputPath.Name = "btnOutputPath";
            this.btnOutputPath.Size = new System.Drawing.Size(266, 92);
            this.btnOutputPath.TabIndex = 15;
            this.btnOutputPath.Text = "Drawing Output Path";
            this.btnOutputPath.UseVisualStyleBackColor = true;
            this.btnOutputPath.Click += new System.EventHandler(this.btnOutputPath_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lblLogFilePath);
            this.panel2.Controls.Add(this.btnLogPath);
            this.panel2.Controls.Add(this.lblExcelFile);
            this.panel2.Controls.Add(this.btnExcelFile);
            this.panel2.Controls.Add(this.lblResultOutputPath);
            this.panel2.Controls.Add(this.btnResultOutputPath);
            this.panel2.Controls.Add(this.lblDrawingOutputPath);
            this.panel2.Controls.Add(this.btnOutputPath);
            this.panel2.Controls.Add(this.lblTemplatePath);
            this.panel2.Controls.Add(this.btnTemplatePath);
            this.panel2.Controls.Add(this.lblConfigFile);
            this.panel2.Controls.Add(this.btnConfigFile);
            this.panel2.Location = new System.Drawing.Point(4, 26);
            this.panel2.Margin = new System.Windows.Forms.Padding(6);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1402, 624);
            this.panel2.TabIndex = 20;
            // 
            // lblLogFilePath
            // 
            this.lblLogFilePath.AutoSize = true;
            this.lblLogFilePath.Location = new System.Drawing.Point(291, 501);
            this.lblLogFilePath.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLogFilePath.Name = "lblLogFilePath";
            this.lblLogFilePath.Size = new System.Drawing.Size(162, 32);
            this.lblLogFilePath.TabIndex = 24;
            this.lblLogFilePath.Text = "lblLogFilePath";
            // 
            // btnLogPath
            // 
            this.btnLogPath.Location = new System.Drawing.Point(7, 471);
            this.btnLogPath.Margin = new System.Windows.Forms.Padding(6);
            this.btnLogPath.Name = "btnLogPath";
            this.btnLogPath.Size = new System.Drawing.Size(266, 92);
            this.btnLogPath.TabIndex = 23;
            this.btnLogPath.Text = "Logs Path";
            this.btnLogPath.UseVisualStyleBackColor = true;
            this.btnLogPath.Click += new System.EventHandler(this.btnLogPath_Click);
            // 
            // lblExcelFile
            // 
            this.lblExcelFile.AutoSize = true;
            this.lblExcelFile.Location = new System.Drawing.Point(292, 122);
            this.lblExcelFile.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblExcelFile.Name = "lblExcelFile";
            this.lblExcelFile.Size = new System.Drawing.Size(130, 32);
            this.lblExcelFile.TabIndex = 22;
            this.lblExcelFile.Text = "lblExcelFile";
            // 
            // btnExcelFile
            // 
            this.btnExcelFile.Location = new System.Drawing.Point(6, 105);
            this.btnExcelFile.Margin = new System.Windows.Forms.Padding(6);
            this.btnExcelFile.Name = "btnExcelFile";
            this.btnExcelFile.Size = new System.Drawing.Size(266, 66);
            this.btnExcelFile.TabIndex = 21;
            this.btnExcelFile.Text = "Excel File";
            this.btnExcelFile.UseVisualStyleBackColor = true;
            this.btnExcelFile.Click += new System.EventHandler(this.btnExcelFile_Click);
            // 
            // lblResultOutputPath
            // 
            this.lblResultOutputPath.AutoSize = true;
            this.lblResultOutputPath.Location = new System.Drawing.Point(290, 397);
            this.lblResultOutputPath.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblResultOutputPath.Name = "lblResultOutputPath";
            this.lblResultOutputPath.Size = new System.Drawing.Size(226, 32);
            this.lblResultOutputPath.TabIndex = 20;
            this.lblResultOutputPath.Text = "lblResultOutputPath";
            // 
            // btnResultOutputPath
            // 
            this.btnResultOutputPath.Location = new System.Drawing.Point(6, 367);
            this.btnResultOutputPath.Margin = new System.Windows.Forms.Padding(6);
            this.btnResultOutputPath.Name = "btnResultOutputPath";
            this.btnResultOutputPath.Size = new System.Drawing.Size(266, 92);
            this.btnResultOutputPath.TabIndex = 19;
            this.btnResultOutputPath.Text = "Result Output Path";
            this.btnResultOutputPath.UseVisualStyleBackColor = true;
            this.btnResultOutputPath.Click += new System.EventHandler(this.btnResultOutputPath_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnBuildObjects);
            this.panel3.Location = new System.Drawing.Point(4, 935);
            this.panel3.Margin = new System.Windows.Forms.Padding(6);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1402, 161);
            this.panel3.TabIndex = 21;
            // 
            // btnLoadTestConfig
            // 
            this.btnLoadTestConfig.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnLoadTestConfig.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnLoadTestConfig.Location = new System.Drawing.Point(13, 1135);
            this.btnLoadTestConfig.Margin = new System.Windows.Forms.Padding(6);
            this.btnLoadTestConfig.Name = "btnLoadTestConfig";
            this.btnLoadTestConfig.Size = new System.Drawing.Size(449, 93);
            this.btnLoadTestConfig.TabIndex = 22;
            this.btnLoadTestConfig.Text = "Load Test Config";
            this.btnLoadTestConfig.UseVisualStyleBackColor = false;
            this.btnLoadTestConfig.Click += new System.EventHandler(this.btnLoadTestConfig_Click);
            // 
            // frmLoopUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1486, 1243);
            this.Controls.Add(this.btnLoadTestConfig);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Margin = new System.Windows.Forms.Padding(6);
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
        private Panel panel2;
        private Label lblResultOutputPath;
        private Button btnResultOutputPath;
        private Label lblExcelFile;
        private Button btnExcelFile;
        private Panel panel3;
        private Button btnLoadTestConfig;
        private Label lblLogFilePath;
        private Button btnLogPath;
    }
}