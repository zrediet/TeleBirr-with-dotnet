using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TeleBirr
{

    public class teleBirr
    {
        public static string? _app_id;
        public static string? _app_key;
        public static string? _public_key;
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

        public teleBirr(
            string app_id,
            string app_key,
            string public_key,
            string notify_url,
            string receiver_name,
            string return_url,
            string short_code,
            string subject,
            string timeout_express,
            string total_amount,
            string nonce,
            string out_trade_no,
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
        }



    }
}