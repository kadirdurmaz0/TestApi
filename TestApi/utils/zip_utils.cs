using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApi.utils
{
    internal class zip_utils
    {
        static public byte[] IonicZipFile(string xml, string fileName)
        {
            // XML verisini UTF8 formatında byte dizisine dönüştürüyoruz.
            byte[] data = Encoding.UTF8.GetBytes(xml);

            // Bellek üzerinde bir zip dosyası oluşturmak için MemoryStream kullanıyoruz.
            MemoryStream zipStream = new MemoryStream();

            // Ionic.Zip kütüphanesi kullanarak zip dosyasını oluşturuyoruz.
            using (ZipFile zip = new ZipFile())
            {
                // Zip dosyasına bir giriş (Entry) ekliyoruz.
                // Bu giriş, verilen fileName ile .xml uzantılı bir dosya ve içeriği ziplenecekData olacaktır.
                ZipEntry zipEleman = zip.AddEntry(fileName + ".xml", data);

                // Zip dosyasını MemoryStream'e kaydediyoruz.
                zip.Save(zipStream);
            }


            zipStream.Seek(0, SeekOrigin.Begin);

            return zipStream.ToArray();
        }
    }
}
