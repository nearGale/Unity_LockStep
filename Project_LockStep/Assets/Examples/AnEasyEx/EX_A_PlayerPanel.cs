using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Mirror.EX_A
{
    public class EX_A_PlayerPanel : MonoSingleton<EX_A_PlayerPanel>
    {
        public Text textValue;
        public Button btnModify;
        public Button btnStart;

        private void Start()
        {
            btnModify.onClick.AddListener(OnBtnModifyClick);
            btnStart.onClick.AddListener(OnBtnStartClick);
        }

        private void Update()
        {
            textValue.text = $"Value:{EX_A_Client.Instance.val}";
        }

        public void OnBtnStartClick()
        {
            Msg_Start_Req msg = new Msg_Start_Req();
            NetworkClient.Send(msg);
        }

        public void OnBtnModifyClick()
        {
            Msg_Command_Req msg = new Msg_Command_Req()
            {
                eCommand = ECommand.multi
            };
            NetworkClient.Send(msg);
        }
    }
}