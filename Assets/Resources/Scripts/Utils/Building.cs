using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    public float size;

    public GameObject gameObject;

    public Building(GameObject gameObject, float size) {
        this.gameObject = gameObject;
        this.size = size;
    }
}
