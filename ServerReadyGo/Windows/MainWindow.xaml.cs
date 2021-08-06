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
using System.Timers;

namespace ServerReadyGo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        Process currentProcess;
        Timer bgProcessChecker;

        public MainWindow()
        {
            InitializeComponent();

            bgProcessChecker = new Timer();
            bgProcessChecker.Interval = 1000;
            bgProcessChecker.Elapsed += bgCheckProcessStatus;
        }

        private void bgCheckProcessStatus(object sender, ElapsedEventArgs e)
        {
            if (currentProcess.HasExited)
            {
                txbProStatus.Text = "正在运行";
            }
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            DoubleAnimation ani = new DoubleAnimation(0, 1, new Duration(TimeSpan.FromSeconds(1)));
            (sender as Grid).BeginAnimation(OpacityProperty, ani);
        }

        private void SetProtectProcess(int PID)
        {
            Process p;
            try
            {
                p = Process.GetProcessById(PID);
            }
            catch
            {
                MessageBox.Show("获取进程失败，可能是因为它不存在。", "错误", MessageBoxButton.OK);
                return;
            }
            if (!p.HasExited)
            {
                MessageBox.Show("无法守护已终止的进程。", "错误", MessageBoxButton.OK);
                return;
            }
            currentProcess = p;
            txbProName.Text = p.ProcessName;
            txbProID.Text = PID.ToString();
        }
    }
}
