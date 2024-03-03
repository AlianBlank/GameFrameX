﻿using NLog;
using Server.Apps;
using Server.Config;
using Server.Core.Actors.Impl;
using Server.Core.Comps;
using Server.Core.Hotfix;
using Server.DBServer;
using Server.DBServer.DbService.MongoDB;
using Server.Log;
using Server.Setting;

namespace Server.Launcher.Common
{
    internal static class AppStartUp
    {
        static readonly NLog.Logger Log = LogManager.GetCurrentClassLogger();

        public static async Task Enter(ServerType serverType)
        {
            try
            {
                var hotfixPath = Directory.GetCurrentDirectory() + "/hotfix";
                if (!Directory.Exists(hotfixPath))
                {
                    Directory.CreateDirectory(hotfixPath);
                }

                GlobalSettings.Load<AppSetting>($"Configs/app_config.{serverType.ToString()}.json", serverType);
                var flag = LoggerHandler.Start();
                if (!flag)
                {
                    return; //启动服务器失败
                }

                Log.Info($"Load Config Start...");
                ConfigManager.Instance.LoadConfig();

                Log.Info($"Load Config End...");

                Log.Info($"launch db service start...");
                ActorLimit.Init(ActorLimit.RuleType.None);
                GameDb.Init(new MongoDbServiceConnection());
                GameDb.Open(GlobalSettings.DataBaseUrl, GlobalSettings.DataBaseName);
                Log.Info($"launch db service end...");

                Log.Info($"regist comps start...");
                await ComponentRegister.Init(typeof(AppsHandler).Assembly);
                Log.Info($"regist comps end...");

                Log.Info($"load hotfix module start");
                await HotfixMgr.LoadHotfixModule();
                Log.Info($"load hotfix module end");

                Log.Info("进入游戏主循环...");
                Console.WriteLine("***进入游戏主循环***");
                GlobalSettings.LaunchTime = DateTime.Now;
                GlobalSettings.IsAppRunning = true;
                TimeSpan delay = TimeSpan.FromSeconds(1);
                while (GlobalSettings.IsAppRunning)
                {
                    await Task.Delay(delay);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"服务器执行异常，e:{e}");
                Log.Fatal(e);
            }

            Console.WriteLine($"退出服务器开始");
            await HotfixMgr.Stop();
            Console.WriteLine($"退出服务器成功");
        }
    }
}