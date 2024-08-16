using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mirror.EX_A
{
    /// <summary>
    /// 此处放置 客户端 运行逻辑
    /// </summary>
    public class ClientRoomSystem : Singleton<ClientRoomSystem>, ISystem
    {
        /// <summary>
        /// 是否在战斗房间内
        /// </summary>
        public bool inBattle;

        /// <summary> 战斗房间是否暂停 </summary>
        public bool battlePause; // TODO: 接协议实现

        public void Start()
        {
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
            inBattle = true;
            // TODO: 出战斗房间

            battlePause = false;
        }

        /// <summary>
        /// 判断服务器的战斗房间，是不是开始了并且运行中（未暂停）
        /// </summary>
        public bool IsBattleRoomRunning()
        {
            return inBattle && !battlePause;
        }
    }
}