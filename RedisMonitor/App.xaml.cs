﻿using Common;
using Microsoft.Extensions.Configuration;
using RedisHelp;
using System.IO;
using System.Windows;

namespace RedisMonitor
{
    public partial class App : Application
    {
        
        public App()
        {
            // 加载配置文件
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) // 设置根目录
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true) // 加载配置文件
                .Build();

            // 加载 Redis 配置
            RedisConnectionHelp.RedisConnectionString = configuration.GetConnectionString("RedisConnectionString");

            // 框架破解的初始化
            ServiceStackHelper.Patch();
        }
    }
}
