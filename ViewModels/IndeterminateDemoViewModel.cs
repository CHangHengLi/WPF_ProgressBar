using ProgressBarDemo.Helpers;
using System.Windows.Input;

namespace ProgressBarDemo.ViewModels
{
    public class IndeterminateDemoViewModel : ViewModelBase
    {
        private bool _isIndeterminate;
        public bool IsIndeterminate
        {
            get => _isIndeterminate;
            set => SetProperty(ref _isIndeterminate, value);
        }

        private double _progressValue;
        public double ProgressValue
        {
            get => _progressValue;
            set => SetProperty(ref _progressValue, value);
        }

        public ICommand ToggleIndeterminateCommand { get; }

        public IndeterminateDemoViewModel()
        {
            ToggleIndeterminateCommand = new RelayCommand(_ => IsIndeterminate = !IsIndeterminate);
            ProgressValue = 50; // 默认值
        }
    }
} 