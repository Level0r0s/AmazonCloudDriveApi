﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AmazonCloudDriveApi
{
    public static class Retry
    {
        public static bool Do(int times, Func<bool> act)
        {
            while (times > 0)
            {
                if (act()) return true;
                times--;
            }
            return false;
        }

        internal static async Task<bool> Do(int times, Func<int, TimeSpan> retryDelay, Func<Task<bool>> act)
        {
            for (int time = 0; time < times - 1; time++)
            {
                if (await act()) return true;
                await Task.Delay(retryDelay(time));
            }
            return await act();
        }
    }
}
