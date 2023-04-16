using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace DefaultNamespace.NetWork
{
    public class NetworkManager : MonoBehaviourPunCallbacks
    {
        private void Start()
        {
            PhotonNetwork.ConnectUsingSettings();
        }

        public override void OnConnectedToMaster()
        {
            RoomOptions options = new RoomOptions
            {
                MaxPlayers = 4,
                IsVisible = false
            };
            PhotonNetwork.JoinOrCreateRoom("test", options, TypedLobby.Default);
        }

        public override void OnJoinedRoom()
        {
            Debug.Log(PhotonNetwork.CurrentRoom.PlayerCount);
        }
    }
}