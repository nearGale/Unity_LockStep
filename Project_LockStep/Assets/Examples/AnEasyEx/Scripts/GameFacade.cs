using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mirror.EX_A
{
    public static class GameFacade
    {
        public static bool isServer = false;

        /// <summary>
        /// 这里放了所有的 system，一帧中运行按照这里的顺序执行
        /// </summary>
        public static List<IServerSystem> serverSystems = new()
        {
            // 服务器端系统
            // 执行
            //     Start()
            //     Update()
            //     LogicUpdate()
            // 服务器端专属
            //     OnStartServer()
            //     OnStopServer()

            ServerTimerSystem.Instance,
            ServerPlayerSystem.Instance,
            ServerMessageSystem.Instance,
            ServerLogicSystem.Instance,
            ServerCommandStorageSystem.Instance,
            ServerCommandSyncSystem.Instance,
        };

        public static List<IClientSystem> clientSystems = new()
        {
            // 客户端系统
            // 执行
            //     Start()
            //     Update()
            //     LogicUpdate()

            ClientMessageSystem.Instance,
            ClientRoomSystem.Instance,
            
            // ==================================
            // 战斗中系统，加速追帧就跑这几个系统
            ClientFrameSyncSystem.Instance,
            ClientLogicSystem.Instance,
            ClientTimerSystem.Instance,
            // ==================================

            ClientChasingFrameSystem.Instance,
        };
    }
}