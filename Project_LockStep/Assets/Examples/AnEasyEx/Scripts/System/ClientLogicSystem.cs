using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mirror.EX_A
{
    /// <summary>
    /// 此处放置 客户端 运行逻辑
    /// </summary>
    public class ClientLogicSystem : Singleton<ClientLogicSystem>, ISystem
    {
        /// <summary> 一个测试用例，用于计算帧同步的结果 </summary>
        public int val;

        public void Start()
        {
        }

        public void Update()
        {
        }

        public void LogicUpdate()
        {
            if (ClientRoomSystem.Instance.IsBattleRoomRunning())
            {
                val += 1;
            }
        }

        /// <summary>
        /// 进入战斗房间时，重置所有的游戏数据
        /// </summary>
        public void BattleStart()
        {
            val = 0;
        }

        public void ProcessCommand(CommandDetail detail)
        {
            switch (detail.eCommand)
            {
                case ECommand.multi:
                    val *= 2;
                    break;
            }
        }
    }
}