using UnityEngine;

public class DragBehavior : MonoBehaviour
{
    private Vector3 offset;
    private bool isDragging = false;

    void OnMouseDown()
    {
        // Convert the pixels to world units
        DragInit();
    }

    void OnMouseUp()
    {
        isDragging = false;
    }

    void Start()
    {
        if (gameObject.GetComponent<BoxCollider2D>() == null)
        {
            gameObject.AddComponent<BoxCollider2D>();
        }
    }

    void Update()
    {
        // 获取鼠标位置并转换为世界坐标
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (isDragging)
        {
            // 更新Object的位置
            transform.position = mousePosition + offset;
        }
    }

    void DragInit()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        offset = transform.position - mousePosition;
        isDragging = true;
    }
}
