using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarbonBlocker.Data
{
    public class UrlList
    {
        private Dictionary<string, UrlInfo> urlInfos = new Dictionary<string, UrlInfo>();

        public void Add(UrlInfo urlInfo)
        {
            urlInfos.Add(urlInfo.Url, urlInfo);
        }

        public UrlInfo Get(string url)
        {
            
            bool ret = urlInfos.TryGetValue(url, out var urlInfo);
            return urlInfo;


        }

        public List<UrlInfo> GetList()
        {

            return urlInfos.Select(r => r.Value).ToList();


        }

    }
}
