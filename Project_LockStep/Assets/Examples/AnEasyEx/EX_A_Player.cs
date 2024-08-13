using System.Linq;
using UnityEngine;
using Utils.Extensions;

namespace Mirror.EX_A
{
    public class EX_A_Player : NetworkBehaviour
    {
        #region Server
        /// <summary>
        /// 当 NetworkBehaviour 对象在服务器上处于活动状态时，会调用此函数。
        /// （仅服务器触发）
        /// 这可能是由NetworkServer触发的。Listen（）用于场景中的对象，或通过NetworkServer。Spawn（）用于动态创建的对象
        /// 这将用于“主机”上的对象以及专用服务器上的对象
        /// </summary>
        public override void OnStartServer()
        {
            base.OnStartServer();

            var playerId = EX_A_Server.Instance.AddPlayer(netIdentity);

            Msg_Join_Rsp msgRsp = new Msg_Join_Rsp()
            {
                playerId = playerId
            };
            netIdentity.connectionToClient.Send(msgRsp);

            var ids = EX_A_Server.Instance.playerDict.Keys.ToList();
            Msg_Join_Ntf msg = new Msg_Join_Ntf()
            {
                playerIds = ids
            };

            NetworkServer.SendToAll(msg);
            EX_A_TextLog.instance.AppendLog($"Server: OnPlayerJoinReq:{playerId}");
            EX_A_TextLog.instance.AppendLog($"Server: DoPlayerJoinNtf:{ids.GetString()}");
        }
        #endregion
    }
}