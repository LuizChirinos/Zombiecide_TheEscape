using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    //variavel do GameObject a ser seguido
    [Header("GameObject a ser seguido")]
    public GameObject target;

    //variaveis para constrains do eixo X
    [Header("Limitadores para a câmera do Eixo X")]
    public float xMin;
    public float xMax;
    //variaveis para constrains do eixo Y
    [Header("Limitadores para a câmera do Eixo Y")]
    public float zMin;
    public float zMax;

    [Header("Suavizador de movimento")]
    public float smoothMov;

    private Vector3 newPos;
    private float newX;
    private float newZ;
    
	// Update is called once per frame
	void LateUpdate () {

        transform.position = Vector3.Lerp(transform.position, newPos, smoothMov*Time.deltaTime);

        ConstrainsPosition();
	}
    void ConstrainsPosition()
    {
        newPos = new Vector3(newX, transform.position.y, newZ);
        ConstrainsX();
        ConstrainsZ();
    }
    void ConstrainsX()
    {
        newX = Mathf.Clamp(target.transform.position.x, xMin, xMax);
    }
    void ConstrainsZ()
    {
        newZ = Mathf.Clamp(target.transform.position.z, zMin, zMax);
    }
}
