using Accord.Video;
using Accord.Video.FFMPEG;
using System.Drawing;
using System.Windows.Forms;
using System;


namespace AvatarPlayer
{
    public partial class Form1 : Form
    {


        private VideoFileSource fileSource1;
        private VideoFileReader videoReader1;
        private VideoFileSource fileSource2;
        private VideoFileReader videoReader2;

        private int currentFrameIndex = 0;


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        
        private void videoSource_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            // Получаем текущее количество кадров в секунду каждого из видео
            double fps1 = (double)videoReader1.FrameRate;
            double fps2 = (double)videoReader2.FrameRate;

            // Переключаем на следующий кадр
            currentFrameIndex++;

            // Определяем откуда выводить следующий кадр
            VideoFileReader currentVideoReader;
            if (IsDivisible(currentFrameIndex, 2))
            {
                currentVideoReader = videoReader2;
            }
            else
            {
                currentVideoReader = videoReader1;
            }

            // Получаем кадр из текущего видео и обновляем изображение на форме
            Bitmap currentFrame = currentVideoReader.ReadVideoFrame();
            pictureBoxVideo.Image = currentFrame;

            // Освобождаем память от старого кадра
            if (currentFrameIndex > 1)
            {
                //currentVideoReader.CloseFrame();
            }

            // При достижении конца видео останавливаем его и перезапускаем показ видео с начала
            //if (currentVideoReader.IsNewFrame())
            //{
            //    currentVideoReader.CloseFrame();
            //    currentVideoReader.Seek(0, SeekOrigin.Begin);
            //}
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            // Останавливаем видео и освобождаем память
            fileSource1.Stop();
            videoReader1.Close();
            videoReader1.Dispose();
            fileSource2.Stop();
            videoReader2.Close();
            videoReader2.Dispose();
            currentFrameIndex = 0;
            pictureBoxVideo.Image = null;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Выбираем файлы видео через диалог открытия файла
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Создаем источники видео из выбранных файлов
                fileSource1 = new VideoFileSource(openFileDialog.FileName);
                videoReader1 = new VideoFileReader();
                videoReader1.Open(openFileDialog.FileName);

                // Переключаем на второй файл и создаем источник видео
                openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    fileSource2 = new VideoFileSource(openFileDialog.FileName);
                    videoReader2 = new VideoFileReader();
                    videoReader2.Open(openFileDialog.FileName);
                }
                else
                {
                    buttonStop_Click(sender, e);
                    return;
                }

                // Устанавливаем обработчик события, который будет вызываться при получении нового кадра
                fileSource1.NewFrame += new NewFrameEventHandler(videoSource_NewFrame);

                // Показываем видео
                fileSource1.Start();
            }
        }

        public bool IsDivisible(int x, int n)
        {
            return (x % n) == 0;
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {

        }
    }
}
