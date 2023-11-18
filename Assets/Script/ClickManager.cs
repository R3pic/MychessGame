using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
    [SerializeField]
    Piece SelectedPiece;
    void Update()
    {
        // 마우스 왼쪽 버튼 클릭 감지
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null)
            {
                // 레이캐스트가 감지한 오브젝트에서 스크립트 컴포넌트 가져오기
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
