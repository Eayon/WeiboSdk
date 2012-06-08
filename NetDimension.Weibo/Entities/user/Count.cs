﻿using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace NetDimension.Weibo.Entities.user
{
	public class Count
	{
		[JsonProperty("id")]
		public string ID { get; internal set; }
		[JsonProperty("followers_count")]
		public string FollowerCount { get; internal set; }
		[JsonProperty("friends_count")]
		public string FriendCount { get; internal set; }
		[JsonProperty("statuses_count")]
		public string StatusCount { get; internal set; }
	}
}
