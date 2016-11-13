namespace Mine.Sweeper
{
    partial class CustomSizeForm
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
            this.label2 = new System.Windows.Forms.Label();
            this.MapWidth = new System.Windows.Forms.TextBox();
            this.MapHeight = new System.Windows.Forms.TextBox();
            this.Mines = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Set = new System.Windows.Forms.Button();
            this.Exit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Висота";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Ширина";
            // 
            // MapWidth
            // 
            this.MapWidth.Location = new System.Drawing.Point(92, 38);
            this.MapWidth.Name = "MapWidth";
            this.MapWidth.Size = new System.Drawing.Size(100, 20);
            this.MapWidth.TabIndex = 1;
            this.MapWidth.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Width_KeyPress);
            // 
            // MapHeight
            // 
            this.MapHeight.Location = new System.Drawing.Point(92, 12);
            this.MapHeight.Name = "MapHeight";
            this.MapHeight.Size = new System.Drawing.Size(100, 20);
            this.MapHeight.TabIndex = 0;
            this.MapHeight.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Height_KeyPress);
            // 
            // Mines
            // 
            this.Mines.Location = new System.Drawing.Point(92, 64);
            this.Mines.Name = "Mines";
            this.Mines.Size = new System.Drawing.Size(100, 20);
            this.Mines.TabIndex = 2;
            this.Mines.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Mines_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 67);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "Міни";
            // 
            // Set
            // 
            this.Set.Location = new System.Drawing.Point(22, 104);
            this.Set.Name = "Set";
            this.Set.Size = new System.Drawing.Size(77, 23);
            this.Set.TabIndex = 3;
            this.Set.Text = "Встановити";
            this.Set.UseVisualStyleBackColor = true;
            this.Set.Click += new System.EventHandler(this.Set_Click);
            // 
            // Exit
            // 
            this.Exit.Location = new System.Drawing.Point(114, 104);
            this.Exit.Name = "Exit";
            this.Exit.Size = new System.Drawing.Size(77, 23);
            this.Exit.TabIndex = 4;
            this.Exit.Text = "Вийти";
            this.Exit.UseVisualStyleBackColor = true;
            this.Exit.Click += new System.EventHandler(this.Exit_Click);
            // 
            // CustomSizeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.ClientSize = new System.Drawing.Size(215, 142);
            this.Controls.Add(this.Exit);
            this.Controls.Add(this.Set);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.Mines);
            this.Controls.Add(this.MapHeight);
            this.Controls.Add(this.MapWidth);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "CustomSizeForm";
            this.Text = "Користувацькі опції гри";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox MapWidth;
        private System.Windows.Forms.TextBox MapHeight;
        private System.Windows.Forms.TextBox Mines;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button Set;
        private System.Windows.Forms.Button Exit;
    }
}