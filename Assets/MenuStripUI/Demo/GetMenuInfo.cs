using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Maything.UI.MenuStripUI
{
    public class GetMenuInfo : MonoBehaviour
    {
        public Text outputText;
        public void GetMenuClickInfo(MenuStripItemData item)
        {
            outputText.text = item.name;
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
