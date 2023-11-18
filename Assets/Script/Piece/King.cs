using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : Piece
{
    private void Awake()
    {
        pieceName = "King";
    }
    protected override List<Cell> GetPossibleMoves()
    {
        List<Cell> possibleMoves = new();

        AddPossibleMove(possibleMoves, locX - 1, locY + 1);
        AddPossibleMove(possibleMoves, locX, locY + 1);
        AddPossibleMove(possibleMoves, locX + 1, locY + 1);
        AddPossibleMove(possibleMoves, locX - 1, locY);
        AddPossibleMove(possibleMoves, locX + 1, locY);
        AddPossibleMove(possibleMoves, locX - 1, locY - 1);
        AddPossibleMove(possibleMoves, locX, locY - 1);
        AddPossibleMove(possibleMoves, locX + 1, locY - 1);

        return possibleMoves;
    }

}
