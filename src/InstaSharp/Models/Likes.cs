﻿using System.Collections.Generic;

namespace InstaSharp.Models {
    /// <summary>
    /// 
    /// </summary>
    public class Likes {
        /// <summary>
        /// Gets or sets the count.
        /// </summary>
        /// <value>
        /// The count.
        /// </value>
        public int Count { get; set; }
        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        public List<UserInfo> Data { get; set; }
    }
}
