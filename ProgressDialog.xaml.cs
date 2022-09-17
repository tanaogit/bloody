using Bloody.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Bloody
{
    /// <summary>
    /// ProgressDialog.xaml の相互作用ロジック
    /// </summary>
    public partial class ProgressDialog : Window
    {
        [DllImport("user32.dll")]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        private const int GWL_STYLE = -16;
        private const int WS_SYSMENU = 0x80000;

        private CancellationTokenSource _cancelToken;
        private ProgressDialogViewModel _viewModel;

        public ProgressDialog()
        {
            InitializeComponent();
            this.Top = SystemParameters.WorkArea.Height - (this.Height + 20);
            this.Left = (SystemParameters.WorkArea.Width / 2) - (this.Width / 2);

            _cancelToken = new CancellationTokenSource();
            _viewModel = new ProgressDialogViewModel();
            this.DataContext = _viewModel;

            ContentRendered += AsyncExecuteCount;

            KeyDown += (sender, e) =>
            {
                if (e.Key == Key.Escape)
                    _cancelToken?.Cancel();
            };
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            IntPtr handle = new WindowInteropHelper(this).Handle;
            SetWindowLong(handle, GWL_STYLE, GetWindowLong(handle, GWL_STYLE) & ~WS_SYSMENU);
        }

        private async void AsyncExecuteCount(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                for (int i = 1; i <= 30; i++)
                {
                    if (_cancelToken.IsCancellationRequested)
                        return;

                    Task.Delay(1000).Wait();

                    _viewModel.ProgressBarState = i;
                    _viewModel.ProgressBarStateNotify = (30 - i).ToString();
                }
            });

            this.Close();
        }
    }
}
