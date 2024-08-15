using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mirror.EX_A
{
    public class ServerTimerSystem : Singleton<ServerTimerSystem>, ISystem
    {
        /// <summary> 匹配大厅的时间戳 </summary>
        public ulong gameServerTick;

        /// <summary> 战斗局内的时间戳 </summary>
        public ulong battleServerTick;

        /// <summary> 战斗服务器是否暂停 </summary>
        public bool battlePause;

        public void Start() 
        {
            gameServerTick = 0;
            battleServerTick = 0;
        }

        public void Update() { }

        public void LogicUpdate() 
        {
            if (!GameFacade.isServer) return;

            gameServerTick++;

            if (GameHelper_Server.IsInBattleRoom() && !battlePause)
            {
                battleServerTick++;
            }
        }

    }
}