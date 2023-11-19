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
                Debug.Log("�÷��̾� ��ȣ: " + player.ActorNumber);
            }
            photonView.RPC("StartGame", RpcTarget.All);
        }
        else
        {
            networkManager.ChatRPC("<color=red> 2���� �÷��̾ �ʿ��մϴ�. </color>");
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
