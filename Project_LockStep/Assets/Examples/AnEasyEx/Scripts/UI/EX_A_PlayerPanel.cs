using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Mirror.EX_A
{
    public class EX_A_PlayerPanel : MonoSingleton<EX_A_PlayerPanel>
    {
        public Text textValue;
        public Text textTick;
        public Button btnModify;
        public Button btnStart;
        public Button btnPause;
        public Button btnResume;

        private void Start()
        {
            btnModify.onClick.AddListener(OnBtnModifyClick);
            btnStart.onClick.AddListener(OnBtnStartClick);
            btnPause.onClick.AddListener(OnBtnPauseClick);
            btnResume.onClick.AddListener(OnBtnResumeClick);
        }

        private void Update()
        {
            //var val = ((float)ClientLogicSystem.Instance.val / 1000);
            //textValue.text = $"Value:{val.ToString("F3")}";
            textValue.text = $"Value:{ClientLogicSystem.Instance.val}";

            var ct = GameHelper_Client.GetClientTick();
            var st = GameHelper_Client.GetBattleServerTick();
            textTick.text = $"client:{ct} \n server:{st} \n delta:{st - ct}";
        }

        public void OnBtnStartClick()
        {
            Msg_BattleStart_Req msg = new Msg_BattleStart_Req();
            NetworkClient.Send(msg);
        }

        public void OnBtnPauseClick()
        {
            Msg_BattlePause_Req msg = new Msg_BattlePause_Req();
            NetworkClient.Send(msg);
        }

        public void OnBtnResumeClick()
        {
            Msg_BattleResume_Req msg = new Msg_BattleResume_Req();
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