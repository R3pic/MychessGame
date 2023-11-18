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
        Debug.Log("게임이 시작됨. 현재턴 : "+currentTurn.ToString());
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
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
                    SelectedPiece.Move(SelectedCell);
                    SwitchTurn();
                    Debug.Log("현재턴"+currentTurn.ToString());
                    return;
                }

                if (SelectedPiece != null)
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
                    if (SelectedPiece != null)
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
