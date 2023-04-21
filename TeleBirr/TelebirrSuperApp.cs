using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeleBirr
{
    public  class TelebirrSuperApp
    {
        public static string? _short_code;
        public static string? _app_key;
        public static string? _app_secret;
        public static string? _merchant_id;
        public static string? _private_key;
        public static string? _url;

        public TelebirrSuperApp(string short_code, string app_key, string app_secret, string merchant_id, string private_key, string url)
        {
            _short_code = short_code;
            _app_key = app_key;
            _app_secret = app_secret;
            _merchant_id = merchant_id;
            _private_key = private_key;
            _url = url;
        }
         
    }
}
