using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Qiniu;
using Qiniu.Conf;
using Qiniu.RS;
using Qiniu.IO;

/**
*<appSettings>
    <add key="USER_AGENT" value="" />
    <add key="ACCESS_KEY" value="" />
    <add key="SECRET_KEY" value="" />
    <add key="RS_HOST" value="" />
    <add key="UP_HOST" value="" />
    <add key="RSF_HOST" value="" />
    <add key="PREFETCH_HOST" value="" />
  </appSettings>
*/
namespace Abp.Common
{
    public class QiniuHelper
    {
        public QiniuHelper()
        {
            Qiniu.Conf.Config.Init();
        }

        /// <summary>
        /// 普通上传文件
        /// </summary>
        /// <param name="bucket"></param>
        /// <param name="key">utf-8 编码</param>
        /// <param name="fname"></param>
        public static void PutFile(string bucket, string key, string fname)
        {
            var policy = new PutPolicy(bucket, 3600);
            string upToken = policy.Token();
            PutExtra extra = new PutExtra();
            IOClient client = new IOClient();
            client.PutFile(upToken, key, fname, extra);
        }

        /// <summary>
        /// 上传文件 没有key
        /// </summary>
        /// <param name="bucket"></param>
        /// <param name="fname"></param>
        public static void PutFileWithoutKey(string bucket, string fname)
        {
            var policy = new PutPolicy(bucket, 3600);
            System.Console.WriteLine(policy);
            string upToken = policy.Token();
            IOClient target = new IOClient();
            PutExtra extra = new PutExtra();
            PutRet ret = target.PutFileWithoutKey(upToken, fname, extra);
            Console.WriteLine(ret.Response.ToString());
        }

    }
}
