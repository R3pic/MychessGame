using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField] int x;
    [SerializeField] int y;
    [SerializeField] private Piece currentPiece;
    [SerializeField] bool isMoveAble = false;

    SpriteRenderer spriteRenderer;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    //Cell 좌표 초기화 이 좌표는 변하지 않을 예정임.
    public void Init(int x, int y)
    {
        name = x + "x" + y;
        this.x = x;
        this.y = y;
        if((x+y)%2 == 0)
        {
            spriteRenderer.color = Color.grey;
        }
    }
    public void SetPiece(Piece piece)
    {
        currentPiece = piece;
    }

    public void PrintInfo()
    {
        Debug.Log("X : " + x + "Y : " + y + " 현재 위치한 말:" + currentPiece);
    }

    public void HilightMoveable()
    {
        spriteRenderer.color = Color.green;
    }
}
