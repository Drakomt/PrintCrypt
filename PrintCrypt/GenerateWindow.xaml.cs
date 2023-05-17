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
//using System.Windows.Input;
//using System.Windows.Media;
//using System.Windows.Media.Imaging;
//using System.Windows.Shapes;

//namespace PrintCrypt
//{
//    /// <summary>
//    /// Interaction logic for GenerateWindow.xaml
//    /// </summary>
//    public partial class GenerateWindow : Window
//    {
//        static int maxi1 = 256;
//        static int maxi2 = 2;
//        int i1 = 0;
//        int i2 = 0;
//        DateTime lastt;
//        public GenerateWindow()
//        {
//            InitializeComponent();
//        }
//        public byte[] Key = null;
//        bool bstart = false;

//        private void StartBt_Click(object sender, EventArgs e)
//        {
//            bstart = true;
//            StartBt.Text = "Move the mouse over the panel below";
//            lastt = DateTime.Now;

//        }
//        int[,] xx = new int[maxi1, maxi2];
//        int[,] yy = new int[maxi1, maxi2];
//        int[,] tt = new int[maxi1, maxi2];
//        private void PadPnl_MouseMove(object sender, MouseEventArgs e)
//        {
//            if (bstart)
//            {
//                xx[i1, i2] = e.X;
//                yy[i1, i2] = e.Y;
//                DateTime t = DateTime.Now;
//                tt[i1, i2] = (int)(t.Ticks - lastt.Ticks);
//                lastt = t;
//                System.Drawing.SolidBrush myBrush = new System.Drawing.SolidBrush(System.Drawing.Color.RoyalBlue);
//                System.Drawing.Graphics formGraphics;
//                formGraphics = PadPnl.CreateGraphics();
//                formGraphics.FillRectangle(myBrush, new Rectangle(e.X, e.Y, 4, 4));
//                myBrush.Dispose();
//                formGraphics.Dispose();

//                i1++;
//                if (i1 == maxi1)
//                {
//                    i1 = 0;
//                    i2++;
//                    if (i2 == maxi2)
//                    {
//                        i2 = 0;
//                        bstart = false;

//                        Key = PrintCrypt_Algo.create_key(maxi1, maxi2, xx, yy, tt);
//                        Close();

//                    }
//                }

//            }
//        }
//    }
//}
