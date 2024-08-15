using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mirror.EX_A
{
    /// <summary>
    /// 加入房间回包，给加入的人，告诉他自己的playerId是多少
    /// </summary>
    public struct Msg_Join_Rsp : NetworkMessage
    {
        public uint playerId;
    }

    /// <summary>
    /// 有玩家加入时，广播给所有人，告诉他们当前房间内有哪些人
    /// </summary>
    public struct Msg_Join_Ntf : NetworkMessage
    {
        public List<uint> playerIds;
    }

    /// <summary>
    /// 战斗开始请求
    /// </summary>
    public struct Msg_BattleStart_Req : NetworkMessage 
    {

    }

    /// <summary>
    /// 战斗开始回包
    /// </summary>
    public struct Msg_BattleStart_Rsp : NetworkMessage
    {

    }

    /// <summary>
    /// 战斗服务器暂停始请求
    /// </summary>
    public struct Msg_BattlePause_Req : NetworkMessage
    {

    }

    /// <summary>
    /// 战斗服务器恢复请求
    /// </summary>
    public struct Msg_BattleResume_Req : NetworkMessage
    {

    }

    /// <summary>
    /// 局内指令请求
    /// </summary>
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
    public struct oneFrameCommands
    {
        /// <summary> 帧号 </summary>
        public ulong serverTick;
        /// <summary> 这帧里的指令集合 </summary>
        public List<CommandDetail> details;
    }

    /// <summary>
    /// 多帧指令集合下发
    /// </summary>
    public struct Msg_Command_Ntf : NetworkMessage
    {
        public ulong curBattleServerTick;
        /// <summary> 拥有多帧的指令集合 </summary>
        public List<oneFrameCommands> commandsSet;
    }
}