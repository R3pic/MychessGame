using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pawn : Piece
{
    [SerializeField]
    public bool isFirstMove;
    private void Awake()
    {
        pieceName = "Pawn";
        isFirstMove = true;
    }

    protected override List<Cell> GetPossibleMoves()
    {
        List<Cell> possibleMoves = new();

        if(team == ChessTeam.black)
        {
            if (isFirstMove && !board[locX, locY - 1].hasCurrentPiece())
            {
                AddPossibleMove(possibleMoves, locX, locY - 2);
            }
            AddPossibleMove(possibleMoves, locX, locY - 1);

            AddPossibleKillMove(possibleMoves, locX + 1, locY - 1);
            AddPossibleKillMove(possibleMoves, locX - 1, locY - 1);
        }
        else if(team == ChessTeam.white)
        {
            if(isFirstMove && !board[locX, locY+1].hasCurrentPiece())
            {
                AddPossibleMove(possibleMoves, locX, locY + 2);
            }
            AddPossibleMove(possibleMoves, locX, locY + 1);

            AddPossibleKillMove(possibleMoves, locX+1, locY+1);
            AddPossibleKillMove(possibleMoves, locX - 1, locY + 1);
        }

        return possibleMoves;
    }

    public override void Move(Cell cell)
    {
        base.Move(cell);
        isFirstMove = false;
    }

    protected override void AddPossibleMove(List<Cell> moves, int x, int y)
    {
        if (IsCellValid(x, y) && !board[x, y].hasCurrentPiece())
        {
            moves.Add(board[x, y]);
        }
    }

    protected override void AddPossibleKillMove(List<Cell> moves, int x, int y)
    {
        if (IsCellValid(x, y) && board[x, y].hasCurrentPiece() && board[x, y].GetPiece().GetTeam() != team)
        {
            moves.Add(board[x, y]);
        }
    }
}
