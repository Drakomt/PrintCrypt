using System;
using System.Drawing;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using AForge.Video;
using AForge.Video.DirectShow;
using ZXing;

namespace PrintCrypt
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void ScanButton_Click(object sender, RoutedEventArgs e)
        {
            ScanWindow scanWindow = new ScanWindow();
            scanWindow.QRCodeScanned += ScanWindow_QRCodeScanned;
            scanWindow.Show();
        }

        private void GenerateKeyButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Implement key generation logic
        }

        private void ScanWindow_QRCodeScanned(string qrCodeContent, Image qrCodeImage)
        {
            MainWindow mainWindow = new MainWindow(qrCodeContent, qrCodeImage, true);
            mainWindow.Show();

            // Close the current window
            Close();
        }
    }

    //public partial class ScanWindow : Window
    //{
    //    public event Action<string, Image> QRCodeScanned;

    //    private VideoCaptureDevice videoSource;
    //    private BarcodeReader barcodeReader;
    //    private PictureBox pictureBox;

    //    public ScanWindow()
    //    {
    //        InitializeComponent();
    //    }

    //    private void StartButton_Click(object sender, RoutedEventArgs e)
    //    {
    //        // Create a new instance of the video capture device
    //        videoSource = new VideoCaptureDevice();

    //        // Set the video device to use
    //        FilterInfoCollection videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
    //        videoSource = new VideoCaptureDevice(videoDevices[0].MonikerString);

    //        // Create a new instance of the barcode reader
    //        barcodeReader = new BarcodeReader();

    //        // Create a new instance of the PictureBox control
    //        pictureBox = new PictureBox();
    //        pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
    //        pictureBox.Dock = DockStyle.Fill;
    //        WindowsFormsHost.Child = pictureBox;

    //        // Subscribe to the NewFrame event of the video source to capture and read each frame
    //        videoSource.NewFrame += VideoSource_NewFrame;

    //        // Start the video source
    //        videoSource.Start();
    //    }

    //    private void StopButton_Click(object sender, RoutedEventArgs e)
    //    {
    //        // Stop the video source
    //        videoSource.Stop();

    //        // Unsubscribe from the NewFrame event of the video source
    //        videoSource.NewFrame -= VideoSource_NewFrame;

    //        // Set the barcode reader instance to null
    //        barcodeReader = null;
    //    }

    //    private void VideoSource_NewFrame(object sender, NewFrameEventArgs eventArgs)
    //    {
    //        // Get the current frame from the video source
    //        Bitmap bitmap = (Bitmap)eventArgs.Frame.Clone();

    //        // Decode the QR code in the current frame using the barcode reader
    //        Result result = barcodeReader.Decode(bitmap);

    //        // Check if a QR code was found in the current frame
    //        if (result != null)
    //        {
    //            // Display the QR code content in a message box
    //            System.Windows.MessageBox.Show("Barcode detected: " + result.Text, "Barcode Detected");

    //            // Stop the video source
    //            videoSource.Stop();

    //            // Unsubscribe from the NewFrame event of the video source
    //            videoSource.NewFrame -= VideoSource_NewFrame;

    //            // Raise the QRCodeScanned event with the QR code content and image
    //            QRCodeScanned?.Invoke(result.Text, bitmap);
    //        }

    //        // Set the current frame in the PictureBox control
    //        pictureBox.Image = bitmap;
    //    }
    //}

    //public partial class MainWindow : Window
    //{
    //    public MainWindow(string qrCodeContent, Image qrCodeImage, bool showQRCodeImage)
    //    {
    //        InitializeComponent();

    //        KeyTextBox.Text = qrCodeContent;

    //        if (showQRCodeImage)
    //            QRCodeImage.Source = ConvertImageToBitmapImage(qrCodeImage);
    //        else
    //            QRCodeImage.Visibility = Visibility.Collapsed;
    //    }

    //    private BitmapImage ConvertImageToBitmapImage(Image image)
    //    {
    //        // Convert the System.Drawing.Image to BitmapImage
    //        Bitmap bitmap = new Bitmap(image);
    //        IntPtr hBitmap = bitmap.GetHbitmap();
    //        BitmapSource bitmapSource = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
    //            hBitmap,
    //            IntPtr.Zero,
    //            Int32Rect.Empty,
    //            BitmapSizeOptions.FromEmptyOptions());
    //        bitmapSource.Freeze();
    //        bitmap.Dispose();

    //        // Return the BitmapImage
    //        BitmapImage bitmapImage = new BitmapImage();
    //        bitmapImage.BeginInit();
    //        bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
    //        bitmapImage.StreamSource = null;
    //        bitmapImage.UriSource = null;
    //        bitmapImage.DecodePixelWidth = bitmapSource.PixelWidth;
    //        bitmapImage.DecodePixelHeight = bitmapSource.PixelHeight;
    //        bitmapImage.EndInit();

    //        return bitmapImage;
    //    }
    //}
}











//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Data;
//using System.Windows.Documents;
//using System.Windows.Input;
//using System.Windows.Media;
//using System.Windows.Media.Imaging;
//using System.Windows.Shapes;

//namespace PrintCrypt
//{
//    /// <summary>
//    /// Interaction logic for LoginWindow.xaml
//    /// </summary>
//    public partial class LoginWindow : Window
//    {
//        public LoginWindow()
//        {
//            InitializeComponent();
//        }
//    }
//}
