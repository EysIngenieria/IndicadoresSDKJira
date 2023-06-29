﻿using System;
using System.Collections.Generic;

namespace MQTT.Infrastructure.Models
{
    public partial class TbLogExecutions
    {
        public long Id { get; set; }
        public DateTime InitDateTime { get; set; }
        public string Observations { get; set; }
        public DateTime? EndDateTime { get; set; }
    }
}
