using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mirror.EX_A
{
    /// <summary>
    /// 此处放置 客户端 运行逻辑
    /// </summary>
    public class EX_A_Client : Singleton<EX_A_Client>
    {
        public ulong clientTick;
        public ulong serverTick;
        public Dictionary<ulong, FrameCommands> frameCommands = new();

        /// <summary> 一个测试用例，用于计算帧同步的结果 </summary>
        public int val;

        public void Start()
        {
            clientTick = 0;
            frameCommands.Clear();
        }

        public void Update()
        {
        }

        public void LogicUpdate()
        {
            ProcessTick();
        }

        private void ProcessTick()
        {
            if (clientTick > serverTick) return;

            clientTick++;

            if (frameCommands.Remove(clientTick, out var oneFramCommands))
            {
                foreach(var cmd in oneFramCommands.details) 
                {
                    ProcessCommand(cmd);
                }
            }

            if (clientTick % 100 == 1)
            {
                val += 1;
            }
        }

        public void OnSyncCommands(Msg_Command_Ntf msg)
        {
            serverTick = msg.curServerTick;
            foreach(var command in msg.oneFrameCommands) // command 是一帧的指令集合
            {
                if(!frameCommands.ContainsKey(command.serverTick))
                {
                    frameCommands.Add(command.serverTick, command);
                }
                else
                {
                    Debug.LogError("ERR!!! 有重复帧的指令");
                }
            }
        }

        private void ProcessCommand(CommandDetail detail)
        {
            val *= 2;
        }
    }
}