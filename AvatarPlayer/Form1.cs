using Accord.Video;
using Accord.Video.FFMPEG;
using System.Drawing;
using System.Windows.Forms;
using System;


namespace AvatarPlayer
{
    public partial class Form1 : Form
    {

        private VideoFileSource fileSource;
        private VideoFileReader videoReader;


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
        }



        private void videoSource_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            // Обновляем изображение на форме при получении кадра видео
            pictureBoxVideo.Image = (Bitmap)eventArgs.Frame.Clone();
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            // Останавливаем видео
            fileSource.Stop();
            videoReader.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Выбираем файл видео через диалог открытия файла
            //OpenFileDialog openFileDialog = new OpenFileDialog();
            //openFileDialog.Filter = "AVI files (*.avi)|*.avi|MP4 files (*.mp4)|*.mp4";
            //if (openFileDialog.ShowDialog() == DialogResult.OK)
            //{
            string videoFile = @"d:\Left.wmv";
                // Создаем источник видео из выбранного файла
                fileSource = new VideoFileSource(videoFile);
                videoReader = new VideoFileReader();
                videoReader.Open(videoFile);

                // Устанавливаем обработчик события, который будет вызываться при получении нового кадра
                fileSource.NewFrame += new NewFrameEventHandler(videoSource_NewFrame);

                // Показываем видео
                fileSource.Start();
            //}

        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }
    }
}
