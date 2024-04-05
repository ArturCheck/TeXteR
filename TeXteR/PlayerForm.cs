using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using NAudio.Wave;

namespace TeXteR
{
    public partial class PlayerForm : Form, IDisposable
    {
        private IWavePlayer wavePlayer;
        private AudioFileReader audioFileReader;
        private string[] songs = new string[0];
        private int currentSongIndex = 0;
        private bool isPlaying = false;
        private TimeSpan songDuration;
        private TimeSpan currentSongPosition = TimeSpan.Zero;
        private TimeSpan currentSongPositionOnPause = TimeSpan.Zero;
        private Timer textMoveTimer = new Timer();
        private bool shouldRestartSong = false;
        private bool repeatSong = false;

        public PlayerForm()
        {
            InitializeComponent();
            textMoveTimer.Interval = 100;
            textMoveTimer.Start();
            this.FormClosing += PlayerForm_FormClosing;
            wavePlayer = new WaveOutEvent();
            InitializeUI();
        }

        private void PlayerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Dispose();
        }

        private void InitializeUI()
        {
            wavePlayer = new WaveOutEvent();
            MakeRoundButton(button1);
            MakeRoundButton(button2);
            MakeRoundButton(button3);
        }

        private void MakeRoundButton(Button button)
        {
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;

            int radius = Math.Min(button.Width, button.Height) / 2;
            button.Region = new Region(new Rectangle(0, 0, button.Width, button.Height));
            button.Paint += (sender, e) =>
            {
                using (System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath())
                {
                    path.AddEllipse(0, 0, button.Width, button.Height);
                    button.Region = new Region(path);
                }
            };
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StopCurrentSong();

            if (currentSongIndex > 0)
            {
                currentSongIndex--;
            }
            else
            {
                currentSongIndex = songs.Length - 1;
            }

            PlayCurrentSong();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            StopCurrentSong();

            if (currentSongIndex < songs.Length - 1)
            {
                currentSongIndex++;
            }
            else if (songs.Length > 0)
            {
                currentSongIndex = 0;
            }

            currentSongPositionOnPause = TimeSpan.Zero;

            PlayCurrentSong();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (isPlaying)
            {
                PauseCurrentSong();
            }
            else
            {
                ResumeCurrentSong();
            }
        }

        private void PauseCurrentSong()
        {
            isPlaying = false;
            button3.Text = " ▶";

            if (wavePlayer != null)
            {
                wavePlayer.Pause();
            }

            if (audioFileReader != null)
            {
                currentSongPositionOnPause = audioFileReader.CurrentTime;
            }

            timer1.Stop();
        }

        private void ResumeCurrentSong()
        {
            isPlaying = true;
            button3.Text = "I I";

            if (wavePlayer != null)
            {
                wavePlayer.Play();
            }

            if (audioFileReader != null)
            {
                audioFileReader.CurrentTime = currentSongPositionOnPause;
            }

            timer1.Start();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = UKR ? "MP3 файли (*.mp3)|*.mp3|Всі файли (*.*)|*.*" : "MP3 Files (*.mp3)|*.mp3|All files (*.*)|*.*";


            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string selectedPath = Path.GetDirectoryName(openFileDialog.FileName);
                StopCurrentSong();
                InitPlayer(selectedPath);

                if (songs.Length > 0)
                {
                    currentSongIndex = 0;

                    currentSongPath = songs[currentSongIndex];

                    PlayCurrentSong();
                    EnableControls();
                    textBox1.Text = Path.GetFileNameWithoutExtension(currentSongPath);
                }
            }
        }

        private void InitPlayer(string folderPath)
        {
            songs = Directory.GetFiles(folderPath, "*.mp3");

            StopCurrentSong();

            if (songs.Length > 0)
            {
                currentSongIndex = 0;

                currentSongPath = songs[currentSongIndex];

                PlayCurrentSong();
            }
            else
            {
                MessageBox.Show(UKR ? "Вибрана папка не містить MP3 файлів." : "The selected folder does not contain MP3 files.");
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            int newPosition = trackBar1.Value;
            SeekToPosition(newPosition);
        }

        private void SeekToPosition(int newPosition)
        {
            if (wavePlayer != null && audioFileReader != null)
            {
                audioFileReader.CurrentTime = TimeSpan.FromSeconds(newPosition);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (isPlaying)
            {
                int currentPosition = GetCurrentPosition();
                trackBar1.Value = currentPosition;
                label1.Text = $"{FormatTime(currentPosition)} / {FormatTime((int)songDuration.TotalSeconds)}";

                if (currentPosition >= (int)songDuration.TotalSeconds)
                {
                    if (repeatSong)
                    {
                        SeekToPosition(0);
                        PlayCurrentSong();
                    }
                    else
                    {
                        button2.PerformClick();
                    }
                }
            }
        }

        private int GetCurrentPosition()
        {
            if (wavePlayer != null && audioFileReader != null)
            {
                return (int)audioFileReader.CurrentTime.TotalSeconds;
            }
            return 0;
        }

        private void PlayCurrentSong()
        {
            if (songs.Length > 0 && currentSongIndex >= 0 && currentSongIndex < songs.Length)
            {
                isPlaying = true;
                button3.Text = "I I";

                if (wavePlayer.PlaybackState == PlaybackState.Stopped || shouldRestartSong)
                {
                    if (audioFileReader != null)
                    {
                        audioFileReader.Dispose();
                    }

                    audioFileReader = new AudioFileReader(songs[currentSongIndex]);
                    wavePlayer.Init(audioFileReader);
                    shouldRestartSong = false;
                }

                wavePlayer.Play();
                songDuration = audioFileReader.TotalTime;

                audioFileReader.CurrentTime = currentSongPosition;

                trackBar1.Maximum = (int)songDuration.TotalSeconds;
                timer1.Start();
                textBox1.Text = Path.GetFileNameWithoutExtension(songs[currentSongIndex]);
                FitTextInTextBox(textBox1);
            }
            else
            {
                MessageBox.Show(UKR ? "Немає доступних пісень або невірний індекс." : "No available songs or incorrect index.");
                StopCurrentSong();
            }
        }

        private void FitTextInTextBox(TextBox textBox)
        {
            Graphics g = CreateGraphics();
            float fontSize = 10;
            Font font = new Font(textBox.Font.FontFamily, fontSize);

            while (TextFits(textBox, font))
            {
                fontSize++;
                font = new Font(textBox.Font.FontFamily, fontSize);
            }

            textBox.Font = new Font(textBox.Font.FontFamily, fontSize - 1);
        }

        private bool TextFits(TextBox textBox, Font font)
        {
            SizeF textSize = TextRenderer.MeasureText(textBox.Text, font, new Size(textBox.Width, int.MaxValue), TextFormatFlags.WordBreak);
            return textSize.Height < textBox.Height;
        }

        private void StopCurrentSong()
        {
            isPlaying = false;
            button3.Text = " ▶";
            shouldRestartSong = true;

            if (wavePlayer != null)
            {
                wavePlayer.Stop();
            }

            if (audioFileReader != null)
            {
                currentSongPosition = TimeSpan.Zero;
                audioFileReader.Dispose();
                audioFileReader = null;

                textBox1.Text = string.Empty;
            }

            timer1.Stop();
        }

        private string FormatTime(int seconds)
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(seconds);
            return $"{timeSpan.Minutes:D2}:{timeSpan.Seconds:D2}";
        }

        private void EnableControls()
        {
            button1.Enabled = true;
            button2.Enabled = true;
            button3.Enabled = true;
            trackBar1.Enabled = true;
            textBox1.Enabled = true;
            режимиПлейруToolStripMenuItem.Enabled = true;
        }

        public new void Dispose()
        {
            StopCurrentSong();
            wavePlayer?.Dispose();
            audioFileReader?.Dispose();
            textMoveTimer?.Dispose();
            base.Dispose();
        }

        private void повторПісніToolStripMenuItem_Click(object sender, EventArgs e)
        {
            repeatSong = !repeatSong;

            if (repeatSong)
            {
                повторПісніToolStripMenuItem.ForeColor = Color.Green;
            }
            else
            {
                повторПісніToolStripMenuItem.ForeColor = Color.FromArgb(192, 0, 0); ;
            }

        }

        private Random random = new Random();
        private string currentSongPath;

        private void випадковийПорядрокToolStripMenuItem_Click(object sender, EventArgs e)
        {
            StopCurrentSong();
            ShuffleSongs();
            PlayCurrentSong();
        }

        private void ShuffleSongs()
        {
            if (songs.Length > 1)
            {
                int n = songs.Length;
                while (n > 1)
                {
                    n--;
                    int k = random.Next(n + 1);
                    string value = songs[k];
                    songs[k] = songs[n];
                    songs[n] = value;
                }

                if (currentSongIndex >= songs.Length)
                {
                    currentSongIndex = 0;
                }
            }
        }

        //Мова

        private bool UKR = true;

        public void languageUKR(object sender, EventArgs e)
        {
            if (!IsFormDisposed())
            {
                вибратиПапкуЗМузикойToolStripMenuItem.Text = "Вибрати папку з музикой";
                режимиПлейруToolStripMenuItem.Text = "Режими плеєру";
                повторПісніToolStripMenuItem.Text = "Повтор пісні";
                випадковийПорядрокToolStripMenuItem.Text = "Змішаний порядок";
                UKR = true;
            }
        }

        public void languageENG(object sender, EventArgs e) 
        {
            if (!IsFormDisposed())
            {
                вибратиПапкуЗМузикойToolStripMenuItem.Text = "Select Music Folder";
                режимиПлейруToolStripMenuItem.Text = "Player Modes";
                повторПісніToolStripMenuItem.Text = "Repeat Song";
                випадковийПорядрокToolStripMenuItem.Text = "Shuffle Order";
                UKR = false;
            }
        }
        private bool IsFormDisposed()
        {
            return this.IsDisposed;
        }
    }
}