﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace NetDimension.Weibo
{

	public enum ResponseType
	{
		Code,
		Token
	}

	public enum DisplayType
	{
		Default,
		Mobile,
		Popup,
		Wap12,
		Wap20,
		JS,
		ApponWeibo
	}

	public enum GrantType
	{
		AuthorizationCode,
		Password,
		RefreshToken
	}

	public enum RequestMethod
	{
		Get,
		Post
	}

	public enum ResetCountType
	{
		/// <summary>
		/// 新微博数
		/// </summary>
		status,
		/// <summary>
		/// 新粉丝数
		/// </summary>
		follower,
		/// <summary>
		/// 新评论数
		/// </summary>
		cmt,
		/// <summary>
		/// 新私信数
		/// </summary>
		dm,
		/// <summary>
		/// 新提及我的微博数
		/// </summary>
		mention_status,
		/// <summary>
		/// 新提及我的评论数
		/// </summary>
		mention_cmt
	}

	public enum RepostCommentType
	{
		NoComment,
		Current,
		Orign,
		Both
	}

	public enum GenderType
	{
		Male,
		Female,
		Unknown
	}

	public enum HotUserCatagory
	{
		/// <summary>
		/// 人气关注
		/// </summary>
		@default,
		/// <summary>
		/// 影视明星
		/// </summary>
		ent,
		/// <summary>
		/// 港台名人
		/// </summary>
		hk_famous,
		/// <summary>
		/// 模特
		/// </summary>
		model,
		/// <summary>
		/// 美食与健康
		/// </summary>
		cooking,
		/// <summary>
		/// 体育名人
		/// </summary>
		sport,
		/// <summary>
		/// 商界名人
		/// </summary>
		finance,
		/// <summary>
		/// IT互联网
		/// </summary>
		tech,
		/// <summary>
		/// 歌手
		/// </summary>
		singer,
		/// <summary>
		/// 作家
		/// </summary>
		writer,
		/// <summary>
		/// 主持人
		/// </summary>
		moderator,
		/// <summary>
		/// 媒体总编
		/// </summary>
		medium,
		/// <summary>
		/// 炒股高手
		/// </summary>
		stockplayer
	}

	public enum EmotionType
	{
		/// <summary>
		/// 普通表情
		/// </summary>
		face,
		/// <summary>
		/// 魔法表情
		/// </summary>
		ani,
		/// <summary>
		/// 动漫表情
		/// </summary>
		cartoon
	}

	public enum LanguageType
	{ 
		/// <summary>
		/// 简体
		/// </summary>
		cnname,
		/// <summary>
		/// 繁体
		/// </summary>
		twname
	}





	internal static class Utility
	{
		public static string BuildQueryString(Dictionary<string, string> parameters)
		{
			List<string> pairs = new List<string>();
			foreach (KeyValuePair<string, string> item in parameters)
			{
				pairs.Add(string.Format("{0}={1}", HttpUtility.UrlEncode(item.Key), HttpUtility.UrlEncode(item.Value)));
			}

			return string.Join("&", pairs.ToArray());
		}

		public static string BuildQueryString(params WeiboParameter[] parameters)
		{
			List<string> pairs = new List<string>();
			foreach (var item in parameters)
			{
				if (item is WeiboStringParameter)
				{
					pairs.Add(string.Format("{0}={1}", HttpUtility.UrlEncode(item.Name), HttpUtility.UrlEncode(((WeiboStringParameter)item).Value)));
				}
			}

			return string.Join("&", pairs.ToArray());
		}

		public static string GetBoundary()
		{
			return HttpUtility.UrlEncode(Convert.ToBase64String(Guid.NewGuid().ToByteArray()));
		}

		public static byte[] BuildPostData(string boundary, params WeiboParameter[] parameters)
		{
			List<WeiboParameter> pairs = parameters.OrderBy(p => p.Name).ToList();

			string division = GetBoundary();

			string header = string.Format("--{0}", boundary);
			string footer = string.Format("--{0}--", boundary);
			string encoding = "iso-8859-1";//iso-8859-1

			StringBuilder contentBuilder = new StringBuilder();

			foreach (WeiboParameter p in pairs)
			{
				if (p is WeiboStringParameter)
				{
					WeiboStringParameter param = p as WeiboStringParameter;
					contentBuilder.AppendLine(header);
					contentBuilder.AppendLine(string.Format("content-disposition: form-data; name=\"{0}\"", param.Name));
					//contentBuilder.AppendLine("Content-Type: text/plain; charset=US-ASCII");// utf-8
					//contentBuilder.AppendLine("Content-Transfer-Encoding: 8bit");
					contentBuilder.AppendLine();
					//contentBuilder.AppendLine(HttpUtility.UrlEncode(param.Value).Replace("+", "%20"));
					contentBuilder.AppendLine(HttpUtility.UrlEncode(param.Value).Replace("+", "%20"));
				}
				else
				{
					WeiboBinaryParameter param = p as WeiboBinaryParameter;
					contentBuilder.AppendLine(header);
					contentBuilder.AppendLine(string.Format("content-disposition: form-data; name=\"{0}\"; filename=\"{1}\"", param.Name, string.Format("upload{0}", BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0))));
					contentBuilder.AppendLine("Content-Type: image/unknown");
					contentBuilder.AppendLine("Content-Transfer-Encoding: binary");
					contentBuilder.AppendLine();
					contentBuilder.AppendLine(Encoding.GetEncoding(encoding).GetString(param.Value));

				}
			}

			contentBuilder.Append(footer);

			return Encoding.GetEncoding(encoding).GetBytes(contentBuilder.ToString());

		}



	}
}
