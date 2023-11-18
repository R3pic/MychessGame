using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Piece : MonoBehaviour
{
    public string pieceName;
    public int locX;
    public int locY;
    public Cell[,] board;
    public void Init(int x, int y, string name, Cell[,] board)
    {
        pieceName = name;
        locX = x;
        locY = y;
        this.board = board;
    }
    public void PrintInfo()
    {
        Debug.Log(pieceName + "의 위치 : "+locX+", "+locY);
    }

    // 지정된 셀이 유효한지 확인 (보드의 경계를 넘지 않는지, 다른 말이 없는지 등)
    protected bool IsCellValid(int x, int y)
    {
        // 보드 경계 확인
        if (x < 0 || x > 7 || y < 0 || y > 7)
        {
            return false;
        }

        // 해당 셀에 다른 말이 있는지 확인 (여기서는 단순화를 위해 생략)
        // 예: return board[x][y].currentPiece == null;

        return true;
    }
    protected abstract void Move();
    public abstract void ShowPossibleMove();


}
