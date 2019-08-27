using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{

    #region instance
    public static Inventory inventory;

    void Awake()
    {
        if (inventory != null)
        {
            Debug.LogWarning("Error of Inventory instance");
            return;
        }
        inventory = this;
    }
    #endregion

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallBack;
    string itemTag;

    public int spaceInventory;

    public List<Item> listItems = new List<Item>();

    private void Start()
    {
        ReloadItems();
    }
    void ReloadItems()
    {
        Debug.Log("Reloading Items");
        for (int i = 0; i <= MainData.lastInventory.Count - 1; i++)
        {
            Debug.Log(MainData.lastInventory[i].name);
            //listItems[i] = MainData.lastInventory[i];
            listItems.Add(MainData.lastInventory[i]);

            if (onItemChangedCallBack != null)
            {
                print("delegate not equal to null");
                onItemChangedCallBack.Invoke();
            }
            else
                print("delegate null");
        }

    }

    public void AddItem(Item item)
    {
        if (listItems.Count >= spaceInventory)
        {
            Debug.Log("Sem Espaco no Inventario");
            return;
        }
        listItems.Add(item);
        MainData.lastInventory.Add(item);

        if (onItemChangedCallBack != null)
        {
            //print("delegate not equal to null");
            onItemChangedCallBack.Invoke();
        }
        else
            print("ERROR");
    }
    public void InsertItem(Item item, int indexItem1)
    {
        listItems.Insert(indexItem1, item);

        if (onItemChangedCallBack != null)
            onItemChangedCallBack.Invoke();
    }
    public void SwitchItems(int indexItem1, int indexItem2)
    {
        Item item1 = listItems[indexItem1];
        Item item2 = listItems[indexItem2];

        listItems[indexItem1] = item2;
        listItems[indexItem2] = item1;

        if (onItemChangedCallBack != null)
            onItemChangedCallBack.Invoke();
    }
    public void RemoveItem(int indexItem1)
    {
        listItems.RemoveAt(indexItem1);
        MainData.lastInventory.RemoveAt(indexItem1);

        if (onItemChangedCallBack != null)
            onItemChangedCallBack.Invoke();
    }
}
