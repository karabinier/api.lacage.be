using Newtonsoft.Json;
using SpotifyAPI.Web.Models;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace api.lacage.be.Controllers
{
    public class BaseSpotifyController : ApiController
    {
        private Token _token = null;
        private string _clientId = "b5f2b69179f74aab89635b6e92c2dc9d";
        private string _clientSecret = "2924baee0eac44748a03efd3d5d6283d";
        //code Received From spotify after application request authorization to the account
        protected string _codeReceived = string.Empty;
        protected string _refreshToken = string.Empty;            
        //private string _codeReceived = "AQDpHDDx5bJ7WqDF111OznXc5E-1kCEGDsLuSDcX_RtOpE4gin2yE5LWGllzq8k-g0UOQ2IVVJCABoKhIaLKCNEVBrQDwNHl2BdLKZ9BZXCD8QSVrikux02oDvi89oVBt53ts5R7YQVc1EnrE7hCCPGGFD2PIZcLw92ruAlkKFF-_6HLvMEHOqWdo5pQyhi5x53Fz61N0DZXW2GOXU2b74M";
        // during auth request a redirect url was specified which must be used in here (and which is on the whitelist)
        protected string _redirectUriUsed = string.Empty;

        private string RequestAuth()
        {
            using (WebClient wc = new WebClient())
            {


                //wc.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(clientId + ":" + clientSecret)));


                //var col = new NameValueCollection();
                //col.Add("grant_type", "client_credentials");
                //col.Add("scope", " ");


                
                byte[] data = null;
                try
                {
                    data = wc.DownloadData(
                        string.Format("https://accounts.spotify.com/authorize/?client_id={0}&response_type=code&redirect_uri={1}",
                        _clientId,
                        "http://localhost/shoppingapi.lacage.be/api/spotifysearch?artistId=3zunDAtRDg7kflREzWAhxl"
                        ));                    
                }
                catch (WebException e)
                {
                    String test = new StreamReader(e.Response.GetResponseStream()).ReadToEnd();
                }
                return Encoding.UTF8.GetString(data);

            }
        }

        protected string RequestAccessTokens()
        {
            using (WebClient wc = new WebClient())
            {
                byte[] data = null;

                wc.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(_clientId + ":" + _clientSecret)));

                if (string.IsNullOrEmpty(_codeReceived) == false)
                {
                    var col = new NameValueCollection();
                    col.Add("grant_type", "authorization_code");
                    col.Add("code", _codeReceived);
                    col.Add("redirect_uri", _redirectUriUsed);

                    
                    string response = string.Empty;
                    try
                    {
                        data = wc.UploadValues("https://accounts.spotify.com/api/token", "POST", col);
                    }
                    catch (WebException e)
                    {
                        String test = new StreamReader(e.Response.GetResponseStream()).ReadToEnd();
                    }
                }
                else if (string.IsNullOrEmpty(_refreshToken) == false)
                {
                    var col = new NameValueCollection();
                    col.Add("grant_type", "refresh_token");
                    col.Add("refresh_token", _refreshToken);

                    string response = string.Empty;
                    try
                    {
                        data = wc.UploadValues("https://accounts.spotify.com/api/token", "POST", col);
                    }
                    catch (WebException e)
                    {
                        String test = new StreamReader(e.Response.GetResponseStream()).ReadToEnd();
                    }
                }
                return Encoding.UTF8.GetString(data);
            }
        }
       
        private string DoAuth()
        {
            using (WebClient wc = new WebClient())
            {


                wc.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(_clientId + ":" + _clientSecret)));

                var col = new NameValueCollection();
                col.Add("grant_type", "client_credentials");
                col.Add("scope", "user-follow-read");

                byte[] data = null;
                try
                {
                    data = wc.UploadValues("https://accounts.spotify.com/api/token", "POST", col);
                }
                catch (WebException e)
                {
                    String test = new StreamReader(e.Response.GetResponseStream()).ReadToEnd();
                }
                return Encoding.UTF8.GetString(data);
                //return JsonConvert.DeserializeObject<Token>(Encoding.UTF8.GetString(data));
                //return JsonConvert.DeserializeObject<Token>();
            }
        }

        private string DoRequest(string address)
        {

            using (WebClient wc = new WebClient())
            {

                #if DEBUG
                //wc.Proxy = new WebProxy("127.0.0.1", 8888);//null;
                #endif

                //wc.Headers.Add("Authorization", AccessToken.TokenType + " " + Convert.ToBase64String(Encoding.UTF8.GetBytes( this.AccessToken.AccessToken)));
                wc.Headers.Add("Authorization", AccessToken.TokenType + " " + this.AccessToken.AccessToken);
                String response = "";
                try
                {
                    byte[] data = wc.DownloadData(address);
                    response = Encoding.UTF8.GetString(data);  
                }
                catch (WebException e)
                {
                    response = new StreamReader(e.Response.GetResponseStream()).ReadToEnd();
                }
                Debug.WriteLine(response);
                return response;
            }
        }

        public Token AccessToken
        {
            get
            {
                if (_token == null)
                {
                    //_token = JsonConvert.DeserializeObject<Token>(DoAuth());
                    _token = JsonConvert.DeserializeObject<Token>(RequestAccessTokens());
                    _refreshToken = _token.RefreshToken;
                }
                return _token;
            }
        }

        

        public T DownloadData<T>(String url)
        {
            string json = DoRequest(url);
            var returnValue = JsonConvert.DeserializeObject(json);
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
