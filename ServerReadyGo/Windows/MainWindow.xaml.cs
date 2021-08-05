using System;
using System.Collections.Generic;
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
using System.Windows.Media.Animation;
using System.ComponentModel;
using System.Diagnostics;

namespace ServerReadyGo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        BackgroundWorker protector;

        public MainWindow()
        {
            InitializeComponent();

            protector = new BackgroundWorker();
            protector.DoWork += Protector_DoWork;

            protector.RunWorkerAsync(4292);
        }

        private void Protector_DoWork(object sender, DoWorkEventArgs e)
        {
            int PID = (int)e.Argument;
            Process p = Process.GetProcessById(PID);

            txbProName.Text = p.ProcessName;
            txbProID.Text = PID.ToString();
            txbProStatus.Text = p.HasExited ? "已终止" : "正在运行";
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            DoubleAnimation ani = new DoubleAnimation(0, 1, new Duration(TimeSpan.FromSeconds(1)));
            (sender as Grid).BeginAnimation(OpacityProperty, ani);
        }
    }
}
