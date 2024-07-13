using UnityEngine;

public class DragBehavior : MonoBehaviour
{
    private Vector3 offset;
    private bool isDragging = false;

    void Start()
    {
        // 检查当前 GameObject 是否有 BoxCollider2D 组件，如果没有则添加一个
        if (gameObject.GetComponent<BoxCollider2D>() == null)
        {
            gameObject.AddComponent<BoxCollider2D>();
        }
    }

    void Update()
    {
        // 获取鼠标位置并转换为世界坐标
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        CheckMouseDown();
        if (isDragging)
        {
            // 更新当前 GameObject 的位置
            transform.position = mousePosition + offset;
        }
        CheckMouseUp();
    }

    void DragInit()
    {
        // 初始化拖拽时的偏移量
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        offset = transform.position - mousePosition;
        isDragging = true;
        Debug.Log("开始拖拽: " + gameObject.name);
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
                    DragInit();
                    // 打印正在拖拽的物体名称
                    Debug.Log("正在拖拽的物体: " + gameObject.name);
                }
            }
        }
    }

    void CheckMouseUp()
    {
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }
    }
}
