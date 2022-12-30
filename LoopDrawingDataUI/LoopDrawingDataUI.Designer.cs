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
            this.btnReadExcel = new System.Windows.Forms.Button();
            this.btnGetData = new System.Windows.Forms.Button();
            this.txtDisplayConnection = new System.Windows.Forms.TextBox();
            this.btnReadTagData = new System.Windows.Forms.Button();
            this.btnConfigFile = new System.Windows.Forms.Button();
            this.lblConfigFile = new System.Windows.Forms.Label();
            this.btnReadConfig = new System.Windows.Forms.Button();
            this.btnReadDataClasses = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnReadExcel
            // 
            this.btnReadExcel.Location = new System.Drawing.Point(7, 352);
            this.btnReadExcel.Margin = new System.Windows.Forms.Padding(6);
            this.btnReadExcel.Name = "btnReadExcel";
            this.btnReadExcel.Size = new System.Drawing.Size(232, 81);
            this.btnReadExcel.TabIndex = 4;
            this.btnReadExcel.Text = "Read Excel";
            this.btnReadExcel.UseVisualStyleBackColor = true;
            this.btnReadExcel.Click += new System.EventHandler(this.btnReadExcel_Click);
            // 
            // btnGetData
            // 
            this.btnGetData.Location = new System.Drawing.Point(7, 258);
            this.btnGetData.Margin = new System.Windows.Forms.Padding(6);
            this.btnGetData.Name = "btnGetData";
            this.btnGetData.Size = new System.Drawing.Size(232, 81);
            this.btnGetData.TabIndex = 5;
            this.btnGetData.Text = "GetSomeData";
            this.btnGetData.UseVisualStyleBackColor = true;
            this.btnGetData.Click += new System.EventHandler(this.btnGetData_Click);
            // 
            // txtDisplayConnection
            // 
            this.txtDisplayConnection.Location = new System.Drawing.Point(283, 258);
            this.txtDisplayConnection.Margin = new System.Windows.Forms.Padding(6);
            this.txtDisplayConnection.Multiline = true;
            this.txtDisplayConnection.Name = "txtDisplayConnection";
            this.txtDisplayConnection.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtDisplayConnection.Size = new System.Drawing.Size(1092, 996);
            this.txtDisplayConnection.TabIndex = 6;
            // 
            // btnReadTagData
            // 
            this.btnReadTagData.Location = new System.Drawing.Point(7, 446);
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
            this.btnConfigFile.Size = new System.Drawing.Size(165, 66);
            this.btnConfigFile.TabIndex = 8;
            this.btnConfigFile.Text = "Config File";
            this.btnConfigFile.UseVisualStyleBackColor = true;
            this.btnConfigFile.Click += new System.EventHandler(this.btnConfigFile_Click);
            // 
            // lblConfigFile
            // 
            this.lblConfigFile.AutoSize = true;
            this.lblConfigFile.Location = new System.Drawing.Point(279, 29);
            this.lblConfigFile.Name = "lblConfigFile";
            this.lblConfigFile.Size = new System.Drawing.Size(148, 32);
            this.lblConfigFile.TabIndex = 9;
            this.lblConfigFile.Text = "lblConfigFile";
            // 
            // btnReadConfig
            // 
            this.btnReadConfig.Location = new System.Drawing.Point(74, 104);
            this.btnReadConfig.Margin = new System.Windows.Forms.Padding(6);
            this.btnReadConfig.Name = "btnReadConfig";
            this.btnReadConfig.Size = new System.Drawing.Size(165, 66);
            this.btnReadConfig.TabIndex = 10;
            this.btnReadConfig.Text = "Read Config";
            this.btnReadConfig.UseVisualStyleBackColor = true;
            this.btnReadConfig.Click += new System.EventHandler(this.btnReadConfig_Click);
            // 
            // btnReadDataClasses
            // 
            this.btnReadDataClasses.Location = new System.Drawing.Point(7, 539);
            this.btnReadDataClasses.Margin = new System.Windows.Forms.Padding(6);
            this.btnReadDataClasses.Name = "btnReadDataClasses";
            this.btnReadDataClasses.Size = new System.Drawing.Size(232, 109);
            this.btnReadDataClasses.TabIndex = 11;
            this.btnReadDataClasses.Text = "Read Data\r\nFrom Excel and DB\r\nNew Classes\r\n";
            this.btnReadDataClasses.UseVisualStyleBackColor = true;
            this.btnReadDataClasses.Click += new System.EventHandler(this.btnReadDataClasses_Click);
            // 
            // frmLoopUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(13F, 32F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1486, 1372);
            this.Controls.Add(this.btnReadDataClasses);
            this.Controls.Add(this.btnReadConfig);
            this.Controls.Add(this.lblConfigFile);
            this.Controls.Add(this.btnConfigFile);
            this.Controls.Add(this.btnReadTagData);
            this.Controls.Add(this.txtDisplayConnection);
            this.Controls.Add(this.btnGetData);
            this.Controls.Add(this.btnReadExcel);
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "frmLoopUI";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private Button btnReadExcel;
        private Button btnGetData;
        private TextBox txtDisplayConnection;
        private Button btnReadTagData;
        private Button btnConfigFile;
        private Label lblConfigFile;
        private Button btnReadConfig;
        private Button btnReadDataClasses;
    }
}