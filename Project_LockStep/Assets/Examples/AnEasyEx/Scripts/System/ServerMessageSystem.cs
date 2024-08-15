using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Mirror.EX_A
{
    /// <summary>
    /// 服务端消息处理
    /// </summary>
    public class ServerMessageSystem : Singleton<ServerMessageSystem>, ISystem
    {

        public void Start() { }

        public void Update() { }

        public void LogicUpdate() { }

        /// <summary>
        /// 注册消息回调
        /// </summary>
        public void RegisterMessageHandler()
        {
            NetworkServer.RegisterHandler<Msg_BattleStart_Req>(OnBattleStartReq);
            NetworkServer.RegisterHandler<Msg_BattlePause_Req>(OnBattlePauseReq);
            NetworkServer.RegisterHandler<Msg_BattleResume_Req>(OnBattleResumeReq);
            NetworkServer.RegisterHandler<Msg_Command_Req>(OnCommandReq);
        }

        #region 消息回调

        private void OnBattleStartReq(NetworkConnectionToClient conn, Msg_BattleStart_Req msg)
        {
            ServerLogicSystem.Instance.isInBattleRoom = true;
            ServerTimerSystem.Instance.battleServerTick = 0;
            ServerTimerSystem.Instance.battlePause = false;
            ServerCommandSyncSystem.Instance.StartBattle();

            Msg_BattleStart_Rsp msgRsp = new Msg_BattleStart_Rsp();
            NetworkServer.SendToAll(msgRsp);
        }

        private void OnBattlePauseReq(NetworkConnectionToClient conn, Msg_BattlePause_Req msg)
        {
            ServerTimerSystem.Instance.battlePause = true;
        }

        private void OnBattleResumeReq(NetworkConnectionToClient conn, Msg_BattleResume_Req msg)
        {
            ServerTimerSystem.Instance.battlePause = false;
        }

        private void OnCommandReq(NetworkConnectionToClient conn, Msg_Command_Req msg)
        {
            var playerId = ServerPlayerSystem.Instance.GetPlayerIdByConnectionId(conn.connectionId);
            if (playerId == 0) return;

            ServerCommandSyncSystem.Instance.CacheClientCommand(playerId, msg.eCommand);
        }

        #endregion


    }
}