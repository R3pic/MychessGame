using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ChessTeam
{
    white, black
}

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
    }

    private void Start()
    {
        generator.PiecesBatch();

    }

}