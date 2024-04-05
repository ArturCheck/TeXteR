using System;
using System.Drawing;
using System.IO;
using Newtonsoft.Json;
using System.Windows.Forms;
using TeXteR;

[Serializable]
public class RichTextBoxSettings
{
    public string RichTextBoxRtf { get; set; }
    public Color RichTextBoxBackColor { get; set; }
    public bool IsRgbTextEnabled { get; set; }

    public RichTextBoxSettings()
    {

    }

    public RichTextBoxSettings(Form1 mainForm)
    {
        RichTextBoxRtf = mainForm.richTextBox1.Rtf;
        RichTextBoxBackColor = mainForm.richTextBox1.BackColor;
        IsRgbTextEnabled = mainForm.isRgbTextEnabled;
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

    public static RichTextBoxSettings LoadFromFile(string filePath)
    {
        try
        {
            string json = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<RichTextBoxSettings>(json);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error loading config settings: " + ex.Message);
            return null;
        }
    }
}

