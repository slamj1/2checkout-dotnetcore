using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace TwoCheckout
{
    public class TwoCheckoutUtil
    {
        public static string StopActiveLineitems(Dictionary<string, string> active )
        {
            string stoppedLineitems = null;
            SaleStopServiceOptions options = new SaleStopServiceOptions();
            foreach (var entry in active)
            {
                options.lineitem_id = Convert.ToInt64(entry.Value);
                try 
	            {	        
		            TwoCheckoutUtil.Request("api/sales/stop_lineitem_recurring", "POST", "admin", options);
                    stoppedLineitems += "," + entry.Value; //log the stopped lineitem in response mesage
	            }
	            catch (TwoCheckoutException)
	            {
		            return "{ 'response_code': 'NOTICE', 'response_message': 'Linetiem " + entry.Value + " could not be stopped.' }";
	            }
            }
            stoppedLineitems = stoppedLineitems.Remove(0, 1); //drop the leading comma
            return "{ 'response_code': 'OK', 'response_message': '" + stoppedLineitems + "' }";
        }

        public static Dictionary<string, string> Active(string response)
        {
            JObject Sale = JObject.Parse(response);
            var Active = new Dictionary<string, string>();
            try
            {
                JToken Invoices = Sale["sale"]["invoices"];
                int i = 0;
                JArray Lineitems;
                foreach (JObject invoice in Invoices)
                {
                    Lineitems = (JArray)invoice["lineitems"];
                    foreach (JObject lineitem in Lineitems)
                    {
                        if ((string)lineitem["billing"]["recurring_status"] == "active")
                        {
                            Active.Add("lineitem_id" + i, (string)lineitem["billing"]["lineitem_id"]);
                            i++;
                        }
                    }
                }
                return Active;
            }
            catch (NullReferenceException)
            {
                return Active;
            }
        }

        public static string Md5Hash(string input)
        {
            MD5CryptoServiceProvider Md5Hasher = new MD5CryptoServiceProvider();
            byte[] Data = Md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));
            StringBuilder SBuilder = new StringBuilder();
            for (int i = 0; i < Data.Length; i++)
            {
                SBuilder.Append(Data[i].ToString("x2"));
            }
            return SBuilder.ToString().ToUpper();
        }

        public static string SerializeObjectToQueryString(object source)
        {
            var type = source.GetType();
            var queryString = new StringBuilder();

            foreach (var property in type.GetProperties())
            {
                var propertyValue = property.GetValue(source, new object[] { });

                if (propertyValue == null)
                    continue;

                queryString.Append(property.Name);
                queryString.Append("=");
                queryString.Append(propertyValue.ToString());
                queryString.Append("&");
            }

            if (queryString.Length > 0)
                queryString.Remove(queryString.Length - 1, 1);

            return queryString.ToString();
        }

        public static string ConvertToJson(object source)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(source, Formatting.None, 
                new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore });
        }

        public static string DictionaryToQueryString(Dictionary<string, string> parameters)
        {
            List<string> Items = new List<string>();

            foreach (var name in parameters)
                Items.Add(string.Concat(name.Key, "=", name.Value));

            return string.Join("&", Items.ToArray());
        }

        public static T MapToObject<T>(string json, string pick = null)
        {
            JObject RawObject = JObject.Parse(json);
            string PickedString = (pick != null) ? JsonConvert.SerializeObject(RawObject[pick]) : JsonConvert.SerializeObject(RawObject);
            return JsonConvert.DeserializeObject<T>(PickedString);
        }

        public static string Request(string urlSuffix, string method, string type, Object args = null)
        {
            HttpWebRequest Request;
            string Url = string.Concat(TwoCheckoutConfig.BaseUrl, urlSuffix);
            string Params = null;
            string Result = null;
            string ContentType = null;
            HttpWebResponse Response = null;
            NetworkCredential Credential = null;

            if (type == "admin")
            {
                ContentType = (method == "POST") ? "application/x-www-form-urlencoded" : "*/*";
                Params = (args != null) ? TwoCheckoutUtil.SerializeObjectToQueryString(args) : null;
                Url = (method == "GET") ? string.Concat(Url, "?", Params) : Url;
                Credential = new NetworkCredential(TwoCheckoutConfig.ApiUsername, TwoCheckoutConfig.ApiPassword);
            }
            else
            {
                ContentType = "application/json";
                Params = TwoCheckoutUtil.ConvertToJson(args);
            }

            try
            {
                Request = WebRequest.Create(Url.ToString()) as HttpWebRequest;
                ServicePointManager.ServerCertificateValidationCallback += delegate { return true; };
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11;
                Request.Credentials = Credential;
                Request.Method = method;
                Request.ContentType = ContentType;
                Request.Accept = "application/json";
                Request.UserAgent = "2Checkout .NET/" + TwoCheckoutConfig.Version;
                if (method == "POST" && Params != "")
                {
                    byte[] byteData = UTF8Encoding.UTF8.GetBytes(Params);
                    Request.ContentLength = byteData.Length;
                    using (Stream postStream = Request.GetRequestStream())
                    {
                        postStream.Write(byteData, 0, byteData.Length);
                    }
                }
                using (Response = Request.GetResponse() as HttpWebResponse)
                {
                    StreamReader reader = new StreamReader(Response.GetResponseStream());
                    Result = reader.ReadToEnd();
                    return Result;
                }
            }
            catch (WebException wex)
            {
                string ResultCode = null;
                if (wex.Response != null)
                {
                    using (HttpWebResponse errorResponse = (HttpWebResponse)wex.Response)
                    {
                        StreamReader reader = new StreamReader(errorResponse.GetResponseStream());
                        Result = reader.ReadToEnd();
                        JObject RawError = JObject.Parse(Result);
                        if (RawError["errors"] != null)
                        {
                            Result = RawError["errors"][0]["message"].ToString();
                            ResultCode = RawError["errors"][0]["code"].ToString();
                        }
                        else if (RawError["exception"] != null)
                        {
                            Result = RawError["exception"]["errorMsg"].ToString();
                            ResultCode = RawError["exception"]["errorCode"].ToString();
                        }
                    }
                } else {
                    Result = wex.Message;
                    ResultCode = "500";
                }
                throw new TwoCheckoutException(Result) { Code = ResultCode };
            }
            finally
            {
                if (Response != null) { Response.Close(); }
            }
        }
    }
}