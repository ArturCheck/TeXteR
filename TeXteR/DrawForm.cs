using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace TeXteR
{
    public partial class DrawForm : Form
    {
        private Bitmap backgroundBitmap;
        private Bitmap drawingBitmap;
        private Stack<Bitmap> undoStack;
        private Stack<Bitmap> redoStack;
        private Point? previousPoint;
        private Color brushColor = Color.Black;
        private Pen drawingPen;

        public DrawForm()
        {
            InitializeComponent();
            drawingPen = new Pen(brushColor, brushSize);
            drawingPen.StartCap = LineCap.Round;
            drawingPen.EndCap = LineCap.Round;
            drawingPen.LineJoin = LineJoin.Round;
            backgroundBitmap = new Bitmap(ClientSize.Width, ClientSize.Height);
            drawingBitmap = new Bitmap(ClientSize.Width, ClientSize.Height);
            undoStack = new Stack<Bitmap>();
            redoStack = new Stack<Bitmap>();
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            UpdateStyles();
            this.Resize += DrawForm_Resize;
        }

        private Bitmap temporaryBackgroundBitmap;

        private void DrawForm_Resize(object sender, EventArgs e)
        {
            temporaryBackgroundBitmap = new Bitmap(backgroundBitmap);

            backgroundBitmap = new Bitmap(ClientSize.Width, ClientSize.Height);
            drawingBitmap = new Bitmap(ClientSize.Width, ClientSize.Height);

            using (Graphics g = Graphics.FromImage(backgroundBitmap))
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.DrawImage(temporaryBackgroundBitmap, new Rectangle(0, 0, backgroundBitmap.Width, backgroundBitmap.Height));
            }

            Invalidate();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.Z))
            {
                UndoAction();
                return true;
            }
            else if (keyData == (Keys.Control | Keys.Y))
            {
                RedoAction();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void UndoAction()
        {
            if (undoStack.Count > 0)
            {
                redoStack.Push(new Bitmap(drawingBitmap));
                drawingBitmap = undoStack.Pop();
                Invalidate();
            }
        }

        private void RedoAction()
        {
            if (redoStack.Count > 0)
            {
                undoStack.Push(new Bitmap(drawingBitmap));
                drawingBitmap = redoStack.Pop();
                Invalidate();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.DrawImage(backgroundBitmap, Point.Empty);
            e.Graphics.DrawImage(drawingBitmap, Point.Empty);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            previousPoint = e.Location;
            SaveUndoState();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (previousPoint.HasValue)
            {
                using (var g = Graphics.FromImage(drawingBitmap))
                {
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    g.DrawLine(drawingPen, previousPoint.Value, e.Location);
                }
                previousPoint = e.Location;
                Invalidate();
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            previousPoint = null;
        }

        public Image GetDrawnImage()
        {
            return (Image)drawingBitmap.Clone();
        }

        private void SaveUndoState()
        {
            undoStack.Push(new Bitmap(drawingBitmap));
            redoStack.Clear();
        }

        private void змінитиФонToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();

            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                using (var g = Graphics.FromImage(backgroundBitmap))
                {
                    g.Clear(colorDialog.Color);
                }

                SaveUndoState();

                Invalidate();
            }
        }

        private void змінитиКолірКістіToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();

            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                brushColor = colorDialog.Color;
                drawingPen.Color = brushColor;
            }
        }

        private void зберегтиНаПристрійToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = UKR ? "Зображення (*.png)|*.png|Всі файли (*.*)|*.*" : "Images (*.png)|*.png|All files (*.*)|*.*";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                bool isBackgroundChanged = (backgroundBitmap != null) && !AreBitmapsEqual(drawingBitmap, backgroundBitmap);

                Bitmap combinedImage = new Bitmap(drawingBitmap.Width, drawingBitmap.Height);

                using (Graphics g = Graphics.FromImage(combinedImage))
                {
                    if (isBackgroundChanged)
                    {
                        g.DrawImage(backgroundBitmap, Point.Empty);
                    }

                    g.DrawImage(drawingBitmap, Point.Empty);
                }

                combinedImage.Save(saveFileDialog.FileName, ImageFormat.Png);
            }
        }

        private bool AreBitmapsEqual(Bitmap bmp1, Bitmap bmp2)
        {
            if (bmp1 == null || bmp2 == null || bmp1.Size != bmp2.Size)
            {
                return false;
            }

            for (int x = 0; x < bmp1.Width; x++)
            {
                for (int y = 0; y < bmp1.Height; y++)
                {
                    if (bmp1.GetPixel(x, y) != bmp2.GetPixel(x, y))
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        //Режим професійного малювання

        private void режимПрофіToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int brushSize = 2;

            if (int.TryParse(toolStripTextBox2.Text, out int newSize))
            {
                if (newSize >= 1 && newSize <= 30)
                {
                    brushSize = newSize;
                }
            }

            int cursorSize = brushSize * 2 + 1;

            Bitmap cursorBitmap = new Bitmap(cursorSize, cursorSize);

            using (Graphics g = Graphics.FromImage(cursorBitmap))
            {
                int borderSize = 3;

                using (Pen pen = new Pen(brushColor, borderSize))
                {
                    g.DrawEllipse(pen, 0, 0, cursorSize - 1, cursorSize - 1);
                }
                using (Pen pen = new Pen(brushColor, borderSize))
                {
                    int halfSize = cursorSize / 2;

                    g.DrawLine(pen, halfSize, 0, halfSize, cursorSize - 1);

                    g.DrawLine(pen, 0, halfSize, cursorSize - 1, halfSize);
                }
            }

            Cursor customCursor = new Cursor(cursorBitmap.GetHicon());

            this.Cursor = customCursor;
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            this.Cursor = Cursors.Default;
            base.OnMouseLeave(e);
        }

        //Зміна розміру вікна

        private void StandartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.MaximumSize = new Size(int.MaxValue, int.MaxValue);
            this.WindowState = FormWindowState.Normal;
            this.Size = new Size(600, 600);
            CenterForm();
        }

        private void х1000ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.MaximumSize = new Size(int.MaxValue, int.MaxValue);
            this.WindowState = FormWindowState.Normal;
            this.Size = new Size(1000, 750);
            CenterForm();
        }

        private void toolStripComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (toolStripComboBox1.SelectedItem != null)
            {
                string selectedOption = toolStripComboBox1.SelectedItem.ToString();

                if (selectedOption == "Стандарт")
                {
                    StandartToolStripMenuItem_Click(sender, e);
                }
                else if (selectedOption == "Розширений")
                {
                    х1000ToolStripMenuItem_Click(sender, e);
                }
                else if (selectedOption == "На весь екран")
                {
                    this.WindowState = FormWindowState.Maximized;
                }
                else if (selectedOption == "Standard")
                {
                    StandartToolStripMenuItem_Click(sender, e);
                }
                else if (selectedOption == "Extended")
                {
                    х1000ToolStripMenuItem_Click(sender, e);
                }
                else if (selectedOption == "Full Screen")
                {
                    this.WindowState = FormWindowState.Maximized;
                }

                toolStripComboBox1.Enabled = false;
                toolStripComboBox1.Enabled = true;

                CenterForm();
            }
        }

        private void CenterForm()
        {
            int screenWidth = Screen.PrimaryScreen.WorkingArea.Width;
            int screenHeight = Screen.PrimaryScreen.WorkingArea.Height;

            int centerX = (screenWidth - this.Width) / 2;
            int centerY = (screenHeight - this.Height) / 2;

            this.Location = new Point(centerX, centerY);
        }

        private void toolStripComboBox1_DropDown(object sender, EventArgs e)
        {
            toolStripComboBox1.Items.Clear();
            if (UKR)
            {
                toolStripComboBox1.Items.Add("Стандарт");
                toolStripComboBox1.Items.Add("Розширений");
                toolStripComboBox1.Items.Add("На весь екран");
            }
            else
            {
                toolStripComboBox1.Items.Add("Standard");
                toolStripComboBox1.Items.Add("Extended");
                toolStripComboBox1.Items.Add("Full Screen");
            }
        }

        private void DrawForm_Load(object sender, EventArgs e)
        {
            this.Size = new Size(600, 600);
            CenterForm();
        }

        //Розмір пензля

        private int brushSize = 2;

        private void toolStripTextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }

            if (e.KeyChar != '\b')
            {
                int value;
                if (!int.TryParse(toolStripTextBox2.Text + e.KeyChar, out value) || value < 1 || value > 30)
                {
                    e.Handled = true;
                }
            }
        }

        private void toolStripTextBox2_TextChanged(object sender, EventArgs e)
        {
            if (int.TryParse(toolStripTextBox2.Text, out int newSize))
            {
                if (newSize >= 1 && newSize <= 30)
                {
                    brushSize = newSize;
                    drawingPen.Width = brushSize;
                }
            }
        }

        //Доп кнопки

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            UndoAction();
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            RedoAction();
        }

        private Color originalBrushColor;

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            if (originalBrushColor == Color.Empty)
            {
                originalBrushColor = brushColor;
                brushColor = backgroundBitmap.GetPixel(0, 0);
                drawingPen.Color = brushColor;
                toolStripMenuItem6.ForeColor = Color.FromArgb(0, 64, 0);
            }
            else
            {
                brushColor = originalBrushColor;
                originalBrushColor = Color.Empty;
                drawingPen.Color = brushColor;
                toolStripMenuItem6.ForeColor = Color.Maroon;
            }
        }

        //Мова

        private bool UKR = true;

        public void languageUKR(object sender, EventArgs e)
        {
            if (!IsFormDisposed())
            {
                редагуванняToolStripMenuItem.Text = "Редагування";
                змінитиФонToolStripMenuItem.Text = "Змінити колір фону";
                змінитиКолірКістіToolStripMenuItem.Text = "Змінити колір кісті";
                додатиМалюнокToolStripMenuItem.Text = "Зберегти малюнок";
                режимПрофіToolStripMenuItem.Text = "Режим професійного малювання";
                toolStripTextBox1.Text = "Розмір кісті:";
                toolStripTextBox3.Text = "Розмір вікна:";
                UKR = true;
            }
        }

        public void languageENG(object sender, EventArgs e)
        {
            if (!IsFormDisposed())
            {
                редагуванняToolStripMenuItem.Text = "Edit";
                змінитиФонToolStripMenuItem.Text = "Change Background Color";
                змінитиКолірКістіToolStripMenuItem.Text = "Change Brush Color";
                додатиМалюнокToolStripMenuItem.Text = "Save Image";
                режимПрофіToolStripMenuItem.Text = "Professional Drawing Mode";
                toolStripTextBox1.Text = "Brush Size:";
                toolStripTextBox3.Text = "Window Size:";
                UKR = false;
            }
        }

        private bool IsFormDisposed()
        {
            return this.IsDisposed;
        }

        private void toolStripTextBox1_Click(object sender, EventArgs e)
        {
            toolStripTextBox1.Enabled = false;
            toolStripTextBox1.Enabled = true;
        }

        private void toolStripTextBox3_Click(object sender, EventArgs e)
        {
            toolStripTextBox3.Enabled = false;
            toolStripTextBox3.Enabled = true;
        }
    }
}
