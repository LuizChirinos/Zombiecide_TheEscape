using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissapearRoof : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //corotina para desaparecer material
        }
    }
}
