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
            this.btnReadData = new System.Windows.Forms.Button();
            this.btnReadExcel = new System.Windows.Forms.Button();
            this.btnGetData = new System.Windows.Forms.Button();
            this.txtDisplayConnection = new System.Windows.Forms.TextBox();
            this.btnReadTagData = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnReadData
            // 
            this.btnReadData.Location = new System.Drawing.Point(40, 44);
            this.btnReadData.Name = "btnReadData";
            this.btnReadData.Size = new System.Drawing.Size(89, 31);
            this.btnReadData.TabIndex = 0;
            this.btnReadData.Text = "Read Data";
            this.btnReadData.UseVisualStyleBackColor = true;
            this.btnReadData.Click += new System.EventHandler(this.btnReadData_Click);
            // 
            // btnReadExcel
            // 
            this.btnReadExcel.Location = new System.Drawing.Point(4, 165);
            this.btnReadExcel.Name = "btnReadExcel";
            this.btnReadExcel.Size = new System.Drawing.Size(125, 38);
            this.btnReadExcel.TabIndex = 4;
            this.btnReadExcel.Text = "Read Excel";
            this.btnReadExcel.UseVisualStyleBackColor = true;
            this.btnReadExcel.Click += new System.EventHandler(this.btnReadExcel_Click);
            // 
            // btnGetData
            // 
            this.btnGetData.Location = new System.Drawing.Point(4, 121);
            this.btnGetData.Name = "btnGetData";
            this.btnGetData.Size = new System.Drawing.Size(125, 38);
            this.btnGetData.TabIndex = 5;
            this.btnGetData.Text = "GetSomeData";
            this.btnGetData.UseVisualStyleBackColor = true;
            this.btnGetData.Click += new System.EventHandler(this.btnGetData_Click);
            // 
            // txtDisplayConnection
            // 
            this.txtDisplayConnection.Location = new System.Drawing.Point(135, 12);
            this.txtDisplayConnection.Multiline = true;
            this.txtDisplayConnection.Name = "txtDisplayConnection";
            this.txtDisplayConnection.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtDisplayConnection.Size = new System.Drawing.Size(590, 557);
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
            // frmLoopUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 643);
            this.Controls.Add(this.btnReadTagData);
            this.Controls.Add(this.txtDisplayConnection);
            this.Controls.Add(this.btnGetData);
            this.Controls.Add(this.btnReadExcel);
            this.Controls.Add(this.btnReadData);
            this.Name = "frmLoopUI";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button btnReadData;
        private Button btnReadExcel;
        private Button btnGetData;
        private TextBox txtDisplayConnection;
        private Button btnReadTagData;
    }
}