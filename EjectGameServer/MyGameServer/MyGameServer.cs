using System;
using System.Collections.Generic;
using System.IO;
using Common;
using ExitGames.Logging;
using ExitGames.Logging.Log4Net;
using log4net.Config;
using MyGameServer.Handler;
using MyGameServer.Managers;
using MyGameServer.Threads;
using Photon.SocketServer;

namespace MyGameServer
{
    // 所有 Server 端的 主类 都要继承自 ApplicationBase
    public class MyGameServer : ApplicationBase
    {
        public static MyGameServer ServerInstance { get; private set;}

        // 日志输出
        public static readonly ILogger log = LogManager.GetCurrentClassLogger();

        public Dictionary<OperationCode, BaseHandler> handlerDict = new Dictionary<OperationCode, BaseHandler>();
        public List<MyClientPeer> peerList = new List<MyClientPeer>();
        
        private SyncPositionThread syncPositionThread = new SyncPositionThread();

        // 当一个客户端请求链接的时候
        // 使用 PeerBase 表示一个客户端的链接
        protected override PeerBase CreatePeer(InitRequest initRequest)
        {
            log.Info("一个客户端连接进来......");
            MyClientPeer peer = new MyClientPeer(initRequest);
            peerList.Add(peer);
            return peer;
        }

        // 初始化
        protected override void Setup()
        {
            ServerInstance = this;

            // 日志初始化
            log4net.GlobalContext.Properties["Photon:ApplicationLogPath"] =
                Path.Combine(this.ApplicationRootPath, "log");
            FileInfo configFileInfo = new FileInfo(Path.Combine(this.BinaryPath, "log4net.config"));
            if (configFileInfo.Exists)
            {
                LogManager.SetLoggerFactory(Log4NetLoggerFactory.Instance); // 让 Photon 知道使用的是 Log4Net 插件
                XmlConfigurator.ConfigureAndWatch(configFileInfo); // 让 Log4Net 插件读取配置文件
            }

            log.Info("Setup Completed!");

            InitHandler();
            syncPositionThread.Run();
        }


        void InitHandler()
        {
            DefaultHandler defaultHandler = new DefaultHandler();
            handlerDict.Add(defaultHandler.opCode, defaultHandler);

            LoginHandler loginHandler = new LoginHandler();
            handlerDict.Add(loginHandler.opCode, loginHandler);

            RegisterHandler registerHandler = new RegisterHandler();
            handlerDict.Add(registerHandler.opCode, registerHandler);

            SyncPositionHandler syncPositionHandler = new SyncPositionHandler();
            handlerDict.Add(syncPositionHandler.opCode, syncPositionHandler);

            SyncPlayerHandler syncPlayerHandler = new SyncPlayerHandler();
            handlerDict.Add(syncPlayerHandler.opCode, syncPlayerHandler);

            SyncAttackHandler syncAttackHandler = new SyncAttackHandler();
            handlerDict.Add(syncAttackHandler.opCode, syncAttackHandler);

            SyncMissileHandler syncMissileHandler = new SyncMissileHandler();
            handlerDict.Add(syncMissileHandler.opCode, syncMissileHandler);

            GenerateMissileHandler generateMissileHandler = new GenerateMissileHandler();
            handlerDict.Add(generateMissileHandler.opCode, generateMissileHandler);

            PlayerStateHandler playerStateHandler = new PlayerStateHandler();
            handlerDict.Add(playerStateHandler.opCode, playerStateHandler);
        }


        // Server端关闭的时候
        protected override void TearDown()
        {
            syncPositionThread.Stop();
            log.Info("服务器关闭！");
        }
    }
}
