using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceGenerator : MonoBehaviour
{
    enum Ptype
    {
        BPawn = 0, BKnight = 1, BBishops = 2, BRook = 3, BQueen = 4, BKing = 5,
        WPawn = 6, WKnight = 7, WBishops = 8, WRook = 9, WQueen = 10, WKing = 11
    }
    private Cell[,] board;
    //생성할때 사용할 체스말들 저장되어있는 배열
    [SerializeField] private GameObject[] Pieces;

    public void Init(Cell[,] board)
    {
        this.board = board;
    }

    public void PiecesBatch()
    {
        for(int i = 0; i < board.GetLength(0); i++)
        {
            GeneratePiece(Ptype.WPawn, i, 1, board);
            GeneratePiece(Ptype.BPawn, i, 6, board);
        }
        //룩 생성
        GeneratePiece(Ptype.BRook, 0, 7, board);
        GeneratePiece(Ptype.BRook, 7, 7, board);
        GeneratePiece(Ptype.WRook, 0, 0, board);
        GeneratePiece(Ptype.WRook, 7, 0, board);
        //나이트 생성
        GeneratePiece(Ptype.BKnight, 1, 7, board);
        GeneratePiece(Ptype.BKnight, 6, 7, board);
        GeneratePiece(Ptype.WKnight, 1, 0, board);
        GeneratePiece(Ptype.WKnight, 6, 0, board);
        //숍 생성
        GeneratePiece(Ptype.BBishops, 2, 7, board);
        GeneratePiece(Ptype.BBishops, 5, 7, board);
        GeneratePiece(Ptype.WBishops, 2, 0, board);
        GeneratePiece(Ptype.WBishops, 5, 0, board);
        //킹 생성
        GeneratePiece(Ptype.BKing, 3, 7, board);
        GeneratePiece(Ptype.WKing, 3, 0, board);
        //퀸 생성
        GeneratePiece(Ptype.BQueen, 4, 7, board);
        GeneratePiece(Ptype.WQueen, 4, 0, board);
    }

    private void GeneratePiece(Ptype pieceType, int x, int y, Cell[,] board)
    {
        Piece tmpPiece = Instantiate(Pieces[(int)pieceType], board[x, y].transform.position, Quaternion.identity).GetComponent<Piece>();
        tmpPiece.Init(x, y, tmpPiece.pieceName, board);
        board[x, y].SetPiece(tmpPiece);
    }
}
