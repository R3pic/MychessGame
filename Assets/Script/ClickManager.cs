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
            //���콺 Ŭ���Ѱ� ����� �ƴϸ�
            if (hit.collider != null)
            {
                Cell SelectedCell = hit.collider.gameObject.GetComponent<Cell>();
                if (SelectedCell != null)
                {
                    //���� ���õ� ü������ ��ġ�� ������ ���� �ű�.
                    photonView.RPC("MovePiece", RpcTarget.All, SelectedPiece.locX, SelectedPiece.locY, SelectedCell.x, SelectedCell.y);
                    SelectedPiece = null;
                    Debug.Log("������"+currentTurn.ToString());
                    return;
                }

                if (SelectedPiece != null && SelectedPiece.GetTeam()==myTeam)
                {
                    // �̹� ���õ� ���� ���� ��
                    if (SelectedPiece == hit.collider.gameObject.GetComponent<Piece>())
                    {
                        // ������ ���� �ٽ� Ŭ���� ���: ���� ���
                        SelectedPiece.HidePossibleMove();
                        SelectedPiece = null;
                    }
                    else
                    {
                        // �ٸ� ���� Ŭ���� ���: ���� ���� �̵� ��ġ ����� �� �� ����
                        SelectedPiece.HidePossibleMove();
                        SelectedPiece = hit.collider.gameObject.GetComponent<Piece>();
                        SelectedPiece.ShowPossibleMove();
                    }
                }
                else
                {
                    // ���õ� ���� ���� ��: ���ο� �� ����
                    SelectedPiece = hit.collider.gameObject.GetComponent<Piece>();
                    if (SelectedPiece != null && SelectedPiece.GetTeam() == myTeam)
                    {
                        SelectedPiece.ShowPossibleMove();
                    }
                }
            }
            else // ����� Ŭ���Ѵٸ�
            {
                if (SelectedPiece != null)
                {
                    SelectedPiece.HidePossibleMove(); // ���� ���õ� ���� �̵� ��ġ �����
                }
                SelectedPiece = null;
            }
        }
    }

    [PunRPC]
    //�� �ΰ��� ���� ��ġ, �� �ΰ��� �̵��� ��ġ.
    public virtual void MovePiece(int pieceX, int pieceY, int cellX, int cellY)
    {
        //���� ������ Piece�� x,y�� ���� ��ġ�� ���� ���� �� �ְ�
        //�� Cell�� ��ġ x,y�� 
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
                    EndMessage.text = ChessTeam.black + "�� �¸��ϼ̽��ϴ�.";
                else
                    EndMessage.text = ChessTeam.white + "�� �¸��ϼ̽��ϴ�.";

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
            Turn_Text.text = "���� �� : " + Black + "\n�� ���� : " + myTeam;
        }
        else
        {
            Turn_Text.text = "���� �� : " + White + "\n�� ���� : " + myTeam;
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
            Debug.Log("�÷��̾� ��ȣ ��: " + player.ActorNumber);
        }
        Turn_Text.gameObject.SetActive(true);
        // �÷��̾� �� Ȯ��
        if (playerList.Count >= 2)
        {
            myTeam = PhotonNetwork.LocalPlayer.ActorNumber == playerList[0].ActorNumber ? ChessTeam.white : ChessTeam.black;
            Turn_Text.text = "���� �� : " + currentTurn + "\n�� ���� : " + myTeam;
        }
        else
        {
            Debug.LogError("����� �÷��̾ �����ϴ�.");
        }
    }

        public ChessTeam MyTeam()
    {
        return myTeam;
    }
}
