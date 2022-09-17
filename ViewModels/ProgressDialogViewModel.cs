using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Bloody.ViewModels
{
    public class ProgressDialogViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private int _progressBarState = 0;
        public int ProgressBarState
        {
            get { return _progressBarState; }
            set
            {
                _progressBarState = value;
                NotifyPropertyChanged();
            }
        }

        private string _progressBarStateNotify = "30";
        public string ProgressBarStateNotify
        {
            get { return _progressBarStateNotify; }
            set
            {
                _progressBarStateNotify = value;
                NotifyPropertyChanged();
            }
        }
    }
}
