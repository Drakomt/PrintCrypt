using System.Drawing;
using System.Windows.Media.Imaging;
using System.Windows;
using System;

namespace PrintCrypt
{
    public partial class MainWindow : Window
    {
        public MainWindow(string qrCodeContent, Image qrCodeImage, bool showQRCodeImage)
        {
            InitializeComponent();

            KeyTextBox.Text = qrCodeContent;

            if (showQRCodeImage)
                QRCodeImage.Source = ConvertImageToBitmapImage(qrCodeImage);
            else
                QRCodeImage.Visibility = Visibility.Collapsed;
        }

        private BitmapImage ConvertImageToBitmapImage(Image image)
        {
            // Convert the System.Drawing.Image to BitmapImage
            Bitmap bitmap = new Bitmap(image);
            IntPtr hBitmap = bitmap.GetHbitmap();
            BitmapSource bitmapSource = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                hBitmap,
                IntPtr.Zero,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());
            bitmapSource.Freeze();
            bitmap.Dispose();

            // Return the BitmapImage
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
            bitmapImage.StreamSource = null;
            bitmapImage.UriSource = null;
            bitmapImage.DecodePixelWidth = bitmapSource.PixelWidth;
            bitmapImage.DecodePixelHeight = bitmapSource.PixelHeight;
            bitmapImage.EndInit();

            return bitmapImage;
        }
    }
}








//using PrintCrypt_Base;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Data;
//using System.Windows.Documents;
//using System.Windows.Forms;
//using System.Windows.Input;
//using System.Windows.Media;
//using System.Windows.Media.Imaging;
//using System.Windows.Navigation;
//using System.Windows.Shapes;

//namespace PrintCrypt
//{
//    /// <summary>
//    /// Interaction logic for MainWindow.xaml
//    /// </summary>
//    public partial class MainWindow : Window
//    {
//        public MainWindow()
//        {
//            InitializeComponent();
//        }

//        private void ScanKeyButton_Clicked(object sender, RoutedEventArgs e)
//        {
//            ScanWindow scanWindow= new ScanWindow();
//            this.Close();
//            scanWindow.Show();

//            //CompScanWindow compScanWindow = new CompScanWindow();
//            //this.Close();
//            //compScanWindow.Show();
//        }

//        GenerateWindow genFrm = new GenerateWindow();
//        private void NewKeyButton_Clicked(object sender, RoutedEventArgs e)
//        {
//            genFrm.Key = null;
//            ShowQRcb.Checked = false;
//            DialogResult res = genFrm.ShowDialog();
//            if (genFrm.Key != null)
//            {
//                Key = genFrm.Key;
//                int chks = Key[0];
//                for (int i = 1; i < 256; i++)
//                {
//                    chks = chks ^ Key[i];
//                }
//                KeyNameEd.Text = PrintCrypt_Algo.genrate_key_name_chks(chks);
//                if (ShowQRcb.Checked)
//                {
//                    if (Key != null)
//                    {
//                        qrCodeImage = CreateQRBitmap();
//                        pictureBox1.Image = qrCodeImage;
//                    }
//                }
//                else
//                {
//                    pictureBox1.Image = null;
//                }

//            }
//        }


//    }
//}
