using System;
using System.Threading.Tasks;

namespace CachingCollections
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Total Memory: {0:n0} bytes", GC.GetTotalMemory(false));

            Console.WriteLine("Press any key to eat memory");
            Console.ReadKey();
            Console.WriteLine("wait removing from cache");
            Task.Factory.StartNew(() =>
            {
                var cachedItem = new CachedItem();
                RuntimeCache.Add("cachedItem", cachedItem);
                Console.WriteLine("Added to cache");
                Console.WriteLine("Total Memory: {0:n0} bytes", GC.GetTotalMemory(false));
            });

            Console.ReadKey();
        }
    }

    class CachedItem// : IDisposable
    {

        public CachedItem()
        {
            _bigArray = new byte[356735678];
            //Console.WriteLine("Obj size: {0} bytes", _bigArray.Length);
        }

        private byte[] _bigArray;


        //public void Dispose()
        //{
        //    _bigArray = null; //важно удалить ссылку на массив, иначе после сборки мусора память не освободится (проверено экспериментально)
        //    Console.WriteLine("CachedItem dispose called.");
        //}
    }
}
