namespace SOP_Client
{
    partial class Main
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
            this.getButton = new System.Windows.Forms.Button();
            this.usersData = new System.Windows.Forms.DataGridView();
            this.addButton = new System.Windows.Forms.Button();
            this.updateButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.usersData)).BeginInit();
            this.SuspendLayout();
            // 
            // getButton
            // 
            this.getButton.Location = new System.Drawing.Point(685, 12);
            this.getButton.Name = "getButton";
            this.getButton.Size = new System.Drawing.Size(103, 29);
            this.getButton.TabIndex = 0;
            this.getButton.Text = "GET";
            this.getButton.UseVisualStyleBackColor = true;
            this.getButton.Click += new System.EventHandler(this.getButton_Click);
            // 
            // usersData
            // 
            this.usersData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.usersData.Location = new System.Drawing.Point(12, 12);
            this.usersData.Name = "usersData";
            this.usersData.Size = new System.Drawing.Size(667, 426);
            this.usersData.TabIndex = 1;
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(685, 47);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(103, 29);
            this.addButton.TabIndex = 2;
            this.addButton.Text = "ADD";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // updateButton
            // 
            this.updateButton.Location = new System.Drawing.Point(685, 82);
            this.updateButton.Name = "updateButton";
            this.updateButton.Size = new System.Drawing.Size(103, 29);
            this.updateButton.TabIndex = 3;
            this.updateButton.Text = "UPDATE";
            this.updateButton.UseVisualStyleBackColor = true;
            this.updateButton.Click += new System.EventHandler(this.updateButton_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.updateButton);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.usersData);
            this.Controls.Add(this.getButton);
            this.Name = "Main";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.usersData)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button getButton;
        private System.Windows.Forms.DataGridView usersData;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button updateButton;
    }
}

