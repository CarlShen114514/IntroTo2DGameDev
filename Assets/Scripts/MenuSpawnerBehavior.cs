using System.Collections;
using System.Collections.Generic;
using Maything.UI.MenuStripUI;
using UnityEngine;

public class MenuSpawnerBehavior : MonoBehaviour
{
     //public ClickBehavior clickBehavior;
     public string menuName = "DefaultMenu";
     public MenuStripEventSystem menuStripEventSystem;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    
    public void ShowDefaultMenu()
    {
        Vector3 mousePos = Input.mousePosition;


        if (MenuStripEventSystem.instance != null)
        {
            MenuStripEventSystem.instance.ShowMenu("DefaultMenu", mousePos);
        }
    }

    public void ShowFolderMenu()
    {
        Vector3 mousePos = Input.mousePosition;


        if (MenuStripEventSystem.instance != null)
        {
            MenuStripEventSystem.instance.ShowMenu("FolderMenu", mousePos);
        }
    }

    public void ShowFileMenu()
    {
        Vector3 mousePos = Input.mousePosition;
        if (MenuStripEventSystem.instance != null)
        {
            MenuStripEventSystem.instance.ShowMenu("FileMenu", mousePos);
        }
    }

    public void ShowFolderWindowMenu()
    {
        Vector3 mousePos = Input.mousePosition;
        if (MenuStripEventSystem.instance != null)
        {
            MenuStripEventSystem.instance.ShowMenu("FolderWindowMenu", mousePos);
        }
    }

    public void ShowLadderMenu()
    {
        Vector3 mousePos = Input.mousePosition;
        if (MenuStripEventSystem.instance != null)
        {
            MenuStripEventSystem.instance.ShowMenu("LadderMenu", mousePos);
        }
    }




}
