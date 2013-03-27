﻿using System.Collections.Generic;
using NetDimension.Json;
using NetDimension.Weibo.Entities.status;

namespace NetDimension.Weibo.Entities.shortUrl
{
    public class ShareStatuses : EntityBase
    {
        [JsonProperty("url_short")]
        public string ShortUrl { get; internal set; }

        [JsonProperty("url_long")]
        public string LongUrl { get; internal set; }

        [JsonProperty("share_statuses")]
        public IEnumerable<Entity> Referers { get; internal set; }
    }
}