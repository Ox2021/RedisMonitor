﻿using RedisHelp;
using RedisMonitor.DTO;
using RedisMonitor.Utils;
using StackExchange.Redis;

namespace RedisMonitor.Services
{
    class RedisData
    {
        private static readonly Dictionary<int, string> protocolDictionary = new()
        {
            { 1, "电商智慧星" },
            { 34, "电商智慧星2" },
            { 3, "电商20kw直流设备" },
            { 33, "喵充充" },
            { 777, "自用测试" },
            { 6, "电商enel设备连接" },
            { 7, "电商五菱设备连接03" },
            { 8, "电商五菱设备连接04" },
            { 9, "电商五菱设备连接05" },
            { 10, "电商五菱设备连接06" },
            { 11, "电商五菱设备连接07" },
            { 12, "电商五菱设备连接08" },
            { 13, "电商五菱设备连接09" },
            { 14, "电商五菱设备连接10" },
            { 15, "电商五菱设备连接11" },
            { 16, "电商五菱设备连接12" },
            { 17, "电商五菱设备连接13" },
            { 18, "电商五菱设备连接14" },
            { 19, "电商五菱设备连接15" },
            { 20, "电商五菱设备连接16" },
            { 21, "电商五菱设备连接17" },
            { 22, "电商五菱设备连接18" },
            { 23, "电商五菱设备连接19" },
            { 24, "电商五菱设备连接20" },
            { 51, "电商五菱设备连接21" },
            { 52, "电商五菱设备连接22" },
            { 53, "电商五菱设备连接23" },
            { 54, "电商五菱设备连接24" },
            { 55, "电商五菱设备连接25" },
            { 56, "电商五菱设备连接26" },
            { 57, "电商五菱设备连接27" },
            { 58, "电商五菱设备连接28" },
            { 59, "电商五菱设备连接29" },
            { 60, "电商五菱设备连接30" },
            { 61, "电商五菱设备连接31" },
            { 62, "电商五菱设备连接32" },
            { 63, "电商五菱设备连接33" },
            { 64, "电商五菱设备连接34" },
            { 65, "电商五菱设备连接35" },
            { 66, "电商五菱设备连接36" },
            { 67, "电商五菱设备连接37" },
            { 68, "电商五菱设备连接38" },
            { 69, "电商五菱设备连接39" },
            { 70, "电商五菱设备连接40" }
        };

        public List<DataItem> GetRedisDataNum()
        {
            ConnectionMultiplexer redis = RedisConnectionHelp.Instance;
            IDatabase db = redis.GetDatabase(1);

            var server = redis.GetServer(RedisConnectionHelp.RedisConnectionString, 6379);

            List<RedisKey> keys = server.Keys(database: 1).ToList(); // 获取第 1 号库中的所有键

            List<DataItem> dataItems = ConvertItems(db, keys);

            return dataItems;
        }

        public List<DataItem> ConvertItems(IDatabase db, List<RedisKey> keys)
        {
            Dictionary<int, long> sentDataMap = new Dictionary<int, long>();
            Dictionary<int, long> receivedData = new Dictionary<int, long>();

            foreach (var key in keys)
            {
                RedisType keyType = db.KeyType(key);
                if (keyType == RedisType.List)
                {
                    // 提取数字部分，避免每次都调用 ToString 和 Split
                    int keyValue = GetKeyValueFromRedisKey(key);

                    if (key.ToString().Contains("发送"))
                    {
                        sentDataMap[keyValue] = db.ListLength(key);
                    }
                    else
                    {
                        receivedData[keyValue] = db.ListLength(key);
                    }
                }
            }

            SortedSet<DataItem> sortedSet = ConvertDictionaryToDataItem(sentDataMap, receivedData);
            return sortedSet.ToList();
        }

        private int GetKeyValueFromRedisKey(RedisKey key)
        {
            // 提取 Redis 键的整数部分，优化字符串处理
            var keyParts = key.ToString().Split("|");
            return int.TryParse(keyParts[0], out int keyValue) ? keyValue : 0;
        }

        public SortedSet<DataItem> ConvertDictionaryToDataItem(
            Dictionary<int, long> sentDataMap,
            Dictionary<int, long> receivedData)
        {
            SortedSet<DataItem> sortedSet = new(new DataItemComparer());

            // 使用 HashSet 避免重复插入相同 key
            HashSet<int> allKeys = new HashSet<int>(sentDataMap.Keys);
            allKeys.UnionWith(receivedData.Keys); // 合并所有键

            foreach (int key in allKeys)
            {
                long sentValue = sentDataMap.ContainsKey(key) ? sentDataMap[key] : 0;
                long receivedValue = receivedData.ContainsKey(key) ? receivedData[key] : 0;

                string protocolName = protocolDictionary.TryGetValue(key, out string name) ? name : "空";

                // 合并生成 DataItem 对象
                DataItem data = new DataItem(key, protocolName, sentValue, receivedValue);
                sortedSet.Add(data);
            }

            return sortedSet;
        }
    }
}
