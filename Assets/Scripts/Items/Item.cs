using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Item", menuName = "Item", order = 0)]
[System.Serializable]
public class Item : ScriptableObject {

    public new string name = "New Item";
    public Sprite icon;
    public float lootChance;
}
