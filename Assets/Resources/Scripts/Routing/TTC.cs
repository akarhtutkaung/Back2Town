using UnityEngine;
using System.Collections;
using System.Collections.Generic;
 
public class TTC : MonoBehaviour {
  static float k_avoid = 30;

  // Return at what time agents 1 and 2 collide if they keep their current velocities
  // or -1 if there is no collision.
  static float computeTTC(Vector3 pos1, Vector3 vel1, float radius1, Vector3 pos2, Vector3 vel2, float radius2) {
    Vector3 relativeVelocity = Vector3.Normalize(vel1 - vel2);
    float dist = Vector3.Distance(pos1, pos2);
    float t = CollisionLibrary.rayCircleIntersectTime(pos2, relativeVelocity, pos1, radius1+radius2, dist);
    return t;
  }

  public static Vector3 computeAgentForces(NPC agentNpc, Vector3 goalPos, List<GameObject> npcs, float moveSpeed, Vector3 finalGoalPos) {
      
      Vector3 acc = new Vector3(0, 0, 0);
      Vector3 agentPos = agentNpc.transform.position;
      Vector3 dir = Vector3.Normalize(goalPos - agentPos);
      acc = dir * moveSpeed;
      
      float agentRad = 1.5f;
      for (int j = 0; j < npcs.Count; j++){
        NPC n = npcs[j].GetComponent<NPC>();
        if (n.gameObject.GetInstanceID() == agentNpc.gameObject.GetInstanceID()) continue;
        float ttc = computeTTC(agentNpc.transform.position, agentNpc.getVelocity(), agentRad, n.transform.position, n.getVelocity(), agentRad);
        if (ttc > 0){
          Vector3 futurePos_id = agentNpc.transform.position + (agentNpc.getVelocity() * ttc);
          Vector3 futurePos_j = n.transform.position + (n.getVelocity() * ttc);
          Vector3 avoidDir = Vector3.Normalize( futurePos_id - futurePos_j );
          Vector3 avoidForce = avoidDir * (1/ttc);
          acc = acc + (avoidForce * k_avoid);
        }
      }
      return acc;
  }
  
  static Vector3 setToLength(Vector3 v, float newL){
    float magnitude = v.magnitude;
    v.x *= newL/magnitude;
    v.y *= newL/magnitude;
    v.z *= newL/magnitude;
    return v;
  }
}