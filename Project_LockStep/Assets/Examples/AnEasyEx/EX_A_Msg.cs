using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mirror.EX_A
{
    public struct Msg_Join_Ntf : NetworkMessage
    {
        public List<uint> playerIds;
    }

    public struct Msg_Join_Rsp : NetworkMessage
    {
        public uint playerId;
    }
}