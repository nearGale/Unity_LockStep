using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mirror.EX_A
{
    public class EX_A_Server: Singleton<EX_A_Server>
    {
        public bool isServer = false;

        private uint playerIndex = 0;

        public Dictionary<uint, NetworkIdentity> playerDict = new();

        public uint AddPlayer(NetworkIdentity player)
        {
            playerIndex++;
            playerDict.Add(playerIndex, player);
            return playerIndex;
        }
    }
}