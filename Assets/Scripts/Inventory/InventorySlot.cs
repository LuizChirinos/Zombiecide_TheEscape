using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour {

    public Image icon;
    Item item;    

    /// <summary>
    /// Adiciona o item no Slot
    /// </summary>
    /// <param name="newitem"></param>
    public void AddToSlot(Item newitem)
    {
        item = newitem;
        icon.sprite = item.icon;
        icon.enabled = true;
    }
    /// <summary>
    /// Limpa o conteúdo do Slot
    /// </summary>
    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
    }
}
