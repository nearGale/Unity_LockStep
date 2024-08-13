using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Utils.Extensions;

namespace Mirror.EX_A
{
    /// <summary>
    /// 客户端消息绑定 + 处理
    /// </summary>
    public class EX_A_MsgHandler : Singleton<EX_A_MsgHandler>
    {
        public void SetupClient()
        {
            NetworkClient.RegisterHandler<Msg_Join_Rsp>(OnJoinRsp);
            NetworkClient.RegisterHandler<Msg_Join_Ntf>(OnJoinNtf);
            NetworkClient.RegisterHandler<Msg_Command_Ntf>(OnDoSthNtf);
        }

        private void OnJoinRsp(Msg_Join_Rsp msg)
        {
            EX_A_TextLog.instance.AppendLog($"Client: OnPlayerJoinRsp:{msg.playerId}");
        }

        private void OnJoinNtf(Msg_Join_Ntf msg)
        {
            EX_A_TextLog.instance.AppendLog($"Client: OnPlayerJoinNtf:{msg.playerIds.GetString()}");
        }

        private void OnDoSthNtf(Msg_Command_Ntf msg)
        {
            EX_A_Client.Instance.OnSyncCommands(msg);
            //EX_A_TextLog.instance.AppendLog($"Client: OnDoSthNtf:{msg.playerId} at tick:{msg.serverTick}");
        }
    }
}