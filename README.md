# WPF ProgressBar 控件演示项目

这是一个基于.NET Core 8.0的WPF应用程序，用于演示ProgressBar控件的各种用法和功能。
![image](https://github.com/user-attachments/assets/b3af6ac3-9f77-4722-87ae-9cf1a024ec6c)

## 项目结构

本项目采用MVVM架构模式，分为以下主要部分：

- `Views` - 包含所有UI视图
- `ViewModels` - 包含视图模型，处理业务逻辑
- `Helpers` - 包含辅助类，如RelayCommand和值转换器
- `Styles` - 包含自定义样式资源

## 演示功能

本项目包含以下演示功能：

1. **基本用法** - 演示ProgressBar的基本属性和用法，包括垂直和水平进度条
2. **不确定模式** - 演示IsIndeterminate属性的使用
3. **多线程更新** - 演示如何在多线程环境中正确更新进度条
4. **自定义样式** - 展示多种自定义进度条样式
5. **文件下载模拟** - 模拟文件下载过程并展示相关进度信息

## 如何运行

1. 确保已安装.NET Core 8.0 SDK或更高版本
2. 打开命令行并导航到解决方案文件夹
3. 执行 `dotnet build` 编译项目
4. 执行 `dotnet run` 运行项目

## 技术要点

- MVVM架构模式
- WPF数据绑定
- 样式和模板自定义
- 多线程编程
- 异步编程模式
- 进度报告模式

## 系统要求

- .NET Core 8.0或更高版本
- Windows 7或更高版本

## 许可证

本项目使用MIT许可证 
