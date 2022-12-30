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
            this.btnReadConfig = new System.Windows.Forms.Button();
            this.btnReadDataClasses = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtDisplayConnection
            // 
            this.txtDisplayConnection.Location = new System.Drawing.Point(152, 121);
            this.txtDisplayConnection.Multiline = true;
            this.txtDisplayConnection.Name = "txtDisplayConnection";
            this.txtDisplayConnection.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtDisplayConnection.Size = new System.Drawing.Size(590, 469);
            this.txtDisplayConnection.TabIndex = 6;
            // 
            // btnReadTagData
            // 
            this.btnReadTagData.Location = new System.Drawing.Point(4, 209);
            this.btnReadTagData.Name = "btnReadTagData";
            this.btnReadTagData.Size = new System.Drawing.Size(125, 38);
            this.btnReadTagData.TabIndex = 7;
            this.btnReadTagData.Text = "Read Data\r\nFrom Excel and DB";
            this.btnReadTagData.UseVisualStyleBackColor = true;
            this.btnReadTagData.Click += new System.EventHandler(this.btnReadTagData_Click);
            // 
            // btnConfigFile
            // 
            this.btnConfigFile.Location = new System.Drawing.Point(40, 12);
            this.btnConfigFile.Name = "btnConfigFile";
            this.btnConfigFile.Size = new System.Drawing.Size(89, 31);
            this.btnConfigFile.TabIndex = 8;
            this.btnConfigFile.Text = "Config File";
            this.btnConfigFile.UseVisualStyleBackColor = true;
            this.btnConfigFile.Click += new System.EventHandler(this.btnConfigFile_Click);
            // 
            // lblConfigFile
            // 
            this.lblConfigFile.AutoSize = true;
            this.lblConfigFile.Location = new System.Drawing.Point(150, 14);
            this.lblConfigFile.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblConfigFile.Name = "lblConfigFile";
            this.lblConfigFile.Size = new System.Drawing.Size(74, 15);
            this.lblConfigFile.TabIndex = 9;
            this.lblConfigFile.Text = "lblConfigFile";
            // 
            // btnReadConfig
            // 
            this.btnReadConfig.Location = new System.Drawing.Point(40, 49);
            this.btnReadConfig.Name = "btnReadConfig";
            this.btnReadConfig.Size = new System.Drawing.Size(89, 31);
            this.btnReadConfig.TabIndex = 10;
            this.btnReadConfig.Text = "Read Config";
            this.btnReadConfig.UseVisualStyleBackColor = true;
            this.btnReadConfig.Click += new System.EventHandler(this.btnReadConfig_Click);
            // 
            // btnReadDataClasses
            // 
            this.btnReadDataClasses.Location = new System.Drawing.Point(4, 253);
            this.btnReadDataClasses.Name = "btnReadDataClasses";
            this.btnReadDataClasses.Size = new System.Drawing.Size(125, 58);
            this.btnReadDataClasses.TabIndex = 11;
            this.btnReadDataClasses.Text = "Read Data\r\nFrom Excel and DB\r\nNew Classes\r\n";
            this.btnReadDataClasses.UseVisualStyleBackColor = true;
            this.btnReadDataClasses.Click += new System.EventHandler(this.btnReadDataClasses_Click);
            // 
            // frmLoopUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 591);
            this.Controls.Add(this.btnReadDataClasses);
            this.Controls.Add(this.btnReadConfig);
            this.Controls.Add(this.lblConfigFile);
            this.Controls.Add(this.btnConfigFile);
            this.Controls.Add(this.btnReadTagData);
            this.Controls.Add(this.txtDisplayConnection);
            this.Name = "frmLoopUI";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private TextBox txtDisplayConnection;
        private Button btnReadTagData;
        private Button btnConfigFile;
        private Label lblConfigFile;
        private Button btnReadConfig;
        private Button btnReadDataClasses;
    }
}