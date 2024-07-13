using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Maything.UI.MenuStripUI
{

    public class MenuStripEventSystem : MonoBehaviour
    {
        [HideInInspector]
        public static MenuStripEventSystem instance;

        public RectTransform canvasTransform;

        [Header("Make Menus")]
        public MenuStripTheme theme;
        public GameObject menuTemplate;
        public GameObject menuItemTemplate;
        public MenuStripData[] menuData;

        [Serializable]
        public class MenuStripEvent : UnityEvent<MenuStripItemData>
        {

        }

        //[HideInInspector]
        //public bool isClickMenu = false;

        [HideInInspector]
        public List<MenuStripUI> menuUIs = new List<MenuStripUI>();

        [SerializeField]
        private MenuStripEvent m_OnMenuClick = new MenuStripEvent();

        RectTransform dockTransform = null;

        public MenuStripEvent onMenuClick
        {
            get { return m_OnMenuClick; }
            set { m_OnMenuClick = value; }
        }
        void Awake()
        {
            instance = this;
        }

        // Start is called before the first frame update
        void Start()
        {
            //instance = this;
            MakeMenuStrip();
        }

        void MakeMenuStrip()
        {
            if (menuData == null) return;
            if (menuData.Length == 0) return;
            if (theme == null) return;
            if (menuTemplate == null) return;
            if (menuItemTemplate == null) return;
            if (canvasTransform == null) return;

            foreach (MenuStripData data in menuData)
            {
                GameObject go = Instantiate(menuTemplate, canvasTransform);
                MenuStripUI menuStripUI = go.GetComponent<MenuStripUI>();
                if (menuStripUI != null)
                {
                    menuStripUI.theme = theme;
                    menuStripUI.menuTemplate = menuTemplate;
                    menuStripUI.menuItemTemplate = menuItemTemplate;
                    menuStripUI.canvasTransform = canvasTransform;
                    menuStripUI.items = MenuHelper.ItemTextToItemData(data.itemText);
                }

                data.menuUI = menuStripUI;

                go.SetActive(false);

                menuUIs.Add(menuStripUI);
            }

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonUp(0))
            {
                //�û����������������Ҫ�رյ�ǰ��ʾ�����в˵���
                //���
                CloseAllMenu();
            }

        }

        public void CloseAllMenu()
        {
            foreach (MenuStripUI ui in menuUIs)
            {
                if (!ui.isClose)
                    ui.CloseMenu();
            }

        }

        public void SetMenuTransform(RectTransform rect)
        {
            dockTransform = rect;
        }

        public void ShowMenu(string key, Vector3 position)
        {
            foreach (MenuStripData data in menuData)
            {
                if (data.key == key)
                {
                    data.menuUI.gameObject.GetComponent<RectTransform>().position = position;
                    data.menuUI.ShowMenu();
                    break;
                }
            }
        }

        public void ShowMenu(string key, RectTransform rect)
        {
            if (rect == null) return;

            Vector3 pos = rect.position - new Vector3(0, rect.rect.height, 0);

            ShowMenu(key, pos);
        }

        public void ShowMenu(string key)
        {
            ShowMenu(key, dockTransform);
        }
    }
}