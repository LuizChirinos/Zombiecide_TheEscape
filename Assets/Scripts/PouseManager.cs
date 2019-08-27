using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PouseManager : MonoBehaviour {
    public GameObject menuPouse;
    public bool isPouse;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPouse)
            {
                PouseGame();
            }            
        }
	}

    void PouseGame()
    {
        if(menuPouse && !isPouse)
        {
            Time.timeScale = 0;
            Cursor.visible = true;
            menuPouse.SetActive(true);
            isPouse = true;
        }
    }

    public void UnPouseGame()
    {
        if (menuPouse && isPouse)
        {
            Time.timeScale = 1;
            Cursor.visible = false;
            menuPouse.SetActive(false);
            isPouse = false;
        }
    }
}
