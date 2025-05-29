//using ServiceStack.Text.Common;
using System.Text;
using Newtonsoft.Json;

namespace FuZhu_Data
{
    /// <summary>
    /// 公共类
    /// </summary>
    public class Common
    {
        /// <summary>
        /// 写入日志(文件路径： /LogFile/OperLog/yyyyMM)
        /// </summary>
        /// <param name="Msg">信息内容</param>
        /// <param name="LoginCode">操作人员</param>
        public static void WriteTxt(string Msg)
        {
          
             string strTime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string s_rpath = Utils.GetMapPath("");
            string s_Dir = "\\Logs\\"+ DateTime.Now.ToString("yyyyMM");
            string updir = s_rpath + s_Dir;
            if (!Directory.Exists(updir))
            {
                Directory.CreateDirectory(updir);
            }
            string s_FileName = DateTime.Now.ToString("yyyy-MM-dd") + ".txt";

            //bool bol = false;
            if (!File.Exists(updir + "\\" + s_FileName))
            {
                //File.Create()返回的是一个FileStream，这个需要关闭，才能对其创建的文件进行写操作
                //使用using自动关闭
                using (File.Create(updir + "\\" + s_FileName))
                {
                  //  bol = true;
                }
            }
            try
            {
                //第二个参数ture是append,如果文件存在，而且 append 为真时，则向文件中追加,如果 append 为假，则改写文件．
                StreamWriter wr = new StreamWriter(Utils.GetMapPath(s_Dir + "\\" + s_FileName), true, Encoding.Default);
                
                wr.WriteLine(strTime +  "     "+ Msg);              
                wr.Close();               
            }
            catch
            {
             
            }
        }

        /// <summary>
        /// 加密(获取签名)
        /// </summary>
        /// <param name="content">内容</param>
        /// <param name="secretKey">秘钥</param>
        /// <returns></returns>
        public static string GetSign(string content, string secretKey)
        {
            string toSignContent = content + secretKey;
            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] inputBytes = System.Text.Encoding.GetEncoding("utf-8").GetBytes(toSignContent);
            byte[] hash = md5.ComputeHash(inputBytes);
            return Convert.ToBase64String(hash);
        }
        //==============================================
        /// <summary>
        /// 返回 data按照字母的顺序排序后进行base64加密的字符串
        /// 检验收到的参数数据是否正确，并返回 结果
        /// (圆通快递)
        /// </summary>
        /// <param name="content">内容</param>
       
        /// <param name="appsec">秘钥</param>
        /// <returns></returns>
        public static  string GetDigestData(string content,  string appsec)
        {
            IDictionary<string, object> dict;
            dict = JsonConvert.DeserializeObject<Dictionary<string, object>>(content);
            //小到大排序 .OrderBy          从大到小排.OrderByDescending
          //  Dictionary<string, object> dic1Asc = dict.OrderBy(o => o.Key).ToDictionary(o => o.Key, p => p.Value);
            var Jsonstr = JsonConvert.SerializeObject(dict);
            string strDigestData = Base64Encode(Jsonstr);
            string strdigest = GetSign(strDigestData , appsec);
            return strdigest;
        }

        /// <summary>
        /// Base64加密，采用utf8编码方式加密
        /// </summary>
        /// <param name="source">待加密的明文</param>
        /// <returns>加密后的字符串</returns>
        public static string Base64Encode(string source)
        {
            return Base64Encode(Encoding.UTF8, source);
        }

        /// <summary>
        /// Base64加密
        /// </summary>
        /// <param name="encodeType">加密采用的编码方式</param>
        /// <param name="source">待加密的明文</param>
        /// <returns></returns>
        public static string Base64Encode(Encoding encodeType, string source)
        {
            string encode = string.Empty;
            byte[] bytes = encodeType.GetBytes(source);
            try
            {
                encode = Convert.ToBase64String(bytes);
            }
            catch
            {
                encode = source;
            }
            return encode;
        }
        //=================================================


    }
}
