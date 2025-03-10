﻿using System.Windows;
using System.Windows.Threading;
using RedisMonitor.DTO;
using RedisMonitor.Services;

namespace RedisMonitor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer _timer;

        public MainWindow()
        {
            InitializeComponent();

            // 初始化定时器，设置每5秒刷新一次数据
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(2); // 每2秒更新一次
            _timer.Tick += Timer_Tick;
            _timer.Start();

            // 初始化时加载数据
            LoadData();

            // 设置数据上下文
            this.DataContext = this;
        }

        // 每次定时器触发时加载数据
        private void Timer_Tick(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            // 获取当前时间
            DateTime dateTime = DateTime.Now;

            // 假设你有一个RedisData类来获取数据
            RedisData redisData = new RedisData();
            List<DataItem> dataItems = redisData.GetRedisDataNum();

            // 更新DataGrid的ItemsSource
            dataGrid.ItemsSource = dataItems;
        }
    }
}
