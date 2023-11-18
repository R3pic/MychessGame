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
        throw new System.NotImplementedException();
    }
}
