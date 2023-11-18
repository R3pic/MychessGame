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

        // ���� ���� (��)
        for (int i = 1; i < board.GetLength(1) && locY + i < board.GetLength(1); i++)
        {
            if (board[locX, locY + i].hasCurrentPiece())
            {
                AddPossibleMove(possibleMoves, locX, locY + i);
                break;
            }
            AddPossibleMove(possibleMoves, locX, locY + i);
        }

        // ���� ���� (�Ʒ�)
        for (int i = 1; i < board.GetLength(1) && locY - i >= 0; i++)
        {
            if (board[locX, locY - i].hasCurrentPiece())
            {
                AddPossibleMove(possibleMoves, locX, locY - i);
                break;
            }
            AddPossibleMove(possibleMoves, locX, locY - i);
        }

        // ���� ���� (������)
        for (int i = 1; i < board.GetLength(0) && locX + i < board.GetLength(0); i++)
        {
            if (board[locX + i, locY].hasCurrentPiece())
            {
                AddPossibleMove(possibleMoves, locX + i, locY);
                break;
            }
            AddPossibleMove(possibleMoves, locX + i, locY);
        }

        // ���� ���� (����)
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
