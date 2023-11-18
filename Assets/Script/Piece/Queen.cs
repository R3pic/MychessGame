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
        List<Cell> possibleMoves = new();
        // 8개 방향 정의: 상, 하, 좌, 우, 대각선 4방향
        int[][] directions = new int[][] {
            new int[] { 1, 0 },  // 상
            new int[] { -1, 0 }, // 하
            new int[] { 0, 1 },  // 우
            new int[] { 0, -1 }, // 좌
            new int[] { 1, 1 },  // 우측 상단 대각선
            new int[] { 1, -1 }, // 우측 하단 대각선
            new int[] { -1, 1 }, // 좌측 상단 대각선
            new int[] { -1, -1 } // 좌측 하단 대각선
        };

        foreach (var direction in directions)
        {
            for (int i = 1; i < board.GetLength(0); i++)
            {
                int newX = locX + i * direction[0];
                int newY = locY + i * direction[1];

                if (newX < 0 || newX >= board.GetLength(0) || newY < 0 || newY >= board.GetLength(1))
                    break; // 범위를 벗어남

                if (board[newX, newY].hasCurrentPiece())
                {
                    AddPossibleMove(possibleMoves, newX, newY);
                    break; // 다른 말을 만남
                }

                AddPossibleMove(possibleMoves, newX, newY);
            }
        }

        return possibleMoves;
    }

}
