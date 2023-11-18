using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessGame : MonoBehaviour
{
    [SerializeField]
    Board board;
    [SerializeField]
    PieceGenerator generator;
    private void Awake()
    {
        board.InitBoard();
        generator.Init(board.GetBoard());
        generator.PiecesBatch();
    }
}