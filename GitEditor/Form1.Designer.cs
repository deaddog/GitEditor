namespace GitEditor
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
            this.components = new System.ComponentModel.Container();
            this.box1 = new GitEditor.Box();
            ((System.ComponentModel.ISupportInitialize)(this.box1)).BeginInit();
            this.SuspendLayout();
            // 
            // box1
            // 
            this.box1.AutoScrollMinSize = new System.Drawing.Size(25, 14);
            this.box1.BackBrush = null;
            this.box1.BackColor = System.Drawing.Color.Black;
            this.box1.CaretColor = System.Drawing.Color.White;
            this.box1.CharHeight = 14;
            this.box1.CharWidth = 7;
            this.box1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.box1.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.box1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.box1.Font = new System.Drawing.Font("Consolas", 9F);
            this.box1.ForeColor = System.Drawing.Color.LightGray;
            this.box1.IndentBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.box1.IsReplaceMode = false;
            this.box1.LineNumberColor = System.Drawing.Color.PowderBlue;
            this.box1.Location = new System.Drawing.Point(0, 0);
            this.box1.Name = "box1";
            this.box1.Paddings = new System.Windows.Forms.Padding(0);
            this.box1.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.box1.ServiceLinesColor = System.Drawing.Color.PowderBlue;
            this.box1.Size = new System.Drawing.Size(943, 434);
            this.box1.TabIndex = 1;
            this.box1.Zoom = 100;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(943, 434);
            this.Controls.Add(this.box1);
            this.Name = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.box1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Box box1;
    }
}

