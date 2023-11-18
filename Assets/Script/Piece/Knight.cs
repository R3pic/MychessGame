using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : Piece
{
    private void Awake()
    {
        pieceName = "Knight";
    }

    protected override List<Cell> GetPossibleMoves()
    {
        List<Cell> possibleMoves = new();

        AddPossibleMove(possibleMoves, locX - 1, locY + 2);
        AddPossibleMove(possibleMoves, locX + 1, locY + 2);
        AddPossibleMove(possibleMoves, locX + 2, locY + 1);
        AddPossibleMove(possibleMoves, locX + 2, locY - 1);
        AddPossibleMove(possibleMoves, locX + 1, locY - 2);
        AddPossibleMove(possibleMoves, locX - 1, locY - 2);
        AddPossibleMove(possibleMoves, locX - 2, locY - 1);
        AddPossibleMove(possibleMoves, locX - 2, locY + 1);
        return possibleMoves;
    }
}
