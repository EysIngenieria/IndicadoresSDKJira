﻿using System;
using System.Collections.Generic;

namespace DB.Data.ModelDb;

public partial class TbLogMessageInSummaryDay
{
    public long Id { get; set; }

    public long IdLogMessageIn { get; set; }

    public DateTime DateDay { get; set; }
}
