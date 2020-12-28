using System;
using System.Collections.Concurrent;

namespace RemoteNotes.UI.Utility
{
    public class CacheManager
    {
        private static readonly Lazy<CacheManager> CacheManagerLazy =
            new Lazy<CacheManager>(() => new CacheManager(), true);

        private readonly ConcurrentDictionary<string, object> _cacheDictionary =
            new ConcurrentDictionary<string, object>();

        public static CacheManager Instance => CacheManagerLazy.Value;

        private CacheManager()
        {
        }

        public void AddOrUpdateCache(string key, object value) =>
            _cacheDictionary.AddOrUpdate(key, value, (_, __) => value);

        public object GetFromCache(string key) => _cacheDictionary[key];

        public bool TryGetFromCache(string key, out object value) => _cacheDictionary.TryGetValue(key, out value);
    }
}