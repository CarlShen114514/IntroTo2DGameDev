using UnityEngine;
using Maything.UI.MenuStripUI;

public class FolderControllerver_0 : MonoBehaviour
{
    public GameObject folderWindow; // 文件夹窗口
    public GameObject closeButton; // 关闭按钮
    private Camera mainCamera;
    public GameObject thisGameObject;

    private GameObject copiedFolder;
    private GameObject copiedcloseButton;
    private GameObject copiedfolderWindow;
    private Vector3 copiedPosition;
    private Quaternion copiedRotation;


    private void Start()
    {
        mainCamera = Camera.main;

        if (gameObject.name == "FolderIcon")
        {
            folderWindow.SetActive(false); // 初始化时隐藏窗口
        }
        Debug.Log("FolderController: Start method called.");
       if (MenuStripEventSystem.instance != null)
       {
           Debug.Log("MenuStripEventSystem instance found.");
           MenuStripEventSystem.instance.onMenuClick.AddListener(OnMenuClicked);
       }
       else
       {
           Debug.LogError("MenuStripEventSystem instance is null.");
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

    private void Destroy()
    {
        if (gameObject.name == "FolderIcon")
        {
            Destroy(folderWindow); // 销毁文件夹窗口
        }
    }

    private void OnMenuClicked(MenuStripItemData itemData)
    {
        Vector2 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Debug.Log("clicked on "+ itemData.name);
        switch (itemData.name)
        {
            case "New..":
                Debug.Log("New");
                break;
            case "Open Folder":
                OpenFolderWindow();
                break;
            case "Copy Folder":
                // 保存当前选中GameObject的信息
                if (gameObject.name == "FolderIcon")
                {
                    copiedFolder = Instantiate(gameObject);
                    copiedfolderWindow = Instantiate(folderWindow);
                    copiedcloseButton = Instantiate(closeButton);
                    copiedPosition = copiedFolder.transform.position;
                    copiedRotation = copiedFolder.transform.rotation;
                    
                    // 销毁临时实例化的对象，因为我们只需要它们的配置
                    //Destroy(copiedFolder);
                    //Destroy(copiedfolderWindow);
                    //Destroy(copiedcloseButton);
                    
                    Debug.Log("Copied GameObject and associated components");
                }
                break;
            case "Paste Folder":
                // 在鼠标位置附近随机粘贴复制的GameObject
                if (copiedFolder != null)
                {
                    mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                    
                    // 生成随机偏移量，范围为-10到10
                    float randomXOffset = UnityEngine.Random.Range(-10f, 10f);
                    float randomYOffset = UnityEngine.Random.Range(-10f, 10f);
                    
                    // 应用偏移量
                    Vector3 pastePosition = new Vector3(mousePosition.x + randomXOffset, mousePosition.y + randomYOffset, copiedPosition.z);
                    
                    // 实例化FolderIcon及其所有组件
                    GameObject newFolderIcon = Instantiate(copiedFolder, pastePosition, copiedRotation);
                    
                    // 更新新FolderIcon的脚本引用
                    FolderController newFolderController = newFolderIcon.GetComponent<FolderController>();
                    if (newFolderController != null)
                    {
                        newFolderController.folderWindow = Instantiate(copiedfolderWindow, pastePosition + (copiedfolderWindow.transform.position - copiedFolder.transform.position), copiedfolderWindow.transform.rotation);
                        newFolderController.closeButton = Instantiate(copiedcloseButton, pastePosition + (copiedcloseButton.transform.position - copiedFolder.transform.position), copiedcloseButton.transform.rotation);
                    }
                    
                    Debug.Log("Pasted GameObject and associated components at " + pastePosition);
                }
                break;
            case "Rename Folder":

                break;
            case "Delete Folder":
                Destroy(thisGameObject);
                Destroy(folderWindow);
                Destroy(closeButton);
                break;
        }
    }


}
