﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TradeRobotics.TradeLibrary
{
    /// <summary>
    /// New tick event
    /// </summary>
    public class TickEventArgs:EventArgs
    {
        /// <summary>
        /// Time of last data
        /// </summary>
        public DateTime TickTime { get; set; }
    }
}
