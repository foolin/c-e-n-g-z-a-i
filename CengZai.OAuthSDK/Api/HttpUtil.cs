using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Collections.Specialized;

namespace CengZai.OAuthSDK.Api
{
    /// <summary>
    /// 版权所有: 版权所有(C) 2012，CengZai.com
    /// 内容摘要: 提交和获取Http的工具
    /// 完成日期：2012年4月28日
    /// 版    本：V1.0 
    /// 作    者：ForLink
    ///    
    /// 修改记录1: 
    /// 修改日期：
    /// 版 本 号：
    /// 修 改 人：
    /// 修改内容：
    /// </summary>
    public class HttpUtil
    {


        /// <summary>
        /// HTTP POST方式请求数据(带图片)
        /// </summary>
        /// <param name="url">URL</param>        
        /// <param name="parameters">POST的数据</param>
        /// <param name="fileByte">图片</param>
        /// <returns></returns>
        public static string HttpGet(string url)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.Method = "GET";
            //request.ContentType = "application/x-www-form-urlencoded";
            request.Accept = "*/*";
            request.Timeout = 15000;
            request.AllowAutoRedirect = false;

            WebResponse response = null;
            string responseStr = null;

            try
            {
                response = request.GetResponse();

                if (response != null)
                {
                    StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                    responseStr = reader.ReadToEnd();
                    reader.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                request = null;
                response = null;
            }

            return responseStr;
        }


        /// <summary>
        /// HTTP POST方式请求数据
        /// </summary>
        /// <param name="url">URL</param>        
        /// <param name="parameters">POST的数据</param>
        /// <returns></returns>
        public static string HttpPost(string url, List<HttpParameter> parameters)
        {
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.Accept = "*/*";
            request.Timeout = 15000;
            request.AllowAutoRedirect = false;

            StreamWriter requestStream = null;
            WebResponse response = null;
            string responseStr = null;

            try
            {
                requestStream = new StreamWriter(request.GetRequestStream());
                if (parameters != null && parameters.Count > 0)
                {
                    StringBuilder strParameters = new StringBuilder();
                    foreach (HttpParameter param in parameters)
                    {
                        if (strParameters.Length > 0)
                        {
                            strParameters.Append("&");
                        }
                        strParameters.Append(param.Name + "=" + Uri.EscapeDataString(param.Value + ""));
                    }
                    requestStream.Write(strParameters.ToString());
                }
                requestStream.Close();

                response = request.GetResponse();
                if (response != null)
                {
                    StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                    responseStr = reader.ReadToEnd();
                    reader.Close();
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                request = null;
                requestStream = null;
                response = null;
            }

            return responseStr;
        }


        /// <summary>
        /// 同步方式发起http post请求，可以同时上传文件
        /// </summary>
        /// <param name="url">请求URL</param>
        /// <param name="queryString">请求参数字符串</param>
        /// <param name="files">上传文件列表</param>
        /// <returns>请求返回值</returns>
        public string HttpPost(string url, string queryString)
        {
            List<HttpParameter> parameters = new List<HttpParameter>();
            if (!string.IsNullOrEmpty(queryString))
            {
                string[] arr = queryString.Split(new char[] { '&' }, StringSplitOptions.RemoveEmptyEntries);
                if (arr.Length > 0)
                {
                    foreach (string keyval in arr)
                    {
                        string[] items = keyval.Split(new char[] { '=' });
                        if (items.Length >= 2)
                        {
                            parameters.Add(new HttpParameter(items[0], items[1]));
                        }
                    }
                }
            }
            return HttpPost(url, parameters);
        }


        /// <summary>
        /// 上传图片文件 | 测试腾讯微博通过
        /// </summary>
        /// <param name="url">提交的地址</param>
        /// <param name="parameters">提交的参数</param>
        /// <param name="formName">文件表单名</param>
        /// <param name="fileName">上传的文件路径  比如： c:\12.jpg</param>
        /// <returns></returns>
        public static string HttpPost(string url, List<HttpParameter> parameters, string formName, string fileName)
        {
            // 这个可以是改变的，也可以是下面这个固定的字符串 
            //string boundary = "----------------------------7d930d1a850658";
            string boundary = "----------------------------" + DateTime.Now.Ticks.ToString("x");

            // 创建request对象 
            HttpWebRequest webrequest = (HttpWebRequest)WebRequest.Create(url);
            webrequest.ContentType = "multipart/form-data;boundary=" + boundary;
            webrequest.Method = "POST";
            //webrequest.Headers.Add("Cookie: " + cookie);
            //webrequest.Referer = refre;

            // 构造发送数据 
            StringBuilder sb = new StringBuilder();

            // 文本域的数据，将user=eking&pass=123456  格式的文本域拆分 ，然后构造 
            foreach (HttpParameter param in parameters)
            {
                sb.Append("--" + boundary);
                sb.Append("\r\n");
                sb.Append("Content-Disposition: form-data;name=\"" + param.Name + "\"");
                sb.Append("\r\n\r\n");
                sb.Append(param.Value);
                sb.Append("\r\n");
            }

            // 文件域的数据 
            sb.Append("--" + boundary);
            sb.Append("\r\n");
            sb.Append("Content-Disposition: form-data;name=\"" + formName + "\";filename=\"" + fileName + "\"");
            sb.Append("\r\n");

            sb.Append("Content-Type: ");
            sb.Append(GetContentType(fileName));
            sb.Append("\r\n\r\n");

            string postHeader = sb.ToString();
            byte[] postHeaderBytes = Encoding.UTF8.GetBytes(postHeader);

            //构造尾部数据 
            byte[] boundaryBytes = Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");

            FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            long length = postHeaderBytes.Length + fileStream.Length + boundaryBytes.Length;
            webrequest.ContentLength = length;

            Stream requestStream = webrequest.GetRequestStream();

            // 输入头部数据 
            requestStream.Write(postHeaderBytes, 0, postHeaderBytes.Length);

            // 输入文件流数据 
            byte[] buffer = new Byte[checked((uint)Math.Min(4096, (int)fileStream.Length))];
            int bytesRead = 0;
            while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                requestStream.Write(buffer, 0, bytesRead);

            // 输入尾部数据 
            requestStream.Write(boundaryBytes, 0, boundaryBytes.Length);
            WebResponse responce = webrequest.GetResponse();
            Stream s = responce.GetResponseStream();
            StreamReader sr = new StreamReader(s);

            // 返回数据流(源码) 
            return sr.ReadToEnd();
        }

        /// <summary>
        /// 上传图片文件 | 测试腾讯微博通过
        /// </summary>
        /// <param name="url">提交的地址</param>
        /// <param name="parameters">提交的参数</param>
        /// <param name="formName">文件表单名</param>
        /// <param name="fileName">上传的文件路径  比如： c:\12.jpg</param>
        /// <returns></returns>
        public static string HttpPost(string url, List<HttpParameter> parameters, string formName, byte[] fileBytes, string fileName = "", string contentType = "application/octetstream")
        {
            // 这个可以是改变的，也可以是下面这个固定的字符串 
            //string boundary = "----------------------------7d930d1a850658";
            string boundary = "----------------------------" + DateTime.Now.Ticks.ToString("x");

            // 创建request对象 
            HttpWebRequest webrequest = (HttpWebRequest)WebRequest.Create(url);
            webrequest.ContentType = "multipart/form-data;boundary=" + boundary;
            webrequest.Method = "POST";
            //webrequest.Headers.Add("Cookie: " + cookie);
            //webrequest.Referer = refre;

            // 构造发送数据 
            StringBuilder sb = new StringBuilder();

            // 文本域的数据，将user=eking&pass=123456  格式的文本域拆分 ，然后构造 
            foreach (HttpParameter param in parameters)
            {
                sb.Append("--" + boundary);
                sb.Append("\r\n");
                sb.Append("Content-Disposition: form-data;name=\"" + param.Name + "\"");
                sb.Append("\r\n\r\n");
                sb.Append(param.Value);
                sb.Append("\r\n");
            }

            // 文件域的数据 
            sb.Append("--" + boundary);
            sb.Append("\r\n");
            sb.Append("Content-Disposition: form-data;name=\"" + formName + "\";filename=\"" + fileName??formName + "\"");
            sb.Append("\r\n");

            sb.Append("Content-Type: ");
            sb.Append(contentType);
            sb.Append("\r\n\r\n");

            string postHeader = sb.ToString();
            byte[] postHeaderBytes = Encoding.UTF8.GetBytes(postHeader);

            //构造尾部数据 
            byte[] boundaryBytes = Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");

            if (fileBytes != null)
            {
                webrequest.ContentLength = postHeaderBytes.Length + fileBytes.Length + boundaryBytes.Length;
            }
            else
            {
                webrequest.ContentLength = postHeaderBytes.Length + boundaryBytes.Length;
            }

            Stream requestStream = webrequest.GetRequestStream();

            // 输入头部数据 
            requestStream.Write(postHeaderBytes, 0, postHeaderBytes.Length);

            // 输入文件流数据
            if (fileBytes != null)
            {
                requestStream.Write(fileBytes, 0, fileBytes.Length);
            }

            // 输入尾部数据 
            requestStream.Write(boundaryBytes, 0, boundaryBytes.Length);
            WebResponse responce = webrequest.GetResponse();
            Stream s = responce.GetResponseStream();
            StreamReader sr = new StreamReader(s);

            // 返回数据流(源码) 
            return sr.ReadToEnd();
        }


        /// <summary>
        /// 同步方式发起http post请求，可以同时上传文件
        /// </summary>
        /// <param name="url">请求URL</param>
        /// <param name="paras">请求参数列表</param>
        /// <param name="files">上传文件列表</param>
        /// <returns>请求返回值</returns>
        public static string HttpPost(string url, List<HttpParameter> parameters, List<HttpParameter> files)
        {
            Stream requestStream = null;
            StreamReader responseReader = null;
            string responseData = null;
            string boundary = DateTime.Now.Ticks.ToString("x");

            HttpWebRequest webRequest = WebRequest.Create(url) as HttpWebRequest;
            //webRequest.ServicePoint.Expect100Continue = false;
            webRequest.Timeout = 20000;
            webRequest.ContentType = "multipart/form-data; boundary=" + boundary;
            webRequest.Method = "POST";
            webRequest.KeepAlive = true;
            //webRequest.Credentials = CredentialCache.DefaultCredentials;

            Stream responseStream = null;

            try
            {
                Stream memStream = new MemoryStream();

                byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");
                string formdataTemplate = "\r\n--" + boundary + "\r\nContent-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";

                foreach (HttpParameter item in parameters)
                {
                    string formitem = string.Format(formdataTemplate, item.Name, Uri.EscapeDataString(item.Value));
                    byte[] formitembytes = Encoding.UTF8.GetBytes(formitem);
                    memStream.Write(formitembytes, 0, formitembytes.Length);
                }

                memStream.Write(boundarybytes, 0, boundarybytes.Length);

                string headerTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: \"{2}\"\r\n\r\n";

                foreach (HttpParameter item in files)
                {
                    string name = item.Name;
                    string filePath = item.Value as string;
                    string file = Path.GetFileName(filePath);
                    string contentType = GetContentType(file);

                    string header = string.Format(headerTemplate, name, file, contentType);
                    byte[] headerbytes = System.Text.Encoding.UTF8.GetBytes(header);

                    memStream.Write(headerbytes, 0, headerbytes.Length);

                    FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                    byte[] buffer = new byte[1024];
                    int bytesRead = 0;

                    while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                    {
                        memStream.Write(buffer, 0, bytesRead);
                    }

                    memStream.Write(boundarybytes, 0, boundarybytes.Length);
                    fileStream.Close();
                }

                webRequest.ContentLength = memStream.Length;

                requestStream = webRequest.GetRequestStream();

                memStream.Position = 0;
                byte[] tempBuffer = new byte[memStream.Length];
                memStream.Read(tempBuffer, 0, tempBuffer.Length);
                memStream.Close();
                requestStream.Write(tempBuffer, 0, tempBuffer.Length);
                requestStream.Close();
                requestStream = null;

                responseStream = webRequest.GetResponse().GetResponseStream();
                responseReader = new StreamReader(responseStream);
                responseData = responseReader.ReadToEnd();
            }
            catch
            {
                throw;
            }
            finally
            {
                if (requestStream != null)
                {
                    requestStream.Close();
                    requestStream = null;
                }

                if (responseStream != null)
                {
                    responseStream.Close();
                    responseStream = null;
                }

                if (responseReader != null)
                {
                    responseReader.Close();
                    responseReader = null;
                }

                webRequest = null;
            }

            return responseData;
        }



        /// <summary>
        /// 同步方式发起http post请求，可以同时上传文件
        /// </summary>
        /// <param name="url">请求URL</param>
        /// <param name="queryString">请求参数字符串</param>
        /// <param name="files">上传文件列表</param>
        /// <returns>请求返回值</returns>
        public static string HttpPost(string url, string queryString, List<HttpParameter> files)
        {
            List<HttpParameter> parameters = new List<HttpParameter>();
            if (!string.IsNullOrEmpty(queryString))
            {
                string[] arr = queryString.Split(new char[] { '&' }, StringSplitOptions.RemoveEmptyEntries);
                if (arr.Length > 0)
                {
                    foreach (string keyval in arr)
                    {
                        string[] items = keyval.Split(new char[] { '=' });
                        if (items.Length >= 2)
                        {
                            parameters.Add(new HttpParameter(items[0],items[1]));
                        }
                    }
                }
            }
            return HttpPost(url, parameters, files);
        }

        /// <summary>
        /// 根据文件名获取文件类型
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetContentType(string fileName)
        {
            string contentType = "application/octetstream";
            try
            {
                string ext = Path.GetExtension(fileName).ToLower();
                Microsoft.Win32.RegistryKey registryKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);

                if (registryKey != null && registryKey.GetValue("Content Type") != null)
                {
                    contentType = registryKey.GetValue("Content Type").ToString();
                }
            }
            catch { }
            return contentType;
        }

    }

    /// <summary>
    /// 请求参数
    /// </summary>
    public class HttpParameter
    {
        private string name = null;
        private string value = null;

        public HttpParameter(string name, string value)
        {
            this.name = name;
            this.value = value + "";
        }

        public HttpParameter(string name, object value)
        {
            this.name = name;
            this.value = value + "";
        }

        public string Name
        {
            get { return name; }
        }

        public string Value
        {
            get { return value; }
        }
    }


}