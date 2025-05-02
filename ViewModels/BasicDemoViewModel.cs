using ProgressBarDemo.Helpers;
using System;
using System.Windows.Input;

namespace ProgressBarDemo.ViewModels
{
    public class BasicDemoViewModel : ViewModelBase
    {
        private double _progressValue;
        public double ProgressValue
        {
            get => _progressValue;
            set => SetProperty(ref _progressValue, value);
        }

        private double _increment = 10;
        public double Increment
        {
            get => _increment;
            set => SetProperty(ref _increment, value);
        }

        public ICommand IncreaseCommand { get; }
        public ICommand DecreaseCommand { get; }
        public ICommand ResetCommand { get; }

        public BasicDemoViewModel()
        {
            IncreaseCommand = new RelayCommand(IncreaseProgress, CanIncreaseProgress);
            DecreaseCommand = new RelayCommand(DecreaseProgress, CanDecreaseProgress);
            ResetCommand = new RelayCommand(_ => ProgressValue = 0);
        }

        private void IncreaseProgress(object obj)
        {
            ProgressValue = Math.Min(100, ProgressValue + Increment);
        }

        private bool CanIncreaseProgress(object obj)
        {
            return ProgressValue < 100;
        }

        private void DecreaseProgress(object obj)
        {
            ProgressValue = Math.Max(0, ProgressValue - Increment);
        }

        private bool CanDecreaseProgress(object obj)
        {
            return ProgressValue > 0;
        }
    }
} 