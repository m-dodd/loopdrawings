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
            this.btnBuildObjects = new System.Windows.Forms.Button();
            this.btnTemplatePath = new System.Windows.Forms.Button();
            this.btnOutputPath = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnResultOutputPath = new System.Windows.Forms.Button();
            this.btnExcelFile = new System.Windows.Forms.Button();
            this.btnLoadTestConfig = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblResultOutputPath = new System.Windows.Forms.Label();
            this.lblTemplatePath = new System.Windows.Forms.Label();
            this.lblDrawingOutputPath = new System.Windows.Forms.Label();
            this.lblExcelFile = new System.Windows.Forms.Label();
            this.lblConfigFile = new System.Windows.Forms.Label();
            this.progressDrawingsComplete = new System.Windows.Forms.ProgressBar();
            this.lblLastDrawingComplete = new System.Windows.Forms.Label();
            this.lblStatusInfo = new System.Windows.Forms.Label();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnConfigFile
            // 
            this.btnConfigFile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(50)))), ((int)(((byte)(65)))));
            this.btnConfigFile.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnConfigFile.FlatAppearance.BorderSize = 0;
            this.btnConfigFile.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(80)))), ((int)(((byte)(120)))));
            this.btnConfigFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfigFile.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnConfigFile.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.btnConfigFile.Location = new System.Drawing.Point(0, 23);
            this.btnConfigFile.Name = "btnConfigFile";
            this.btnConfigFile.Size = new System.Drawing.Size(198, 60);
            this.btnConfigFile.TabIndex = 8;
            this.btnConfigFile.Text = "Config File";
            this.btnConfigFile.UseVisualStyleBackColor = false;
            this.btnConfigFile.Click += new System.EventHandler(this.btnConfigFile_Click);
            // 
            // btnBuildObjects
            // 
            this.btnBuildObjects.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(50)))), ((int)(((byte)(65)))));
            this.btnBuildObjects.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnBuildObjects.FlatAppearance.BorderSize = 0;
            this.btnBuildObjects.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(80)))), ((int)(((byte)(120)))));
            this.btnBuildObjects.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBuildObjects.Font = new System.Drawing.Font("Nirmala UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnBuildObjects.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.btnBuildObjects.Location = new System.Drawing.Point(198, 434);
            this.btnBuildObjects.Name = "btnBuildObjects";
            this.btnBuildObjects.Size = new System.Drawing.Size(1397, 60);
            this.btnBuildObjects.TabIndex = 12;
            this.btnBuildObjects.Text = "Generate Drawing Data";
            this.btnBuildObjects.UseVisualStyleBackColor = false;
            this.btnBuildObjects.Click += new System.EventHandler(this.btnBuildObjects_Click);
            // 
            // btnTemplatePath
            // 
            this.btnTemplatePath.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(50)))), ((int)(((byte)(65)))));
            this.btnTemplatePath.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnTemplatePath.FlatAppearance.BorderSize = 0;
            this.btnTemplatePath.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(80)))), ((int)(((byte)(120)))));
            this.btnTemplatePath.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTemplatePath.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnTemplatePath.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.btnTemplatePath.Location = new System.Drawing.Point(0, 83);
            this.btnTemplatePath.Name = "btnTemplatePath";
            this.btnTemplatePath.Size = new System.Drawing.Size(198, 60);
            this.btnTemplatePath.TabIndex = 13;
            this.btnTemplatePath.Text = "Template Path";
            this.btnTemplatePath.UseVisualStyleBackColor = false;
            this.btnTemplatePath.Click += new System.EventHandler(this.btnTemplatePath_Click);
            // 
            // btnOutputPath
            // 
            this.btnOutputPath.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(50)))), ((int)(((byte)(65)))));
            this.btnOutputPath.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnOutputPath.FlatAppearance.BorderSize = 0;
            this.btnOutputPath.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(80)))), ((int)(((byte)(120)))));
            this.btnOutputPath.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOutputPath.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnOutputPath.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.btnOutputPath.Location = new System.Drawing.Point(0, 263);
            this.btnOutputPath.Name = "btnOutputPath";
            this.btnOutputPath.Size = new System.Drawing.Size(198, 60);
            this.btnOutputPath.TabIndex = 15;
            this.btnOutputPath.Text = "Drawing Output Path";
            this.btnOutputPath.UseVisualStyleBackColor = false;
            this.btnOutputPath.Click += new System.EventHandler(this.btnOutputPath_Click);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(30)))), ((int)(((byte)(54)))));
            this.panel2.Controls.Add(this.btnOutputPath);
            this.panel2.Controls.Add(this.btnResultOutputPath);
            this.panel2.Controls.Add(this.btnExcelFile);
            this.panel2.Controls.Add(this.btnTemplatePath);
            this.panel2.Controls.Add(this.btnConfigFile);
            this.panel2.Controls.Add(this.btnLoadTestConfig);
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Font = new System.Drawing.Font("Nirmala UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(198, 494);
            this.panel2.TabIndex = 20;
            // 
            // btnResultOutputPath
            // 
            this.btnResultOutputPath.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(50)))), ((int)(((byte)(65)))));
            this.btnResultOutputPath.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnResultOutputPath.FlatAppearance.BorderSize = 0;
            this.btnResultOutputPath.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(80)))), ((int)(((byte)(120)))));
            this.btnResultOutputPath.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnResultOutputPath.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnResultOutputPath.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.btnResultOutputPath.Location = new System.Drawing.Point(0, 203);
            this.btnResultOutputPath.Name = "btnResultOutputPath";
            this.btnResultOutputPath.Size = new System.Drawing.Size(198, 60);
            this.btnResultOutputPath.TabIndex = 19;
            this.btnResultOutputPath.Text = "Result Output Path";
            this.btnResultOutputPath.UseVisualStyleBackColor = false;
            this.btnResultOutputPath.Click += new System.EventHandler(this.btnResultOutputPath_Click);
            // 
            // btnExcelFile
            // 
            this.btnExcelFile.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(50)))), ((int)(((byte)(65)))));
            this.btnExcelFile.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnExcelFile.FlatAppearance.BorderSize = 0;
            this.btnExcelFile.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(80)))), ((int)(((byte)(120)))));
            this.btnExcelFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExcelFile.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnExcelFile.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.btnExcelFile.Location = new System.Drawing.Point(0, 143);
            this.btnExcelFile.Name = "btnExcelFile";
            this.btnExcelFile.Size = new System.Drawing.Size(198, 60);
            this.btnExcelFile.TabIndex = 21;
            this.btnExcelFile.Text = "Excel File";
            this.btnExcelFile.UseVisualStyleBackColor = false;
            this.btnExcelFile.Click += new System.EventHandler(this.btnExcelFile_Click);
            // 
            // btnLoadTestConfig
            // 
            this.btnLoadTestConfig.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.btnLoadTestConfig.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnLoadTestConfig.FlatAppearance.BorderSize = 0;
            this.btnLoadTestConfig.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLoadTestConfig.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.btnLoadTestConfig.Location = new System.Drawing.Point(0, 434);
            this.btnLoadTestConfig.Name = "btnLoadTestConfig";
            this.btnLoadTestConfig.Size = new System.Drawing.Size(198, 60);
            this.btnLoadTestConfig.TabIndex = 22;
            this.btnLoadTestConfig.Text = "Load Test Config";
            this.btnLoadTestConfig.UseVisualStyleBackColor = false;
            this.btnLoadTestConfig.Click += new System.EventHandler(this.btnLoadTestConfig_Click);
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(198, 23);
            this.panel1.TabIndex = 24;
            // 
            // lblResultOutputPath
            // 
            this.lblResultOutputPath.AutoSize = true;
            this.lblResultOutputPath.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblResultOutputPath.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(161)))), ((int)(((byte)(170)))));
            this.lblResultOutputPath.Location = new System.Drawing.Point(222, 219);
            this.lblResultOutputPath.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblResultOutputPath.Name = "lblResultOutputPath";
            this.lblResultOutputPath.Size = new System.Drawing.Size(149, 21);
            this.lblResultOutputPath.TabIndex = 20;
            this.lblResultOutputPath.Text = "lblResultOutputPath";
            // 
            // lblTemplatePath
            // 
            this.lblTemplatePath.AutoSize = true;
            this.lblTemplatePath.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblTemplatePath.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(161)))), ((int)(((byte)(170)))));
            this.lblTemplatePath.Location = new System.Drawing.Point(222, 99);
            this.lblTemplatePath.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTemplatePath.Name = "lblTemplatePath";
            this.lblTemplatePath.Size = new System.Drawing.Size(119, 21);
            this.lblTemplatePath.TabIndex = 14;
            this.lblTemplatePath.Text = "lblTemplatePath";
            // 
            // lblDrawingOutputPath
            // 
            this.lblDrawingOutputPath.AutoSize = true;
            this.lblDrawingOutputPath.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblDrawingOutputPath.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(161)))), ((int)(((byte)(170)))));
            this.lblDrawingOutputPath.Location = new System.Drawing.Point(222, 282);
            this.lblDrawingOutputPath.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblDrawingOutputPath.Name = "lblDrawingOutputPath";
            this.lblDrawingOutputPath.Size = new System.Drawing.Size(165, 21);
            this.lblDrawingOutputPath.TabIndex = 16;
            this.lblDrawingOutputPath.Text = "lblDrawingOutputPath";
            // 
            // lblExcelFile
            // 
            this.lblExcelFile.AutoSize = true;
            this.lblExcelFile.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblExcelFile.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(161)))), ((int)(((byte)(170)))));
            this.lblExcelFile.Location = new System.Drawing.Point(222, 162);
            this.lblExcelFile.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblExcelFile.Name = "lblExcelFile";
            this.lblExcelFile.Size = new System.Drawing.Size(85, 21);
            this.lblExcelFile.TabIndex = 22;
            this.lblExcelFile.Text = "lblExcelFile";
            // 
            // lblConfigFile
            // 
            this.lblConfigFile.AutoSize = true;
            this.lblConfigFile.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblConfigFile.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(161)))), ((int)(((byte)(170)))));
            this.lblConfigFile.Location = new System.Drawing.Point(222, 39);
            this.lblConfigFile.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblConfigFile.Name = "lblConfigFile";
            this.lblConfigFile.Size = new System.Drawing.Size(97, 21);
            this.lblConfigFile.TabIndex = 9;
            this.lblConfigFile.Text = "lblConfigFile";
            // 
            // progressDrawingsComplete
            // 
            this.progressDrawingsComplete.Location = new System.Drawing.Point(222, 338);
            this.progressDrawingsComplete.Name = "progressDrawingsComplete";
            this.progressDrawingsComplete.Size = new System.Drawing.Size(1174, 44);
            this.progressDrawingsComplete.TabIndex = 23;
            this.progressDrawingsComplete.Visible = false;
            // 
            // lblLastDrawingComplete
            // 
            this.lblLastDrawingComplete.AutoSize = true;
            this.lblLastDrawingComplete.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblLastDrawingComplete.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(161)))), ((int)(((byte)(170)))));
            this.lblLastDrawingComplete.Location = new System.Drawing.Point(1403, 352);
            this.lblLastDrawingComplete.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblLastDrawingComplete.Name = "lblLastDrawingComplete";
            this.lblLastDrawingComplete.Size = new System.Drawing.Size(181, 21);
            this.lblLastDrawingComplete.TabIndex = 24;
            this.lblLastDrawingComplete.Text = "lblLastDrawingComplete";
            // 
            // lblStatusInfo
            // 
            this.lblStatusInfo.AutoSize = true;
            this.lblStatusInfo.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblStatusInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(161)))), ((int)(((byte)(170)))));
            this.lblStatusInfo.Location = new System.Drawing.Point(222, 397);
            this.lblStatusInfo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblStatusInfo.Name = "lblStatusInfo";
            this.lblStatusInfo.Size = new System.Drawing.Size(96, 21);
            this.lblStatusInfo.TabIndex = 25;
            this.lblStatusInfo.Text = "lblStatusInfo";
            // 
            // frmLoopUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(42)))), ((int)(((byte)(62)))));
            this.ClientSize = new System.Drawing.Size(1595, 494);
            this.Controls.Add(this.lblStatusInfo);
            this.Controls.Add(this.lblLastDrawingComplete);
            this.Controls.Add(this.progressDrawingsComplete);
            this.Controls.Add(this.lblConfigFile);
            this.Controls.Add(this.lblExcelFile);
            this.Controls.Add(this.btnBuildObjects);
            this.Controls.Add(this.lblDrawingOutputPath);
            this.Controls.Add(this.lblTemplatePath);
            this.Controls.Add(this.lblResultOutputPath);
            this.Controls.Add(this.panel2);
            this.Name = "frmLoopUI";
            this.Text = "Duco Design Loop Drawing Generator";
            this.Load += new System.EventHandler(this.frmLoopUI_Load);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Button btnConfigFile;
        private Button btnBuildObjects;
        private Button btnTemplatePath;
        private Button btnOutputPath;
        private Panel panel2;
        private Button btnResultOutputPath;
        private Button btnExcelFile;
        private Button btnLoadTestConfig;
        private Panel panel1;
        private Label lblResultOutputPath;
        private Label lblTemplatePath;
        private Label lblDrawingOutputPath;
        private Label lblExcelFile;
        private Label lblConfigFile;
        private ProgressBar progressDrawingsComplete;
        private Label lblLastDrawingComplete;
        private Label lblStatusInfo;
    }
}