﻿using System.ComponentModel.DataAnnotations.Schema;

namespace RedisMonitor.Models // 根据实际的命名空间修改
{
    [Table("cfg_protocol")]
    public class CfgProtocol
    {
        // 主键，自动增长
        public int Id { get; set; }

        // cmuid 字段，允许为 null
        public int? Cmuid { get; set; }

        // protocolid 字段，不允许为 null
        public int ProtocolId { get; set; }

        // protocolname 字段，不能为空
        public string ProtocolName { get; set; }

        // communication 字段，不允许为 null
        public int Communication { get; set; }

        // parameter 字段，不能为空
        public string Parameter { get; set; }

        // selected 字段，不允许为 null
        public int Selected { get; set; }

        // protocoltag 字段，允许为 null
        public string ProtocolTag { get; set; }
    }
}
