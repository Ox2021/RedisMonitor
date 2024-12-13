# Redis Monitor
Redis Monitor 是一个用于监控Redis实例的WPF应用程序。它提供了可视化的方式来监控Redis数据的积压数量。
# 项目运行效果
![image](https://github.com/user-attachments/assets/1ee898e9-0abf-41e9-8d5d-b453cedd0a19)

## 安装和运行

1. 克隆该仓库：

    ```bash
    git clone https://github.com/yourusername/redis-monitor.git
    ```

2. 确保你安装了 .NET 8.0 或更高版本。

3. 安装所需的依赖项：

    ```bash
    dotnet restore
    ```

4. 在 `appsettings.json` 文件中配置 Redis 连接（默认为 `Default` 和 `Backup` 连接）。

    ```json
    {
      "RedisConnections": {
        "Default": {
          "ConnectionString": "your-redis-server:6379",
          "Password": "yourpassword",
          "AllowAdmin": true,
          "AbortConnect": false
        },
        "Backup": {
          "ConnectionString": "your-backup-redis-server:6379",
          "Password": "yourbackup-password",
          "AllowAdmin": false,
          "AbortConnect": true
        }
      }
    }
    ```

5. 启动应用：

    ```bash
    dotnet run
    ```
***

# 代码提交规范
* feat: 新增功能（feature）
* fix: 修复 Bug
* docs: 修改文档（如 README 文件等）
* style: 代码样式的更改（如空格、格式化等，不影响逻辑）
* refactor: 代码重构（既不修复 Bug 也不添加功能）
* perf: 性能优化
* test: 增加/修改测试代码
* chore: 其他杂项修改（如依赖更新、构建工具配置等）
