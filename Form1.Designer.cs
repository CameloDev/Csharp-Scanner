using System.Drawing;

namespace Dujob
{
    partial class Form1
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.siticoneButton1 = new Siticone.Desktop.UI.WinForms.SiticoneButton();
            this.siticonePictureBox1 = new Siticone.Desktop.UI.WinForms.SiticonePictureBox();
            this.siticoneTextBox2 = new Siticone.Desktop.UI.WinForms.SiticoneTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.siticoneButton2 = new Siticone.Desktop.UI.WinForms.SiticoneButton();
            this.siticoneVProgressBar1 = new Siticone.Desktop.UI.WinForms.SiticoneVProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.siticonePictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // siticoneButton1
            // 
            resources.ApplyResources(this.siticoneButton1, "siticoneButton1");
            this.siticoneButton1.Animated = true;
            this.siticoneButton1.BackColor = System.Drawing.Color.Transparent;
            this.siticoneButton1.BorderColor = System.Drawing.Color.Transparent;
            this.siticoneButton1.BorderRadius = 15;
            this.siticoneButton1.BorderStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            this.siticoneButton1.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.siticoneButton1.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.siticoneButton1.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.siticoneButton1.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.siticoneButton1.FillColor = System.Drawing.Color.Transparent;
            this.siticoneButton1.ForeColor = System.Drawing.Color.White;
            this.siticoneButton1.Name = "siticoneButton1";
            this.siticoneButton1.UseTransparentBackground = true;
            this.siticoneButton1.Click += new System.EventHandler(this.siticoneButton1_Click);
            // 
            // siticonePictureBox1
            // 
            resources.ApplyResources(this.siticonePictureBox1, "siticonePictureBox1");
            this.siticonePictureBox1.AutoRoundedCorners = true;
            this.siticonePictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.siticonePictureBox1.BorderRadius = 58;
            this.siticonePictureBox1.FillColor = System.Drawing.Color.Transparent;
            this.siticonePictureBox1.ImageRotate = 0F;
            this.siticonePictureBox1.Name = "siticonePictureBox1";
            this.siticonePictureBox1.TabStop = false;
            // 
            // siticoneTextBox2
            // 
            resources.ApplyResources(this.siticoneTextBox2, "siticoneTextBox2");
            this.siticoneTextBox2.Animated = true;
            this.siticoneTextBox2.BackColor = System.Drawing.Color.Transparent;
            this.siticoneTextBox2.BorderColor = System.Drawing.Color.Transparent;
            this.siticoneTextBox2.BorderRadius = 15;
            this.siticoneTextBox2.BorderThickness = 0;
            this.siticoneTextBox2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.siticoneTextBox2.DefaultText = "";
            this.siticoneTextBox2.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.siticoneTextBox2.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.siticoneTextBox2.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.siticoneTextBox2.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.siticoneTextBox2.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(56)))), ((int)(((byte)(58)))));
            this.siticoneTextBox2.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.siticoneTextBox2.ForeColor = System.Drawing.Color.Transparent;
            this.siticoneTextBox2.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.siticoneTextBox2.Name = "siticoneTextBox2";
            this.siticoneTextBox2.PasswordChar = '\0';
            this.siticoneTextBox2.PlaceholderForeColor = System.Drawing.Color.PaleTurquoise;
            this.siticoneTextBox2.PlaceholderText = "                     INSIRA O PIN          ";
            this.siticoneTextBox2.SelectedText = "";
            this.siticoneTextBox2.TextChanged += new System.EventHandler(this.siticoneTextBox2_TextChanged);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.ForeColor = System.Drawing.Color.Transparent;
            this.label2.Name = "label2";
            this.label2.Click += new System.EventHandler(this.Resposta_Click_1);
            // 
            // siticoneButton2
            // 
            resources.ApplyResources(this.siticoneButton2, "siticoneButton2");
            this.siticoneButton2.Animated = true;
            this.siticoneButton2.AutoRoundedCorners = true;
            this.siticoneButton2.BackColor = System.Drawing.Color.Transparent;
            this.siticoneButton2.BorderColor = System.Drawing.Color.Transparent;
            this.siticoneButton2.BorderRadius = 11;
            this.siticoneButton2.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.siticoneButton2.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.siticoneButton2.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.siticoneButton2.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.siticoneButton2.FillColor = System.Drawing.Color.Transparent;
            this.siticoneButton2.ForeColor = System.Drawing.Color.White;
            this.siticoneButton2.IndicateFocus = true;
            this.siticoneButton2.Name = "siticoneButton2";
            this.siticoneButton2.UseTransparentBackground = true;
            this.siticoneButton2.Click += new System.EventHandler(this.siticoneButton2_Click_1);
            // 
            // siticoneVProgressBar1
            // 
            resources.ApplyResources(this.siticoneVProgressBar1, "siticoneVProgressBar1");
            this.siticoneVProgressBar1.AutoRoundedCorners = true;
            this.siticoneVProgressBar1.BackColor = System.Drawing.Color.Transparent;
            this.siticoneVProgressBar1.BorderRadius = 15;
            this.siticoneVProgressBar1.FillColor = System.Drawing.Color.Transparent;
            this.siticoneVProgressBar1.ForeColor = System.Drawing.Color.PaleTurquoise;
            this.siticoneVProgressBar1.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.siticoneVProgressBar1.Name = "siticoneVProgressBar1";
            this.siticoneVProgressBar1.ProgressColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(56)))), ((int)(((byte)(58)))));
            this.siticoneVProgressBar1.ProgressColor2 = System.Drawing.Color.Transparent;
            this.siticoneVProgressBar1.ShowText = true;
            this.siticoneVProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.siticoneVProgressBar1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.siticoneVProgressBar1.TextOffset = new System.Drawing.Point(2, 2);
            this.siticoneVProgressBar1.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.siticoneVProgressBar1.UseTransparentBackground = true;
            this.siticoneVProgressBar1.UseWaitCursor = true;
            this.siticoneVProgressBar1.ValueChanged += new System.EventHandler(this.siticoneVProgressBar1_ValueChanged);
            // 
            // Form1
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Controls.Add(this.siticoneVProgressBar1);
            this.Controls.Add(this.siticoneButton2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.siticonePictureBox1);
            this.Controls.Add(this.siticoneTextBox2);
            this.Controls.Add(this.siticoneButton1);
            this.Cursor = System.Windows.Forms.Cursors.Cross;
            this.DoubleBuffered = true;
            this.ForeColor = System.Drawing.Color.Transparent;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.Opacity = 0.9D;
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.siticonePictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private Siticone.Desktop.UI.WinForms.SiticoneButton siticoneButton1;
        private Siticone.Desktop.UI.WinForms.SiticonePictureBox siticonePictureBox1;
        private Siticone.Desktop.UI.WinForms.SiticoneTextBox siticoneTextBox2;
        private System.Windows.Forms.Label label2;
        private Siticone.Desktop.UI.WinForms.SiticoneButton siticoneButton2;
        private Siticone.Desktop.UI.WinForms.SiticoneVProgressBar siticoneVProgressBar1;
    }
}


