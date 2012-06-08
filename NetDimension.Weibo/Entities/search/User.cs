﻿using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace NetDimension.Weibo.Entities.search
{
	public class User
	{
		[JsonProperty("screen_name")]
		public string ScreenName { get; internal set; }
		[JsonProperty("followers_count")]
		public int FollowersCount { get; internal set; }
		[JsonProperty("uid")]
		public string UID { get; internal set; }
	}
}
