using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queen : Piece
{
    private void Awake()
    {
        pieceName = "Queen";
    }

    protected override List<Cell> GetPossibleMoves()
    {
        throw new System.NotImplementedException();
    }

}
