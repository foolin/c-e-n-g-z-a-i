using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Collections.Specialized;

namespace CengZai.OAuthSDK.Api
{
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
        public static string HttpPost(string url, IDictionary<object, object> parameters)
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
                    foreach (string key in parameters.Keys)
                    {
                        if (strParameters.Length > 0)
                        {
                            strParameters.Append("&");
                        }
                        strParameters.Append(key + "=" + Uri.EscapeDataString(parameters[key] + ""));
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
        /// 上传图片文件 | 测试腾讯微博通过
        /// </summary>
        /// <param name="url">提交的地址</param>
        /// <param name="parameters">提交的参数</param>
        /// <param name="formName">文件表单名</param>
        /// <param name="fileName">上传的文件路径  比如： c:\12.jpg</param>
        /// <returns></returns>
        public static string HttpPost(string url, IDictionary<object, object> parameters, string formName, string fileName)
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
            foreach (string key in parameters.Keys)
            {
                sb.Append("--" + boundary);
                sb.Append("\r\n");
                sb.Append("Content-Disposition: form-data;name=\"" + key + "\"");
                sb.Append("\r\n\r\n");
                sb.Append(parameters[key]);
                sb.Append("\r\n");
            }

            // 文件域的数据 
            sb.Append("--" + boundary);
            sb.Append("\r\n");
            sb.Append("Content-Disposition: form-data;name=\"" + formName + "\";filename=\"" + fileName + "\"");
            sb.Append("\r\n");

            sb.Append("Content-Type: ");
            sb.Append("image/jpeg");
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
        public static string HttpPost(string url, IDictionary<object, object> parameters, string formName, byte[] fileBytes)
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
            foreach (string key in parameters.Keys)
            {
                sb.Append("--" + boundary);
                sb.Append("\r\n");
                sb.Append("Content-Disposition: form-data;name=\"" + key + "\"");
                sb.Append("\r\n\r\n");
                sb.Append(parameters[key]);
                sb.Append("\r\n");
            }

            // 文件域的数据 
            sb.Append("--" + boundary);
            sb.Append("\r\n");
            sb.Append("Content-Disposition: form-data;name=\"" + formName + "\";filename=\"" + formName + ".jpg" + "\"");
            sb.Append("\r\n");

            sb.Append("Content-Type: ");
            sb.Append("image/jpeg");
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


    }

}



#region __已删除-代码备份用__



///// <summary>
///// 文件信息
///// </summary>
//public class MyFileInfo
//{
//    /// <summary>
//    /// 表单名
//    /// </summary>
//    public string FormName { set; get; }
//    /// <summary>
//    /// 文件名
//    /// </summary>
//    public string FileName { set; get; }
//    /// <summary>
//    /// 文件流
//    /// </summary>
//    public byte[] FileStream { set; get; }

//}

///// <summary>
///// HTTP POST方式请求数据(带图片)
///// </summary>
///// <param name="url">URL</param>        
///// <param name="parameters">POST的数据</param>
///// <param name="fileBytes">图片</param>
///// <returns></returns>
//public static string HttpPost(string url, IDictionary<object, object> parameters, string formName, byte[] fileBytes)
//{
//    string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
//    byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");

//    HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(url);
//    wr.ContentType = "multipart/form-data;boundary=" + boundary;
//    wr.Method = "POST";
//    wr.KeepAlive = true;
//    wr.Credentials = System.Net.CredentialCache.DefaultCredentials;

//    Stream rs = wr.GetRequestStream();
//    string responseStr = null;

//    string formdataTemplate = "Content-Disposition: form-data;name=\"{0}\"\r\n\r\n{1}";
//    foreach (string key in parameters.Keys)
//    {
//        rs.Write(boundarybytes, 0, boundarybytes.Length);
//        string formitem = string.Format(formdataTemplate, key, parameters[key]);
//        byte[] formitembytes = System.Text.Encoding.UTF8.GetBytes(formitem);
//        rs.Write(formitembytes, 0, formitembytes.Length);
//    }
//    rs.Write(boundarybytes, 0, boundarybytes.Length);

//    string headerTemplate = "Content-Disposition: form-data;name=\"{0}\";filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n";
//    string header = string.Format(headerTemplate, formName, formName + ".jpg", "text/plain");//image/jpeg
//    byte[] headerbytes = System.Text.Encoding.UTF8.GetBytes(header);
//    rs.Write(headerbytes, 0, headerbytes.Length);
//    rs.Write(fileBytes, 0, fileBytes.Length);

//    byte[] trailer = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
//    rs.Write(trailer, 0, trailer.Length);
//    rs.Close();

//    WebResponse wresp = null;
//    try
//    {
//        wresp = wr.GetResponse();
//        Stream stream2 = wresp.GetResponseStream();
//        StreamReader reader2 = new StreamReader(stream2);
//        responseStr = reader2.ReadToEnd();
//    }
//    catch (Exception ex)
//    {
//        if (wresp != null)
//        {
//            wresp.Close();
//            wresp = null;
//        }
//        throw ex;
//    }
//    finally
//    {
//        wr = null;
//    }
//    return responseStr;
//}



//public static string HttpPost(string url, IDictionary<object, object> parameters, List<MyFileInfo> files)
//{
//    string boundary = "----------------------------" +
//    DateTime.Now.Ticks.ToString("x");


//    HttpWebRequest httpWebRequest2 = (HttpWebRequest)WebRequest.Create(url);
//    httpWebRequest2.ContentType = "multipart/form-data;boundary=" +
//    boundary;
//    httpWebRequest2.Method = "POST";
//    httpWebRequest2.KeepAlive = true;
//    httpWebRequest2.Credentials =
//    System.Net.CredentialCache.DefaultCredentials;


//    Stream memStream = new System.IO.MemoryStream();

//    byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" +
//    boundary + "\r\n");


//    string formdataTemplate = "\r\n--" + boundary +
//    "\r\nContent-Disposition: form-data;name=\"{0}\";\r\n\r\n{1}";

//    foreach (string key in parameters.Keys)
//    {
//        memStream.Write(boundarybytes, 0, boundarybytes.Length);
//        string formitem = string.Format(formdataTemplate, key, parameters[key]);
//        byte[] formitembytes = System.Text.Encoding.UTF8.GetBytes(formitem);
//        memStream.Write(formitembytes, 0, formitembytes.Length);
//    }


//    if (files != null && files.Count > 0)
//    {
//        //string headerTemplate = "Content-Disposition: form-data;name=\"{0}\";filename=\"{1}\"\r\n Content-Type: application/octet-stream\r\n\r\n";
//        string headerTemplate = "Content-Disposition: form-data;name=\"{0}\";filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n";
//        for (int i = 0; i < files.Count; i++)
//        {
//            //string header = string.Format(headerTemplate, "file" + i, files[i]);
//            string header = string.Format(headerTemplate, files[i].FormName, files[i].FormName + ".jpg", "application/octet-stream");
//            byte[] headerbytes = System.Text.Encoding.UTF8.GetBytes(header);
//            memStream.Write(headerbytes, 0, headerbytes.Length);
//            if (files[i].FileStream != null && files[i].FileStream.Length > 0)
//            {
//                memStream.Write(boundarybytes, 0, boundarybytes.Length);   //边界字符串
//                memStream.Write(files[i].FileStream, 0, files[i].FileStream.Length);
//            }
//            else if (!string.IsNullOrEmpty(files[i].FileName))
//            {
//                memStream.Write(boundarybytes, 0, boundarybytes.Length);   //边界字符串

//                FileStream fileStream = new FileStream(files[i].FileName, FileMode.Open, FileAccess.Read);
//                byte[] buffer = new byte[1024];
//                int bytesRead = 0;
//                while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
//                {
//                    memStream.Write(buffer, 0, bytesRead);
//                }
//                fileStream.Close();

//                //一次性读取方法
//                //byte[] fileBytes = File.ReadAllBytes(files[i].FileName);
//                //memStream.Write(fileBytes, 0, fileBytes.Length);
//            }
//            else
//            {
//                continue;
//            }


//        }
//    }
//    byte[] trailer = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
//    memStream.Write(trailer, 0, trailer.Length);

//    httpWebRequest2.ContentLength = memStream.Length;

//    Stream requestStream = httpWebRequest2.GetRequestStream();

//    memStream.Position = 0;
//    byte[] tempBuffer = new byte[memStream.Length];
//    memStream.Read(tempBuffer, 0, tempBuffer.Length);
//    memStream.Close();
//    requestStream.Write(tempBuffer, 0, tempBuffer.Length);
//    requestStream.Close();


//    WebResponse webResponse2 = httpWebRequest2.GetResponse();
//    Stream stream2 = webResponse2.GetResponseStream();
//    StreamReader reader2 = new StreamReader(stream2);
//    string responseStr = reader2.ReadToEnd();
//    webResponse2.Close();
//    httpWebRequest2 = null;
//    webResponse2 = null;

//    return responseStr;
//}

///// <summary>
///// HTTP POST方式请求数据(带图片)
///// </summary>
///// <param name="url">URL</param>        
///// <param name="parameters">POST的数据</param>
///// <param name="fileBytes">图片</param>
///// <returns></returns>
//public static string HttpPost(string url, IDictionary<object, object> parameters, IDictionary<string, byte[]> files)
//{
//    string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
//    byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");

//    HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(url);
//    wr.ContentType = "multipart/form-data;boundary=" + boundary;
//    wr.Method = "POST";
//    wr.KeepAlive = true;
//    wr.Credentials = System.Net.CredentialCache.DefaultCredentials;

//    Stream rs = wr.GetRequestStream();
//    string responseStr = null;

//    string formdataTemplate = "Content-Disposition: form-data;name=\"{0}\"\r\n\r\n{1}";
//    foreach (string key in parameters.Keys)
//    {
//        rs.Write(boundarybytes, 0, boundarybytes.Length);
//        string formitem = string.Format(formdataTemplate, key, parameters[key]);
//        byte[] formitembytes = System.Text.Encoding.UTF8.GetBytes(formitem);
//        rs.Write(formitembytes, 0, formitembytes.Length);
//    }

//    foreach (string fileName in files.Keys)
//    {
//        rs.Write(boundarybytes, 0, boundarybytes.Length);
//        string headerTemplate = "Content-Disposition: form-data;name=\"{0}\";filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n";
//        string header = string.Format(headerTemplate, fileName, files[fileName], "text/plain");//image/jpeg
//        byte[] headerbytes = System.Text.Encoding.UTF8.GetBytes(header);
//        rs.Write(headerbytes, 0, headerbytes.Length);
//        rs.Write(files[fileName], 0, files[fileName].Length);
//    }

//    byte[] trailer = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
//    rs.Write(trailer, 0, trailer.Length);
//    rs.Close();

//    WebResponse wresp = null;
//    try
//    {
//        wresp = wr.GetResponse();
//        Stream stream2 = wresp.GetResponseStream();
//        StreamReader reader2 = new StreamReader(stream2);
//        responseStr = reader2.ReadToEnd();
//    }
//    catch (Exception ex)
//    {
//        if (wresp != null)
//        {
//            wresp.Close();
//            wresp = null;
//        }
//        throw ex;
//    }
//    finally
//    {
//        wr = null;
//    }
//    return responseStr;
//}


//public static string HttpPost(string url, IDictionary<object, object> parameters, List<MyFileInfo> files)
//{
//    //声明临时缓存文件
//    Stream memoryPostStream = new MemoryStream();

//    // 这个可以是改变的，也可以是下面这个固定的字符串 
//    //string boundary = "----------------------------7d930d1a850658";
//    string boundary = "----------------------------" + DateTime.Now.Ticks.ToString("x");

//    // 构造发送数据头
//    StringBuilder sbHeader = new StringBuilder();
//    // 提交参数数据
//    foreach (string key in parameters.Keys)
//    {
//        sbHeader.Append("--" + boundary);
//        sbHeader.Append("\r\n");
//        sbHeader.Append("Content-Disposition: form-data;name=\"" + key + "\"");
//        sbHeader.Append("\r\n\r\n");
//        sbHeader.Append(parameters[key]);
//        sbHeader.Append("\r\n");
//    }
//    byte[] postHeaderBytes = Encoding.UTF8.GetBytes(sbHeader.ToString());
//    // 输入头部数据 
//    memoryPostStream.Write(postHeaderBytes, 0, postHeaderBytes.Length);

//    //构造文件数据
//    Stream postFilesStream = null;
//    if (files != null && files.Count > 0)
//    {
//        postFilesStream = new MemoryStream();
//        StringBuilder sbFileHeader = new StringBuilder();
//        sbFileHeader.Append("--" + boundary);
//        sbFileHeader.Append("\r\n");
//        sbFileHeader.Append("Content-Disposition: form-data;name=\"{0}\";filename=\"{1}\"");
//        sbFileHeader.Append("\r\n");
//        sbFileHeader.Append("Content-Type: ");
//        sbFileHeader.Append("image/jpeg");
//        sbFileHeader.Append("\r\n\r\n");
//        foreach (MyFileInfo file in files)
//        {
//            // 文件域的头部数据 
//            string strFileHeader = string.Format(sbFileHeader.ToString(), file.FormName, file.FileName);
//            byte[] fileHeaderBytes = Encoding.UTF8.GetBytes(strFileHeader);
//            memoryPostStream.Write(postHeaderBytes, 0, fileHeaderBytes.Length);

//            //文件数据
//            if (file.FileStream != null && file.FileStream.Length > 0)
//            {
//                memoryPostStream.Write(file.FileStream, 0, file.FileStream.Length);
//            }
//            else
//            {
//                FileStream fileStream = new FileStream(file.FileName, FileMode.Open, FileAccess.Read);
//                // 输入文件流数据 
//                byte[] buffer = new Byte[checked((uint)Math.Min(4096, (int)fileStream.Length))];
//                int bytesRead = 0;
//                while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
//                    memoryPostStream.Write(buffer, 0, bytesRead);
//            }
//        }
//    }

//    //构造尾部数据 
//    byte[] boundaryBytes = Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
//    memoryPostStream.Write(boundaryBytes, 0, boundaryBytes.Length);


//    // 创建request对象 
//    HttpWebRequest webrequest = (HttpWebRequest)WebRequest.Create(url);
//    webrequest.ContentType = "multipart/form-data;boundary=" + boundary;
//    webrequest.Method = "POST";
//    //webrequest.Headers.Add("Cookie: " + cookie);
//    //webrequest.Referer = refre;
//    webrequest.ContentLength = memoryPostStream.Length; //数据长度

//    //数据流
//    //Stream requestStream = webrequest.GetRequestStream();
//    //memoryPostStream.Position = 0;
//    //var tempBuffer = new byte[memoryPostStream.Length];
//    //memoryPostStream.Read(tempBuffer, 0, tempBuffer.Length);
//    //memoryPostStream.Close();
//    //requestStream.Write(tempBuffer, 0, tempBuffer.Length);
//    //requestStream.Close();

//    //数据流
//    Stream requestStream = webrequest.GetRequestStream();
//    byte[] buffer2 = new Byte[checked((uint)Math.Min(4096, (int)memoryPostStream.Length))];
//    int bytesRead2 = 0;
//    while ((bytesRead2 = memoryPostStream.Read(buffer2, 0, buffer2.Length)) != 0)
//        requestStream.Write(buffer2, 0, bytesRead2);

//    WebResponse response = webrequest.GetResponse();
//    Stream s = response.GetResponseStream();
//    StreamReader sr = new StreamReader(s);
//    // 返回数据流(源码) 
//    string responseStr = sr.ReadToEnd();
//    response.Close();

//    return responseStr;
//}
#endregion
