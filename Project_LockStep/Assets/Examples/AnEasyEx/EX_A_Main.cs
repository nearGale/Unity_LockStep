using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Mirror.EX_A
{
    public class EX_A_Main : Singleton<EX_A_Main>
    {
        int tick = 0;

        public void Start()
        {
        }

        public void Update()
        {
        }

        public void LogicUpdate()
        {
            if (EX_A_Server.Instance.isServer)
            {
                tick++;
                //Debug.Log($"server tick:{tick}");
            }
        }
    }
}