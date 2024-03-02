namespace MerryDllFramework
{
    partial class Messages
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
            this.btnYES = new System.Windows.Forms.Button();
            this.btnNO = new System.Windows.Forms.Button();
            this.lblMes = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnYES
            // 
            this.btnYES.BackColor = System.Drawing.Color.LimeGreen;
            this.btnYES.Font = new System.Drawing.Font("SimSun", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnYES.Location = new System.Drawing.Point(77, 114);
            this.btnYES.Name = "btnYES";
            this.btnYES.Size = new System.Drawing.Size(116, 50);
            this.btnYES.TabIndex = 1;
            this.btnYES.Text = "YES";
            this.btnYES.UseVisualStyleBackColor = false;
            this.btnYES.Click += new System.EventHandler(this.btnYES_Click);
            // 
            // btnNO
            // 
            this.btnNO.BackColor = System.Drawing.Color.Red;
            this.btnNO.Font = new System.Drawing.Font("SimSun", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnNO.Location = new System.Drawing.Point(276, 114);
            this.btnNO.Name = "btnNO";
            this.btnNO.Size = new System.Drawing.Size(110, 50);
            this.btnNO.TabIndex = 2;
            this.btnNO.Text = "NO";
            this.btnNO.UseVisualStyleBackColor = false;
            this.btnNO.Click += new System.EventHandler(this.btnNO_Click);
            // 
            // lblMes
            // 
            this.lblMes.AutoSize = true;
            this.lblMes.Font = new System.Drawing.Font("SimSun", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblMes.Location = new System.Drawing.Point(35, 37);
            this.lblMes.Name = "lblMes";
            this.lblMes.Size = new System.Drawing.Size(82, 24);
            this.lblMes.TabIndex = 3;
            this.lblMes.Text = "label1";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(454, 197);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(17, 23);
            this.button1.TabIndex = 0;
            this.button1.UseVisualStyleBackColor = true;
            // 
            // Message
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(483, 232);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lblMes);
            this.Controls.Add(this.btnNO);
            this.Controls.Add(this.btnYES);
            this.Name = "Message";
            this.Text = "Message";
            this.Load += new System.EventHandler(this.Message_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Message_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnYES;
        private System.Windows.Forms.Button btnNO;
        private System.Windows.Forms.Label lblMes;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button button1;
    }
}