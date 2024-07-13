using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Maything.UI.MenuStripUI
{
    public class MenuBarUI : MonoBehaviour
    {
        public float textSpace = 12;

        public RectTransform canvasTransform;

        public MenuStripTheme theme;

        [Header("Template")]
        public GameObject menuTemplate;
        public GameObject menuItemTemplate;
        public GameObject menuBarItemTemplate;

        [Header("Binding")]
        public Image background;
        public RectTransform content;

        public MenuBarData[] menuBarData;

        bool isOrderBarItem = false;
        RectTransform ownerTransform;

        // Start is called before the first frame update
        void Start()
        {
            ownerTransform = GetComponent<RectTransform>();
            background.color = theme.menuBarBackground;
            Initialization();
        }

        void Initialization()
        {
            foreach (MenuBarData data in menuBarData)
            {
                GameObject go = Instantiate(menuBarItemTemplate, content);
                Text text = go.GetComponent<Text>();
                MenuBarItemUI barItemUI = go.GetComponent<MenuBarItemUI>();
                barItemUI.menuBarData = data;
                barItemUI.theme = theme;
                barItemUI.menuBarUI = this;

                if (text != null)
                {
                    text.text = data.name;
                    text.color = theme.menuBarTextColor;
                }
                if (data.itemData != null && data.itemData.Count > 0)
                {
                    GameObject menugo = Instantiate(menuTemplate, canvasTransform);
                    MenuStripUI menuStripUI = menugo.GetComponent<MenuStripUI>();
                    if (menuStripUI != null)
                    {
                        menuStripUI.theme = theme;
                        menuStripUI.menuTemplate = menuTemplate;
                        menuStripUI.menuItemTemplate = menuItemTemplate;
                        menuStripUI.canvasTransform = canvasTransform;
                        menuStripUI.items = MenuHelper.ItemTextToItemData(data.itemText);  //data.itemData;
                    }

                    data.menuBarUI = go;
                    data.menuStripUI = menuStripUI;

                    menugo.SetActive(false);

                    if (MenuStripEventSystem.instance != null)
                        MenuStripEventSystem.instance.menuUIs.Add(menuStripUI);

                }
            }
        }

        // Update is called once per frame
        void Update()
        {
            UpdateBarItemPosition();
        }

        void UpdateBarItemPosition()
        {
            if (isOrderBarItem) return;

            float offsetX = textSpace;

            for (int i = 0; i < menuBarData.Length; i++)
            {
                RectTransform rect = menuBarData[i].menuBarUI.GetComponent<RectTransform>();

                if (rect.rect.width <= 0) return;

                rect.localPosition = new Vector3(ownerTransform.rect.width / -2f + offsetX, ownerTransform.rect.height - rect.rect.height / 2f, 0);
                offsetX += rect.rect.width + textSpace;

            }

            isOrderBarItem = true;

        }

        public void CloseAllMenu()
        {
            foreach (MenuBarData data in menuBarData)
            {
                if (data.menuStripUI != null)
                    data.menuStripUI.CloseMenu();
            }
        }
    }
}