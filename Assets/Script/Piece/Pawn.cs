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

        // ���� ������ �� ĭ �̵��� �� �ִ� ���
        if (IsCellValid(locX , locY + 1))
        {
            possibleMoves.Add(board[locX, locY + 1]);
        }

        // ���� ��ġ���� ���� ������ �� ĭ �̵��� �� �ִ� ��� (��: ���� 2��° ���ο� ���� ��)
        if (locY == 1 && IsCellValid(locX, locY+2))
        {
            possibleMoves.Add(board[locX, locY + 2]);
        }

        return possibleMoves;
    }
}
