namespace LinqLabs
{
    partial class FrmChooseForm
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
            this.btnHome1 = new System.Windows.Forms.Button();
            this.btnHome2 = new System.Windows.Forms.Button();
            this.btnHome3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnHome1
            // 
            this.btnHome1.Font = new System.Drawing.Font("微軟正黑體", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnHome1.Location = new System.Drawing.Point(39, 34);
            this.btnHome1.Margin = new System.Windows.Forms.Padding(2);
            this.btnHome1.Name = "btnHome1";
            this.btnHome1.Size = new System.Drawing.Size(154, 38);
            this.btnHome1.TabIndex = 0;
            this.btnHome1.Text = "作業一";
            this.btnHome1.UseVisualStyleBackColor = true;
            this.btnHome1.Click += new System.EventHandler(this.btnHome1_Click);
            // 
            // btnHome2
            // 
            this.btnHome2.Font = new System.Drawing.Font("微軟正黑體", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnHome2.Location = new System.Drawing.Point(39, 98);
            this.btnHome2.Margin = new System.Windows.Forms.Padding(2);
            this.btnHome2.Name = "btnHome2";
            this.btnHome2.Size = new System.Drawing.Size(154, 38);
            this.btnHome2.TabIndex = 0;
            this.btnHome2.Text = "作業二";
            this.btnHome2.UseVisualStyleBackColor = true;
            this.btnHome2.Click += new System.EventHandler(this.btnHome2_Click);
            // 
            // btnHome3
            // 
            this.btnHome3.Font = new System.Drawing.Font("微軟正黑體", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.btnHome3.Location = new System.Drawing.Point(39, 157);
            this.btnHome3.Margin = new System.Windows.Forms.Padding(2);
            this.btnHome3.Name = "btnHome3";
            this.btnHome3.Size = new System.Drawing.Size(154, 38);
            this.btnHome3.TabIndex = 0;
            this.btnHome3.Text = "作業三";
            this.btnHome3.UseVisualStyleBackColor = true;
            this.btnHome3.Click += new System.EventHandler(this.btnHome3_Click);
            // 
            // FrmChooseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 360);
            this.Controls.Add(this.btnHome3);
            this.Controls.Add(this.btnHome2);
            this.Controls.Add(this.btnHome1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FrmChooseForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmChooseForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnHome1;
        private System.Windows.Forms.Button btnHome2;
        private System.Windows.Forms.Button btnHome3;
    }
}