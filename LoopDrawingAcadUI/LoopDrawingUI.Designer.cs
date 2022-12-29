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
            this.txtAttributes = new System.Windows.Forms.TextBox();
            this.txtBlocks = new System.Windows.Forms.TextBox();
            this.lblAttributeList = new System.Windows.Forms.Label();
            this.lblBlockNames = new System.Windows.Forms.Label();
            this.btnReadBlocks = new System.Windows.Forms.Button();
            this.btnPopulateAttributes = new System.Windows.Forms.Button();
            this.btnLoadAttributes = new System.Windows.Forms.Button();
            this.btnSelectDrawing = new System.Windows.Forms.Button();
            this.lblDwgPath = new System.Windows.Forms.Label();
            this.lblOutputPath = new System.Windows.Forms.Label();
            this.btnOutputPath = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtAttributes
            // 
            this.txtAttributes.Location = new System.Drawing.Point(902, 133);
            this.txtAttributes.Margin = new System.Windows.Forms.Padding(6);
            this.txtAttributes.Multiline = true;
            this.txtAttributes.Name = "txtAttributes";
            this.txtAttributes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtAttributes.Size = new System.Drawing.Size(632, 598);
            this.txtAttributes.TabIndex = 7;
            // 
            // txtBlocks
            // 
            this.txtBlocks.Location = new System.Drawing.Point(254, 133);
            this.txtBlocks.Margin = new System.Windows.Forms.Padding(6);
            this.txtBlocks.Multiline = true;
            this.txtBlocks.Name = "txtBlocks";
            this.txtBlocks.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtBlocks.Size = new System.Drawing.Size(632, 598);
            this.txtBlocks.TabIndex = 6;
            // 
            // lblAttributeList
            // 
            this.lblAttributeList.AutoSize = true;
            this.lblAttributeList.Location = new System.Drawing.Point(906, 102);
            this.lblAttributeList.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblAttributeList.Name = "lblAttributeList";
            this.lblAttributeList.Size = new System.Drawing.Size(384, 25);
            this.lblAttributeList.TabIndex = 9;
            this.lblAttributeList.Text = "Attribute Tag / Value (?) for Loop Block";
            // 
            // lblBlockNames
            // 
            this.lblBlockNames.AutoSize = true;
            this.lblBlockNames.Location = new System.Drawing.Point(248, 102);
            this.lblBlockNames.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblBlockNames.Name = "lblBlockNames";
            this.lblBlockNames.Size = new System.Drawing.Size(280, 25);
            this.lblBlockNames.TabIndex = 8;
            this.lblBlockNames.Text = "Block Names in Block Table";
            // 
            // btnReadBlocks
            // 
            this.btnReadBlocks.Location = new System.Drawing.Point(12, 133);
            this.btnReadBlocks.Margin = new System.Windows.Forms.Padding(6);
            this.btnReadBlocks.Name = "btnReadBlocks";
            this.btnReadBlocks.Size = new System.Drawing.Size(218, 75);
            this.btnReadBlocks.TabIndex = 10;
            this.btnReadBlocks.Text = "Read Blocks";
            this.btnReadBlocks.UseVisualStyleBackColor = true;
            this.btnReadBlocks.Click += new System.EventHandler(this.btnReadBlocks_Click);
            // 
            // btnPopulateAttributes
            // 
            this.btnPopulateAttributes.Location = new System.Drawing.Point(12, 219);
            this.btnPopulateAttributes.Margin = new System.Windows.Forms.Padding(6);
            this.btnPopulateAttributes.Name = "btnPopulateAttributes";
            this.btnPopulateAttributes.Size = new System.Drawing.Size(218, 75);
            this.btnPopulateAttributes.TabIndex = 12;
            this.btnPopulateAttributes.Text = "Populate Attributes";
            this.btnPopulateAttributes.UseVisualStyleBackColor = true;
            this.btnPopulateAttributes.Click += new System.EventHandler(this.btnPopulateAttributes_Click);
            // 
            // btnLoadAttributes
            // 
            this.btnLoadAttributes.Location = new System.Drawing.Point(12, 304);
            this.btnLoadAttributes.Name = "btnLoadAttributes";
            this.btnLoadAttributes.Size = new System.Drawing.Size(218, 73);
            this.btnLoadAttributes.TabIndex = 13;
            this.btnLoadAttributes.Text = "Load Attributes";
            this.btnLoadAttributes.UseVisualStyleBackColor = true;
            this.btnLoadAttributes.Click += new System.EventHandler(this.btnLoadAttributes_Click);
            // 
            // btnSelectDrawing
            // 
            this.btnSelectDrawing.Location = new System.Drawing.Point(12, 759);
            this.btnSelectDrawing.Margin = new System.Windows.Forms.Padding(6);
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
            this.lblDwgPath.Location = new System.Drawing.Point(260, 784);
            this.lblDwgPath.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblDwgPath.Name = "lblDwgPath";
            this.lblDwgPath.Size = new System.Drawing.Size(134, 25);
            this.lblDwgPath.TabIndex = 15;
            this.lblDwgPath.Text = "DrawingPath";
            // 
            // lblOutputPath
            // 
            this.lblOutputPath.AutoSize = true;
            this.lblOutputPath.Location = new System.Drawing.Point(263, 871);
            this.lblOutputPath.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblOutputPath.Name = "lblOutputPath";
            this.lblOutputPath.Size = new System.Drawing.Size(124, 25);
            this.lblOutputPath.TabIndex = 17;
            this.lblOutputPath.Text = "Output path";
            // 
            // btnOutputPath
            // 
            this.btnOutputPath.Location = new System.Drawing.Point(15, 846);
            this.btnOutputPath.Margin = new System.Windows.Forms.Padding(6);
            this.btnOutputPath.Name = "btnOutputPath";
            this.btnOutputPath.Size = new System.Drawing.Size(218, 75);
            this.btnOutputPath.TabIndex = 16;
            this.btnOutputPath.Text = "Output Path";
            this.btnOutputPath.UseVisualStyleBackColor = true;
            this.btnOutputPath.Click += new System.EventHandler(this.btnOutputPath_Click);
            // 
            // LoopDrawingUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1600, 967);
            this.Controls.Add(this.lblOutputPath);
            this.Controls.Add(this.btnOutputPath);
            this.Controls.Add(this.lblDwgPath);
            this.Controls.Add(this.btnSelectDrawing);
            this.Controls.Add(this.btnLoadAttributes);
            this.Controls.Add(this.btnPopulateAttributes);
            this.Controls.Add(this.btnReadBlocks);
            this.Controls.Add(this.lblAttributeList);
            this.Controls.Add(this.lblBlockNames);
            this.Controls.Add(this.txtAttributes);
            this.Controls.Add(this.txtBlocks);
            this.Margin = new System.Windows.Forms.Padding(6);
            this.Name = "LoopDrawingUI";
            this.Text = "LoopDrawingUI";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtAttributes;
        private System.Windows.Forms.TextBox txtBlocks;
        private System.Windows.Forms.Label lblAttributeList;
        private System.Windows.Forms.Label lblBlockNames;
        private System.Windows.Forms.Button btnReadBlocks;
        private System.Windows.Forms.Button btnPopulateAttributes;
        private System.Windows.Forms.Button btnLoadAttributes;
        private System.Windows.Forms.Button btnSelectDrawing;
        private System.Windows.Forms.Label lblDwgPath;
        private System.Windows.Forms.Label lblOutputPath;
        private System.Windows.Forms.Button btnOutputPath;
    }
}