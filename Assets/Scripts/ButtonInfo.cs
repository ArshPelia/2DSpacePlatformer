using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonInfo : MonoBehaviour
{

    public int ItemID;
    public Text PriceTxt;
    public Text DmgText;
    public GameObject ShopManager;

    // Update is called once per frame
    void Update()
    {
        PriceTxt.text = ShopManager.GetComponent<ShopManager>().shopItems[1, ItemID].ToString();
        if (ItemID < 3) {
            if (ShopManager.GetComponent<ShopManager>().shopItems[1, ItemID].ToString() == "0")
                PriceTxt.text = "Owned";
            DmgText.text = "Dmg: " + ShopManager.GetComponent<ShopManager>().shopItems[2, ItemID].ToString();
        }
        if (ItemID >= 3 && ItemID <= 5)
            DmgText.text = "Qty: " + ShopManager.GetComponent<ShopManager>().shopItems[2, ItemID].ToString();
    }
}
