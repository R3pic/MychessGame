using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : Piece
{

    private void Awake()
    {
        pieceName = "Pawn";
    }
    protected override void Move()
    {

    }

    public override void ShowPossibleMove()
    {
        List<Cell> possibleMoves = GetPossibleMoves();
        foreach (Cell cell in possibleMoves)
        {
            cell.GetComponent<SpriteRenderer>().color = Color.green;
        }
                    
    }

    private List<Cell> GetPossibleMoves()
    {
        List<Cell> possibleMoves = new List<Cell>();

        // 폰이 앞으로 한 칸 이동할 수 있는 경우
        if (IsCellValid(locX , locY + 1))
        {
            possibleMoves.Add(board[locX, locY + 1]);
        }

        // 시작 위치에서 폰이 앞으로 두 칸 이동할 수 있는 경우 (예: 폰이 2번째 라인에 있을 때)
        if (locY == 1 && IsCellValid(locX, locY+2))
        {
            possibleMoves.Add(board[locX, locY + 2]);
        }

        return possibleMoves;
    }
}
