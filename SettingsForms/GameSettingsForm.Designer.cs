namespace RogueSquadronUI
{
    partial class GameSettings
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.B_Cancel = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.B_SaveAndClose = new System.Windows.Forms.Button();
            this.T_ResolutionY = new System.Windows.Forms.TextBox();
            this.T_ResolutionX = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.B_Cancel);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.B_SaveAndClose);
            this.panel1.Controls.Add(this.T_ResolutionY);
            this.panel1.Controls.Add(this.T_ResolutionX);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(5, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(230, 77);
            this.panel1.TabIndex = 1;
            // 
            // B_Cancel
            // 
            this.B_Cancel.Location = new System.Drawing.Point(123, 43);
            this.B_Cancel.Name = "B_Cancel";
            this.B_Cancel.Size = new System.Drawing.Size(93, 23);
            this.B_Cancel.TabIndex = 3;
            this.B_Cancel.Text = "Cancel";
            this.B_Cancel.UseVisualStyleBackColor = true;
            this.B_Cancel.Click += new System.EventHandler(this.B_Cancel_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(105, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(12, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "x";
            // 
            // B_SaveAndClose
            // 
            this.B_SaveAndClose.Location = new System.Drawing.Point(3, 43);
            this.B_SaveAndClose.Name = "B_SaveAndClose";
            this.B_SaveAndClose.Size = new System.Drawing.Size(96, 23);
            this.B_SaveAndClose.TabIndex = 2;
            this.B_SaveAndClose.Text = "Write to Memory";
            this.B_SaveAndClose.UseVisualStyleBackColor = true;
            this.B_SaveAndClose.Click += new System.EventHandler(this.B_SaveAndClose_Click);
            // 
            // T_ResolutionY
            // 
            this.T_ResolutionY.Location = new System.Drawing.Point(123, 17);
            this.T_ResolutionY.Name = "T_ResolutionY";
            this.T_ResolutionY.Size = new System.Drawing.Size(93, 20);
            this.T_ResolutionY.TabIndex = 2;
            this.T_ResolutionY.TextChanged += new System.EventHandler(this.T_ResolutionY_TextChanged);
            // 
            // T_ResolutionX
            // 
            this.T_ResolutionX.Location = new System.Drawing.Point(3, 17);
            this.T_ResolutionX.Name = "T_ResolutionX";
            this.T_ResolutionX.Size = new System.Drawing.Size(96, 20);
            this.T_ResolutionX.TabIndex = 1;
            this.T_ResolutionX.TextChanged += new System.EventHandler(this.T_ResolutionX_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Resolution:";
            // 
            // GameSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(241, 85);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GameSettings";
            this.Text = "Game Settings";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox T_ResolutionY;
        private System.Windows.Forms.TextBox T_ResolutionX;
        private System.Windows.Forms.Button B_SaveAndClose;
        private System.Windows.Forms.Button B_Cancel;
    }
}