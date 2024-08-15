using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mirror.EX_A
{
    public static partial class GameHelper_Server
    {
        /// <summary>
        /// 判断是否正在局内
        /// </summary>
        public static bool IsInBattleRoom()
        {
            return ServerLogicSystem.Instance.isInBattleRoom;
        }

        /// <summary>
        /// 获取服务器当前帧号
        /// </summary>
        public static ulong GetGameServerTick()
        {
            return ServerTimerSystem.Instance.gameServerTick;
        }

        /// <summary>
        /// 获取服务器 局内当前帧号
        /// </summary>
        public static ulong GetBattleServerTick()
        {
            return ServerTimerSystem.Instance.battleServerTick;
        }

    }
}