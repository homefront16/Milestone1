namespace MinesweeperGUI
{
    partial class Form2
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
            this.panelButtonHolder = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // panelButtonHolder
            // 
            this.panelButtonHolder.Location = new System.Drawing.Point(12, 12);
            this.panelButtonHolder.Name = "panelButtonHolder";
            this.panelButtonHolder.Size = new System.Drawing.Size(616, 426);
            this.panelButtonHolder.TabIndex = 0;
            this.panelButtonHolder.Paint += new System.Windows.Forms.PaintEventHandler(this.panelButtonHolder_Paint);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(963, 672);
            this.Controls.Add(this.panelButtonHolder);
            this.Name = "Form2";
            this.Text = "Minesweeper";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelButtonHolder;
    }
}