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
        // 8�� ���� ����: ��, ��, ��, ��, �밢�� 4����
        int[][] directions = new int[][] {
            new int[] { 1, 0 },  // ��
            new int[] { -1, 0 }, // ��
            new int[] { 0, 1 },  // ��
            new int[] { 0, -1 }, // ��
            new int[] { 1, 1 },  // ���� ��� �밢��
            new int[] { 1, -1 }, // ���� �ϴ� �밢��
            new int[] { -1, 1 }, // ���� ��� �밢��
            new int[] { -1, -1 } // ���� �ϴ� �밢��
        };

        foreach (var direction in directions)
        {
            for (int i = 1; i < board.GetLength(0); i++)
            {
                int newX = locX + i * direction[0];
                int newY = locY + i * direction[1];

                if (newX < 0 || newX >= board.GetLength(0) || newY < 0 || newY >= board.GetLength(1))
                    break; // ������ ���

                if (board[newX, newY].hasCurrentPiece())
                {
                    AddPossibleMove(possibleMoves, newX, newY);
                    break; // �ٸ� ���� ����
                }

                AddPossibleMove(possibleMoves, newX, newY);
            }
        }

        return possibleMoves;
    }

}
