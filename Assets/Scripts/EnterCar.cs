using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterCar : MonoBehaviour
{

    [Header("Referencia para configuracoes do jogador")]
    public GameObject playerObj;
    MovePlayer movePlayer;
    [Header("Referencia para configuracoes do carro")]
    public CarController carCtrl;
    public CameraFollow cameraFollow;

    [Header("Variavel de Interacao")]
    public bool canInteract;

    private void Start()
    {
        movePlayer = playerObj.GetComponent<MovePlayer>();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (carCtrl.usingCar)
            {
                ExitsTheCar();
            }
            else
            {
                if (this.canInteract)
                {
                    EntersTheCar();
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //só pode entrar no carro se estiver proximo ao mesmo
            canInteract = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //esta fora da area de interacao
            canInteract = false;
        }
    }

    void EntersTheCar()
    {
        Debug.Log("Entrou no carro");
        cameraFollow.target = transform.parent.parent.gameObject;
        playerObj.SetActive(false);
        carCtrl.usingCar = true;
        carCtrl.rb.isKinematic = false;
    }
    void ExitsTheCar()
    {
        Debug.Log("Saiu do carro");
        cameraFollow.target = playerObj;
        playerObj.transform.position = new Vector3(transform.position.x + 2f, 2f, transform.position.z + 2f);
        playerObj.SetActive(true);
        carCtrl.usingCar = false;
        carCtrl.rb.isKinematic = true;
    }
}