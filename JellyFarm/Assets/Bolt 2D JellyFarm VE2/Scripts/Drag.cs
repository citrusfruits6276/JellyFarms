using UnityEngine;
using System;

public class Drag : MonoBehaviour
{
    private bool isDragging = false;
    private bool isPressed = false;
    private bool dragStarted = false;
    public Collider2D sellCol;
    private Vector3 offset;
    public float pickTime;

    private Collider2D col;

    private float minX, maxX, minY, maxY;
    private Vector3 originalPosition; // 🟡 되돌아갈 위치 저장

    void Awake()
    {
        col = GetComponent<Collider2D>();
    }

    void Start()
    {
        minX = GameManager.instance.topLeft.position.x;
        maxX = GameManager.instance.bottomRight.position.x;
        minY = GameManager.instance.bottomRight.position.y;
        maxY = GameManager.instance.topLeft.position.y;
    }

    void Update()
    {
        if (isPressed)
        {
            pickTime += Time.deltaTime;

            if (!dragStarted && pickTime > 0.2f)
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

                if (col.OverlapPoint(mousePos2D))
                {
                    dragStarted = true;
                    isDragging = true;

                    originalPosition = transform.position;

                    offset = transform.position - new Vector3(mousePos.x, mousePos.y, transform.position.z);
                }
                else
                {
                    isPressed = false;
                }
            }

            if (isDragging)
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3 targetPos = new Vector3(mousePos.x, mousePos.y, transform.position.z) + offset;

                transform.position = targetPos;
            }
        }
    }

    void OnMouseDown()
    {
        isPressed = true;
        pickTime = 0f;
        dragStarted = false;
    }

    void OnMouseUp()
    {
        if (isDragging)
        {
            Vector3 pos = transform.position;
            Vector2 point = new Vector2(pos.x, pos.y);

            // ✅ 디버그용 로그
            Debug.Log("드래그 놓은 위치: " + point);
            Debug.Log("판매 콜라이더 위치: " + sellCol.bounds.center + ", 크기: " + sellCol.bounds.size);

            // ✅ 판매 영역 검사
            if (sellCol != null && sellCol.OverlapPoint(point))
            {
                Debug.Log("판매 영역에 드롭됨!");

                // jellyLevel이 있다면 인덱스 수정 필요

                int jellyId = GetComponent<Jelly>().jellyId;
                int jellyLevel = GetComponent<Jelly>().jellyLevel;
                Money.instance.targetGold += GameManager.instance.jellyGoldList[jellyId] * jellyLevel;
                Money.instance.targetGold = Math.Min(Money.instance.targetGold, 99999999);
                Destroy(gameObject);
                return;
            }
            else
            {
                Debug.Log("판매 영역이 아님!");
            }

            // ✅ 화면 밖일 경우 되돌리기
            bool outOfBounds =
                pos.x < minX || pos.x > maxX ||
                pos.y < minY || pos.y > maxY;

            if (outOfBounds)
            {
                transform.position = originalPosition;
                Debug.Log("화면 밖이라 되돌림");
            }
        }

        // 상태 초기화
        isPressed = false;
        isDragging = false;
        pickTime = 0f;
        dragStarted = false;
    }
}