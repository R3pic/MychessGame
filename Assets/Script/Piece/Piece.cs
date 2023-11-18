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
        Debug.Log(pieceName + "�� ��ġ : "+locX+", "+locY);
    }

    // ������ ���� ��ȿ���� Ȯ�� (������ ��踦 ���� �ʴ���)
    protected bool IsCellValid(int x, int y)
    {
        // ���� ��� Ȯ��
        if (x < 0 || x > 7 || y < 0 || y > 7)
        {
            return false;
        }
        return true;
    }
    /*
     Piece�� Cell�� ��ġ�� �̵���.
     */
    public virtual void Move(Cell cell)
    {
        HidePossibleMove();
        CurrentCell.SetPiece(null);
        CurrentCell = cell;
        //���� �̵��Ϸ��� ���� ���� �ִٸ�.
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

            // �ش� ��ġ�� ���� �ְ�, ���� ���� �ٸ� ���� ���Ѵٸ� �߰��Ѵ�.
            if (pieceAtCell != null && pieceAtCell.GetTeam() != team)
            {
                moves.Add(board[x, y]);
            }
            // �ش� ��ġ�� ���� ���ٸ� �߰��Ѵ�.
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
