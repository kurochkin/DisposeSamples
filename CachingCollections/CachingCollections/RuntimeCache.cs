using System;
using System.Runtime.Caching;

namespace CachingCollections
{
    static class RuntimeCache
    {
        private static readonly CacheItemPolicy policy = new CacheItemPolicy
        {
            SlidingExpiration = TimeSpan.FromSeconds(5),
            Priority = CacheItemPriority.Default,
            RemovedCallback =
                arg =>
                {
                    Console.WriteLine("Removing from cache....");

                    var disposable = arg.CacheItem.Value as IDisposable;
                    if (disposable != null)
                        disposable.Dispose();

                    Console.WriteLine("Start collection....");

                    GC.Collect(); //не обязательно вызывать, память освободится при следующем вызове сборки мусора. Вызов может выполняться долго
                    Console.WriteLine("Collected.");
                }
        };


        public static void Add(string key, object value)
        {
            MemoryCache.Default.Add(key, value, policy);
        }
    }
}
