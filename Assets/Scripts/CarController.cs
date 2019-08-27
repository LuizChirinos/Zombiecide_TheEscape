using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour {

    //colocar referencia para objetivo atual
    //public ObjetiveClass objClass;

    //Rotacao final do carro
    public Quaternion nextRot;
    Vector3 initialPos;

    public Rigidbody rb;

    [Header("Velocidade do Carro")]
    public float carVel;
    [Header("Velocidade de Rotacao Horizontal")]
    public float xVelRot;
    [Header("Velocidade de Rotacao Vertical")]
    public float yVelRot;

    public bool usingCar;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        initialPos = transform.position;
        this.usingCar = false;
    }

	// Update is called once per frame
	void FixedUpdate () {
        if (this.usingCar)
        {
            float xmov = Input.GetAxis("Horizontal");
            float ymov = Input.GetAxis("Vertical");

            Vector3 movement = new Vector3(xmov, 0f, ymov);
            rb.velocity = movement * carVel;
            transform.position = new Vector3(transform.position.x, initialPos.y, transform.position.z);

            //rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, 0.001f);

            //condicoes para girar carro

            //se as setas estiverem pressionadas
            if (xmov != 0 || ymov != 0)
            {
                if (xmov > 0)   //seta DIREITA ou D
                {
                    //rotacao para direita
                    nextRot = Quaternion.Euler(nextRot.x, 90f, nextRot.z);
                    transform.rotation = Quaternion.Lerp(transform.rotation, nextRot, 0.1f * Time.deltaTime * xVelRot);
                }
                else if (xmov < 0)  //seta ESQUERDA ou A
                {
                    //rotacao para esquerda
                    nextRot = Quaternion.Euler(0f, -90f, 0f);
                    transform.rotation = Quaternion.Lerp(transform.rotation, nextRot, 0.1f * Time.deltaTime * xVelRot);

                }
                if (ymov > 0)   //seta PARA CIMA ou W
                {
                    //rotacao para cima
                    nextRot = Quaternion.Euler(0f, 0f, 0f);
                    transform.rotation = Quaternion.Lerp(transform.rotation, nextRot, 0.1f * Time.deltaTime * yVelRot);

                }
                else if (ymov < 0)  //seta PARA BAIXO ou S
                {
                    //rotacao para baixo
                    nextRot = Quaternion.Euler(0f, 180f, 0f);
                    transform.rotation = Quaternion.Lerp(transform.rotation, nextRot, 0.1f * Time.deltaTime * yVelRot);

                }
            }

            //se nenhuma das setas ou WASD estiverem pressionadas, ajusta a rotacao do carro
            else
            {
                //transform.rotation = Quaternion.Lerp(transform.rotation, nextRot, 0.1f * Time.deltaTime * 50f);
                rb.velocity = Vector3.zero;
            }
        }
        
    }
}
