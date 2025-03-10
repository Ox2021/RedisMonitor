﻿using RedisMonitor.DTO;

namespace RedisMonitor.Utils
{
    // 自定义比较器，按照DatabaseId排序
    public class DataItemComparer : IComparer<DataItem?>
    {
        public int Compare(DataItem? x, DataItem? y)
        {
            // 如果 x 和 y 都为 null，则视为相等
            if (x == null && y == null)
                return 0;

            // 如果 x 为 null，视为小于 y
            if (x == null)
                return -1;

            // 如果 y 为 null，视为大于 x
            if (y == null)
                return 1;

            // 升序排序，根据 ProtocolId
            return x.ProtocolId.CompareTo(y.ProtocolId);
        }
    }
}
