using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Utils.Extensions;

namespace Mirror.EX_A
{
    /// <summary>
    /// 客户端  消息绑定 + 处理
    /// </summary>
    public class ClientMessageSystem : Singleton<ClientMessageSystem>, ISystem
    {

        public void Start()
        {
        }

        public void Update()
        {
        }

        public void LogicUpdate()
        {
        }

        public void SetupClient()
        {
            NetworkClient.RegisterHandler<Msg_Join_Rsp>(OnJoinRsp);
            NetworkClient.RegisterHandler<Msg_Join_Ntf>(OnJoinNtf);
            NetworkClient.RegisterHandler<Msg_BattleStart_Rsp>(OnBattleStart);
            NetworkClient.RegisterHandler<Msg_Command_Ntf>(OnDoSthNtf);
        }

        private void OnJoinRsp(Msg_Join_Rsp msg)
        {
            GameHelper_Common.UILog($"Client: OnPlayerJoinRsp:{msg.playerId}");
        }

        private void OnJoinNtf(Msg_Join_Ntf msg)
        {
            GameHelper_Common.UILog($"Client: OnPlayerJoinNtf:{msg.playerIds.GetString()}");
        }

        private void OnBattleStart(Msg_BattleStart_Rsp msg)
        {
            ClientLogicSystem.Instance.BattleStart();
        }

        private void OnDoSthNtf(Msg_Command_Ntf msg)
        {
            ClientLogicSystem.Instance.OnSyncCommands(msg);
        }
    }
}