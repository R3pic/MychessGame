using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
    [SerializeField]
    Piece SelectedPiece;
    void Update()
    {
        // ���콺 ���� ��ư Ŭ�� ����
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null)
            {
                // ����ĳ��Ʈ�� ������ ������Ʈ���� ��ũ��Ʈ ������Ʈ ��������
                SelectedPiece = hit.collider.gameObject.GetComponent<Piece>();

                if (SelectedPiece != null)
                {
                    SelectedPiece.PrintInfo();
                    SelectedPiece.ShowPossibleMove();
                }
            }
            else
            {
                SelectedPiece = null;
            }
        }
    }
}
