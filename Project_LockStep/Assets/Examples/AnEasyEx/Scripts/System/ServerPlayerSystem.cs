using Org.BouncyCastle.Asn1.X509;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mirror.EX_A
{
    public class ServerPlayerSystem : Singleton<ServerPlayerSystem>, ISystem
    {
        private uint _playerIndex = 0;

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
        }

        public uint AddPlayer(NetworkIdentity player)
        {
            _playerIndex++;
            playerDict.Add(_playerIndex, player);
            conn2playerId.Add(player.connectionToClient.connectionId, _playerIndex);
            return _playerIndex;
        }

        /// <summary>
        /// 根据 连接的Id，获取 玩家ID
        /// </summary>
        /// <param name="connectionId">网络连接的 id</param>
        public uint GetPlayerIdByConnectionId(int connectionId)
        {
            if (!conn2playerId.TryGetValue(connectionId, out var playerId)) return 0;

            return playerId; // player id 不会为0， 0是非法值
        }
    }
}