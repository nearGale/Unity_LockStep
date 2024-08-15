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
        public ulong clientTick;
        public ulong serverTick;

        /// <summary>
        /// 字典：帧 -> 这一帧的指令集合
        /// </summary>
        public Dictionary<ulong, oneFrameCommands> frameCommandDict = new();

        /// <summary> 一个测试用例，用于计算帧同步的结果 </summary>
        public int val;

        public void Start()
        {
            clientTick = 0;
            frameCommandDict.Clear();
        }

        public void Update()
        {
        }

        public void LogicUpdate()
        {
            ProcessTick();
        }

        public void BattleStart()
        {
            clientTick = 0;
            val = 0;
        }

        // TODO: tick 出 command 逻辑拆出
        // TODO: 同步随机数
        private void ProcessTick()
        {
            if (clientTick >= serverTick) return;

            if (frameCommandDict.Remove(clientTick, out var oneFramCommands))
            {
                foreach(var cmd in oneFramCommands.details) 
                {
                    ProcessCommand(clientTick, cmd);
                }
            }

            if (clientTick % 100 == 1)
            {
                val += 1;
            }

            clientTick++;
        }

        public void OnSyncCommands(Msg_Command_Ntf msg)
        {
            //GameHelper_Common.UILog($"Client: Rcv: {clientTick} {Time.time}");
            serverTick = msg.curBattleServerTick;
            foreach(var command in msg.commandsSet) // command 是一帧的指令集合
            {
                GameHelper_Common.UILog($"Client: Rcv: {command.serverTick} at {clientTick}/{serverTick}");
                if (!frameCommandDict.ContainsKey(command.serverTick))
                {
                    frameCommandDict.Add(command.serverTick, command);
                }
                else
                {
                    Debug.LogError("ERR!!! 有重复帧的指令");
                }
            }
        }

        private void ProcessCommand(ulong tick, CommandDetail detail)
        {
            val *= 2;
            GameHelper_Common.UILog($"Client: ProcessCommand:{detail.playerId} {detail.eCommand} at tick:{tick}");
        }
    }
}