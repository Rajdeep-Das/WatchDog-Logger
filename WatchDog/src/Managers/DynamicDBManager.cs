﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WatchDog.src.Helpers;
using WatchDog.src.Models;

namespace WatchDog.src.Managers
{
    internal static class DynamicDBManager
    {
        private static string _connectionString = WatchDogExternalDbConfig.ConnectionString;

        private static bool isExternalDb() => !string.IsNullOrEmpty(_connectionString);

        public static async Task<IEnumerable<WatchLog>> GetAllWatchLogs() =>
            isExternalDb() switch
            {
                true => await ExternalDbHelper.GetAllWatchLogs(),
                false => LiteDBHelper.GetAllWatchLogs()
            };

        public static async Task InsertWatchLog(WatchLog log)
        {
            if (isExternalDb())
            {
                await ExternalDbHelper.InsertWatchLog(log);
            }
            else
            {
                LiteDBHelper.InsertWatchLog(log);
            }
        }

        public static async Task<IEnumerable<WatchExceptionLog>> GetAllWatchExceptionLogs() =>
            isExternalDb() switch
            {
                true => await ExternalDbHelper.GetAllWatchExceptionLogs(),
                false => LiteDBHelper.GetAllWatchExceptionLogs()
            };

        public static async Task InsertWatchExceptionLog(WatchExceptionLog log)
        {
            if (isExternalDb())
            {
                await ExternalDbHelper.InsertWatchExceptionLog(log);
            }
            else
            {
                LiteDBHelper.InsertWatchExceptionLog(log);
            }
        }
    }
}
