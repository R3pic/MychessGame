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
        throw new System.NotImplementedException();
    }
}
