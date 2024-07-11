using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class IconBehavior : MonoBehaviour
{
    public string spritePath = "Sprites/";
    void Start()
    {
        Sprite iconSprite;
        if (gameObject.CompareTag("folder"))
        {
            iconSprite = Resources.Load<Sprite>(spritePath + "Folder");
        }
        else
        {
            iconSprite = Resources.Load<Sprite>(spritePath + gameObject.name);
        }
        
        if (iconSprite != null)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = iconSprite;
            transform.localScale = new Vector3(5, 5, 0);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
