using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    Cell[,] board;
    [SerializeField]
    GameObject CellPrefab;
    public void InitBoard()
    {
        board = new Cell[8, 8];
        for (int y = 0; y < 8; y++)
        {
            for (int x = 0; x < 8; x++)
            {
                Cell cell = Instantiate(CellPrefab, new Vector3(x, y, 0), Quaternion.identity).GetComponent<Cell>();
                board[x, y] = cell;
                cell.transform.SetParent(transform);
                board[x, y].Init(x, y);
            }
        }
    }

    public Cell[,] GetBoard()
    {
        return board;
    }

    public Piece GetPieceAt(int x, int y)
    {
        if (x >= 0 && x < 8 && y >= 0 && y < 8)
        {
            return board[x, y].GetPiece();
        }
        return null;
    }

    public void SetPieceAt(int x, int y, Piece piece)
    {
        if (x >= 0 && x < 8 && y >= 0 && y < 8)
        {
            board[x, y].SetPiece(piece);
        }
    }
}
