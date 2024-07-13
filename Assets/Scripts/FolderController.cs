using UnityEngine;

public class FolderController : MonoBehaviour
{
    public GameObject folderWindow; // 文件夹窗口
    public GameObject closeButton; // 关闭按钮
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;

        if (gameObject.name == "FolderIcon")
        {
            folderWindow.SetActive(false); // 初始化时隐藏窗口
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 worldPoint = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D[] hits = Physics2D.RaycastAll(worldPoint, Vector2.zero);
            
            foreach (var hit in hits)
            {
                if (hit.collider != null)
                {
                    if (hit.collider.gameObject == closeButton)
                    {
                        CloseFolderWindow(); // 直接调用关闭窗口方法
                        return; // 优先处理 CloseButton 点击
                    }
                    else if (hit.collider.gameObject == gameObject)
                    {
                        OpenFolderWindow(); // 直接调用打开窗口方法
                    }
                }
            }
        }
    }

    private void OpenFolderWindow()
    {
        if (gameObject.name == "FolderIcon")
        {
            folderWindow.SetActive(true); // 当点击文件夹图标时打开窗口
            Debug.Log("已经点击文件夹图标");
        }
    }

    private void CloseFolderWindow()
    {
        if (gameObject.name == "CloseIcon")
        {
            folderWindow.SetActive(false); // 当点击关闭按钮时关闭窗口
            Debug.Log("已经点击关闭图标");
        }
    }
}
