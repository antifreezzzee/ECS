using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace DefaultNamespace.NetWork
{
    public class NetworkManager : MonoBehaviourPunCallbacks
    {
        public GameObject PlayerSample;
        public List<Transform> SpawnPoints;
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
            var id = PhotonNetwork.LocalPlayer.ActorNumber;
            Debug.Log("Joined room with " +
                      PhotonNetwork.CurrentRoom.PlayerCount +
                      " players and ID is " +
                      "id");
            if (id> SpawnPoints.Count + 1)
            {
                Debug.LogError("no spawn point");
            }
            else
            {
                PhotonNetwork.Instantiate(PlayerSample.name, SpawnPoints[id - 1].position, Quaternion.identity);
            }
        }
    }
}