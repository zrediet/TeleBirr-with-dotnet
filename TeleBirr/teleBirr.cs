using System.Buffers.Text;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TeleBirr
{

    public class teleBirr
    {
        public static string? _app_id; //d1a11eda84bc462881263c500701f1e2
        public static string? _app_key; //9a87922442c9486e9128d6c0c98779e3
        public static string? _public_key; 
        //MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAlletxLWhxMpSxEGUk3CIpBV2cBnqWm+mKe1yF4ErEv9SoSdhDlOUqOQubOJVWAH274UP
        //psxCKO+hM9Ftd+TulIu2iD/c9Z5mlbAB9uDleU0uXLS/rIp/rWLi6rcWZbwh4xwqqf3xAFK50g0fC24mYSK3vWjVrv8+WCLlQHiFewc6c8xPxQH9L
        //DlU24CHoi4f5DwYRSdzcBAwk4+GFa7PKr4xteejv1t/tpjrzMuoAAJvqJFphP2XISZMBpUEaFvqfZwA7wL+1ChlVhhWNcWm8
        public static string? _notify_url;
        public static string? _receiver_name;
        public static string? _return_url;
        public static string? _short_code;
        public static string? _subject;
        public static string? _timeout_express;
        public static string? _total_amount;
        public static string? _nonce;
        public static string? _out_trade_no;
        public static string? _api = "http://196.188.120.3:10443/service-openup/toTradeWebPay";
        
        public teleBirr(string app_id, string app_key, string public_key, string notify_url, string receiver_name, string return_url, string short_code, string subject, string timeout_express, string total_amount, string nonce, string out_trade_no,
            string api = "http://196.188.120.3:10443/service-openup/toTradeWebPay")
        {
            _api = api;
            _app_id = app_id;
            _app_key = app_key;
            _public_key = public_key;
            _notify_url = notify_url;
            _receiver_name = receiver_name;
            _return_url = return_url;
            _short_code = short_code;
            _subject = subject;
            _timeout_express = timeout_express;
            _total_amount = total_amount;
            _nonce = nonce;
            _out_trade_no = out_trade_no;
           Dictionary<string,string> ussdPayload = Ussd();
           var ussdResult = Encrypt_Ussd(ussdPayload, public_key);
            
        }

        public Dictionary<string, string> Ussd()
        {
            var ussd = new Dictionary<string, string>
            {
                {
                    "appId",
                    _app_id
                },
                {
                    "notifyUrl",
                    _notify_url
                },
                {
                    "outTradeNo",
                    _out_trade_no
                },
                {
                    "receiverName",
                    _receiver_name
                },
                {
                    "returnUrl",
                    _return_url
                },
                {
                    "shortCode",
                    _short_code
                },
                {
                    "subject",
                    _subject
                },
                {
                    "timeoutExpress",
                    _timeout_express
                },
                {
                    "total_amount",
                    _total_amount
                },
                {
                    "nonce",
                    _nonce
                },
                {
                    "timestamp",
                    Convert.ToInt32(DateTime.Now.Ticks * 1000).ToString()
                }
            };
            return ussd;
        }

        //Required Methods
        // Encrypt_Ussd(string, ussd, string public_key)
        // Encrypt(string public_key, string msg)
        // Sign (string ussd, string app_key)
        // Request_Params()
        // Send_Request()
        // Decrypt(string public_key, string payload)

        public string Encrypt_Ussd(Dictionary<string,string> ussd, string public_key)
        {
            //Divide the public_key every 64 characters
            var size = public_key.Length; //length of public key
            var part = size / 64;
            var start = 0;
            //string? formatted_Public_Key;
            var t = part;
            string result=null;
            while (start < public_key.Length)
            {
                var temp = new string(public_key);
                temp = temp.Substring(start, part - start)+"\n";
                start = part;
                part += t;
                result = temp;
            }

            var r = "-----BEGIN CERTIFICATE-----" + "\n" + result + "\n" + "-----END CERTIFICATE-----";
            var ussd_json = JsonConvert.SerializeObject(r);
            // then encrypt the ussd_json
            
            var encrypt = Encrypt(_public_key, ussd_json);

            return encrypt;
        }

        public  string Encrypt(string public_key, string msg)
        {    
            RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
            RSA.ImportCspBlob(System.Convert.FromBase64String(public_key));

            //var cipher = RSA.ToXmlString(true);
            var cipherText = "b" + "";

            //chunk the msg every 117 length and encrypt
            // if there is any remaining string, encrypt the remaining strings

            var st = msg.ToCharArray();
            var floor = Math.Floor((decimal)(msg.Length/117));

            foreach (var i in st)
            {
                
                if (i == 0)
                {
                    byte[] data = Encoding.UTF8.GetBytes(msg[(i + 1) * 117].ToString());
                    cipherText += RSA.Encrypt(data,true);
                }
                else
                {
                    byte[] data = Encoding.UTF8.GetBytes(msg[i * 117].ToString());
                    cipherText += RSA.Encrypt(data,true);
                }
            }

            var s = msg.Length % 117;
            
            cipherText += Encoding.UTF8.GetBytes((msg.Length/117) * 117 ? msg.Length);
             


            foreach (var i in Enumerable.Range(0, msg.Length / 117))
            {
                //convert the string to UTF8 Encoding
                byte[] data = Encoding.UTF8.GetBytes(msg[i * 117].ToString());
                //Encrypt the data and append to cipherText
                cipherText += RSA.Encrypt(data,true);
                
                //cipherText += cipher.encrypt(msg[(i * 117):((i + 1) * 117)].encode("utf8"));
            }
            

            //cipherText += cipher.encrypt(msg[(((msg.Length)  /  117)  *  117) || (msg.Length)].encode("utf8"));
            //cipherText += RSA.Encrypt(msg[(msg.Length)  /  117)  *  117) : msg.Length].encode("utf8"));

            //cipherText = RSA.EncryptValue(cipherT)
            
            return cipherText;
        }

        public string Sign(Dictionary<string,string> ussd, string app_key)
        {
            return "";
        }
         
        public string Request_Params()
        {
            return "";

        }
    }
}