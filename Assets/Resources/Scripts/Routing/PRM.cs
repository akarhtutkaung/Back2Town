using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PRM : MonoBehaviour
{
  //Set which nodes are connected to which neighbors (graph edges) based on PRM rules

  public static Dictionary<int, Node> connectNeighbors(Dictionary<int, Node> nodes, List<Building> objects) {
    // Debug.Log(objects.Count);
    for (int i = 0; i < nodes.Count; i++) {
      nodes[i].neighborsID = new List<int>();  //Clear neighbors list
      nodes[i].neighborsCost = new Dictionary<int, float>();  //Clear neighborsCost list
      for (int j = 0; j < nodes.Count; j++) {
        if (i == j) continue; //don't connect to myself
        Vector3 dir = Vector3.Normalize(nodes[j].position - nodes[i].position);
        float dist = Vector3.Distance(nodes[i].position, nodes[j].position);
        bool hit = CollisionLibrary.rayObjectListIntersect(nodes[i].position, dir, objects, dist);
        if (hit == false) {
          // Debug.Log("NoIntersetct");
          nodes[i].neighborsID.Add(j);
          nodes[i].neighborsCost[j] = dist;
        }
      }
    }
    return nodes;
  }

  //This is probably a bad idea and you shouldn't use it...
  public static int closestNodeID(Vector3 pos, Dictionary<int, Node> nodes) {
    int closestID = 0;
    float minDist = 999999;
    List<int> keys = new List<int>(nodes.Keys.ToArray());
    for (int i = 0; i < nodes.Count; i++) {
      Node node = nodes[keys[i]];
      float dist = Vector3.Distance(pos, node.position);
      if (dist < minDist) {
        closestID = keys[i];
        minDist = dist;
      }
    }
    return closestID;
  }

  public static List<Node> planPath(Vector3 startPos, Vector3 goalPos, Dictionary<int, Node> nodes) {
    List<Node> path = new List<Node>();

    int startID = closestNodeID(startPos, nodes);
    int goalID = closestNodeID(goalPos, nodes);
    
    //path = runBFS(nodePos, numNodes, startID, goalID);
    path = Dijkstra.runDijkstra(nodes, startID, goalID);

    return path;
  }
}