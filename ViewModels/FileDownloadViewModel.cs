using ProgressBarDemo.Helpers;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProgressBarDemo.ViewModels
{
    public class FileDownloadViewModel : ViewModelBase
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

        private long _downloadedBytes;
        public long DownloadedBytes
        {
            get => _downloadedBytes;
            set => SetProperty(ref _downloadedBytes, value);
        }

        private long _totalBytes = 1024 * 1024 * 100; // 默认100MB
        public long TotalBytes
        {
            get => _totalBytes;
            set => SetProperty(ref _totalBytes, value);
        }

        private bool _isDownloading;
        public bool IsDownloading
        {
            get => _isDownloading;
            set => SetProperty(ref _isDownloading, value);
        }

        private CancellationTokenSource _cts;

        public ICommand StartDownloadCommand { get; }
        public ICommand CancelDownloadCommand { get; }

        public FileDownloadViewModel()
        {
            StartDownloadCommand = new RelayCommand(_ => StartDownload(), _ => !IsDownloading);
            CancelDownloadCommand = new RelayCommand(_ => CancelDownload(), _ => IsDownloading);
        }

        private async void StartDownload()
        {
            IsDownloading = true;
            ProgressValue = 0;
            DownloadedBytes = 0;
            StatusText = "开始下载...";
            
            _cts = new CancellationTokenSource();
            DateTime startTime = DateTime.Now;
            
            try
            {
                // 使用Progress<T>报告进度
                var progress = new Progress<long>(bytesReceived =>
                {
                    DownloadedBytes = bytesReceived;
                    ProgressValue = (double)bytesReceived / TotalBytes * 100;
                    
                    // 计算下载速度和剩余时间
                    TimeSpan elapsed = DateTime.Now - startTime;
                    double bytesPerSecond = bytesReceived / elapsed.TotalSeconds;
                    
                    if (bytesPerSecond > 0)
                    {
                        long remainingBytes = TotalBytes - bytesReceived;
                        double remainingSeconds = remainingBytes / bytesPerSecond;
                        TimeSpan remainingTime = TimeSpan.FromSeconds(remainingSeconds);
                        
                        StatusText = $"下载进度: {ProgressValue:0.0}% - " +
                                      $"{FormatBytes(bytesReceived)}/{FormatBytes(TotalBytes)} - " +
                                      $"速度: {FormatBytes((long)bytesPerSecond)}/s - " +
                                      $"剩余时间: {(int)remainingTime.TotalMinutes}分{remainingTime.Seconds}秒";
                    }
                });

                // 异步执行模拟下载
                var result = await Task.Run(() => SimulateFileDownload(progress, _cts.Token));
                
                // 根据返回结果决定显示何种状态
                if (result)
                {
                    StatusText = "下载完成！";
                }
                else
                {
                    StatusText = "下载已取消";
                }
            }
            catch (Exception ex)
            {
                StatusText = $"下载错误: {ex.Message}";
            }
            finally
            {
                IsDownloading = false;
                _cts?.Dispose();
                _cts = null;
            }
        }

        private void CancelDownload()
        {
            _cts?.Cancel();
            // 立即更新UI状态
            StatusText = "正在取消下载...";
            IsDownloading = false;
        }

        private bool SimulateFileDownload(IProgress<long> progress, CancellationToken token)
        {
            // 模拟下载文件
            long bytesReceived = 0;
            long chunkSize = 1024 * 256; // 每次下载256KB
            Random random = new Random();

            while (bytesReceived < TotalBytes)
            {
                // 检查取消请求，但不抛出异常
                if (token.IsCancellationRequested)
                {
                    return false; // 返回false表示下载被取消
                }
                
                // 模拟随机下载速度
                Thread.Sleep(random.Next(50, 150));
                
                // 模拟接收数据
                bytesReceived += chunkSize;
                if (bytesReceived > TotalBytes)
                {
                    bytesReceived = TotalBytes;
                }
                
                // 报告进度
                progress.Report(bytesReceived);
            }
            
            return true; // 返回true表示下载成功完成
        }

        private string FormatBytes(long bytes)
        {
            string[] suffixes = { "B", "KB", "MB", "GB", "TB" };
            int counter = 0;
            double number = bytes;
            
            while (number >= 1024 && counter < suffixes.Length - 1)
            {
                number /= 1024;
                counter++;
            }
            
            return $"{number:0.0} {suffixes[counter]}";
        }
    }
} 