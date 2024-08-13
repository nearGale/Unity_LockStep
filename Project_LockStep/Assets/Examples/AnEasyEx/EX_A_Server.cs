using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mirror.EX_A
{
    public class EX_A_Server: Singleton<EX_A_Server>
    {
        public bool isServer = false;

        public ulong serverTick = 0;

        private uint _playerIndex = 0;

        /// <summary> 上一次同步的帧号 </summary>
        private ulong _lastSyncedFrame = 0;

        /// <summary> 相隔多少帧同步一下 </summary>
        private ulong _syncIntervalFrames = 5;

        private bool roomStart;

        private Dictionary<ulong, List<CommandDetail>> cachedCommands = new();

        public Dictionary<uint, NetworkIdentity> playerDict = new();
        public Dictionary<int, uint> conn2playerId = new();

        public void Start()
        {
        }

        public void Update()
        {
        }

        public void LogicUpdate()
        {
            if (!roomStart) return;

            serverTick++;
            //Debug.Log($"server tick:{tick}");

            if(serverTick - _lastSyncedFrame >= _syncIntervalFrames)
            {
                SyncCommands();
            }
        }

        public uint AddPlayer(NetworkIdentity player)
        {
            _playerIndex++;
            playerDict.Add(_playerIndex, player);
            conn2playerId.Add(player.connectionToClient.connectionId, _playerIndex);
            return _playerIndex;
        }

        // TODO: 把消息处理单独出去
        public void StartServer()
        {
            NetworkServer.RegisterHandler<Msg_Start_Req>(OnStartReq);
            NetworkServer.RegisterHandler<Msg_Command_Req>(OnCommandReq);
        }

        private void OnStartReq(NetworkConnectionToClient conn, Msg_Start_Req msg)
        {
            roomStart = true;
        }

        private void OnCommandReq(NetworkConnectionToClient conn, Msg_Command_Req msg)
        {
            if (!conn2playerId.TryGetValue(conn.connectionId, out var playerId)) return;

            var modifyDetail = new CommandDetail()
            {
                playerId = playerId,
                eCommand = msg.eCommand
            };

            if (!cachedCommands.TryGetValue(serverTick, out var details))
            {
                // TODO: 对象池
                var lst = new List<CommandDetail>() { modifyDetail };

                cachedCommands.Add(serverTick, lst);
            }
            else
            {
                details.Add(modifyDetail);
            }
        }


        private void SyncCommands()
        {
            List<FrameCommands> lst = new(); // TODO:

            foreach(var kvPair in cachedCommands) 
            {
                List<CommandDetail> commandDetails = new(kvPair.Value); // 这里要看池化的时候，是不是需要新取一个引用，把原来的还进池里
                FrameCommands cmd = new()
                {
                    serverTick = kvPair.Key,
                    details = commandDetails
                };
                lst.Add(cmd);
            }

            cachedCommands.Clear();

            Msg_Command_Ntf msg = new Msg_Command_Ntf()
            {
                curServerTick = serverTick,
                oneFrameCommands = lst,
            };

            NetworkServer.SendToAll(msg);
        }

    }
}