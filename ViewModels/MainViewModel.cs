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
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Bloody.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public MainViewModel()
        {
            this.MainImage = CaptureBitmap();
        }

        /// <summary>
        /// OpenCvSharp4から内臓カメラにアクセスしてキャプチャーした画像データを取得
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ApplicationException"></exception>
        private WriteableBitmap CaptureBitmap()
        {
            using (var capture = new OpenCvSharp.VideoCapture())
            using (var imgMat = new OpenCvSharp.Mat())
            {
                // カメラの起動　
                capture.Open((int)CaptureDevices.Default);
                if (!capture.IsOpened())
                    throw new ApplicationException("カメラの起動に失敗しました");

                // 画像データ取得
                capture.Read(imgMat);
                if (imgMat.Empty())
                    throw new ApplicationException("画像データの取得に失敗しました");

                return imgMat.ToWriteableBitmap();
            }
        }

        private WriteableBitmap _mainImage;
        public WriteableBitmap MainImage
        {
            get { return _mainImage; }
            set
            {
                _mainImage = value;
                NotifyPropertyChanged();
            }
        }
    }
}
