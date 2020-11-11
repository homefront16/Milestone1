namespace MinesweeperGUI
{
    partial class Form1
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioEasy = new System.Windows.Forms.RadioButton();
            this.radioModerate = new System.Windows.Forms.RadioButton();
            this.radioDifficult = new System.Windows.Forms.RadioButton();
            this.btnPlayGame = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnPlayGame);
            this.groupBox1.Controls.Add(this.radioDifficult);
            this.groupBox1.Controls.Add(this.radioModerate);
            this.groupBox1.Controls.Add(this.radioEasy);
            this.groupBox1.Location = new System.Drawing.Point(70, 31);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(278, 229);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Select Difficulty";
            // 
            // radioEasy
            // 
            this.radioEasy.AutoSize = true;
            this.radioEasy.Location = new System.Drawing.Point(21, 28);
            this.radioEasy.Name = "radioEasy";
            this.radioEasy.Size = new System.Drawing.Size(48, 17);
            this.radioEasy.TabIndex = 0;
            this.radioEasy.TabStop = true;
            this.radioEasy.Text = "Easy";
            this.radioEasy.UseVisualStyleBackColor = true;
            // 
            // radioModerate
            // 
            this.radioModerate.AutoSize = true;
            this.radioModerate.Location = new System.Drawing.Point(21, 68);
            this.radioModerate.Name = "radioModerate";
            this.radioModerate.Size = new System.Drawing.Size(70, 17);
            this.radioModerate.TabIndex = 1;
            this.radioModerate.TabStop = true;
            this.radioModerate.Text = "Moderate";
            this.radioModerate.UseVisualStyleBackColor = true;
            // 
            // radioDifficult
            // 
            this.radioDifficult.AutoSize = true;
            this.radioDifficult.Location = new System.Drawing.Point(21, 107);
            this.radioDifficult.Name = "radioDifficult";
            this.radioDifficult.Size = new System.Drawing.Size(60, 17);
            this.radioDifficult.TabIndex = 2;
            this.radioDifficult.TabStop = true;
            this.radioDifficult.Text = "Difficult";
            this.radioDifficult.UseVisualStyleBackColor = true;
            // 
            // btnPlayGame
            // 
            this.btnPlayGame.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPlayGame.Location = new System.Drawing.Point(135, 139);
            this.btnPlayGame.Name = "btnPlayGame";
            this.btnPlayGame.Size = new System.Drawing.Size(128, 68);
            this.btnPlayGame.TabIndex = 3;
            this.btnPlayGame.Text = "Play Game";
            this.btnPlayGame.UseVisualStyleBackColor = true;
            this.btnPlayGame.Click += new System.EventHandler(this.btnPlayGame_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Minesweeper";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnPlayGame;
        private System.Windows.Forms.RadioButton radioDifficult;
        private System.Windows.Forms.RadioButton radioModerate;
        private System.Windows.Forms.RadioButton radioEasy;
    }
}

