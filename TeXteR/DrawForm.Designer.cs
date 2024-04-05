namespace TeXteR
{
    partial class DrawForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DrawForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.редагуванняToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.змінитиФонToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.змінитиКолірКістіToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.додатиМалюнокToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.режимПрофіToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripTextBox2 = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripTextBox3 = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripComboBox1 = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.menuStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.редагуванняToolStripMenuItem,
            this.додатиМалюнокToolStripMenuItem,
            this.режимПрофіToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(811, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // редагуванняToolStripMenuItem
            // 
            this.редагуванняToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.редагуванняToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.змінитиФонToolStripMenuItem,
            this.змінитиКолірКістіToolStripMenuItem});
            this.редагуванняToolStripMenuItem.Name = "редагуванняToolStripMenuItem";
            this.редагуванняToolStripMenuItem.Size = new System.Drawing.Size(110, 24);
            this.редагуванняToolStripMenuItem.Text = "Редагування";
            // 
            // змінитиФонToolStripMenuItem
            // 
            this.змінитиФонToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.змінитиФонToolStripMenuItem.Image = global::TeXteR.Properties.Resources.Background;
            this.змінитиФонToolStripMenuItem.Name = "змінитиФонToolStripMenuItem";
            this.змінитиФонToolStripMenuItem.Size = new System.Drawing.Size(228, 26);
            this.змінитиФонToolStripMenuItem.Text = "Змінити колір фону";
            this.змінитиФонToolStripMenuItem.Click += new System.EventHandler(this.змінитиФонToolStripMenuItem_Click);
            // 
            // змінитиКолірКістіToolStripMenuItem
            // 
            this.змінитиКолірКістіToolStripMenuItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.змінитиКолірКістіToolStripMenuItem.Image = global::TeXteR.Properties.Resources.BrushColor;
            this.змінитиКолірКістіToolStripMenuItem.Name = "змінитиКолірКістіToolStripMenuItem";
            this.змінитиКолірКістіToolStripMenuItem.Size = new System.Drawing.Size(228, 26);
            this.змінитиКолірКістіToolStripMenuItem.Text = "Змінити колір кісті";
            this.змінитиКолірКістіToolStripMenuItem.Click += new System.EventHandler(this.змінитиКолірКістіToolStripMenuItem_Click);
            // 
            // додатиМалюнокToolStripMenuItem
            // 
            this.додатиМалюнокToolStripMenuItem.Name = "додатиМалюнокToolStripMenuItem";
            this.додатиМалюнокToolStripMenuItem.Size = new System.Drawing.Size(154, 24);
            this.додатиМалюнокToolStripMenuItem.Text = "Зберегти малюнок";
            this.додатиМалюнокToolStripMenuItem.Click += new System.EventHandler(this.зберегтиНаПристрійToolStripMenuItem_Click);
            // 
            // режимПрофіToolStripMenuItem
            // 
            this.режимПрофіToolStripMenuItem.Name = "режимПрофіToolStripMenuItem";
            this.режимПрофіToolStripMenuItem.Size = new System.Drawing.Size(257, 24);
            this.режимПрофіToolStripMenuItem.Text = "Режим професійного малювання";
            this.режимПрофіToolStripMenuItem.Click += new System.EventHandler(this.режимПрофіToolStripMenuItem_Click);
            // 
            // menuStrip2
            // 
            this.menuStrip2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.menuStrip2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.menuStrip2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripTextBox1,
            this.toolStripTextBox2,
            this.toolStripTextBox3,
            this.toolStripComboBox1,
            this.toolStripMenuItem4,
            this.toolStripMenuItem5,
            this.toolStripMenuItem6});
            this.menuStrip2.Location = new System.Drawing.Point(0, 374);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Size = new System.Drawing.Size(811, 32);
            this.menuStrip2.TabIndex = 1;
            this.menuStrip2.Text = "menuStrip2";
            // 
            // toolStripTextBox1
            // 
            this.toolStripTextBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.toolStripTextBox1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStripTextBox1.Name = "toolStripTextBox1";
            this.toolStripTextBox1.ReadOnly = true;
            this.toolStripTextBox1.Size = new System.Drawing.Size(100, 28);
            this.toolStripTextBox1.Text = "Розмір кісті:";
            this.toolStripTextBox1.Click += new System.EventHandler(this.toolStripTextBox1_Click);
            // 
            // toolStripTextBox2
            // 
            this.toolStripTextBox2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.toolStripTextBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.toolStripTextBox2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStripTextBox2.Name = "toolStripTextBox2";
            this.toolStripTextBox2.Size = new System.Drawing.Size(25, 28);
            this.toolStripTextBox2.Text = "2";
            this.toolStripTextBox2.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.toolStripTextBox2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.toolStripTextBox2_KeyPress);
            this.toolStripTextBox2.TextChanged += new System.EventHandler(this.toolStripTextBox2_TextChanged);
            // 
            // toolStripTextBox3
            // 
            this.toolStripTextBox3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.toolStripTextBox3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStripTextBox3.Name = "toolStripTextBox3";
            this.toolStripTextBox3.ReadOnly = true;
            this.toolStripTextBox3.Size = new System.Drawing.Size(105, 28);
            this.toolStripTextBox3.Text = "Розмір вікна:";
            this.toolStripTextBox3.Click += new System.EventHandler(this.toolStripTextBox3_Click);
            // 
            // toolStripComboBox1
            // 
            this.toolStripComboBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.toolStripComboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.toolStripComboBox1.Name = "toolStripComboBox1";
            this.toolStripComboBox1.Size = new System.Drawing.Size(135, 28);
            this.toolStripComboBox1.DropDown += new System.EventHandler(this.toolStripComboBox1_DropDown);
            this.toolStripComboBox1.SelectedIndexChanged += new System.EventHandler(this.toolStripComboBox1_SelectedIndexChanged);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(36, 28);
            this.toolStripMenuItem4.Text = "↩";
            this.toolStripMenuItem4.Click += new System.EventHandler(this.toolStripMenuItem4_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(36, 28);
            this.toolStripMenuItem5.Text = "↪";
            this.toolStripMenuItem5.Click += new System.EventHandler(this.toolStripMenuItem5_Click);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.toolStripMenuItem6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(40, 28);
            this.toolStripMenuItem6.Text = "⌧";
            this.toolStripMenuItem6.Click += new System.EventHandler(this.toolStripMenuItem6_Click);
            // 
            // DrawForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(811, 406);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.menuStrip2);
            this.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DrawForm";
            this.Text = "DrawForm";
            this.Load += new System.EventHandler(this.DrawForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem редагуванняToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem змінитиФонToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem змінитиКолірКістіToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem додатиМалюнокToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem режимПрофіToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox1;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox2;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox3;
        private System.Windows.Forms.ToolStripComboBox toolStripComboBox1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem6;
    }
}