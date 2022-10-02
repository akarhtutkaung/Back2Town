using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public int ID;

    public Vector3 position;

    public Dictionary<int, float> neighborsCost;

    public List<int> neighborsID;

    public Node parent;

    public Node(int ID, Vector3 position) {
        this.ID = ID;
        this.neighborsCost = new Dictionary<int, float>();
        this.neighborsID = new List<int>();
        this.position = position;
    }
}
