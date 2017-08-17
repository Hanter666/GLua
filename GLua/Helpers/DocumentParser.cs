using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngleSharp;
using GLua.Models;
using System.Net;
using AngleSharp.Parser.Html;

namespace GLua.Helpers
{
    /// <summary>
    /// Wiki parser
    /// </summary>
    static class DocumentParser
    {
        /// <summary>
        /// Get Functions from Wiki
        /// </summary>
        /// <param name="BaseUrl">Host Url</param>
        /// <param name="WikiUrl">Specific Wiki url with info</param>
        /// <returns></returns>
        public async static Task<List<Function>> ParseWiki(string BaseUrl, string WikiUrl)
        {
            var Functions = new List<Function>();
            using (var document = await BrowsingContext.New(Configuration.Default.WithDefaultLoader()).OpenAsync(WikiUrl))
            {
                var content = document.QuerySelectorAll("body > ul > li");

                foreach (var roots in content)
                {
                    string root = roots.QuerySelector("h1").TextContent;
                    Function f = new Function()
                    {
                        Name = root
                    };

                    foreach (var parent in roots.QuerySelectorAll("body > ul > li>ul > li"))
                    {
                        Function child = new Function();
                        var element = parent.QuerySelector("h2");
                        if (element == null)
                        {
                            element = parent.QuerySelector("a");
                            child.Name = element.TextContent;
                            child.Url = element.GetAttribute("href");
                            child.Side = element.ClassName;
                            f.Childs.Add(child);
                            continue;
                        }
                        else
                        {
                            child.Name = element.TextContent.Replace("»", "").Trim();
                            child.Url = BaseUrl + element.GetAttribute("href");
                        }

                        foreach (var function in parent.QuerySelectorAll("ul>li>a"))
                        {
                            string funcName = function.TextContent;
                            string sideString = function.ClassName;
                            if (string.IsNullOrWhiteSpace(sideString))
                            {
                                sideString = "shared_m";
                            }
                            string funcUrl = function.GetAttribute("href");

                            Function func = new Function()
                            {
                                Side = sideString,
                                Name = funcName,
                                Url = BaseUrl + funcUrl                              
                            };
                            DocumentParser.ParseAddInfoPage(func);
                            child.Childs.Add(func);
                        }
                        f.Childs.Add(child);
                    }
                    Functions.Add(f);
                }
            }

            return Functions;
        }
        /// <summary>
        /// Parse Function and add info in it
        /// </summary>
        /// <param name="node"></param>
        public static async void ParseAddInfoPage(Function node)
        {
            string url = node.Url;
            if (url == null)
            {
                return;
            }
            var f = node;
            using (WebClient client = new WebClient())
            {              
                string htmlRaw = await client.DownloadStringTaskAsync(url+ "?action=raw");
                string formated = htmlRaw.Trim().Replace("{", "").Replace("}", "").Replace("|", "");
                var args = formated.Split(new string[] { "Arg" }, StringSplitOptions.RemoveEmptyEntries).ToList();
                args.RemoveAt(0);
                foreach (string arg in args)
                {
                    string[] splited = arg.Trim().Split('\n');
                    List<string> clearArgs = new List<string>();
                    foreach (string item in splited)
                    {
                        string clearedArg = item.Replace("type=", "").Replace("name=", "").Replace("desc=", "");
                        clearArgs.Add(clearedArg);                       
                    }
                    f.Args.Add(clearArgs.ToArray());
                }
            }
            node = f;
        }
    }
}
