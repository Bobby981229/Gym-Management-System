using System;
using System.Collections.Generic;
using System.IO;
//using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Config
/// </summary>
namespace Gym_Management_System
{
    public class Config
    {

        /*****************************Test data for production environments*****************************/
        public static string alipay_public_key = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "Demo\\alipay_rsa_public_key.pem";
        //Here you need to configure the original private key that has not been converted by PKCS8
        public static string merchant_private_key = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "Demo\\rsa_private_key.pem ";
        public static string merchant_public_key = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "Demo\\rsa_public_key.pem";
        public static string appId = "2015042200550512";   
        public static string serverUrl = "https://openapi.alipay.com/gateway.do";
        public static string mapiUrl = "https://mapi.alipay.com/gateway.do";
        public static string monitor2088222892972212Url = "http://mcloudmonitor.com/gateway.do";
        public static string pid = "2088102146891244";
        /*****************************Test data for production environments*****************************/


        public static string charset = "utf-8";//"utf-8";
         public static string sign_type = "RSA";
         public static string version = "1.0";
     

        public Config()
        {
            //
        }

        public static string getMerchantPublicKeyStr()
        {
            StreamReader sr = new StreamReader(merchant_public_key);
            string pubkey = sr.ReadToEnd();
            sr.Close();
            if (pubkey != null)
            {
              pubkey=  pubkey.Replace("-----BEGIN PUBLIC KEY-----", "");
              pubkey = pubkey.Replace("-----END PUBLIC KEY-----", "");
              pubkey = pubkey.Replace("\r", "");
              pubkey = pubkey.Replace("\n", "");
            }
            return pubkey;
        }

        public static string getMerchantPriveteKeyStr()
        {
            StreamReader sr = new StreamReader(merchant_private_key);
            string pubkey = sr.ReadToEnd();
            sr.Close();
            if (pubkey != null)
            {
                pubkey = pubkey.Replace("-----BEGIN PUBLIC KEY-----", "");
                pubkey = pubkey.Replace("-----END PUBLIC KEY-----", "");
                pubkey = pubkey.Replace("\r", "");
                pubkey = pubkey.Replace("\n", "");
            }
            return pubkey;
        }

    }
}