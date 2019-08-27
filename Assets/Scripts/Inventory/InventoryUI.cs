using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour {

    [Header("Colocar GameObject itemsParent")]
    public Transform itemsParent;
    [Header("Colocar GameObject Inventory")]
    public GameObject inventoryUI;
    [Space]
    Inventory inventory;
    InventorySlot[] slots;

    // Use this for initialization
    void Start () {
        inventory = Inventory.inventory;
        inventory.onItemChangedCallBack += UpdateUI;
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
		Time.timeScale = 1f;
	}

    void Update()
    {
        //Debug.Log (Time.timeScale);
        //Debug.Log(fpsController.m_MouseLook.lockCursor);
        if (Input.GetKeyDown(KeyCode.I))
        {
            //Debug.Log(inventoryUI.activeSelf);
            if (!inventoryUI.activeSelf)
            {
                Pause();
                Debug.Log("Abre inventário");
            }
            else if (inventoryUI.activeSelf)
            {
                Unpause();
                Debug.Log("Fecha inventário");
            }
        }
    }

    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < inventory.listItems.Count)
            {
                slots[i].AddToSlot(inventory.listItems[i]);
								
                Debug.Log("Updating UI");
            }
            else {
                slots[i].ClearSlot();
            }
        }
    }

    void Pause()
    {
        inventoryUI.SetActive(true);

        //UnlockMouseCursor ();
    }
    public void Unpause()
    {
        inventoryUI.SetActive(false);

        //LockMouseCursor ();
    }

	public void UnlockMouseCursor()
	{
		Cursor.lockState = CursorLockMode.None;
		//Cursor.lockState = CursorLockMode.Confined;
		Cursor.visible = true;
	}

	public void LockMouseCursor()
	{
		Cursor.lockState = CursorLockMode.None;
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}
}
