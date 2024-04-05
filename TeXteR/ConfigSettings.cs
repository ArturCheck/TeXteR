using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace TeXteR
{
    [Serializable]
    public class ConfigSettings
    {
        public Color RichTextBoxBackColor { get; set; }
        public Color RichTextBoxForeColor { get; set; }
        public float RichTextBoxFontSize { get; set; }
        public string RichTextBoxFontFamily { get; set; }
        public FontStyle RichTextBoxFontStyle { get; set; }
        public bool IsRgbModeEnabled { get; set; }
        public bool IsRgbTextEnabled { get; set; }
        public int RgbBorderHue { get; set; }
        public Color MenuStrip1BackColor { get; set; }
        public Color MenuStrip2BackColor { get; set; }
        public Color ToolStripTextBox1BackColor { get; set; }
        public Color ToolStripTextBox2BackColor { get; set; }
        public Color iToolStripMenuItemColor { get; set; }

        public ConfigSettings()
        {

        }

        public ConfigSettings(Form1 mainForm)
        {
            RichTextBoxBackColor = mainForm.richTextBox1.BackColor;
            RichTextBoxForeColor = mainForm.richTextBox1.ForeColor;
            RichTextBoxFontSize = mainForm.richTextBox1.SelectionFont != null ? mainForm.richTextBox1.SelectionFont.Size : mainForm.richTextBox1.Font.Size;
            RichTextBoxFontFamily = mainForm.richTextBox1.SelectionFont != null ? mainForm.richTextBox1.SelectionFont.FontFamily.Name : mainForm.richTextBox1.Font.FontFamily.Name;
            RichTextBoxFontStyle = mainForm.richTextBox1.SelectionFont != null ? mainForm.richTextBox1.SelectionFont.Style : mainForm.richTextBox1.Font.Style;
            IsRgbModeEnabled = mainForm.isRgbBorderEnabled;
            IsRgbTextEnabled = mainForm.isRgbTextEnabled;
            RgbBorderHue = mainForm.borderHue;
            MenuStrip1BackColor = mainForm.menuStrip1.BackColor;
            MenuStrip2BackColor = mainForm.menuStrip2.BackColor;
            ToolStripTextBox1BackColor = mainForm.toolStripTextBox1.BackColor;
            ToolStripTextBox2BackColor = mainForm.toolStripTextBox2.BackColor;
            iToolStripMenuItemColor = mainForm.iToolStripMenuItem.BackColor;
        }

        public void SaveToFile(string filePath)
        {
            try
            {
                string json = JsonConvert.SerializeObject(this, Formatting.Indented);
                File.WriteAllText(filePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error saving config settings: " + ex.Message);
            }
        }

        public static ConfigSettings LoadFromFile(string filePath)
        {
            try
            {
                string json = File.ReadAllText(filePath);
                return JsonConvert.DeserializeObject<ConfigSettings>(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error loading config settings: " + ex.Message);
                return null;
            }
        }
    }
}
