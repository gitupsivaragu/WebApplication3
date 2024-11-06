using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Memory;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WebApplication3
{
    public class LoginBusinessLayer : Ilogin
    {
       private readonly IJsonwebtoken _jsonWebtoken;
       public LoginBusinessLayer(IJsonwebtoken jsonWebtoken)
        {
     
            _jsonWebtoken= jsonWebtoken;
        }

        public Task<string> Login(Loginuser login)
        {
            try
            {
                // Checking DB validation  encripted and decripted
                //  validation  user name and pwd  invalid based  client side 
                if (login.UserName == "john" && login.password == "123")
                    return Task.FromResult(_jsonWebtoken.GetToken(login));
                else
                    return   Task.FromResult("Invalid UserName & password ");

            }
            catch {
            //Loggging
            }
            return Task.FromResult(string.Empty);

        }
    }

    public interface IMemarycached
    {
        T Get<T>(string key);
        void Set<T>(string key, T value, TimeSpan? absoluteExpireTime = null, TimeSpan? slidingExpiration = null, CacheItemPriority priority = CacheItemPriority.Normal);
        IDictionary<string, object> GetAllCaches();
        void Remove(string key);
        void Clear();
    }
    public class Memarycached : IMemarycached
    {
        private readonly IMemoryCache _memoryCache;

        private readonly HashSet<string> _keys = new HashSet<string>();

        public Memarycached(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        public void Clear()
        {
            throw new NotImplementedException();
        }

        public T Get<T>(string key)
        {
            
            _memoryCache.TryGetValue(key, out T value);
            return value;
        }

        public IDictionary<string, object> GetAllCaches()
        {
            var allItems = new Dictionary<string, object>();
            foreach (var key in _keys.ToList()) // ToList to avoid collection modification issues
            {
                if (_memoryCache.TryGetValue(key, out object value))
                {
                    allItems[key] = value;
                }
                else
                {
                    // Item might have expired and removed, clean up keys
                    _keys.Remove(key);
                }
            }
            return allItems;


        }

        public void Remove(string key)
        {
            throw new NotImplementedException();
        }

        public void Set<T>(string key, T value, TimeSpan? absoluteExpireTime = null, TimeSpan? slidingExpiration = null, CacheItemPriority priority = CacheItemPriority.Normal)
        {
            var _options = new MemoryCacheEntryOptions
            {
                // Apply absolute expiration if provided
                AbsoluteExpirationRelativeToNow = absoluteExpireTime,
                // Apply sliding expiration if provided
                SlidingExpiration = slidingExpiration,
                // Set the priority of the cache item
                Priority = priority
            };
            // Set the cache item with the options
            _memoryCache.Set(key, value, _options);
            // Add the key to the tracking set to maintain a list of all keys
            _keys.Add(key);
        }
    }
}
