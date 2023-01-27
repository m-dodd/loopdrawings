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
            this.txtDisplayConnection = new System.Windows.Forms.TextBox();
            this.btnReadTagData = new System.Windows.Forms.Button();
            this.btnConfigFile = new System.Windows.Forms.Button();
            this.lblConfigFile = new System.Windows.Forms.Label();
            this.btnReadDataClasses = new System.Windows.Forms.Button();
            this.btnBuildObjects = new System.Windows.Forms.Button();
            this.lblTemplatePath = new System.Windows.Forms.Label();
            this.btnTemplatePath = new System.Windows.Forms.Button();
            this.lblDrawingOutputPath = new System.Windows.Forms.Label();
            this.btnOutputPath = new System.Windows.Forms.Button();
            this.lblSiteID = new System.Windows.Forms.Label();
            this.lblSiteIDHardCoded = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtDisplayConnection
            // 
            this.txtDisplayConnection.Location = new System.Drawing.Point(282, 574);
            this.txtDisplayConnection.Margin = new System.Windows.Forms.Padding(6);
            this.txtDisplayConnection.Multiline = true;
            this.txtDisplayConnection.Name = "txtDisplayConnection";
            this.txtDisplayConnection.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtDisplayConnection.Size = new System.Drawing.Size(1092, 680);
            this.txtDisplayConnection.TabIndex = 6;
            // 
            // btnReadTagData
            // 
            this.btnReadTagData.Location = new System.Drawing.Point(15, 574);
            this.btnReadTagData.Margin = new System.Windows.Forms.Padding(6);
            this.btnReadTagData.Name = "btnReadTagData";
            this.btnReadTagData.Size = new System.Drawing.Size(232, 81);
            this.btnReadTagData.TabIndex = 7;
            this.btnReadTagData.Text = "Read Data\r\nFrom Excel and DB";
            this.btnReadTagData.UseVisualStyleBackColor = true;
            this.btnReadTagData.Click += new System.EventHandler(this.btnReadTagData_Click);
            // 
            // btnConfigFile
            // 
            this.btnConfigFile.Location = new System.Drawing.Point(74, 26);
            this.btnConfigFile.Margin = new System.Windows.Forms.Padding(6);
            this.btnConfigFile.Name = "btnConfigFile";
            this.btnConfigFile.Size = new System.Drawing.Size(179, 66);
            this.btnConfigFile.TabIndex = 8;
            this.btnConfigFile.Text = "Config File";
            this.btnConfigFile.UseVisualStyleBackColor = true;
            this.btnConfigFile.Click += new System.EventHandler(this.btnConfigFile_Click);
            // 
            // lblConfigFile
            // 
            this.lblConfigFile.AutoSize = true;
            this.lblConfigFile.Location = new System.Drawing.Point(279, 30);
            this.lblConfigFile.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblConfigFile.Name = "lblConfigFile";
            this.lblConfigFile.Size = new System.Drawing.Size(148, 32);
            this.lblConfigFile.TabIndex = 9;
            this.lblConfigFile.Text = "lblConfigFile";
            // 
            // btnReadDataClasses
            // 
            this.btnReadDataClasses.Location = new System.Drawing.Point(15, 668);
            this.btnReadDataClasses.Margin = new System.Windows.Forms.Padding(6);
            this.btnReadDataClasses.Name = "btnReadDataClasses";
            this.btnReadDataClasses.Size = new System.Drawing.Size(232, 124);
            this.btnReadDataClasses.TabIndex = 11;
            this.btnReadDataClasses.Text = "Read Data\r\nFrom Excel and DB\r\nNew Classes\r\n";
            this.btnReadDataClasses.UseVisualStyleBackColor = true;
            this.btnReadDataClasses.Click += new System.EventHandler(this.btnReadDataClasses_Click);
            // 
            // btnBuildObjects
            // 
            this.btnBuildObjects.Location = new System.Drawing.Point(21, 360);
            this.btnBuildObjects.Margin = new System.Windows.Forms.Padding(6);
            this.btnBuildObjects.Name = "btnBuildObjects";
            this.btnBuildObjects.Size = new System.Drawing.Size(232, 124);
            this.btnBuildObjects.TabIndex = 12;
            this.btnBuildObjects.Text = "Attempt new classes";
            this.btnBuildObjects.UseVisualStyleBackColor = true;
            this.btnBuildObjects.Click += new System.EventHandler(this.btnBuildObjects_Click);
            // 
            // lblTemplatePath
            // 
            this.lblTemplatePath.AutoSize = true;
            this.lblTemplatePath.Location = new System.Drawing.Point(279, 121);
            this.lblTemplatePath.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTemplatePath.Name = "lblTemplatePath";
            this.lblTemplatePath.Size = new System.Drawing.Size(184, 32);
            this.lblTemplatePath.TabIndex = 14;
            this.lblTemplatePath.Text = "lblTemplatePath";
            // 
            // btnTemplatePath
            // 
            this.btnTemplatePath.Location = new System.Drawing.Point(74, 104);
            this.btnTemplatePath.Margin = new System.Windows.Forms.Padding(6);
            this.btnTemplatePath.Name = "btnTemplatePath";
            this.btnTemplatePath.Size = new System.Drawing.Size(179, 66);
            this.btnTemplatePath.TabIndex = 13;
            this.btnTemplatePath.Text = "Template Path";
            this.btnTemplatePath.UseVisualStyleBackColor = true;
            this.btnTemplatePath.Click += new System.EventHandler(this.btnTemplatePath_Click);
            // 
            // lblDrawingOutputPath
            // 
            this.lblDrawingOutputPath.AutoSize = true;
            this.lblDrawingOutputPath.Location = new System.Drawing.Point(279, 199);
            this.lblDrawingOutputPath.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDrawingOutputPath.Name = "lblDrawingOutputPath";
            this.lblDrawingOutputPath.Size = new System.Drawing.Size(250, 32);
            this.lblDrawingOutputPath.TabIndex = 16;
            this.lblDrawingOutputPath.Text = "lblDrawingOutputPath";
            // 
            // btnOutputPath
            // 
            this.btnOutputPath.Location = new System.Drawing.Point(74, 182);
            this.btnOutputPath.Margin = new System.Windows.Forms.Padding(6);
            this.btnOutputPath.Name = "btnOutputPath";
            this.btnOutputPath.Size = new System.Drawing.Size(179, 79);
            this.btnOutputPath.TabIndex = 15;
            this.btnOutputPath.Text = "Drawing Output Path";
            this.btnOutputPath.UseVisualStyleBackColor = true;
            this.btnOutputPath.Click += new System.EventHandler(this.btnOutputPath_Click);
            // 
            // lblSiteID
            // 
            this.lblSiteID.AutoSize = true;
            this.lblSiteID.Location = new System.Drawing.Point(282, 306);
            this.lblSiteID.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSiteID.Name = "lblSiteID";
            this.lblSiteID.Size = new System.Drawing.Size(40, 32);
            this.lblSiteID.TabIndex = 17;
            this.lblSiteID.Text = "94";
            // 
            // lblSiteIDHardCoded
            // 
            this.lblSiteIDHardCoded.AutoSize = true;
            this.lblSiteIDHardCoded.Location = new System.Drawing.Point(169, 306);
            this.lblSiteIDHardCoded.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSiteIDHardCoded.Name = "lblSiteIDHardCoded";
            this.lblSiteIDHardCoded.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblSiteIDHardCoded.Size = new System.Drawing.Size(84, 32);
            this.lblSiteIDHardCoded.TabIndex = 18;
            this.lblSiteIDHardCoded.Text = "Site ID";
            // 
            // frmLoopUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1486, 1261);
            this.Controls.Add(this.lblSiteIDHardCoded);
            this.Controls.Add(this.lblSiteID);
            this.Controls.Add(this.lblDrawingOutputPath);
            this.Controls.Add(this.btnOutputPath);
            this.Controls.Add(this.lblTemplatePath);
            this.Controls.Add(this.btnTemplatePath);
            this.Controls.Add(this.btnBuildObjects);
            this.Controls.Add(this.btnReadDataClasses);
            this.Controls.Add(this.lblConfigFile);
            this.Controls.Add(this.btnConfigFile);
            this.Controls.Add(this.btnReadTagData);
            this.Controls.Add(this.txtDisplayConnection);
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "frmLoopUI";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.frmLoopUI_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private TextBox txtDisplayConnection;
        private Button btnReadTagData;
        private Button btnConfigFile;
        private Label lblConfigFile;
        private Button btnReadDataClasses;
        private Button btnBuildObjects;
        private Label lblTemplatePath;
        private Button btnTemplatePath;
        private Label lblDrawingOutputPath;
        private Button btnOutputPath;
        private Label lblSiteID;
        private Label lblSiteIDHardCoded;
    }
}