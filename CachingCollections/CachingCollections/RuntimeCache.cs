using System;
using System.Runtime.Caching;
using System.Threading;
using System.Threading.Tasks;

namespace CachingCollections
{
    static class RuntimeCache
    {
        private static readonly CacheItemPolicy policy = new CacheItemPolicy
        {
            SlidingExpiration = TimeSpan.FromSeconds(5),
            Priority = CacheItemPriority.Default,
            RemovedCallback = RemoveCallback
              
        };

        private static void RemoveCallback(CacheEntryRemovedArguments arg)
        {
            Console.WriteLine("Removing from cache....");

            var disposable = arg.CacheItem.Value as IDisposable;
            if (disposable != null)
                disposable.Dispose();

        
            Task.Factory.StartNew(() =>
            {
                Thread.Sleep(500);
                Console.WriteLine("Start collection....");
                GC.Collect(); //не обязательно вызывать, память освободится при следующем вызове сборки мусора. Вызов может выполняться долго
                Console.WriteLine("Collected.");
                Console.WriteLine("Total Memory: {0:n0} bytes", GC.GetTotalMemory(false));
            });

           

        }

        public static void Add(string key, object value)
        {
            MemoryCache.Default.Add(key, value, policy);
        }
    }
}
