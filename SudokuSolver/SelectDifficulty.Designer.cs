namespace SudokuSolver
{
    partial class SelectDifficulty
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
            this.label1 = new System.Windows.Forms.Label();
            this.btn_Easy = new System.Windows.Forms.Button();
            this.btn_Normal = new System.Windows.Forms.Button();
            this.btn_Hard = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe Print", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(115, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(182, 36);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select Difficulty";
            // 
            // btn_Easy
            // 
            this.btn_Easy.Location = new System.Drawing.Point(55, 82);
            this.btn_Easy.Name = "btn_Easy";
            this.btn_Easy.Size = new System.Drawing.Size(89, 39);
            this.btn_Easy.TabIndex = 1;
            this.btn_Easy.Text = "Easy";
            this.btn_Easy.UseVisualStyleBackColor = true;
            this.btn_Easy.Click += new System.EventHandler(this.btnEasy_Click);
            // 
            // btn_Normal
            // 
            this.btn_Normal.Location = new System.Drawing.Point(162, 82);
            this.btn_Normal.Name = "btn_Normal";
            this.btn_Normal.Size = new System.Drawing.Size(85, 39);
            this.btn_Normal.TabIndex = 2;
            this.btn_Normal.Text = "Normal";
            this.btn_Normal.UseVisualStyleBackColor = true;
            this.btn_Normal.Click += new System.EventHandler(this.btn_Normal_Click);
            // 
            // btn_Hard
            // 
            this.btn_Hard.Enabled = false;
            this.btn_Hard.Location = new System.Drawing.Point(264, 82);
            this.btn_Hard.Name = "btn_Hard";
            this.btn_Hard.Size = new System.Drawing.Size(92, 39);
            this.btn_Hard.TabIndex = 3;
            this.btn_Hard.Text = "Hard";
            this.btn_Hard.UseVisualStyleBackColor = true;
            this.btn_Hard.Click += new System.EventHandler(this.btn_Hard_Click);
            // 
            // SelectDifficulty
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Thistle;
            this.ClientSize = new System.Drawing.Size(425, 162);
            this.Controls.Add(this.btn_Hard);
            this.Controls.Add(this.btn_Normal);
            this.Controls.Add(this.btn_Easy);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SelectDifficulty";
            this.Opacity = 0.9D;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SelectDifficulty";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_Easy;
        private System.Windows.Forms.Button btn_Normal;
        private System.Windows.Forms.Button btn_Hard;
    }
}