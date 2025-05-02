using ProgressBarDemo.Helpers;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ProgressBarDemo.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private ViewModelBase _currentViewModel;

        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            set => SetProperty(ref _currentViewModel, value);
        }

        public ObservableCollection<string> DemoPages { get; } = new ObservableCollection<string>
        {
            "基本用法",
            "不确定模式",
            "多线程更新",
            "自定义样式",
            "文件下载模拟"
        };

        private string _selectedDemo;
        public string SelectedDemo
        {
            get => _selectedDemo;
            set
            {
                if (SetProperty(ref _selectedDemo, value))
                {
                    UpdateCurrentViewModel();
                }
            }
        }

        public ICommand NavigateCommand { get; }

        public MainViewModel()
        {
            NavigateCommand = new RelayCommand(Navigate);
            SelectedDemo = DemoPages[0]; // 默认选择第一个演示
        }

        private void Navigate(object parameter)
        {
            if (parameter is string demoName)
            {
                SelectedDemo = demoName;
            }
        }

        private void UpdateCurrentViewModel()
        {
            CurrentViewModel = _selectedDemo switch
            {
                "基本用法" => new BasicDemoViewModel(),
                "不确定模式" => new IndeterminateDemoViewModel(),
                "多线程更新" => new ThreadingDemoViewModel(),
                "自定义样式" => new CustomStyleDemoViewModel(),
                "文件下载模拟" => new FileDownloadViewModel(),
                _ => new BasicDemoViewModel()
            };
        }
    }
} 