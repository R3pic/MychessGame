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
        throw new System.NotImplementedException();
    }
}
