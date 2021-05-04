
namespace Galaga
{
    partial class Ranking
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
            this.lvwRanking = new System.Windows.Forms.ListView();
            this.btnExit = new Galaga.Ui.CustomButton();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("맑은 고딕", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(25, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(152, 48);
            this.label1.TabIndex = 0;
            this.label1.Text = "Ranking";
            // 
            // lvwRanking
            // 
            this.lvwRanking.BackColor = System.Drawing.SystemColors.Control;
            this.lvwRanking.Font = new System.Drawing.Font("맑은 고딕", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lvwRanking.HideSelection = false;
            this.lvwRanking.Location = new System.Drawing.Point(34, 105);
            this.lvwRanking.Name = "lvwRanking";
            this.lvwRanking.Size = new System.Drawing.Size(905, 543);
            this.lvwRanking.TabIndex = 1;
            this.lvwRanking.UseCompatibleStateImageBehavior = false;
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.Font = new System.Drawing.Font("맑은 고딕", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnExit.ForeColor = System.Drawing.Color.White;
            this.btnExit.Location = new System.Drawing.Point(764, 674);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(175, 58);
            this.btnExit.TabIndex = 2;
            this.btnExit.Text = "Exit";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // Ranking
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Galaga.Properties.Resources.배경;
            this.ClientSize = new System.Drawing.Size(978, 744);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.lvwRanking);
            this.Controls.Add(this.label1);
            this.Name = "Ranking";
            this.Text = "Ranking";
            this.Load += new System.EventHandler(this.Ranking_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListView lvwRanking;
        private Ui.CustomButton btnExit;
    }
}