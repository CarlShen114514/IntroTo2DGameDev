using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
public class ClickBehavior : MonoBehaviour
{
    public float doubleClickTime = 0.3f;
    // 定义双击事件
    public UnityEvent onDoubleClick;
    public UnityEvent onRightClick;

    private float lastClickTime = -1f;

    void Start()
    {
        if (gameObject.GetComponent<BoxCollider2D>() == null)
        {
            gameObject.AddComponent<BoxCollider2D>();
        }
    }

    void Update()
    {   
        CheckMouseDown();
        CheckSingleClick();
        CheckRightClick();
    }

    void CheckDoubleClick()
    {
        float timeSinceLastClick = Time.time - lastClickTime;

        // 检查是否在双击时间间隔内
        if (timeSinceLastClick <= doubleClickTime)
        {
            // 触发双击事件
            if (onDoubleClick != null)
            {
                onDoubleClick.Invoke();
            }
        }

        // 更新最后一次点击时间
        lastClickTime = Time.time;
    }

    void CheckSingleClick()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
            if (hit.collider != null)
            {
                if (hit.collider.gameObject == gameObject)
                {
                    gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
                }
                else{
                    gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                }
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
    }

    void CheckRightClick()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(1))
        {
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);
            if (hit.collider != null)
            {
                if (hit.collider.gameObject == gameObject)
                {
                    onRightClick.Invoke();
                }
            }
        }
    }

    void CheckMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D[] hits = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider != null && hit.collider.gameObject == gameObject)
                {
                    CheckDoubleClick();
                }
            }
        }
    }
    public void testRightCLick()
    {
        Debug.Log("Right button clicked.");
    }

    public void testDoubleClick()
    {
        Debug.Log("Double clicked.");
    }
}
