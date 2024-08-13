using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mirror.EX_A
{
    public struct Msg_Join_Ntf : NetworkMessage
    {
        public List<uint> playerIds;
    }

    public struct Msg_Start_Req : NetworkMessage
    {

    }

    public struct Msg_Join_Rsp : NetworkMessage
    {
        public uint playerId;
    }

    public struct Msg_Command_Req : NetworkMessage
    {
        public ECommand eCommand;
    }

    public enum ECommand
    {
        multi,
    }

    public struct CommandDetail
    {
        public uint playerId;
        public ECommand eCommand;
    }

    /// <summary>
    /// 单帧的指令集合
    /// </summary>
    public struct FrameCommands
    {
        /// <summary> 帧号 </summary>
        public ulong serverTick;
        /// <summary> 这帧里的指令集合 </summary>
        public List<CommandDetail> details;
    }

    public struct Msg_Command_Ntf : NetworkMessage
    {
        public ulong curServerTick;
        /// <summary> 拥有多帧的指令集合 </summary>
        public List<FrameCommands> oneFrameCommands;
    }
}