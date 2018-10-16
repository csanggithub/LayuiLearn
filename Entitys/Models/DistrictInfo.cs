using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.DynamicData;

namespace Entitys.Models
{
    [TableName("districtinfos")]
    public class DistrictInfo
    {
        /// <summary>
        /// Id
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 地区名称
        /// </summary>
        public string DistrictName { get; set; }

        /// <summary>
        /// 父级Id
        /// </summary>
        public int Pid { get; set; }
    }
}



using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            int j = 0;
            string w = "";
            string z = "";
            string u = "";
            try
            {
                //StreamReader file = File.OpenText("json1.json");
                //JsonTextReader reader = new JsonTextReader(file);
                //JObject jsonObject = (JObject)JToken.ReadFrom(reader);
                //var isDelete = (bool)jsonObject["isDelete"];
                //var AccCode = (uint)jsonObject["AccCode"];
                //var Id = (uint)jsonObject["Id"];

                //file.Close();

                byte[] byData = new byte[130000];
                char[] charData = new char[130000];
                FileStream file = new FileStream("area.json", FileMode.Open);
                file.Seek(0, SeekOrigin.Begin);
                file.Read(byData, 0, 130000); //byData传进来的字节数组,用以接受FileStream对象中的数据,第2个参数是字节数组中开始写入数据的位置,它通常是0,表示从数组的开端文件中向数组写数据,最后一个参数规定从文件读多少字符.
                Decoder d = Encoding.Default.GetDecoder();
                d.GetChars(byData, 0, byData.Length, charData, 0);
                //Console.WriteLine(charData);
                var str = new string(charData); ;
                file.Close();
                var e = JsonConvert.DeserializeObject<Dictionary<string, object>>(str);
                var list = new List<DistrictInfo>();
                //for(int i=0;i<33;i++)
                //{
                //    //list.Add(new DistrictInfo { Id =  });
                //}

                foreach (var i in e)
                {

                    var a = (JObject)i.Value;
                    list.Add(new DistrictInfo { Id = Convert.ToInt32(i.Key), DistrictName = a["name"].ToString(), Pid = 0 });
                    var aqq = a["child"].ToString();
                    if (!string.IsNullOrEmpty(a["child"].ToString()) && j < 31)
                    {
                        var b = a["child"].ToString();
                        var aaa = JsonConvert.DeserializeObject<Dictionary<string, object>>(b);
                        foreach (var x in aaa)
                        {
                            var a1 = (JObject)x.Value;
                            list.Add(new DistrictInfo { Id = Convert.ToInt32(x.Key), DistrictName = a1["name"].ToString(), Pid = Convert.ToInt32(i.Key) });
                            if (!string.IsNullOrEmpty(a1["child"].ToString()))
                            {
                                var a3 = a1["child"].ToString();
                                var a4 = JsonConvert.DeserializeObject<Dictionary<string, string>>(a3);
                                foreach (var a5 in a4)
                                {
                                    list.Add(new DistrictInfo { Id = Convert.ToInt32(a5.Key), DistrictName = a5.Value, Pid = Convert.ToInt32(x.Key) });
                                    u = a5.Key;
                                }

                            }
                            z = x.Key;
                        }

                    }
                    w = i.Key;
                    j++;

                }
            }
            catch (Exception ex)
            {

            }
        }
    }

    public class AreaInfo
    {
        public string name { get; set; }
        public object child { get; set; }
    }

    public class DistrictInfo
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 地区名称
        /// </summary>
        public string DistrictName { get; set; }

        /// <summary>
        /// 父级Id
        /// </summary>
        public int Pid { get; set; }
    }
}
