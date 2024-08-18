using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mirror.EX_A
{
    /// <summary>
    /// 此处放置 客户端 运行逻辑
    /// </summary>
    public class ClientLogicSystem : Singleton<ClientLogicSystem>, IClientSystem
    {
        /// <summary> 一个测试用例，用于计算帧同步的结果 </summary>
        public int val;

        /// <summary> 
        /// 上一次执行时的客户端逻辑帧号，变大了才执行下一次
        /// </summary>
        private ulong _lastProcessClientTick;

        #region system func
        public void OnClientConnect()
        {
        }

        public void OnClientDisconnect()
        {
            ClearData();
        }

        public void Start()
        {
        }

        public void Update()
        {
        }

        public void LogicUpdate()
        {
            if (!ClientRoomSystem.Instance.IsBattleRoomRunning()) return;

            var clientTick = GameHelper_Client.GetClientTick();
            if(clientTick > _lastProcessClientTick)
            {
                val += 1;
                _lastProcessClientTick = clientTick;
            }

            if(clientTick - _lastProcessClientTick > 1)
            {
                var err = $"ERR!!!! ClientLogicSystem [clientTick - lastProcessTick > 1] {clientTick} {_lastProcessClientTick}";
                GameHelper_Common.UILog(err);
                throw new Exception(err);
            }
        }
        #endregion

        private void ClearData()
        {
            val = 0;
            _lastProcessClientTick = 0;
        }

        /// <summary>
        /// 战斗房间开始时，重置所有的游戏数据
        /// </summary>
        public void BattleStart()
        {
            ClearData();
        }

        /// <summary>
        /// 战斗房间结束时，重置所有的游戏数据
        /// </summary>
        public void BattleStop()
        {
            ClearData();
        }

        public void ProcessCommand(CommandDetail detail)
        {
            switch (detail.eCommand)
            {
                case ECommand.multi:
                    val += ClientRandomSystem.Instance.GetRandomInt(50, 5000);
                    break;
            }
        }
    }
}