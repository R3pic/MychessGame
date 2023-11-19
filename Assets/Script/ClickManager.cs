using Photon.Pun;
using System.Linq;
using TMPro;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
    [SerializeField]
    Piece SelectedPiece;
    [SerializeField]
    Board board;
    [SerializeField]
    Cell[,] currentboard;
    [SerializeField]
    private ChessTeam currentTurn = ChessTeam.white;
    [SerializeField]
    private TMP_Text Turn_Text;
    [SerializeField]
    private GameObject EndPanel;
    [SerializeField]
    private TMP_Text EndMessage;
    private PhotonView photonView;
    private ChessTeam myTeam;

    private string White = "White";
    private string Black = "Black";
    private void Awake()
    {
        photonView = GetComponent<PhotonView>();
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && currentTurn == myTeam)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
            //마우스 클릭한게 허공이 아니면
            if (hit.collider != null)
            {
                Cell SelectedCell = hit.collider.gameObject.GetComponent<Cell>();
                if (SelectedCell != null)
                {
                    //현재 선택된 체스말의 위치를 선택한 셀로 옮김.
                    photonView.RPC("MovePiece", RpcTarget.All, SelectedPiece.locX, SelectedPiece.locY, SelectedCell.x, SelectedCell.y);
                    SelectedPiece = null;
                    Debug.Log("현재턴"+currentTurn.ToString());
                    return;
                }

                if (SelectedPiece != null && SelectedPiece.GetTeam()==myTeam)
                {
                    // 이미 선택된 말이 있을 때
                    if (SelectedPiece == hit.collider.gameObject.GetComponent<Piece>())
                    {
                        // 동일한 말을 다시 클릭한 경우: 선택 취소
                        SelectedPiece.HidePossibleMove();
                        SelectedPiece = null;
                    }
                    else
                    {
                        // 다른 말을 클릭한 경우: 이전 말의 이동 위치 숨기고 새 말 선택
                        SelectedPiece.HidePossibleMove();
                        SelectedPiece = hit.collider.gameObject.GetComponent<Piece>();
                        SelectedPiece.ShowPossibleMove();
                    }
                }
                else
                {
                    // 선택된 말이 없을 때: 새로운 말 선택
                    SelectedPiece = hit.collider.gameObject.GetComponent<Piece>();
                    if (SelectedPiece != null && SelectedPiece.GetTeam() == myTeam)
                    {
                        SelectedPiece.ShowPossibleMove();
                    }
                }
            }
            else // 허공을 클릭한다면
            {
                if (SelectedPiece != null)
                {
                    SelectedPiece.HidePossibleMove(); // 현재 선택된 말의 이동 위치 숨기기
                }
                SelectedPiece = null;
            }
        }
    }

    [PunRPC]
    //앞 두개는 원래 위치, 뒤 두개는 이동할 위치.
    public virtual void MovePiece(int pieceX, int pieceY, int cellX, int cellY)
    {
        //먼저 선택한 Piece의 x,y로 현재 위치한 셀을 구할 수 있고
        //갈 Cell의 위치 x,y로 
        currentboard = board.GetBoard();
        Piece piece = board.GetPieceAt(pieceX, pieceY);
        piece.HidePossibleMove();
        piece.CurrentCell.SetPiece(null);
        piece.CurrentCell = currentboard[cellX, cellY];
        if (currentboard[cellX, cellY].hasCurrentPiece())
        {
            if(currentboard[cellX, cellY].GetPiece().pieceName == "King")
            {
                if (currentboard[cellX, cellY].GetPiece().GetTeam() == ChessTeam.white)
                    EndMessage.text = ChessTeam.black + "이 승리하셨습니다.";
                else
                    EndMessage.text = ChessTeam.white + "가 승리하셨습니다.";

                EndPanel.SetActive(true);
            }
            currentboard[cellX, cellY].DestroyPiece();
        }
        currentboard[cellX, cellY].SetPiece(piece);
        piece.locX = cellX;
        piece.locY = cellY;
        piece.transform.position = piece.CurrentCell.transform.position;
        if(piece is Pawn)
        {
            Pawn tmp = (Pawn)piece;
            tmp.isFirstMove = false;
        }
        SwitchTurn();
    }

    [PunRPC]
    public void SwitchTurn()
    {
        currentTurn = (currentTurn == ChessTeam.white) ? ChessTeam.black : ChessTeam.white;
        if (currentTurn == ChessTeam.black)
        {
            Turn_Text.text = "현재 턴 : " + Black + "\n니 색깔 : " + myTeam;
        }
        else
        {
            Turn_Text.text = "현재 턴 : " + White + "\n니 색깔 : " + myTeam;
        }
        SelectedPiece = null;
    }

    public ChessTeam GetCurrentTurn()
    {
        return currentTurn;
    }

    [PunRPC]
    public void InitializeTeam()
    {
        var playerList = PhotonNetwork.PlayerList.OrderBy(p => p.ActorNumber).ToList();

        foreach (var player in playerList)
        {
            Debug.Log("플레이어 번호 순: " + player.ActorNumber);
        }
        Turn_Text.gameObject.SetActive(true);
        // 플레이어 수 확인
        if (playerList.Count >= 2)
        {
            myTeam = PhotonNetwork.LocalPlayer.ActorNumber == playerList[0].ActorNumber ? ChessTeam.white : ChessTeam.black;
            Turn_Text.text = "현재 턴 : " + currentTurn + "\n니 색깔 : " + myTeam;
        }
        else
        {
            Debug.LogError("충분한 플레이어가 없습니다.");
        }
    }

        public ChessTeam MyTeam()
    {
        return myTeam;
    }
}
