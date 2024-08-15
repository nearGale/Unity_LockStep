using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mirror.EX_A
{
    /// <summary>
    /// 服务端 下发指令消息
    /// </summary>
    public class ServerCommandSyncSystem : Singleton<ServerCommandSyncSystem>, ISystem
    {
        /// <summary> 上一次同步的帧号 </summary>
        private ulong _lastSyncedFrame = 0;

        private Dictionary<ulong, List<CommandDetail>> cachedCommands = new();

        public void Start()
        {
        }

        public void Update()
        {
        }

        public void LogicUpdate()
        {
            if (!GameFacade.isServer) return;

            if (!GameHelper_Server.IsInBattleRoom()) return; // 进入房间后同时重置帧号，开始使用帧同步
                                                             // 因为在大厅进入时间不同，不使用帧同步，使用Mirror原生同步即可


            var battleServerTick = GameHelper_Server.GetBattleServerTick();

            if (battleServerTick - _lastSyncedFrame >= ConstVaiables.CommandSetSyncIntervalFrames)
            {
                SyncCommands();
                _lastSyncedFrame = battleServerTick;
            }
        }

        public void StartBattle()
        {
            _lastSyncedFrame = 0;
            cachedCommands.Clear();
        }

        /// <summary>
        /// 缓存客户端指令，等待时机下发指令集合
        /// </summary>
        public void CacheClientCommand(uint playerId, ECommand eCommand)
        {
            var battleServerTick = GameHelper_Server.GetBattleServerTick();
            var modifyDetail = new CommandDetail()
            {
                playerId = playerId,
                eCommand = eCommand
            };

            if (!cachedCommands.TryGetValue(battleServerTick, out var details))
            {
                // TODO: 对象池
                var lst = new List<CommandDetail>() { modifyDetail };

                cachedCommands.Add(battleServerTick, lst);
            }
            else
            {
                details.Add(modifyDetail);
            }
            GameHelper_Common.UILog($"Server: Rcv cmd at {battleServerTick}");
        }


        private void SyncCommands()
        {
            var battleServerTick = GameHelper_Server.GetBattleServerTick();
            List<oneFrameCommands> lst = new(); // TODO:

            foreach (var kvPair in cachedCommands)
            {
                List<CommandDetail> commandDetails = new(kvPair.Value); // 这里要看池化的时候，是不是需要新取一个引用，把原来的还进池里
                oneFrameCommands cmd = new()
                {
                    serverTick = kvPair.Key,
                    details = commandDetails
                };
                lst.Add(cmd);
            }

            cachedCommands.Clear();

            Msg_Command_Ntf msg = new Msg_Command_Ntf()
            {
                curBattleServerTick = battleServerTick,
                commandsSet = lst,
            };

            NetworkServer.SendToAll(msg);
            if (lst.Count > 0)
            {
                GameHelper_Common.UILog($"Server: SyncCommands at {battleServerTick}");
            }
        }
    }
}