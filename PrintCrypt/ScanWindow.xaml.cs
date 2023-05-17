using System;
using System.Drawing;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Media;
using AForge.Video;
using AForge.Video.DirectShow;
using ZXing;

namespace PrintCrypt
{
    public partial class ScanWindow : Window
    {
        public event Action<string, Image> QRCodeScanned;
        private VideoCaptureDevice videoSource;
        private BarcodeReader barcodeReader;
        private PictureBox pictureBox;

        public ScanWindow()
        {
            InitializeComponent();
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            // Create a new instance of the video capture device
            videoSource = new VideoCaptureDevice();

            // Set the video device to use
            FilterInfoCollection videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            videoSource = new VideoCaptureDevice(videoDevices[0].MonikerString);

            // Create a new instance of the barcode reader
            barcodeReader = new BarcodeReader();

            // Create a new instance of the PictureBox control
            pictureBox = new PictureBox();
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox.Dock = DockStyle.Fill;
            WindowsFormsHost.Child = pictureBox;

            // Subscribe to the NewFrame event of the video source to capture and read each frame
            videoSource.NewFrame += VideoSource_NewFrame;

            // Start the video source
            videoSource.Start();
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            // Stop the video source
            videoSource.Stop();

            // Unsubscribe from the NewFrame event of the video source
            videoSource.NewFrame -= VideoSource_NewFrame;

            // Set the barcode reader instance to null
            barcodeReader = null;
        }

        private void VideoSource_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            // Get the current frame from the video source
            Bitmap bitmap = (Bitmap)eventArgs.Frame.Clone();

            // Decode the QR code in the current frame using the barcode reader
            Result result = barcodeReader.Decode(bitmap);

            // Check if a QR code was found in the current frame
            if (result != null)
            {

                //Display the QR code content in a message box
                System.Windows.MessageBox.Show("Barcode detected: " + result.Text, "Barcode Detected");
                //System.Windows.MessageBox.Show(result.Text);

                // Stop the video source
                videoSource.Stop();

                // Unsubscribe from the NewFrame event of the video source
                videoSource.NewFrame -= VideoSource_NewFrame;

                //// Convert the Bitmap to ImageSource
                //var imageSourceConverter = new ImageSourceConverter();
                //var imageSource = (ImageSource)imageSourceConverter.ConvertFrom(bitmap);



                System.Windows.Application.Current.Dispatcher.Invoke(() =>
                {
                    // Redirect to the MainWindow with the QR code content and image
                    MainWindow mainWindow = new MainWindow(result.Text, bitmap, true);
                    mainWindow.Show();

                    // Close the current window
                    Close();
                });

                //// Redirect to the MainWindow with the QR code content and image
                //MainWindow mainWindow = new MainWindow(result.Text, bitmap, true);
                //mainWindow.Show();

                ////// Dispose of the video source and barcode reader instances
                ////videoSource.Stop();
                ////barcodeReader = null;

                //// Close the current window
                //Close();
            }

            // Set the current frame in the PictureBox control
            pictureBox.Image = bitmap;
        }
    }
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
//using AForge.Video.DirectShow;
//using AForge.Video;
//using ZXing;
//using System.Windows.Forms;
//using System.Drawing;

//namespace PrintCrypt
//{
//    /// <summary>
//    /// Interaction logic for ScanWindow.xaml
//    /// </summary>
//    public partial class ScanWindow : Window
//    {
//        public ScanWindow()
//        {
//            InitializeComponent();
//        }

//        private VideoCaptureDevice videoSource;
//        private BarcodeReader barcodeReader;
//        private PictureBox pictureBox;

//        private void StartButton_Click(object sender, RoutedEventArgs e)
//        {
//            // Create a new instance of the video capture device
//            videoSource = new VideoCaptureDevice();

//            // Set the video device to use
//            FilterInfoCollection videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
//            videoSource = new VideoCaptureDevice(videoDevices[0].MonikerString);

//            // Create a new instance of the barcode reader
//            barcodeReader = new BarcodeReader();

//            // Create a new instance of the PictureBox control
//            pictureBox = new PictureBox();
//            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
//            pictureBox.Dock = DockStyle.Fill;
//            WindowsFormsHost.Child = pictureBox;

//            // Subscribe to the NewFrame event of the video source to capture and read each frame
//            videoSource.NewFrame += new NewFrameEventHandler(videoSource_NewFrame);

//            // Start the video source
//            videoSource.Start();
//        }

//        private void StopButton_Click(object sender, RoutedEventArgs e)
//        {
//            // Stop the video source
//            videoSource.Stop();

//            // Unsubscribe from the NewFrame event of the video source
//            videoSource.NewFrame -= new NewFrameEventHandler(videoSource_NewFrame);

//            // Set the barcode reader instance to null
//            barcodeReader = null;
//        }

//        private void videoSource_NewFrame(object sender, NewFrameEventArgs eventArgs)
//        {
//            // Get the current frame from the video source
//            Bitmap bitmap = (Bitmap)eventArgs.Frame.Clone();

//            // Decode the QR code in the current frame using the barcode reader
//            Result result = barcodeReader.Decode(bitmap);

//            // Check if a QR code was found in the current frame
//            if (result != null)
//            {
//                // Display the QR code content in a message box
//                System.Windows.MessageBox.Show("Barcode detected: " + result.Text, "Barcode Detected");
//                //System.Windows.MessageBox.Show(result.Text);

//                // Stop the video source
//                videoSource.Stop();

//                // Unsubscribe from the NewFrame event of the video source
//                videoSource.NewFrame -= new NewFrameEventHandler(videoSource_NewFrame);

//                // Dispose of the video source and barcode reader instances
//                videoSource.Stop();
//                barcodeReader = null;
//            }

//            // Set the current frame in the PictureBox control
//            pictureBox.Image = bitmap;
//        }

//    }
//}
