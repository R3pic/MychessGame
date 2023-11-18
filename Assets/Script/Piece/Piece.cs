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
        Debug.Log(pieceName + "�� ��ġ : "+locX+", "+locY);
    }

    // ������ ���� ��ȿ���� Ȯ�� (������ ��踦 ���� �ʴ���, �ٸ� ���� ������ ��)
    protected bool IsCellValid(int x, int y)
    {
        // ���� ��� Ȯ��
        if (x < 0 || x > 7 || y < 0 || y > 7)
        {
            return false;
        }

        // �ش� ���� �ٸ� ���� �ִ��� Ȯ�� (���⼭�� �ܼ�ȭ�� ���� ����)
        // ��: return board[x][y].currentPiece == null;

        return true;
    }
    protected abstract void Move();
    public abstract void ShowPossibleMove();


}
