using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bishop : Piece
{
    private void Awake()
    {
        pieceName = "Bishop";
    }

    protected override List<Cell> GetPossibleMoves()
    {
        List<Cell> possibleMoves = new();

        //���� ��
        for (int i = 1; i < board.GetLength(0) && i < board.GetLength(1) && locX + i < board.GetLength(0) && locY + i < board.GetLength(1); i++)
        {
            if (board[locX + i, locY + i].hasCurrentPiece())
            {
                AddPossibleMove(possibleMoves, locX + i, locY + i);
                break;
            }
            AddPossibleMove(possibleMoves, locX + i, locY + i);
        }

        //���� �Ʒ�
        for (int i = 1; i < board.GetLength(0) && locX + i < board.GetLength(0) && locY - i >= 0; i++)
        {
            if (board[locX + i, locY - i].hasCurrentPiece())
            {
                AddPossibleMove(possibleMoves, locX + i, locY - i);
                break;
            }
            AddPossibleMove(possibleMoves, locX + i, locY - i);
        }

        //���� �Ʒ�
        for (int i = 1; i < board.GetLength(0) && locX - i >= 0 && locY - i >= 0; i++)
        {
            if (board[locX - i, locY - i].hasCurrentPiece())
            {
                AddPossibleMove(possibleMoves, locX - i, locY - i);
                break;
            }
            AddPossibleMove(possibleMoves, locX - i, locY - i);
        }

        //���� ��
        for (int i = 1; i < board.GetLength(0) && locX - i >= 0 && locY + i < board.GetLength(1); i++)
        {
            if (board[locX - i, locY + i].hasCurrentPiece())
            {
                AddPossibleMove(possibleMoves, locX - i, locY + i);
                break;
            }
            AddPossibleMove(possibleMoves, locX - i, locY + i);
        }

        return possibleMoves;
    }
}
