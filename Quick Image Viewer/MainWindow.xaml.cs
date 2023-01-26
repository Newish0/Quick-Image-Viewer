using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ImageMagick;
using Microsoft.Win32;

namespace Quick_Image_Viewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }



        private void LoadImage(string filePath)
        {

            MagickImage image;
            
            try
            {
                image = new MagickImage(filePath);
            }
            catch
            {
                MessageBox.Show("File not supported!");
                return;
            }

            BitmapImage bmp = new BitmapImage();
            Stream stream = new MemoryStream();

            image.Write(stream, MagickFormat.Bmp); // tmp solution

            bmp.BeginInit();
            bmp.StreamSource = stream;
            bmp.EndInit();

            image.Dispose();

            ImageOutput.Source = bmp;
        }

        private void OpenFileHandler(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = false;
            Nullable<bool> result = fileDialog.ShowDialog();
            if (result == true)
            {
                string fileName = fileDialog.FileName;
                FilePathInput.Text = fileName;

                LoadImage(fileName);
            }
        }
    }
}
