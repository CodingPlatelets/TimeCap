﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TimCap.Utils
{
    public static class PostMan
    {
        public static void GetPage(string posturl, string postData)
        {
            // Stream outstream = null;
            // Stream instream = null;
            // StreamReader sr = null;
            // HttpWebResponse response = null;
            // HttpWebRequest request = null;
            // Encoding encoding = Encoding.UTF8;
            // byte[] data = encoding.GetBytes(postData);
            // try
            // {
            //     // 设置参数
            //     request = WebRequest.Create(posturl) as HttpWebRequest;
            //     CookieContainer cookieContainer = new CookieContainer();
            //     request.CookieContainer = cookieContainer;
            //     request.AllowAutoRedirect = true;
            //     request.Method = "POST";
            //     request.ContentType = "application/json";
            //     request.ContentLength = data.Length;
            //     request.
            //     outstream = request.GetRequestStream();
            //     outstream.Write(data, 0, data.Length);
            //     outstream.Close();
            //     //发送请求并获取相应回应数据
            //     response = request.GetResponse() as HttpWebResponse;
            //     //直到request.GetResponse()程序才开始向目标网页发送Post请求
            //     instream = response.GetResponseStream();
            //     sr = new StreamReader(instream, encoding);
            //     //返回结果网页（html）代码
            //     string content = sr.ReadToEnd();
            //     string err = string.Empty;
            //     //   Response.Write(content);
            //     return content;
            // }
            // catch (Exception ex)
            // {
            //     string err = ex.Message;
            //     return string.Empty;
            // }

        }
    }
}