using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography.X509Certificates;

namespace TeleBirr
{
    public  class Utils
    {


        public string Sign_Sha256(Dictionary<string,string> data, string private_key=null)
        {
            //Sort the USSD Ascending order
            var list = data.Keys.ToList();
            list.Sort();

            var ussd_for_string_a = "";
            var ussd_for_string_b = "";
            foreach (var key in list)
            {
                if (ussd_for_string_a == "")
                {
                    ussd_for_string_a = key + "=" + list;
                }
                else
                {
                    ussd_for_string_a += "&" + key + "=" + list;
                }
            }

            if (private_key == null)
            {
                byte[] byteToHash = Encoding.UTF8.GetBytes(ussd_for_string_a);
                
               // ussd_for_string_b = SHA256.Create(byteToHash.);
            }
            else
            {
               // ussd_for_string_b = sign_rsa
            }


            return ussd_for_string_b;
        }

        public string SignWithRSA(string data,string key,string sign_type="SHA256withRSA")
        {
            if (sign_type == "SHA256withRSA")
            {
                var key_byte = Encoding.UTF8.GetBytes(key);
                var digest = SHA256.Create();
                digest.ComputeHash(Encoding.UTF8.GetBytes(data));
                var signer = 
            }
            else
            {
                return "Only SHA256withRSA hash allowed";
            }
        }

        /// <summary>
        /// Returns Globally Unique string to use as Nonce string
        /// </summary>
        /// <returns></returns>
        public string CreateNonceString()
        {
            return Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Returns TimeStamp 
        /// </summary>
        /// <returns></returns>
        public string CreateTimeStamp()
        {
            return DateTime.Now.Ticks.ToString();
        }

        /// <summary>
        /// Returns Randomized Merchant Order Id
        /// </summary>
        /// <returns></returns>
        public string CreateMerchantOrderId()
        {
            //var orderNo = Random.Shared.Next(100, 100000).ToString();
            var orderNo = DateTime.Now.Ticks.ToString();
            return orderNo;
        }
    }
}
