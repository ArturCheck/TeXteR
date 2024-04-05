namespace TeXteR
{
    partial class PlayerForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlayerForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.вибратиПапкуЗМузикойToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.режимиПлейруToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.повторПісніToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.випадковийПорядрокToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.вибратиПапкуЗМузикойToolStripMenuItem,
            this.режимиПлейруToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(432, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // вибратиПапкуЗМузикойToolStripMenuItem
            // 
            this.вибратиПапкуЗМузикойToolStripMenuItem.Name = "вибратиПапкуЗМузикойToolStripMenuItem";
            this.вибратиПапкуЗМузикойToolStripMenuItem.Size = new System.Drawing.Size(200, 24);
            this.вибратиПапкуЗМузикойToolStripMenuItem.Text = "Вибрати папку з музикой";
            this.вибратиПапкуЗМузикойToolStripMenuItem.Click += new System.EventHandler(this.button4_Click);
            // 
            // режимиПлейруToolStripMenuItem
            // 
            this.режимиПлейруToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.повторПісніToolStripMenuItem,
            this.випадковийПорядрокToolStripMenuItem});
            this.режимиПлейруToolStripMenuItem.Enabled = false;
            this.режимиПлейруToolStripMenuItem.Name = "режимиПлейруToolStripMenuItem";
            this.режимиПлейруToolStripMenuItem.Size = new System.Drawing.Size(131, 24);
            this.режимиПлейруToolStripMenuItem.Text = "Режими плеєру";
            // 
            // повторПісніToolStripMenuItem
            // 
            this.повторПісніToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.повторПісніToolStripMenuItem.ForeColor = System.Drawing.Color.Maroon;
            this.повторПісніToolStripMenuItem.Image = global::TeXteR.Properties.Resources.Repeat;
            this.повторПісніToolStripMenuItem.Name = "повторПісніToolStripMenuItem";
            this.повторПісніToolStripMenuItem.Size = new System.Drawing.Size(225, 26);
            this.повторПісніToolStripMenuItem.Text = "Повтор пісні";
            this.повторПісніToolStripMenuItem.Click += new System.EventHandler(this.повторПісніToolStripMenuItem_Click);
            // 
            // випадковийПорядрокToolStripMenuItem
            // 
            this.випадковийПорядрокToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.випадковийПорядрокToolStripMenuItem.ForeColor = System.Drawing.Color.Black;
            this.випадковийПорядрокToolStripMenuItem.Image = global::TeXteR.Properties.Resources.Shuffle;
            this.випадковийПорядрокToolStripMenuItem.Name = "випадковийПорядрокToolStripMenuItem";
            this.випадковийПорядрокToolStripMenuItem.Size = new System.Drawing.Size(225, 26);
            this.випадковийПорядрокToolStripMenuItem.Text = "Змішаний порядок";
            this.випадковийПорядрокToolStripMenuItem.Click += new System.EventHandler(this.випадковийПорядрокToolStripMenuItem_Click);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.button1.Enabled = false;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button1.ForeColor = System.Drawing.Color.Olive;
            this.button1.Location = new System.Drawing.Point(12, 191);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(80, 50);
            this.button1.TabIndex = 2;
            this.button1.Text = "I◀◀";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.button2.Enabled = false;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button2.ForeColor = System.Drawing.Color.Olive;
            this.button2.Location = new System.Drawing.Point(340, 191);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(80, 50);
            this.button2.TabIndex = 3;
            this.button2.Text = "▶▶I";
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.Fuchsia;
            this.button3.Enabled = false;
            this.button3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button3.ForeColor = System.Drawing.Color.Black;
            this.button3.Location = new System.Drawing.Point(179, 182);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(76, 59);
            this.button3.TabIndex = 4;
            this.button3.Text = " ▶";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // textBox1
            // 
            this.textBox1.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.textBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Enabled = false;
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 19.8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBox1.Location = new System.Drawing.Point(0, 31);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(432, 94);
            this.textBox1.TabIndex = 1;
            this.textBox1.TabStop = false;
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // trackBar1
            // 
            this.trackBar1.Enabled = false;
            this.trackBar1.Location = new System.Drawing.Point(12, 131);
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(408, 56);
            this.trackBar1.TabIndex = 6;
            this.trackBar1.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(176, 131);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 16);
            this.label1.TabIndex = 7;
            this.label1.Text = "00:00 / 00:00";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // PlayerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.ClientSize = new System.Drawing.Size(432, 253);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "PlayerForm";
            this.Text = "PlayerForm";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripMenuItem вибратиПапкуЗМузикойToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem режимиПлейруToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem повторПісніToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem випадковийПорядрокToolStripMenuItem;
    }
}