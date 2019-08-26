using System;
using System.Collections.Generic;
using System.Text;
using StackExchange.Redis;

namespace CodeSampleConsoleApp.RedisSample
{
    public class RedisLock
    {
        public static void  Case1()
        {
            IConnectionMultiplexer server = ConnectionMultiplexer.Connect("localhost:6379");
            if(!server.IsConnected)
            {
                Console.WriteLine("can not connect to specific redis server");
                return;
            }
            if(server.GetDatabase(0).LockTake("redisLock", "a lock", TimeSpan.FromSeconds(10)))
            {
                server.GetDatabase(0).StringSet("currentVersion", "1.0.0.0");
                server.GetDatabase(0).LockRelease("redisLock", "a lock");
            }
            else
            {

            }
        }
    }
}
