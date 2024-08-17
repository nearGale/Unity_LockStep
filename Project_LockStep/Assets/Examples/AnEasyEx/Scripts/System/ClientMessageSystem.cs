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
            NetworkClient.RegisterHandler<Msg_PlayerConnect_Rsp>(OnPlayerConnectRsp);
            NetworkClient.RegisterHandler<Msg_PlayerIdentify_Rsp>(OnPlayerIdentifyRsp);
            NetworkClient.RegisterHandler<Msg_ClientWillDisconnect_Rsp>(OnClientWillDisconnectRsp);

            NetworkClient.RegisterHandler<Msg_Join_Ntf>(OnJoinNtf);
            NetworkClient.RegisterHandler<Msg_BattleStart_Rsp>(OnBattleStart);
            NetworkClient.RegisterHandler<Msg_Command_Ntf>(OnCommandNtf);
        }

        private void OnPlayerConnectRsp(Msg_PlayerConnect_Rsp msg)
        {
            GameHelper_Common.UILog($"Client: OnPlayerConnectRsp");

            Msg_PlayerIdentify_Req msgIdentify = new()
            {
                playerName = GameHelper_Client.GetLocalPlayerName(),
            };
            NetworkClient.Send(msgIdentify);
        }

        private void OnPlayerIdentifyRsp(Msg_PlayerIdentify_Rsp msg)
        {
            GameHelper_Common.UILog($"Client: OnPlayerIdentifyRsp playerId:{msg.playerId}");
        }

        private void OnClientWillDisconnectRsp(Msg_ClientWillDisconnect_Rsp msg)
        {
            GameHelper_Common.UILog($"Client: WillDisconnect");

            NetworkClient.Disconnect();
        }

        private void OnJoinNtf(Msg_Join_Ntf msg)
        {
            GameHelper_Common.UILog($"Client: OnPlayerJoinNtf:{msg.playerIds.GetString()}");
        }

        private void OnBattleStart(Msg_BattleStart_Rsp msg)
        {
            var randomSeed = msg.randomSeed;

            ClientRandomSystem.Instance.BattleStart(randomSeed); // 设置随机数种子
            ClientRoomSystem.Instance.BattleStart();
            ClientTimerSystem.Instance.BattleStart();
            ClientLogicSystem.Instance.BattleStart();
        }

        // TODO: 重连 BattleReconnect
        // TODO: battleRoomId

        private void OnCommandNtf(Msg_Command_Ntf msg)
        {
            ClientFrameSyncSystem.Instance.OnSyncCommands(msg);
        }
    }
}