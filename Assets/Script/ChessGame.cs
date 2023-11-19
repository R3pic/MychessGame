using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum ChessTeam
{
    white, black
}

public class ChessGame : MonoBehaviour
{
    [SerializeField] Board board;
    [SerializeField] PieceGenerator generator;
    [SerializeField] NetworkManager networkManager;
    [SerializeField] ClickManager clickManager;
    private PhotonView photonView;

    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
    }

    public void OnStartGameButtonClicked()
    {

        if (PhotonNetwork.PlayerList.Length == 2)
        {
            foreach (var player in PhotonNetwork.PlayerList)
            {
                Debug.Log("플레이어 번호: " + player.ActorNumber);
            }
            photonView.RPC("StartGame", RpcTarget.All);
        }
        else
        {
            networkManager.ChatRPC("<color=red> 2명의 플레이어가 필요합니다. </color>");
        }
    }


    [PunRPC]
    void StartGame()
    {
        board.InitBoard();
        generator.Init(board.GetBoard());
        generator.PiecesBatch();
        networkManager.LobbyPanel.SetActive(false);
        networkManager.RoomPanel.SetActive(false);
        networkManager.LoginPanel.SetActive(false);
        clickManager.InitializeTeam();
    }

}
