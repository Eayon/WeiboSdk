﻿using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
namespace NetDimension.Weibo.Entities.shortUrl
{
	public class CommentCount
	{
		[JsonProperty("url_short")]
		public string ShortUrl { get; internal set; }
		[JsonProperty("url_long")]
		public string LongUrl { get; internal set; }
		[JsonProperty("comment_counts")]
		public string CommentCounts { get; internal set; }
	}
}
