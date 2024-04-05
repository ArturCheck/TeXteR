using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;

namespace TeXteR
{
    public partial class Form1 : Form
    {
        private System.Windows.Forms.Timer timer;
        private Timer rgbTextTimer;
        private Timer rgbBorderTimer;

        public bool isRgbTextEnabled = false;
        public bool isRgbBorderEnabled = false;

        public int textHue = 0; 
        public int borderHue = 30;

        public Form1()
        {
            InitializeComponent();
            this.SizeChanged += Form1_SizeChanged;
            richTextBox1.KeyPress += richTextBox1_KeyPress;
            richTextBox1.KeyDown += richTextBox1_KeyDown;
            richTextBox1.KeyUp += richTextBox1_KeyUp;

            timer = new System.Windows.Forms.Timer();
            timer.Interval = 1000;
            timer.Tick += Timer_Tick;

            timer.Start();

            // Таймер для RGB тексту
            rgbTextTimer = new Timer();
            rgbTextTimer.Interval = 100;
            rgbTextTimer.Tick += RgbTextTimer_Tick;

            // Таймер для RGB рамки
            rgbBorderTimer = new Timer();
            rgbBorderTimer.Interval = 100;
            rgbBorderTimer.Tick += RgbBorderTimer_Tick;

            // Запустити таймери
            rgbTextTimer.Start();
            rgbBorderTimer.Start();

            if (!Directory.Exists(audioFolder))
            {
                Directory.CreateDirectory(audioFolder);
            }

            wmp = new WindowsMediaPlayer();

            richTextBox1.AllowDrop = true;
            richTextBox1.DragDrop += richTextBox1_DragDrop;
            richTextBox1.DragEnter += richTextBox1_DragEnter;

            CheckFileArguments();
        }

        private void CheckFileArguments()
        {
            string[] args = Environment.GetCommandLineArgs();

            if (args.Length > 1)
            {
                string filePath = args[1];

                if (Path.GetExtension(filePath).ToLower() == ".txr" && File.Exists(filePath))
                {
                    LoadSettingsFromFile(filePath);
                }
            }
        }

        private void LoadSettingsFromFile(string filePath)
        {
            RichTextBoxSettings richTextBoxSettings = RichTextBoxSettings.LoadFromFile(filePath);
            if (richTextBoxSettings != null)
            {
                openedFileName = filePath;
                ApplySettings(richTextBoxSettings);
            }
        }

        private string openedFileName;

        private void ApplySettings(RichTextBoxSettings richTextBoxSettings)
        {
            richTextBox1.Rtf = richTextBoxSettings.RichTextBoxRtf;
            richTextBox1.BackColor = richTextBoxSettings.RichTextBoxBackColor;

            isRgbTextEnabled = richTextBoxSettings.IsRgbTextEnabled;

            richTextBox1.SelectionStart = richTextBox1.TextLength;
            richTextBox1.ScrollToCaret();

            if (!string.IsNullOrEmpty(openedFileName))
            {
                string fileNameWithExtension = Path.GetFileName(openedFileName);
                toolStripTextBox3.Text = fileNameWithExtension;
                toolStripTextBox3.ForeColor = Color.Green;
                toolStripTextBox3.ReadOnly = true;

                CheckAudioFilesInText();
            }
        }

        private void richTextBox1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void richTextBox1_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);

            if (files != null && files.Length > 0)
            {
                InsertFiles(files);
            }
        }

        private void InsertFiles(string[] files)
        {
            foreach (string filePath in files)
            {
                string fileExtension = Path.GetExtension(filePath).ToLower();
                string audioFileName = Path.GetFileName(filePath);

                if (fileExtension == ".mp3" || fileExtension == ".wav")
                {
                    if (!audioFiles.ContainsKey(audioFileName))
                    {
                        // Зберегти аудіофайл в папці AudioFiles
                        string savePath = Path.Combine(audioFolder, audioFileName);
                        File.Copy(filePath, savePath, true);

                        audioFiles.Add(audioFileName, savePath); // Зберегти шлях до файлу в словнику

                        InsertAudioText(audioFileName);
                    }
                    else
                    {
                        InsertAudioText(audioFileName);
                    }
                }
                else if (fileExtension == ".bmp" || fileExtension == ".jpg" || fileExtension == ".png" || fileExtension == ".gif")
                {
                    richTextBox1.SelectedText = Environment.NewLine;
                    richTextBox1.SelectionAlignment = HorizontalAlignment.Center;

                    Clipboard.SetImage(Image.FromFile(filePath));
                    richTextBox1.Paste();
                    PasteInf();
                }
            }
        }

        public HorizontalAlignment GetLineAlignment(int lineNumber)
        {
            string lineText = richTextBox1.Lines.Length > lineNumber ? richTextBox1.Lines[lineNumber] : "";

            if (!string.IsNullOrEmpty(lineText))
            {
                char firstChar = lineText[0];
                if (char.IsLetterOrDigit(firstChar))
                {
                    return HorizontalAlignment.Left;
                }
                else if (char.IsWhiteSpace(firstChar))
                {
                    return HorizontalAlignment.Center;
                }
                else
                {
                    return HorizontalAlignment.Right;
                }
            }
            return HorizontalAlignment.Left;
        }

        private void RgbTextTimer_Tick(object sender, EventArgs e)
        {
            if (isRgbTextEnabled)
            {
                textHue = (textHue + 1) % 360; // Швидкість 1
                Color textColor = ColorFromHSL(textHue, 1.0, 0.5);
                richTextBox1.ForeColor = textColor;
            }
        }

        private void RgbBorderTimer_Tick(object sender, EventArgs e)
        {
            if (isRgbBorderEnabled)
            {
                borderHue = (borderHue + 1) % 360;  // Швидкість 1
                Color menuBackColor = ColorFromHSL(borderHue, 1.0, 0.5);
                using (Graphics g = richTextBox1.CreateGraphics())
                {
                    menuStrip1.BackColor = menuBackColor;
                    menuStrip2.BackColor = menuBackColor;
                    toolStripTextBox1.BackColor = menuBackColor;
                    toolStripTextBox2.BackColor = menuBackColor;
                    iToolStripMenuItem.BackColor = menuBackColor;
                    this.Invalidate();
                }
            }
        }

        private Color ColorFromHSL(double hue, double saturation, double lightness)
        {
            double chroma = (1 - Math.Abs(2 * lightness - 1)) * saturation;
            double huePrime = hue / 60;
            double secondComponent = chroma * (1 - Math.Abs(huePrime % 2 - 1));

            double red = 0, green = 0, blue = 0;

            if (huePrime >= 0 && huePrime < 1)
            {
                red = chroma;
                green = secondComponent;
            }
            else if (huePrime >= 1 && huePrime < 2)
            {
                red = secondComponent;
                green = chroma;
            }
            else if (huePrime >= 2 && huePrime < 3)
            {
                green = chroma;
                blue = secondComponent;
            }
            else if (huePrime >= 3 && huePrime < 4)
            {
                green = secondComponent;
                blue = chroma;
            }
            else if (huePrime >= 4 && huePrime < 5)
            {
                red = secondComponent;
                blue = chroma;
            }
            else if (huePrime >= 5 && huePrime < 6)
            {
                red = chroma;
                blue = secondComponent;
            }

            double lightnessAdjustment = lightness - chroma / 2;
            red += lightnessAdjustment;
            green += lightnessAdjustment;
            blue += lightnessAdjustment;

            int r = (int)(red * 255);
            int g = (int)(green * 255);
            int b = (int)(blue * 255);

            return Color.FromArgb(r, g, b);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            DateTime currentTime = DateTime.Now;

            toolStripTextBox1.Text = currentTime.ToString("HH:mm:ss");

            toolStripTextBox2.Text = currentTime.ToString("dd.MM.yyyy");
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            richTextBox1.BackColor = Color.LightGray;
            richTextBox1.ForeColor = Color.Black;

            int leftIndent = 5;
            richTextBox1.SelectionIndent = leftIndent;

            LoadLanguageSetting();

            initialRtf = richTextBox1.Rtf;
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            richTextBox1.Size = new Size(this.Width - 16, this.Height - 39);
        }

        private void richTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void richTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                int currentLine = richTextBox1.GetLineFromCharIndex(richTextBox1.SelectionStart);
                int nextLineIndex = richTextBox1.GetFirstCharIndexFromLine(currentLine + 1);

                if (nextLineIndex != -1)
                {
                    richTextBox1.SelectionStart = nextLineIndex;
                }
                else
                {
                    richTextBox1.AppendText(Environment.NewLine);
                    richTextBox1.SelectionStart = richTextBox1.Text.Length;
                }

                e.Handled = true;
            }
        }

        private void richTextBox1_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void richTextBox1_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void richTextBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Point screenPoint = richTextBox1.PointToScreen(e.Location);
                contextMenuStrip1.Show(screenPoint);
            }
        }

        //Меню редагування

        //Файл

        private void зберегтиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = UKR ? "Файли редактора (*.txr)|*.txr" : "Editor files (*.txr)|*.txr";

            if (toolStripTextBox3.Text == "Введіть назву файлу" || toolStripTextBox3.Text == "Enter the file name")
            {
                saveFileDialog.FileName = "";
            }
            else
            {
                saveFileDialog.FileName = toolStripTextBox3.Text;
            }
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                RichTextBoxSettings richTextSettings = new RichTextBoxSettings(this);
                richTextSettings.SaveToFile(saveFileDialog.FileName);
            }
        }

        private void загрузитиToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = UKR ? "Файли редактора (*.txr)|*.txr" : "Editor files (*.txr)|*.txr";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                RichTextBoxSettings richTextBoxSettings = RichTextBoxSettings.LoadFromFile(openFileDialog.FileName);
                if (richTextBoxSettings != null)
                {
                    richTextBox1.Rtf = richTextBoxSettings.RichTextBoxRtf;
                    richTextBox1.BackColor = richTextBoxSettings.RichTextBoxBackColor;

                    isRgbTextEnabled = richTextBoxSettings.IsRgbTextEnabled;

                    richTextBox1.SelectionStart = richTextBox1.TextLength;
                    richTextBox1.ScrollToCaret();

                    string fileNameWithExtension = Path.GetFileName(openFileDialog.FileName);
                    toolStripTextBox3.Text = fileNameWithExtension;
                    toolStripTextBox3.ForeColor = Color.Green;
                    toolStripTextBox3.ReadOnly = true;

                    CheckAudioFilesInText();

                    initialRtf = richTextBox1.Rtf;
                }
            }
        }

        private void CheckAudioFilesInText()
        {
            foreach (string lineText in richTextBox1.Lines)
            {
                if (audioFiles.ContainsKey(lineText))
                {
                    continue;
                }
                string audioFilePath = Path.Combine(audioFolder, lineText);
                if (File.Exists(audioFilePath) && (audioFilePath.EndsWith(".mp3") || audioFilePath.EndsWith(".wav")))
                {
                    audioFiles.Add(lineText, audioFilePath);
                }
            }
        }

        //Конфіг

        private void зберегтиToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = UKR ? "Файли конфігурації (*.cfg)|*.cfg" : "Configuration files (*.cfg)|*.cfg";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                ConfigSettings configSettings = new ConfigSettings(this);
                configSettings.SaveToFile(saveFileDialog.FileName);
            }
        }

        private void загрузитиToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = UKR ? "Файли конфігурації (*.cfg)|*.cfg" : "Configuration files (*.cfg)|*.cfg";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                ConfigSettings configSettings = ConfigSettings.LoadFromFile(openFileDialog.FileName);
                if (configSettings != null)
                {
                    // Встановлення параметрів для richTextBox1
                    richTextBox1.BackColor = configSettings.RichTextBoxBackColor;
                    richTextBox1.ForeColor = configSettings.RichTextBoxForeColor;

                    // Створення нового об'єкта Font з параметрами шрифту
                    Font newFont = new Font(configSettings.RichTextBoxFontFamily, configSettings.RichTextBoxFontSize, configSettings.RichTextBoxFontStyle);
                    richTextBox1.Font = newFont;

                    // Встановлення параметрів для меню та текстових полів
                    menuStrip1.BackColor = configSettings.MenuStrip1BackColor;
                    menuStrip2.BackColor = configSettings.MenuStrip2BackColor;
                    toolStripTextBox1.BackColor = configSettings.ToolStripTextBox1BackColor;
                    toolStripTextBox2.BackColor = configSettings.ToolStripTextBox2BackColor;
                    iToolStripMenuItem.BackColor = configSettings.iToolStripMenuItemColor;

                    // Оновлення RGB параметрів
                    isRgbBorderEnabled = configSettings.IsRgbModeEnabled;
                    borderHue = configSettings.RgbBorderHue;
                    isRgbTextEnabled = configSettings.IsRgbTextEnabled;
                }
            }
        }

        //Меню персоналізація

        private void темнаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.BackColor = Color.FromArgb(64, 64, 64);
            richTextBox1.ForeColor = Color.White;
        }

        private void світлаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.BackColor = Color.LightGray;
            richTextBox1.ForeColor = Color.Black;
        }

        private void змінаШрифтуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontDialog fontDialog = new FontDialog();

            fontDialog.Font = richTextBox1.SelectionFont;
            fontDialog.Color = richTextBox1.SelectionColor;

            if (fontDialog.ShowDialog() == DialogResult.OK)
            {
                Color currentTextColor = richTextBox1.SelectionColor;

                richTextBox1.SelectionFont = fontDialog.Font;

                richTextBox1.SelectionColor = currentTextColor;
            }
        }

        //Меню фото

        private void додатиФотоЗФайлівToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = UKR ? "Файли зображень (*.bmp;*.jpg;*.png;*.gif)|*.bmp;*.jpg;*.png;*.gif|Всі файли (*.*)|*.*" : "Image files (*.bmp;*.jpg;*.png;*.gif)|*.bmp;*.jpg;*.png;*.gif|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string imagePath = openFileDialog.FileName;
                if (File.Exists(imagePath))
                {
                    richTextBox1.SelectedText = Environment.NewLine;
                    richTextBox1.SelectionAlignment = HorizontalAlignment.Center;

                    Clipboard.SetImage(Image.FromFile(imagePath));
                    richTextBox1.Paste();
                    PasteInf();
                }
            }
        }

        private void PasteInf ()
        {
            int currentLine = richTextBox1.GetLineFromCharIndex(richTextBox1.SelectionStart);
            int nextLineIndex = richTextBox1.GetFirstCharIndexFromLine(currentLine + 1);

            if (nextLineIndex != -1)
            {
                richTextBox1.SelectionStart = nextLineIndex;
            }
            else
            {
                richTextBox1.AppendText(Environment.NewLine);
                richTextBox1.SelectionStart = richTextBox1.Text.Length;
            }
        }

        private void richTextBox1_MouseDown(object sender, MouseEventArgs e)
        {
            int charIndex = richTextBox1.GetCharIndexFromPosition(e.Location);
            int lineIndex = richTextBox1.GetLineFromCharIndex(charIndex);
            int start = richTextBox1.SelectionStart;
            int length = richTextBox1.SelectionLength;

            if (length > 0 && richTextBox1.SelectedRtf.Contains(@"{\pict") && e.Button == MouseButtons.Left)
            {
                int photoWidth = GetPhotoWidth();
                int photoHeight = GetPhotoHeight();

                int photoX = richTextBox1.GetPositionFromCharIndex(start).X;
                int photoY = richTextBox1.GetPositionFromCharIndex(start).Y;

                Rectangle photoBounds = new Rectangle(photoX, photoY, photoWidth, photoHeight);

                if (photoBounds.Contains(e.Location))
                {
                    Cursor = Cursors.Default;
                }
            }
            else
            {
                string lineText = "";

                if (lineIndex >= 0 && lineIndex < richTextBox1.Lines.Length)
                {
                    lineText = richTextBox1.Lines[lineIndex];
                }

                if (audioFiles.ContainsKey(lineText))
                {
                    richTextBox1.SelectionStart = richTextBox1.GetFirstCharIndexFromLine(lineIndex) + lineText.Length;
                    richTextBox1.SelectionLength = 0;

                    Cursor = Cursors.Hand;
                    System.Threading.Thread.Sleep(250);

                    Cursor = Cursors.Default;

                    if (isAudioPlaying)
                    {
                        StopAudio();
                        isAudioPlaying = false;
                    }
                    else
                    {
                        PlayAudio(lineText);
                        isAudioPlaying = true;
                    }
                }
            }
        }

        private int GetPhotoWidth()
        {
            if (richTextBox1.SelectedRtf.Contains(@"{\pict"))
            {
                IDataObject data = Clipboard.GetDataObject();
                if (data != null && data.GetDataPresent(DataFormats.Bitmap))
                {
                    Bitmap image = (Bitmap)data.GetData(DataFormats.Bitmap);
                    return image.Width;
                }
            }

            return 0;
        }

        private int GetPhotoHeight()
        {
            if (richTextBox1.SelectedRtf.Contains(@"{\pict"))
            {
                IDataObject data = Clipboard.GetDataObject();
                if (data != null && data.GetDataPresent(DataFormats.Bitmap))
                {
                    Bitmap image = (Bitmap)data.GetData(DataFormats.Bitmap);
                    return image.Height;
                }
            }

            return 0;
        }

        private DrawForm drawFormInstance;

        private void додатиНовеФотоToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (drawFormInstance == null || drawFormInstance.IsDisposed)
            {
                drawFormInstance = new DrawForm();
                drawFormInstance.Show(this);
                if (UKR)
                {
                    drawFormInstance.languageUKR(sender, e);
                }
                else
                {
                    drawFormInstance.languageENG(sender, e);
                }
            }
        }

        //Меню розміщення елементів

        private void поЦентруToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionAlignment = HorizontalAlignment.Center;
        }

        private void зліваToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionAlignment = HorizontalAlignment.Left;
        }

        private void справаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionAlignment = HorizontalAlignment.Right;
        }


        //Меню своя кастомізація

        private void колірТекстуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();

            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.SelectionColor = colorDialog.Color;
            }
        }

        private void фонToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();

            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                richTextBox1.BackColor = colorDialog.Color;
            }
        }

        private void рамкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                Color menuBackColor = colorDialog.Color;

                menuStrip1.BackColor = menuBackColor;
                menuStrip2.BackColor = menuBackColor;
                toolStripTextBox1.BackColor = menuBackColor;
                toolStripTextBox2.BackColor = menuBackColor;
                iToolStripMenuItem.BackColor = menuBackColor;

                Color textColor = Color.FromArgb(240, 240, 240);

                if (IsDarkColor(menuBackColor))
                {
                    редагуванняToolStripMenuItem.ForeColor = textColor;
                    змінитиШрифтToolStripMenuItem.ForeColor = textColor;
                    фотоToolStripMenuItem.ForeColor = textColor;
                    розміщенняТекстуToolStripMenuItem.ForeColor = textColor;
                    свояКастомізаціяToolStripMenuItem.ForeColor = textColor;
                    режимRGBToolStripMenuItem.ForeColor = textColor;
                    аудіоToolStripMenuItem.ForeColor = textColor;
                    моваToolStripMenuItem.ForeColor = textColor;
                    toolStripTextBox1.ForeColor = textColor;
                    toolStripTextBox2.ForeColor = textColor;
                    iToolStripMenuItem.ForeColor = textColor;
                }
                else
                {
                    редагуванняToolStripMenuItem.ForeColor = SystemColors.ControlText;
                    змінитиШрифтToolStripMenuItem.ForeColor = SystemColors.ControlText;
                    фотоToolStripMenuItem.ForeColor = SystemColors.ControlText;
                    розміщенняТекстуToolStripMenuItem.ForeColor = SystemColors.ControlText;
                    свояКастомізаціяToolStripMenuItem.ForeColor = SystemColors.ControlText;
                    режимRGBToolStripMenuItem.ForeColor = SystemColors.ControlText;
                    аудіоToolStripMenuItem.ForeColor = SystemColors.ControlText;
                    моваToolStripMenuItem.ForeColor = SystemColors.ControlText;
                    toolStripTextBox1.ForeColor = SystemColors.ControlText;
                    toolStripTextBox2.ForeColor = SystemColors.ControlText;
                    iToolStripMenuItem.ForeColor = SystemColors.ControlText;
                }

                this.Invalidate();
            }
        }

        private bool IsDarkColor(Color color)
        {
            double luminance = (0.299 * color.R + 0.587 * color.G + 0.114 * color.B) / 255;
            return luminance < 0.5;
        }

        //Меню RGB режим

        //Для тексту
        private void вклToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isRgbTextEnabled = true;
            rgbTextTimer.Start();
        }

        private void виклToolStripMenuItem_Click(object sender, EventArgs e)
        {
            isRgbTextEnabled = false;
            rgbTextTimer.Stop();
        }

        //Для рамки
        private void вклToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            isRgbBorderEnabled = true;
            rgbBorderTimer.Start();
        }

        private void виклToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            isRgbBorderEnabled = false;
            rgbBorderTimer.Stop();
        }

        //Меню аудіо

        private PlayerForm playerFormInstance;

        private void плеєрToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (playerFormInstance == null || playerFormInstance.IsDisposed)
            {
                playerFormInstance = new PlayerForm();
                playerFormInstance.Show(this);
                if (UKR)
                {
                    playerFormInstance.languageUKR(sender, e);
                }
                else
                {
                    playerFormInstance.languageENG(sender, e);
                }
            }
        }

        private Dictionary<string, string> audioFiles = new Dictionary<string, string>();
        private string currentPlayingAudioFile;
        private string audioFolder = Path.Combine(Application.StartupPath, "AudioFiles");
        private bool isAudioPlaying = false;
        private WindowsMediaPlayer wmp;


        private void InsertAudioText(string audioFileName)
        {
            richTextBox1.SelectedText = Environment.NewLine;
            richTextBox1.SelectionAlignment = HorizontalAlignment.Center;

            richTextBox1.SelectionColor = Color.FromArgb(192, 64, 0);
            richTextBox1.SelectionFont = new System.Drawing.Font(richTextBox1.Font, System.Drawing.FontStyle.Underline);
            richTextBox1.SelectedText = audioFileName;

            richTextBox1.SelectionColor = richTextBox1.ForeColor;
            richTextBox1.SelectionFont = richTextBox1.Font;
            PasteInf();
        }

        private void додатиАудіоToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = UKR ? "Аудіо файли (*.mp3;*.wav)|*.mp3;*.wav|Всі файли (*.*)|*.*" : "Audio files (*.mp3;*.wav)|*.mp3;*.wav|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string audioFilePath = openFileDialog.FileName;
                string audioFileName = Path.GetFileName(audioFilePath);

                if (!audioFiles.ContainsKey(audioFileName))
                {
                    // Зберегти аудіофайл в папці AudioFiles
                    string savePath = Path.Combine(audioFolder, audioFileName);
                    File.Copy(audioFilePath, savePath, true);

                    audioFiles.Add(audioFileName, savePath); // Зберегти шлях до файлу в словнику

                    InsertAudioText(audioFileName);
                }
                else
                {
                    InsertAudioText(audioFileName);
                }
                richTextBox1.TextChanged += richTextBox1_TextChanged;
            }
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            List<string> filesToRemove = new List<string>();

            foreach (var audioFile in audioFiles)
            {
                if (!richTextBox1.Text.Contains(audioFile.Key))
                {
                    filesToRemove.Add(audioFile.Key);
                    StopAudio();
                }
            }
        }

        private void PlayAudio(string audioFileName)
        {
            if (wmp != null)
            {
                StopAudio();
            }

            try
            {
                currentPlayingAudioFile = audioFileName;
                string filePath = audioFiles[audioFileName];

                wmp.URL = filePath;
                wmp.controls.play();
                wmp.PlayStateChange += Wmp_PlayStateChange;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error playing audio: {ex}");
            }
        }

        private void Wmp_PlayStateChange(int newState)
        {
            if (newState == (int)WMPPlayState.wmppsStopped)
            {
                isAudioPlaying = false;
            }
        }

        private void StopAudio()
        {
            if (wmp != null)
            {
                wmp.controls.stop();
                wmp.PlayStateChange -= Wmp_PlayStateChange;
            }
        }

        //Меню мова

        private bool UKR = true;

        private void українськаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (drawFormInstance != null)
            {
                drawFormInstance.languageUKR(sender, e);
            }
            else if (playerFormInstance != null)
            {
                playerFormInstance.languageUKR(sender, e);
            }
            else if (informationInstance != null)
            {
                informationInstance.languageUKR(sender, e);
            }
            SetLanguageUkrainian();
            SaveLanguageSetting(true);
        }

        private void англійськаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (drawFormInstance != null)
            {
                drawFormInstance.languageENG(sender, e);
            }
            else if (playerFormInstance != null)
            {
                playerFormInstance.languageENG(sender, e);
            }
            else if (informationInstance != null)
            {
                informationInstance.languageENG(sender, e);
            }
            SetLanguageEnglish();
            SaveLanguageSetting(false);
        }

        private void SaveLanguageSetting(bool isUkrainian)
        {
            try
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);

                if (config.AppSettings.Settings.AllKeys.Contains("Language"))
                {
                    config.AppSettings.Settings["Language"].Value = isUkrainian ? "Ukrainian" : "English";
                }
                else
                {
                    config.AppSettings.Settings.Add("Language", isUkrainian ? "Ukrainian" : "English");
                }

                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");
            }
            catch (ConfigurationErrorsException)
            {
                
            }
        }
        private void LoadLanguageSetting()
        {
            try
            {
                Configuration config = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath);

                if (config.AppSettings.Settings["Language"] != null)
                {
                    string languageSetting = config.AppSettings.Settings["Language"].Value;

                    if (languageSetting == "Ukrainian")
                    {
                        SetLanguageUkrainian();
                    }
                    else if (languageSetting == "English")
                    {
                        SetLanguageEnglish();
                    }
                }
                else
                {
                    SetLanguageUkrainian();
                }
            }
            catch (ConfigurationErrorsException)
            {
                
            }
        }

        private void SetLanguageUkrainian()
        {
            редагуванняToolStripMenuItem.Text = "Редагування";
            зберегтиToolStripMenuItem.Text = "Файл";
            зберегтиToolStripMenuItem1.Text = "Зберегти";
            завантажитиToolStripMenuItem.Text = "Загрузити";
            загрузитиToolStripMenuItem.Text = "Конфігурація";
            зберегтиToolStripMenuItem2.Text = "Зберегти";
            загрузитиToolStripMenuItem1.Text = "Загрузити";
            змінитиШрифтToolStripMenuItem.Text = "Персоналізація";
            змінаШрифтуToolStripMenuItem.Text = "Зміна шрифту";
            змінаТемиToolStripMenuItem.Text = "Зміна теми";
            темнаToolStripMenuItem.Text = "Темна";
            світлаToolStripMenuItem.Text = "Світла";
            фотоToolStripMenuItem.Text = "Фото";
            додатиФотоЗФайлівToolStripMenuItem.Text = "Додати фото";
            додатиНовеФотоToolStripMenuItem.Text = "Малювання";
            аудіоToolStripMenuItem.Text = "Аудіо";
            додатиАудіоToolStripMenuItem.Text = "Додати аудіо";
            плеєрToolStripMenuItem.Text = "Плеєр";
            розміщенняТекстуToolStripMenuItem.Text = "Розміщення тексту";
            поЦентруToolStripMenuItem.Text = "По центру";
            зліваToolStripMenuItem.Text = "З краю";
            ліваToolStripMenuItem.Text = "Лівого";
            праваToolStripMenuItem.Text = "Правого";
            свояКастомізаціяToolStripMenuItem.Text = "Власне налаштування";
            колірТекстуToolStripMenuItem.Text = "Колір тексту";
            колірЗадньогоФонуToolStripMenuItem.Text = "Колір фону або рамок";
            фонToolStripMenuItem1.Text = "Фон";
            рамкаToolStripMenuItem.Text = "Рамка";
            режимRGBToolStripMenuItem.Text = "Режим RGB";
            текстToolStripMenuItem.Text = "Текст";
            вклToolStripMenuItem.Text = "Вкл";
            виклToolStripMenuItem.Text = "Викл";
            фонToolStripMenuItem.Text = "Рамка";
            вклToolStripMenuItem1.Text = "Вкл";
            виклToolStripMenuItem1.Text = "Викл";
            моваToolStripMenuItem.Text = "Мова";
            українськаToolStripMenuItem.Text = "Українська";
            англійськаToolStripMenuItem.Text = "Англійська";
            змінитиРозмірШрифтуToolStripMenuItem.Text = "Змінити шрифт";
            змінитиКолірToolStripMenuItem.Text = "Змінити колір";
            змінитиФонToolStripMenuItem.Text = "Розміщення тексту";
            поЦентруToolStripMenuItem1.Text = "По центру";
            зліваToolStripMenuItem1.Text = "Зліва";
            справаToolStripMenuItem.Text = "Справа";
            додатиФотоToolStripMenuItem.Text = "Додати фото";
            додатиАудіоToolStripMenuItem1.Text = "Додати аудіо";
            if (toolStripTextBox3.Text == "Enter the file name")
            {
                toolStripTextBox3.Text = "Введіть назву файлу";
            }
            UKR = true;
        }

        private void SetLanguageEnglish()
        {
            редагуванняToolStripMenuItem.Text = "Edit";
            зберегтиToolStripMenuItem.Text = "File";
            зберегтиToolStripMenuItem1.Text = "Save";
            завантажитиToolStripMenuItem.Text = "Load";
            загрузитиToolStripMenuItem.Text = "Config";
            зберегтиToolStripMenuItem2.Text = "Save";
            загрузитиToolStripMenuItem1.Text = "Load";
            змінитиШрифтToolStripMenuItem.Text = "Personalization";
            змінаШрифтуToolStripMenuItem.Text = "Change Font";
            змінаТемиToolStripMenuItem.Text = "Change Theme";
            темнаToolStripMenuItem.Text = "Dark";
            світлаToolStripMenuItem.Text = "Light";
            фотоToolStripMenuItem.Text = "Photo";
            додатиФотоЗФайлівToolStripMenuItem.Text = "Add Photo";
            додатиНовеФотоToolStripMenuItem.Text = "Draw";
            аудіоToolStripMenuItem.Text = "Audio";
            додатиАудіоToolStripMenuItem.Text = "Add Audio";
            плеєрToolStripMenuItem.Text = "Player";
            розміщенняТекстуToolStripMenuItem.Text = "Text Alignment";
            поЦентруToolStripMenuItem.Text = "Center";
            зліваToolStripMenuItem.Text = "From the edge";
            ліваToolStripMenuItem.Text = "Left";
            праваToolStripMenuItem.Text = "Right";
            свояКастомізаціяToolStripMenuItem.Text = "Own customization";
            колірТекстуToolStripMenuItem.Text = "Text Color";
            колірЗадньогоФонуToolStripMenuItem.Text = "Background or Frames Color";
            фонToolStripMenuItem1.Text = "Background";
            рамкаToolStripMenuItem.Text = "Frames";
            режимRGBToolStripMenuItem.Text = "RGB Mode";
            текстToolStripMenuItem.Text = "Text";
            вклToolStripMenuItem.Text = "On";
            виклToolStripMenuItem.Text = "Off";
            фонToolStripMenuItem.Text = "Frames";
            вклToolStripMenuItem1.Text = "On";
            виклToolStripMenuItem1.Text = "Off";
            моваToolStripMenuItem.Text = "Language";
            українськаToolStripMenuItem.Text = "Ukrainian";
            англійськаToolStripMenuItem.Text = "English";
            змінитиРозмірШрифтуToolStripMenuItem.Text = "Change Font";
            змінитиКолірToolStripMenuItem.Text = "Change Color";
            змінитиФонToolStripMenuItem.Text = "Text Alignment";
            поЦентруToolStripMenuItem1.Text = "Center";
            зліваToolStripMenuItem1.Text = "Left";
            справаToolStripMenuItem.Text = "Right";
            додатиФотоToolStripMenuItem.Text = "Add Photo";
            додатиАудіоToolStripMenuItem1.Text = "Add Audio";
            if (toolStripTextBox3.Text == "Введіть назву файлу")
            {
                toolStripTextBox3.Text = "Enter the file name";
            }
            UKR = false;
        }

        private void toolStripTextBox2_Click(object sender, EventArgs e)
        {
            toolStripTextBox2.Enabled = false;
            toolStripTextBox2.Enabled = true;
        }

        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {
            toolStripTextBox1.Enabled = false;
            toolStripTextBox1.Enabled = true;
        }

        //Інформація

        private Information informationInstance;

        private void iToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (informationInstance == null || informationInstance.IsDisposed)
            {
                informationInstance = new Information();
                informationInstance.Show(this);
                if (UKR)
                {
                    informationInstance.languageUKR(sender, e);
                }
                else
                {
                    informationInstance.languageENG(sender, e);
                }
            }
        }

        private void toolStripTextBox3_Click(object sender, EventArgs e)
        {
            if (toolStripTextBox3.Text == "Введіть назву файлу" || toolStripTextBox3.Text == "Enter the file name")
            {
                toolStripTextBox3.ForeColor = Color.Black;
                toolStripTextBox3.Text = "";
            }
        }

        private void toolStripTextBox3_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(toolStripTextBox3.Text))
            {
                toolStripTextBox3.Text = (UKR) ? "Введіть назву файлу" : "Enter the file name";
                toolStripTextBox3.ForeColor = Color.Gray;
            }
        }

        private string initialRtf;

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (richTextBox1.Text == "" || richTextBox1.Rtf == initialRtf)
            {
                
            }
            else
            {
                string fileNameWithExtension = Path.GetFileName(toolStripTextBox3.Text);

                string promptText;
                if (toolStripTextBox3.Text == "Введіть назву файлу" || toolStripTextBox3.Text == "Enter the file name")
                {
                    promptText = (UKR) ? "Зберегти зміни в файлі?" : "Save changes to the file?";
                }
                else
                {
                    promptText = (UKR) ? $"Зберегти нові зміни у файлі ({fileNameWithExtension})?" : $"Save the new changes to the file ({fileNameWithExtension})?";
                }

                DialogResult result = MessageBox.Show(
                    promptText,
                    "Confirmation",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

                if (result == DialogResult.Yes)
                {
                    SaveChanges();
                }
            }
        }

        private void SaveChanges()
        {
            зберегтиToolStripMenuItem_Click(this, EventArgs.Empty);
        }
    }
}