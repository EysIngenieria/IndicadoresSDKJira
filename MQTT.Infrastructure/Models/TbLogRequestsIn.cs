﻿using System;
using System.Collections.Generic;

namespace MQTT.Infrastructure.Models
{
    public partial class TbLogRequestsIn
    {
        public long Id { get; set; }
        public int IdEndPoint { get; set; }
        public string DataQuery { get; set; }
        public string DataBody { get; set; }
        public DateTime CreationDate { get; set; }
        public bool? Processed { get; set; }
        public string Observations { get; set; }

        public virtual TbEndPoints IdEndPointNavigation { get; set; }
    }
}
