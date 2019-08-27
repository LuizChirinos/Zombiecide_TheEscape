using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveManager : MonoBehaviour {

    [Header("Quantidade de suprimentos do jogador")]
    public int supplyPoints;

    [Header("GameObject Painel de Vitoria")]
    public GameObject panelWin;
    Inventory inv;

    private void Start()
    {
        inv = Inventory.inventory;
    }

    /// <summary>
    /// Calcula o número de suprimentos que o jogador tem.
    /// </summary>
    void CalculateSupply()
    {
        foreach(Item _item in inv.listItems)
        {
            if(_item.name == "Canned Food")
            {
                supplyPoints++;
                Debug.Log("Você tem " + supplyPoints + _item.name);
            }
            else if (_item.name == "Medic Kit")
            {
                supplyPoints++;
                Debug.Log("Você tem " + supplyPoints + _item.name);
            }
            else if (_item.name == "Fresh Food")
            {
                supplyPoints++;
                Debug.Log("Você tem " + supplyPoints + _item.name);
            }
        }
        VictoryCheck();
    }

    /// <summary>
    /// Verifica se o jogador tem o número de suprimentos nescessários.
    /// </summary>
    void VictoryCheck()
    {
        if(supplyPoints >= 6)
        {
            //Chama a tela/painel de vitória.
            Debug.Log("VITÓRIA!");
            //Ativa Painel de Vitoria
            panelWin.SetActive(true);
            //Para o tempo
            Time.timeScale = 0f;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.lockState = CursorLockMode.Confined;
        }
        else
        {
            Debug.Log("Você ainda não tem 6 suprimentos!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Carro"))
        {
            Debug.Log("Carro entrou.");
            CalculateSupply();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Carro"))
        {
            supplyPoints = 0;
            Debug.Log("supplyPoints = " + supplyPoints);
        }
    }
}
