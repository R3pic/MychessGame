using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    [SerializeField] public int x;
    [SerializeField] public int y;
    [SerializeField] private Piece currentPiece;
    [SerializeField] bool isMoveable = false;
    [SerializeField] bool isDark = false;

    SpriteRenderer spriteRenderer;
    Collider2D colllider2d;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.material = new Material(spriteRenderer.material);
        colllider2d = GetComponent<Collider2D>();
    }

    //Cell 좌표 초기화 이 좌표는 변하지 않을 예정임.
    public void Init(int x, int y)
    {
        colllider2d.enabled = false;
        name = x + "x" + y;
        this.x = x;
        this.y = y;
        if((x+y)%2 == 0)
        {
            isDark = true;
            spriteRenderer.material.SetFloat("_isDark", isDark ? 1.0f : 0.0f);
        }
    }
    public void SetPiece(Piece piece)
    {
        currentPiece = piece;
    }

    public Piece GetPiece()
    {
        if(currentPiece == null)
            return null;
        return currentPiece;
    }

    public void DestroyPiece()
    {
        currentPiece.gameObject.SetActive(false);
    }

    public void PrintInfo()
    {
        Debug.Log("X : " + x + "Y : " + y + " 현재 위치한 말:" + currentPiece);
    }

    public void SetIsMoveable(bool Toggle)
    {
        isMoveable = Toggle;
        colllider2d.enabled = isMoveable;
        spriteRenderer.material.SetFloat("_isMoveable", isMoveable ? 1f : 0f);
    }

    public bool hasCurrentPiece()
    {
        return currentPiece != null;
    }
}
