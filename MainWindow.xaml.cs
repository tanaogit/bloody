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
using OpenCvSharp.WpfExtensions;
using Bloody.Enums;
using Bloody.ViewModels;
using System.Threading;

namespace Bloody
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel _viewModel;
        private bool _isReminderExecute = true;

        public MainWindow()
        {
            InitializeComponent();

            try
            {
                _viewModel = new MainViewModel();
                this.DataContext = _viewModel;

                ContentRendered += async (sender, e) =>
                {
                    await Task.Delay(5000);

                    if (_isReminderExecute)
                    {
                        ReminderNotifyWindow rw = new ReminderNotifyWindow();
                        rw.ShowDialog();

                        ShowProgressDialog();
                        this.Close();
                    }
                };

                KeyDown += (sender, e) =>
                {
                    if (e.Key == Key.Enter)
                    {
                        _isReminderExecute = false;

                        ShowProgressDialog();
                        this.Close();
                    }
                    else if (e.Key == Key.Escape)
                    {
                        _isReminderExecute = false;
                        this.Close();
                    }
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show("予期せぬエラーが発生しました\r\n" + ex.Message);
            }
        }

        private void ShowProgressDialog()
        {
            ProgressDialog pd = new ProgressDialog();
            pd.ShowDialog();
        }
    }
}
