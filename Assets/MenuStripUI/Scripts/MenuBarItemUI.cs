using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Maything.UI.MenuStripUI
{

    public class MenuBarItemUI : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public MenuBarData menuBarData;
        public MenuStripTheme theme;
        public MenuBarUI menuBarUI;

        RectTransform ownerTransform;
        Text textControl;
        // Start is called before the first frame update
        void Start()
        {
            ownerTransform = GetComponent<RectTransform>();
            textControl = GetComponent<Text>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        void ShowOwnerMenu()
        {
            if (menuBarData != null && menuBarData.menuStripUI != null)
            {
                menuBarData.menuStripUI.GetComponent<RectTransform>().position = ownerTransform.position - new Vector3(0, ownerTransform.rect.height, 0);
                menuBarData.menuStripUI.ShowMenu();
            }
        }

        void IPointerDownHandler.OnPointerDown(UnityEngine.EventSystems.PointerEventData eventData)
        {
            ShowOwnerMenu();
        }

        void IPointerEnterHandler.OnPointerEnter(UnityEngine.EventSystems.PointerEventData eventData)
        {
            if (MenuStripEventSystem.instance != null)
                MenuStripEventSystem.instance.CloseAllMenu();

            if (menuBarUI != null)
                menuBarUI.CloseAllMenu();

            ShowOwnerMenu();

            textControl.color = theme.menuBarSelectedTextColor;

        }

        void IPointerExitHandler.OnPointerExit(UnityEngine.EventSystems.PointerEventData eventData)
        {
            textControl.color = theme.menuBarTextColor;

        }
    }
}