using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.Purchasing;

public class ClickManager : MonoBehaviour
{
    [SerializeField]
    Piece SelectedPiece;
    [SerializeField]
    private ChessTeam currentTurn = ChessTeam.white;

    private void Awake()
    {
        Debug.Log("������ ���۵�. ������ : "+currentTurn.ToString());
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
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
                    SelectedPiece.Move(SelectedCell);
                    SwitchTurn();
                    Debug.Log("������"+currentTurn.ToString());
                    return;
                }

                if (SelectedPiece != null)
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
                    if (SelectedPiece != null)
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


    public void SwitchTurn()
    {
        currentTurn = (currentTurn == ChessTeam.white) ? ChessTeam.black : ChessTeam.white;
        SelectedPiece = null;
    }

    public ChessTeam GetCurrentTurn()
    {
        return currentTurn;
    }
}
