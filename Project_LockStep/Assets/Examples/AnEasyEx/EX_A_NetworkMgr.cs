using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Mirror.EX_A
{
    [AddComponentMenu("")]
    public class EX_A_NetworkMgr : NetworkManager
    {
        /// <summary>
        /// Called on the server when a client adds a new player with NetworkClient.AddPlayer.
        /// <para>The default implementation for this function creates a new player object from the playerPrefab.</para>
        /// </summary>
        /// <param name="conn">Connection from client.</param>
        public override void OnServerAddPlayer(NetworkConnectionToClient conn)
        {
            base.OnServerAddPlayer(conn);
            //EX_A_Server.Instance.
        }

        /// <summary>
        /// Called on the server when a client disconnects.
        /// <para>This is called on the Server when a Client disconnects from the Server. Use an override to decide what should happen when a disconnection is detected.</para>
        /// </summary>
        /// <param name="conn">Connection from client.</param>
        public override void OnServerDisconnect(NetworkConnectionToClient conn)
        {
            base.OnServerDisconnect(conn);
            //Player.ResetPlayerNumbers();
        }

        public override void OnStartServer()
        {
            base.OnStartServer();
            EX_A_Server.Instance.isServer = true;
            EX_A_Server.Instance.StartServer();
        }

        public override void OnStartClient()
        {
            EX_A_MsgHandler.Instance.SetupClient();
        }
    }
}