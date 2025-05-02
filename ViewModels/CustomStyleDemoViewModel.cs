using ProgressBarDemo.Helpers;
using System;
using System.Windows.Input;

namespace ProgressBarDemo.ViewModels
{
    public class CustomStyleDemoViewModel : ViewModelBase
    {
        private double _progressValue = 65;
        public double ProgressValue
        {
            get => _progressValue;
            set => SetProperty(ref _progressValue, value);
        }

        private string _selectedStyle = "默认样式";
        public string SelectedStyle
        {
            get => _selectedStyle;
            set => SetProperty(ref _selectedStyle, value);
        }

        public string[] AvailableStyles { get; } = 
        {
            "默认样式",
            "圆角样式",
            "渐变样式",
            "动画样式",
            "带文本样式"
        };

        public ICommand UpdateProgressCommand { get; }
        
        public CustomStyleDemoViewModel()
        {
            UpdateProgressCommand = new RelayCommand(_ => 
            {
                // 随机更新进度
                var random = new Random();
                ProgressValue = random.Next(0, 101);
            });
        }
    }
} 