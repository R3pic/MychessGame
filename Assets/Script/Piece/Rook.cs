using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rook : Piece
{
    private void Awake()
    {
        pieceName = "Rook";
    }

    protected override List<Cell> GetPossibleMoves()
    {
        List<Cell> possibleMoves = new();

        // 수직 방향 (위)
        for (int i = 1; i < board.GetLength(1) && locY + i < board.GetLength(1); i++)
        {
            if (board[locX, locY + i].hasCurrentPiece())
            {
                AddPossibleMove(possibleMoves, locX, locY + i);
                break;
            }
            AddPossibleMove(possibleMoves, locX, locY + i);
        }

        // 수직 방향 (아래)
        for (int i = 1; i < board.GetLength(1) && locY - i >= 0; i++)
        {
            if (board[locX, locY - i].hasCurrentPiece())
            {
                AddPossibleMove(possibleMoves, locX, locY - i);
                break;
            }
            AddPossibleMove(possibleMoves, locX, locY - i);
        }

        // 수평 방향 (오른쪽)
        for (int i = 1; i < board.GetLength(0) && locX + i < board.GetLength(0); i++)
        {
            if (board[locX + i, locY].hasCurrentPiece())
            {
                AddPossibleMove(possibleMoves, locX + i, locY);
                break;
            }
            AddPossibleMove(possibleMoves, locX + i, locY);
        }

        // 수평 방향 (왼쪽)
        for (int i = 1; i < board.GetLength(0) && locX - i >= 0; i++)
        {
            if (board[locX - i, locY].hasCurrentPiece())
            {
                AddPossibleMove(possibleMoves, locX - i, locY);
                break;
            }
            AddPossibleMove(possibleMoves, locX - i, locY);
        }

        return possibleMoves;
    }
}
