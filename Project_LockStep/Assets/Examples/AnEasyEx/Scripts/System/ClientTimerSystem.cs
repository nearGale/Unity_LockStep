using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mirror.EX_A
{
    /// <summary>
    /// 此处放置 客户端 运行逻辑
    /// </summary>
    public class ClientTimerSystem : Singleton<ClientTimerSystem>, ISystem
    {
        /// <summary> 服务器大厅帧号 </summary>
        public ulong gameServerTick;

        /// <summary> 服务器战斗房间内的帧号（最新的） </summary>
        public ulong battleServerTick;

        /// <summary> 客户端当前帧号（战斗房间内的） </summary>
        public ulong clientTick;

        public void Start()
        {
            clientTick = 0;
        }

        public void Update()
        {
        }

        public void LogicUpdate()
        {
        }

        /// <summary>
        /// 进入战斗房间时
        /// </summary>
        public void BattleStart()
        {
            clientTick = 0;
        }
    }
}