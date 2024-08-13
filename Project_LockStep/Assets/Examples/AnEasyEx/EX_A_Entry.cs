using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Mirror.EX_A
{
    public class EX_A_Entry: MonoBehaviour
    {
        void Start()
        {
            EX_A_Main.Create();
            EX_A_Server.Create();
            EX_A_MsgHandler.Create();
            EX_A_Main.Instance.Start();
        }

        void Update()
        {
            EX_A_Main.Instance.Update();
        }

        private void FixedUpdate()
        {
            EX_A_Main.Instance.LogicUpdate();
        }
    }
}