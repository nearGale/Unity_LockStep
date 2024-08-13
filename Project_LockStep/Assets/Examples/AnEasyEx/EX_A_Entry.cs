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
            EX_A_Client.Create();

            EX_A_Main.Instance.Start();
            EX_A_Server.Instance.Start();
            EX_A_Client.Instance.Start();
        }

        void Update()
        {
            EX_A_Main.Instance.Update();
            EX_A_Server.Instance.Update();
            EX_A_Client.Instance.Update();
        }

        private void FixedUpdate()
        {
            EX_A_Main.Instance.LogicUpdate();
            EX_A_Server.Instance.LogicUpdate();
            EX_A_Client.Instance.LogicUpdate();
        }
    }
}