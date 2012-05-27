﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Codeplex.Data;

namespace NetDimension.Weibo.Interface
{
	public class ShortUrlInterface: WeiboInterface
	{
		public ShortUrlInterface(Client client)
			: base(client)
		{

		}

		public dynamic Clicks(string url_short)
		{
			return DynamicJson.Parse(Client.GetCommand("short_url/clicks", new WeiboStringParameter("url_short", url_short)));
		}

		public dynamic Referers(string url_short)
		{
			return DynamicJson.Parse(Client.GetCommand("short_url/referers", new WeiboStringParameter("url_short", url_short)));
		}

		public dynamic Locations(string url_short)
		{
			return DynamicJson.Parse(Client.GetCommand("short_url/locations", new WeiboStringParameter("url_short", url_short)));
		}

		public dynamic Info(params string[] url_short)
		{
			List<WeiboStringParameter> parameters = new List<WeiboStringParameter>();

			foreach (string u in url_short)
			{
				parameters.Add(new WeiboStringParameter("url_short", u));
			}

			return DynamicJson.Parse(Client.GetCommand("short_url/info", parameters.ToArray()));
		}

		/// <summary>
		/// 将一个或多个长链接转换成短链接 
		/// </summary>
		/// <param name="url_long">需要转换的长链接，需要URLencoded，最多不超过20个。 </param>
		/// <returns></returns>
		public dynamic Shorten(params string[] url_long)
		{
			List<WeiboStringParameter> parameters = new List<WeiboStringParameter>();

			foreach (string u in url_long)
			{
				parameters.Add(new WeiboStringParameter("url_long", u));
			}
			return DynamicJson.Parse(Client.GetCommand("short_url/shorten", parameters.ToArray()));
		}

		/// <summary>
		/// 将一个或多个短链接还原成原始的长链接 
		/// </summary>
		/// <param name="url_short">需要还原的短链接，需要URLencoded，最多不超过20个 </param>
		/// <returns></returns>
		public dynamic Expand(params string[] url_short)
		{
			List<WeiboStringParameter> parameters = new List<WeiboStringParameter>();

			foreach (string u in url_short)
			{
				parameters.Add(new WeiboStringParameter("url_short", u));
			}
			return DynamicJson.Parse(Client.GetCommand("short_url/expand", parameters.ToArray()));

		}


		/// <summary>
		/// 取得一个短链接在微博上的微博分享数（包含原创和转发的微博） 
		/// </summary>
		/// <param name="url_short">需要取得分享数的短链接</param>
		/// <returns></returns>
		public dynamic ShareCounts(string url_short)
		{
			return DynamicJson.Parse(Client.GetCommand("short_url/share/counts", new WeiboStringParameter("url_short", url_short)));
		}

		/// <summary>
		/// 取得包含指定单个短链接的最新微博内容 
		/// </summary>
		/// <param name="url_short">需要取得关联微博内容的短链接</param>
		/// <param name="since_id">若指定此参数，则返回ID比since_id大的微博（即比since_id时间晚的微博），默认为0 </param>
		/// <param name="max_id">指定此参数，则返回ID小于或等于max_id的微博，默认为0 </param>
		/// <param name="count">可选参数，返回结果的页序号，有分页限制</param>
		/// <param name="page">可选参数，每次返回的最大记录数（即页面大小），不大于200 </param>
		/// <returns></returns>
		public dynamic ShareStatuses(string urlShort, string sinceID = "", string maxID = "", int count = 20, int page = 1)
		{
			List<WeiboStringParameter> parameters = new List<WeiboStringParameter>();
			parameters.Add(new WeiboStringParameter("url_short", urlShort));

			if (!string.IsNullOrWhiteSpace(sinceID))
				parameters.Add(new WeiboStringParameter("since_id", sinceID));
			if (!string.IsNullOrWhiteSpace(maxID))
				parameters.Add(new WeiboStringParameter("max_id", maxID));

			parameters.Add(new WeiboStringParameter("count", count));
			parameters.Add(new WeiboStringParameter("page", page));

			return DynamicJson.Parse(Client.GetCommand("short_url/share/statuses", parameters.ToArray()));
		}

		/// <summary>
		/// 取得一个短链接在微博上的微博评论数 
		/// </summary>
		/// <param name="url_short">需要取得评论数的短链接</param>
		/// <returns></returns>
		public dynamic CommentCounts(string url_short)
		{
			return DynamicJson.Parse(Client.GetCommand("short_url/comment/counts", new WeiboStringParameter("url_short", url_short)));
		}

		/// <summary>
		/// 取得包含指定单个短链接的最新微博评论内容 
		/// </summary>
		/// <param name="url_short">需要取得关联微博评论内容的短链接</param>
		/// <param name="since_id">若指定此参数，则返回ID比since_id大的评论（即比since_id时间晚的评论），默认为0 </param>
		/// <param name="max_id">若指定此参数，则返回ID小于或等于max_id的评论，默认为0 </param>
		/// <param name="count">可选参数，每次返回的最大记录数（即页面大小），不大于200 </param>
		/// <param name="page">可选参数，返回结果的页序号，有分页限制</param>
		/// <returns></returns>
		public dynamic CommentComments(string urlShort, string sinceID = "", string maxID = "", int count = 20, int page = 1)
		{
			List<WeiboStringParameter> parameters = new List<WeiboStringParameter>();
			parameters.Add(new WeiboStringParameter("url_short", urlShort));

			if (!string.IsNullOrWhiteSpace(sinceID))
				parameters.Add(new WeiboStringParameter("since_id", sinceID));
			if (!string.IsNullOrWhiteSpace(maxID))
				parameters.Add(new WeiboStringParameter("max_id", maxID));

			parameters.Add(new WeiboStringParameter("count", count));
			parameters.Add(new WeiboStringParameter("page", page));

			return DynamicJson.Parse(Client.GetCommand("short_url/comment/comments", parameters.ToArray()));
		}

	}
}