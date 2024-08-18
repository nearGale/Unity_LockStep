using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils.Extensions;

namespace Mirror.EX_A
{
    /// <summary>
    /// 服务端 存放全部指令消息
    /// </summary>
    public class ServerCommandStorageSystem : Singleton<ServerCommandStorageSystem>, IServerSystem
    {
        /// <summary>
        /// 缓存了从 第1帧 开始的指令，所有的指令
        /// 
        /// 字典：帧号 -> 这一帧的指令集合
        /// </summary>
        private Dictionary<ulong, List<CommandDetail>> _allCommands = new();

        #region system func
        public void OnStartServer()
        {
            _allCommands.Clear();
        }

        public void OnStopServer()
        {
            _allCommands.Clear();
        }

        public void Start()
        {
        }

        public void Update()
        {
        }

        public void LogicUpdate()
        {
        }

        #endregion

        /// <summary>
        /// 战斗房间开始时
        /// </summary>
        public void StartBattle()
        {
            _allCommands.Clear();
        }

        /// <summary>
        /// 战斗房间结束时
        /// </summary>
        public void StopBattle()
        {
            _allCommands.Clear();
        }

        /// <summary>
        /// 将一帧的指令集合长期存储，用于后续断线重连，全量重发
        /// </summary>
        /// <param name="frame">帧号</param>
        /// <param name="commands">这一帧的指令集合</param>
        public void StoreCommand(ulong frame, List<CommandDetail> commands)
        {
            List<CommandDetail> cmd = new(commands); // TODO: 池化
            _allCommands.Add(frame, cmd);
        }

        /// <summary>
        /// 将从战斗开始的所有的指令同步给客户端
        /// TODO: 大的话考虑分包
        /// </summary>
        public void SyncAllCommands(NetworkConnectionToClient conn)
        {
            var battleServerTick = GameHelper_Server.GetBattleServerTick();
            List<oneFrameCommands> lst = new(); // TODO: 池化

            List<ulong> frames = new(); // 缓存同步了哪些帧 TODO: 池化
            foreach (var kvPair in _allCommands)
            {
                // key: 帧号 (ulong)
                // value: 这一帧的指令集合 (List<CommandDetail>)

                frames.Add(kvPair.Key);

                // 塞到协议中
                List<CommandDetail> commandDetails = new(kvPair.Value); // 这里要看池化的时候，是不是需要新取一个引用，把原来的还进池里
                oneFrameCommands cmd = new()
                {
                    serverTick = kvPair.Key,
                    details = commandDetails
                };
                lst.Add(cmd);
            }

            frames.Sort();


            var syncFrameMax = frames.Count > 0 ? frames[frames.Count - 1] : 0;

            Msg_CommandAll_Rsp msg = new Msg_CommandAll_Rsp()
            {
                syncedBattleServerTick = syncFrameMax,
                commandsSet = lst,
            };
            conn.Send(msg);

            if (lst.Count > 0)
            {
                GameHelper_Common.UILog($"Server: SyncCommands_All at {battleServerTick} frames:0->{syncFrameMax} {frames.GetString()}");
            }
        }
    }
}