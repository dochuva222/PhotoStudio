using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace PhotoStudioApp.Tools
{
    public static class Tools
    {
        public static BitmapImage BytesToImage(byte[] bytes)
        {
            using (MemoryStream memoryStream = new MemoryStream(bytes))
            {
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = memoryStream;
                image.EndInit();
                return image;
            }
        }
        public static IEnumerable<T> MyExcept<T>(this IEnumerable<T> orgList, IEnumerable<T> toRemove)
        {
            var list = orgList.ToList();
            foreach (var x in toRemove)
            {
                list.Remove(x);
            }
            return list;
        }
    }
}
