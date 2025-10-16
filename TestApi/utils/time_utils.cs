using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApi.utils
{
    internal class time_utils
    {
        static public string timeNowTypeDate()
        {

            string now = DateTime.Now.ToString("yyyy-MM-dd");   // Tarih bilgilerini alıyoruz
             return now;
        }

        static public string timeNowTypeTimeZone()
        {

            string now = DateTime.Now.ToString("HH:mm:ss.fffffffzzz");   // Saat bilgilerini alıyoruz
            return now;
        }
    }
}
