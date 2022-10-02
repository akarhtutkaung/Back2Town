using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Dijkstra : MonoBehaviour
{

  //Dijkstra
  public static List<Node> runDijkstra(Dictionary<int, Node> nodes, int startID, int goalID) {
    int numNodes = nodes.Count;
    List<Node> path = new List<Node>();
    Dictionary<int, float> totalCostTo = new Dictionary<int, float>(); // Total Cost from start node toward the node provided
    List<int> openList = new List<int>();
    bool goalFound = false;

    List<int> keys = new List<int>(nodes.Keys.ToArray());

    for (int i=0; i<numNodes; i++) {
      int id = keys[i];
      nodes[id].parent = null;
      if (id != startID) {
        totalCostTo[id] = Mathf.Infinity;
      } else {
        totalCostTo[id] = 0.0f;
      }
    }

    openList.Add(startID);
    while (openList.Count > 0) {
      int currentNodeID = openList[0];
      for (int i=0; i<nodes[currentNodeID].neighborsID.Count; i++) {
        int neighborNodeID = nodes[currentNodeID].neighborsID[i];
        float newTotalCost = totalCostTo[currentNodeID] + nodes[currentNodeID].neighborsCost[neighborNodeID];
        if(nodes[currentNodeID].parent is not null){
          if(nodes[neighborNodeID].ID == nodes[currentNodeID].parent.ID){
            continue;
          }
        }
        if (totalCostTo[neighborNodeID] > newTotalCost) {
          if(goalFound == true){
            if(totalCostTo[neighborNodeID] < totalCostTo[goalID]){
              nodes[neighborNodeID].parent = nodes[currentNodeID];
              totalCostTo[neighborNodeID] = newTotalCost;
              openList.Add(neighborNodeID);
            }
          } else {
            nodes[neighborNodeID].parent = nodes[currentNodeID];
            totalCostTo[neighborNodeID] = newTotalCost;
            openList.Add(neighborNodeID);
          }
        }
        if (neighborNodeID == goalID) {
          // goal found
          // Debug.Log("Goal Founds!");
          goalFound = true;
          // break;
        }
      }
      openList.RemoveAt(0);
      if (goalFound) {
        openList.Clear();
      }
    }
    if (!goalFound) {
      // Debug.Log("Goal not founds");
      path.Add(nodes[startID]);
      return null;
    }
    
    Node prevNode = nodes[goalID].parent;
    path.Add(nodes[goalID]);

    while (prevNode is not null) {
      path.Insert(0, prevNode);
      prevNode = prevNode.parent;
    }
    return path;
  }
}