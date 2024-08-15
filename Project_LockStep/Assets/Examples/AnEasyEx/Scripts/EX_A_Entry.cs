using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Mirror.EX_A
{
    public class EX_A_Entry: MonoBehaviour
    {
        List<ISystem> systems = new()
        {
            // 服务器逻辑
            ServerTimerSystem.Instance,
            ServerMessageSystem.Instance,
            ServerLogicSystem.Instance,
            ServerCommandSyncSystem.Instance,

            // 客户端逻辑
            ClientMessageSystem.Instance,
            ClientLogicSystem.Instance,
        };

        void Start()
        {
            GameFacade.isServer = false;

            foreach(var system in systems)
            {
                system.Start();
            }
        }

        void Update()
        {
            foreach (var system in systems)
            {
                system.Update();
            }
        }

        private void FixedUpdate()
        {
            foreach (var system in systems)
            {
                system.LogicUpdate();
            }
        }
    }
}