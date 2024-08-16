using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mirror.EX_A
{
    /// <summary>
    /// 此处放置 客户端 同步帧数据，当帧模拟
    /// </summary>
    public class ClientFrameSyncSystem : Singleton<ClientFrameSyncSystem>, ISystem
    {
        /// <summary>
        /// 字典：帧 -> 这一帧的指令集合
        /// </summary>
        public Dictionary<ulong, oneFrameCommands> frameCommandDict = new();

        public void Start()
        {
        }

        public void Update()
        {
        }

        public void LogicUpdate()
        {
            ProcessTick();
        }

        /// <summary>
        /// 进入战斗房间时
        /// </summary>
        public void BattleStart()
        {
            frameCommandDict.Clear();
        }

        // TODO: tick 出 command 逻辑拆出
        // TODO: 同步随机数
        private void ProcessTick()
        {
            var clientTick = GameHelper_Client.GetClientTick();
            var battleServerTick = GameHelper_Client.GetBattleServerTick();

            if (clientTick >= battleServerTick) return;

            if (frameCommandDict.Remove(clientTick, out var oneFramCommands))
            {
                foreach (var cmd in oneFramCommands.details)
                {
                    ProcessCommand(clientTick, cmd);
                }
            }
        }

        public void OnSyncCommands(Msg_Command_Ntf msg)
        {
            //GameHelper_Common.UILog($"Client: Rcv: {clientTick} {Time.time}");

            var clientTick = GameHelper_Client.GetClientTick();
            var battleServerTick = GameHelper_Client.GetBattleServerTick();

            battleServerTick = msg.curBattleServerTick;
            foreach (var command in msg.commandsSet) // command 是一帧的指令集合
            {
                GameHelper_Common.UILog($"Client: Rcv: {command.serverTick} at {clientTick}/{battleServerTick}");
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
            ClientLogicSystem.Instance.ProcessCommand(detail);
            GameHelper_Common.UILog($"Client: ProcessCommand:{detail.playerId} {detail.eCommand} at tick:{tick}");
        }
    }
}