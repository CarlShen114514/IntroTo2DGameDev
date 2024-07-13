// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// namespace Maything.UI.MenuStripUI
// {

//     public class RightPopMenu : MonoBehaviour
//     {
//         public string menuName;

//         // Start is called before the first frame update
//         void Start()
//         {

//         }

//         // Update is called once per frame
//         void Update()
//         {
//             if (Input.GetMouseButton(1))
//             {
//                 Vector3 mousePos = Input.mousePosition;

//                 if (MenuStripEventSystem.instance != null)
//                 {
//                     MenuStripEventSystem.instance.ShowMenu(menuName, mousePos);
//                 }

//             }
//         }

//     }
// }

// using System.Collections;
// using System.Collections.Generic;
// using Unity.VisualScripting;
// using UnityEngine;

// namespace Maything.UI.MenuStripUI
// {
//     public class RightPopMenu : MonoBehaviour
//     {
//         public string menuName;
//         public string targetTag; // 新增一个公共字符串属性，用于存储目标Tag的名称
//         private void Start()
//         {
//             string targetTag =  this.gameObject.tag;
//         }

//         private void Update()
//         {
//             if (Input.GetMouseButtonDown(1)) // 改为GetMouseButtonDown以确保只在鼠标按钮按下瞬间执行
//             {
//                 // 获取鼠标点击的屏幕坐标
//                 Vector3 mousePos = Input.mousePosition;
//                 Debug.Log("Click ");
//                 // 将屏幕坐标转换为世界坐标
//                 Ray ray = Camera.main.ScreenPointToRay(mousePos);
//                 RaycastHit hit;
                
//                 // 使用Physics.Raycast进行射线检测
//                 if (Physics.Raycast(ray, out hit))
//                 {
//                     Debug.Log("Click " + hit.transform.gameObject.name);
//                     // 检查被击中的游戏对象是否具有目标Tag
//                     if (hit.transform.gameObject.CompareTag(targetTag))
//                     {
//                         Debug.Log("Click Tag" + hit.transform.gameObject.tag);
//                         if (MenuStripEventSystem.instance != null)
//                         {
//                             // 显示菜单
//                             MenuStripEventSystem.instance.ShowMenu(menuName, mousePos);
//                         }
//                     }
//                 }
//             }
//         }
//     }
// }

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Maything.UI.MenuStripUI
{
    public class RightPopMenu : MonoBehaviour
    {
        public string defaultMenuName = "DefaultMenu";
        private string currentMenuName;

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(1)) // 右键单击
            {
                Vector3 mousePos = Input.mousePosition;
                Ray ray = Camera.main.ScreenPointToRay(mousePos);
                RaycastHit hit;
                Debug.Log("try hit");

                if (Physics.Raycast(ray, out hit))
                {
                    Debug.Log("Hit"+hit.collider.gameObject.name);
                    GameObject clickedObject = hit.collider.gameObject;

                    // 根据标签设置菜单名称
                    switch (clickedObject.tag)
                    {
                        case "Test":
                            currentMenuName = "TestMenu";
                            break;
                        case "Folder":
                            currentMenuName = "FolderMenu";
                            break;
                        // 添加更多标签和对应的菜单名称
                        default:
                            currentMenuName = defaultMenuName;
                            break;
                    }

                    if (MenuStripEventSystem.instance != null)
                    {
                        MenuStripEventSystem.instance.ShowMenu(currentMenuName, mousePos);
                    }
                }
                else
                {
                    // 如果未点击到任何对象，显示默认菜单
                    currentMenuName = defaultMenuName;
                    if (MenuStripEventSystem.instance != null)
                    {
                        MenuStripEventSystem.instance.ShowMenu(currentMenuName, mousePos);
                    }
                }
            }
        }
    }
}
