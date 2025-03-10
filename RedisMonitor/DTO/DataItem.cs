﻿namespace RedisMonitor.DTO
{
    public class DataItem
    {
        public int ProtocolId { get; set; }
        public string ProtocolName { get; set; }
        public long Send { get; set; }
        public long Receive { get; set; }

        public DataItem(int protocolId, String protocolName, long send, long receive)
        {
            ProtocolId = protocolId;
            ProtocolName = protocolName;
            Send = send;
            Receive = receive;
        }

        // 重写 Equals 方法
        public override bool Equals(object obj)
        {
            if (obj is DataItem other)
            {
                return ProtocolId == other.ProtocolId;
            }
            return false;
        }

        // 重写 GetHashCode 方法
        public override int GetHashCode()
        {
            return ProtocolId.GetHashCode();
        }
    }

}
