using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using AngleSharp;
using GLua.Helpers;

namespace GLua.Models
{
    class Wiki
    {
        string BaseUrl = "http://wiki.garrysmod.com";
        string WikiUrl = "http://wiki.garrysmod.com/navbar";
        string WikiUpdatePage = "https://gmod.facepunch.com/changes";
        string FileName = "Wiki.json";
        string LastUpdate = Settings.Default.LastUpdate;
        public List<Function> Functions { get ; set; } = new List<Function>();

        public async void Get()
        {
            Functions = await DocumentParser.ParseWiki(BaseUrl, WikiUrl);
            SaveJsonObj();
        }

        public void LoadJsonObj()
        {
            using (StreamReader sr = new StreamReader("Obj"+FileName))
            {
                string str = sr.ReadToEndAsync().Result;
                Functions = JsonConvert.DeserializeObject<List<Function>>(str);
            }          
        }

        public async Task<bool> HaveUpdate()
        {
            var document = await BrowsingContext.New(Configuration.Default.WithDefaultLoader()).OpenAsync(WikiUpdatePage);
            var UpdateTime = document.QuerySelector("version").TextContent;
            document.Dispose();
            if (UpdateTime != LastUpdate)
            {
                if (UpdateTime == null)
                {
                    UpdateTime = "error";
                }
                LastUpdate = UpdateTime;
                Settings.Default.LastUpdate = UpdateTime;
                Settings.Default.Save();
                return true;
            }
            return false;
        }

        public async void SaveJson()
        {
            if (Functions.Count > 0)
            {               
                StringBuilder sb = new StringBuilder();
                using (StringWriter sw = new StringWriter(sb))
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    writer.Formatting = Formatting.Indented;
                    writer.WriteStartObject();
                    foreach (Function root in Functions)
                    {
                        writer.WritePropertyName(root.Name);
                        writer.WriteStartObject();
                        foreach (Function classes in root.Childs)
                        {
                            writer.WritePropertyName(classes.Name);
                            writer.WriteStartObject();
                            if (classes.Childs.Count > 0)
                            {
                                foreach (Function func in classes.Childs)
                                {
                                    writer.WritePropertyName(func.Name);
                                    writer.WriteStartObject();
                                    writer.WriteEndObject();
                                }
                            }
                            writer.WriteEndObject();
                        }
                        writer.WriteEndObject();
                    }
                    writer.WriteEndObject();
                }

                using (StreamWriter wr = new StreamWriter(FileName))
                {
                    await wr.WriteAsync(sb.ToString());
                }
                sb.Clear();
            }
        }

        public async void SaveJsonObj()
        {
            if (Functions.Count>0)
            {
                await Task.Factory.StartNew(() =>
                {
                    using (StreamWriter sw = new StreamWriter("Obj"+FileName))
                    {
                        sw.Write(JsonConvert.SerializeObject(Functions));
                    }
                });
            }                           
        }
    }
}
