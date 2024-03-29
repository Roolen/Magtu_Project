﻿using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AngleSharp;
using System.Linq;
using System.IO;
using System.Threading;
using AngleSharp.Html.Dom;

namespace Model
{
    public class ParserHtml
    {
        public static async Task<News[]> ParseNews(string url)
        {
            var config = Configuration.Default.WithDefaultLoader();
            var document = await BrowsingContext.New(config).OpenAsync(url);

            List<News> news = new List<News>();

            var titlePostList = document.QuerySelectorAll("h3[data-region-content=forum-post-core-subject]");
            var authorPostList = document.QuerySelectorAll("h3[data-region-content=forum-post-core-subject]+address a");
            var contentPostList = document.QuerySelectorAll(".post-content-container");

            for (int i = 0; i < titlePostList.Length; i++)
            {
                news.Add(new News(titlePostList[i].TextContent, authorPostList[i].TextContent, "",
                            new StringBuilder(contentPostList[i].TextContent)));
            }

            return news.ToArray();
        }

        public static async Task<string> Authorize(string user, string password)
        {
            var config = Configuration.Default.WithDefaultLoader().WithDefaultCookies();
            var document = await BrowsingContext.New(config).OpenAsync("https://newlms.magtu.ru/login/index.php");

            var loginToken = document.QuerySelectorAll("#login input[name=logintoken]").First().GetAttribute("value");

            var form = (IHtmlFormElement) document.QuerySelector("#login");
            await form.SubmitAsync(new { username = user, password = password, logintoken = loginToken });
            
            var cookie = document.Cookie;
            var idSession = cookie.Split(new char[] { '=' })[1];

            var profDoc = await BrowsingContext.New(config).OpenAsync("https://newlms.magtu.ru/user/profile.php");
            var checkElement = profDoc.QuerySelector("#modal-header h4");
            var isAuth = (checkElement?.TextContent == "Подтвердить") ? false
                                                                     : true;
            if (isAuth == false) return null;
            return idSession ?? null;
        }

        public static async Task<string> GetAvatarAddress(string idSession)
        {
            var config = Configuration.Default.WithDefaultLoader().WithDefaultCookies();
            var context = BrowsingContext.New(config); 
            context.SetCookie(new Url("https://newlms.magtu.ru/login/index.php"), "MoodleSession=" + idSession);
            var document = await context.OpenAsync("https://newlms.magtu.ru/login/index.php");

            var avatar = document.QuerySelector(".userpicture")?.Attributes?.GetNamedItem("src")?.Value;

            return avatar;
        }
    }
}
