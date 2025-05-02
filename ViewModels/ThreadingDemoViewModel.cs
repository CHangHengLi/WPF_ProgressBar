using ProgressBarDemo.Helpers;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProgressBarDemo.ViewModels
{
    public class ThreadingDemoViewModel : ViewModelBase
    {
        private double _progressValue;
        public double ProgressValue
        {
            get => _progressValue;
            set => SetProperty(ref _progressValue, value);
        }

        private string _statusText = "就绪";
        public string StatusText
        {
            get => _statusText;
            set => SetProperty(ref _statusText, value);
        }

        private bool _isProcessing;
        public bool IsProcessing
        {
            get => _isProcessing;
            set => SetProperty(ref _isProcessing, value);
        }

        private CancellationTokenSource _cts;

        public ICommand StartProcessCommand { get; }
        public ICommand CancelProcessCommand { get; }

        public ThreadingDemoViewModel()
        {
            StartProcessCommand = new RelayCommand(_ => StartProcess(), _ => !IsProcessing);
            CancelProcessCommand = new RelayCommand(_ => CancelProcess(), _ => IsProcessing);
        }

        private async void StartProcess()
        {
            IsProcessing = true;
            ProgressValue = 0;
            StatusText = "正在处理...";
            
            _cts = new CancellationTokenSource();
            
            try
            {
                // 使用Progress<T>报告进度
                var progress = new Progress<int>(value =>
                {
                    ProgressValue = value;
                    StatusText = $"正在处理: {value}%";
                });

                // 异步执行任务
                var result = await Task.Run(() => ProcessDataWithProgress(progress, _cts.Token));
                
                // 根据返回结果确定是完成还是取消
                if (result)
                {
                    StatusText = "处理完成！";
                }
                else
                {
                    StatusText = "操作已取消";
                }
            }
            catch (Exception ex)
            {
                StatusText = $"错误: {ex.Message}";
            }
            finally
            {
                IsProcessing = false;
                _cts?.Dispose();
                _cts = null;
            }
        }

        private void CancelProcess()
        {
            _cts?.Cancel();
            // 立即更新UI状态，不等待异步操作完成
            StatusText = "正在取消操作...";
            IsProcessing = false;
        }

        private bool ProcessDataWithProgress(IProgress<int> progress, CancellationToken token)
        {
            for (int i = 0; i <= 100; i++)
            {
                // 检查取消请求，但不抛出异常
                if (token.IsCancellationRequested)
                {
                    return false; // 返回false表示操作被取消
                }
                
                // 模拟耗时操作
                Thread.Sleep(100);
                
                // 报告进度
                progress.Report(i);
            }
            
            return true; // 返回true表示操作成功完成
        }
    }
} 