using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace TeleBirr
{
    public  class Utils
    {

        /// <summary>
        /// Returns Globally Unique string to use as Nonce string
        /// </summary>
        /// <returns></returns>
        public static string CreateNonceString()
        {
            return Guid.NewGuid().ToString();
        }

        /// <summary>
        /// Returns TimeStamp 
        /// </summary>
        /// <returns></returns>
        public static string CreateTimeStamp()
        {
            return DateTime.Now.Ticks.ToString();
        }

        /// <summary>
        /// Returns Randomized Merchant Order Id
        /// </summary>
        /// <returns></returns>
        public static string CreateMerchantOrderId()
        {
            //var orderNo = Random.Shared.Next(100, 100000).ToString();
            var orderNo = DateTime.Now.Ticks.ToString();
            return orderNo;
        }
    }
}
