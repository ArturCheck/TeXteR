using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TeXteR
{
    public partial class Information : Form
    {
        public Information()
        {
            InitializeComponent();
        }

        public void languageUKR(object sender, EventArgs e)
        {
            if (richTextBox1.IsHandleCreated)
            {
                richTextBox1.Clear();

                richTextBox1.AppendText("Текстовий фото - аудіо\r\nредактор\r\n(TeXtoR)\r\nСтворений в 2023 році\r\nАвтор проекту: Завада Артур");

                int indexTeXtoR = richTextBox1.Text.IndexOf("(TeXtoR)");

                if (indexTeXtoR != -1)
                {
                    richTextBox1.Select(indexTeXtoR, "(TeXtoR)".Length);
                    richTextBox1.SelectionColor = Color.Green;
                }

                int indexAuthor = richTextBox1.Text.IndexOf("Завада Артур");

                if (indexAuthor != -1)
                {
                    richTextBox1.Select(indexAuthor, "Завада Артур".Length);
                    richTextBox1.SelectionColor = Color.Blue;
                }

                richTextBox1.SelectAll();
                richTextBox1.SelectionAlignment = HorizontalAlignment.Center;
                richTextBox1.DeselectAll();
                richTextBox1.Enabled = false;
                richTextBox1.Enabled = true;
            }
        }

        public void languageENG(object sender, EventArgs e)
        {
            if (richTextBox1.IsHandleCreated)
            {
                richTextBox1.Clear();

                richTextBox1.AppendText("Text Photo - Audio\r\nEditor\r\n(TeXtoR)\r\nCreated in 2023\r\nProject Author: Artur Zavada");

                int index = richTextBox1.Text.IndexOf("(TeXtoR)");

                if (index != -1)
                {
                    richTextBox1.Select(index, "(TeXtoR)".Length);
                    richTextBox1.SelectionColor = Color.Green;
                }

                int indexAuthor = richTextBox1.Text.IndexOf("Artur Zavada");

                if (indexAuthor != -1)
                {
                    richTextBox1.Select(indexAuthor, "Artur Zavada".Length);
                    richTextBox1.SelectionColor = Color.Blue;
                }

                richTextBox1.SelectAll();
                richTextBox1.SelectionAlignment = HorizontalAlignment.Center;
                richTextBox1.DeselectAll();
                richTextBox1.Enabled = false;
                richTextBox1.Enabled = true;
            }
        }

        private void richTextBox1_Click(object sender, EventArgs e)
        {
            richTextBox1.Enabled = false;
            richTextBox1.Enabled = true;
        }
    }
}
