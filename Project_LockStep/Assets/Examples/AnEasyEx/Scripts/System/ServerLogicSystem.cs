using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mirror.EX_A
{
    public class ServerLogicSystem: Singleton<ServerLogicSystem>, ISystem
    {
        /// <summary> 是否正在局内 </summary>
        public bool isInBattleRoom;

        public void Start() { }

        public void Update() { }

        public void LogicUpdate() { }

    }
}