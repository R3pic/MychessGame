using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Piece : MonoBehaviour
{
    public string pieceName;
    public int locX;
    public int locY;
    public Cell[,] board;
    public Cell CurrentCell;
    public ChessTeam team;
    public void Init(int x, int y, string name, Cell[,] board)
    {
        pieceName = name;
        locX = x;
        locY = y;
        this.board = board;
        CurrentCell = board[x,y];
        if(y == 0 || y == 1)
        {
            team = ChessTeam.white;
        }
        else
        {
            team = ChessTeam.black;
        }
    }
    public void PrintInfo()
    {
        Debug.Log(pieceName + "의 위치 : "+locX+", "+locY);
    }

    // 지정된 셀이 유효한지 확인 (보드의 경계를 넘지 않는지)
    protected bool IsCellValid(int x, int y)
    {
        // 보드 경계 확인
        if (x < 0 || x > 7 || y < 0 || y > 7)
        {
            return false;
        }
        return true;
    }
    /*
     Piece를 Cell의 위치로 이동함.
     */
    public virtual void Move(Cell cell)
    {
        HidePossibleMove();
        CurrentCell.SetPiece(null);
        CurrentCell = cell;
        //만약 이동하려는 셀에 말이 있다면.
        if (cell.hasCurrentPiece())
        {
            cell.DestroyPiece();
            cell.SetPiece(this);
        }
        else
        {
            cell.SetPiece(this);
        }
        locX = cell.x;
        locY = cell.y;
        transform.position = cell.transform.position;
    }

    public virtual void ShowPossibleMove()
    {
        List<Cell> possibleMoves = GetPossibleMoves();
        foreach (Cell cell in possibleMoves)
        {
            cell.SetIsMoveable(true);
        }

    }

    public virtual void HidePossibleMove()
    {
        List<Cell> possibleMoves = GetPossibleMoves();
        foreach (Cell cell in possibleMoves)
        {
            cell.SetIsMoveable(false);
        }
    }

    protected abstract List<Cell> GetPossibleMoves();

    protected virtual void AddPossibleMove(List<Cell> moves, int x, int y)
    {
        if (IsCellValid(x, y))
        {
            Piece pieceAtCell = board[x, y].GetPiece();

            // 해당 위치에 말이 있고, 현재 말과 다른 팀에 속한다면 추가한다.
            if (pieceAtCell != null && pieceAtCell.GetTeam() != team)
            {
                moves.Add(board[x, y]);
            }
            // 해당 위치에 말이 없다면 추가한다.
            else if (pieceAtCell == null)
            {
                moves.Add(board[x, y]);
            }
        }
    }

    protected virtual void AddPossibleKillMove(List<Cell> moves, int x, int y)
    {
        if (IsCellValid(x, y) && board[x, y].hasCurrentPiece() && board[x, y].GetPiece().GetTeam() != team)
        {
            moves.Add(board[x, y]);
        }
    }

    public ChessTeam GetTeam()
    {
        return team;
    }
}
