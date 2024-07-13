using UnityEngine;

public class IconDragHandler : MonoBehaviour
{
    private Vector3 offset; // 鼠标和物体中心的偏移量
    private bool isDragging = false; // 标记是否正在拖动
    private Transform originalParent; // 初始父对象
    void Start()
    {

        // 如果物体没有 BoxCollider2D，则添加一个
        if (gameObject.GetComponent<BoxCollider2D>() == null)
        {
            gameObject.AddComponent<BoxCollider2D>();
        }

        originalParent = transform.parent; // 记录初始父对象
    }

    void Update()
    {
        // 获取鼠标位置并转换为世界坐标
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        CheckMouseDown();
        // 如果正在拖动，更新物体位置
        if (isDragging)
        {
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
        // Debug.Log("开始拖拽: " + gameObject.name);
    }
    void CheckMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D[] hits = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
            RaycastHit2D hit;
            if (hits.Length == 0)
            {
                return;
            }
            else
            {
                hit = hits[^1];
            }
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                DragInit();
                transform.SetParent(Camera.main.transform, true);
            }
        }
    }

    void CheckMouseUp()
    {
        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            SetParent(gameObject);
            isDragging = false;
        }
    }

    // 检查拖动结束的位置并更新父对象
    void SetParent(GameObject currentObject)
    {
        // 获取所有潜在的父对象
        GameObject[] potentialParents = GameObject.FindGameObjectsWithTag("Window");
        // Debug.Log("找到的父亲数量: " + potentialParents.Length);

        GameObject folderWindow = null;
        GameObject desktop = null;
        bool isInsideFolderWindow = false;

        // 检查是否在 FolderWindow 内
        foreach (var parentObject in potentialParents)
        {
            // Debug.Log("检查父亲: " + parentObject.name);

            if (IsInside(parentObject) && parentObject.name == "FolderWindowBound")
            {
                folderWindow = parentObject;
                isInsideFolderWindow = true;
                // Debug.Log("在 FolderWindow 内");
            }
            else if (IsInside(parentObject) && parentObject.name == "Desktop")
            {
                desktop = parentObject;
                // Debug.Log("在 Desktop 内");
            }
        }
        // 优先选择 FolderWindow，其次选择 Desktop
        if (isInsideFolderWindow && folderWindow != null)
        {
            // Debug.Log("Dropped inside: " + folderWindow.name);
            currentObject.transform.SetParent(folderWindow.transform, true); // 更新父对象为 FolderWindow
        }
        else if (desktop != null)
        {
            // Debug.Log("Dropped inside: " + desktop.name);
            currentObject.transform.SetParent(desktop.transform, true); // 更新父对象为 Desktop
        }
        else
        {
            // 如果不在任何指定区域内，则恢复原父对象
            currentObject.transform.SetParent(originalParent, true);
        }
    }

    // 检查物体是否在指定的父对象区域内
    bool IsInside(GameObject parentObject)
    {
        if (parentObject == null)
        {
            // Debug.Log("Parent object is null.");
            return false;
        }

        BoxCollider2D parentCollider = parentObject.GetComponent<BoxCollider2D>();
        if (parentCollider == null)
        {
            // Debug.Log("Parent object " + parentObject.name + " does not have a BoxCollider2D.");
            return false;
        }

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = parentCollider.bounds.center.z; // 设置 z 坐标以匹配父对象
        bool isInside = parentCollider.bounds.Contains(mousePosition);
        // Debug.Log("Mouse is inside " + parentObject.name + ": " + isInside);
        return isInside;
    }
}
    