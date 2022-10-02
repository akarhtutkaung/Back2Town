using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionLibrary : MonoBehaviour
{
  static GameObject objToIgnore = GameObject.Find("GroundMain");
  
  public static float rayCircleIntersectTime(Vector3 center, Vector3 dir, Vector3 modelPos, float r, float max_dist) {
    Vector3 toCircle = center - modelPos;
    
    float a = dir.sqrMagnitude;
    float b = -2 * Vector3.Dot(dir,toCircle);
    float tmp = (toCircle.x*toCircle.x)+(toCircle.y*toCircle.y)+(toCircle.z*toCircle.z);
    float c = tmp - (r*r); //different of squared distances
    
    float d = b*b - 4*a*c; //discriminant 
    
    if (d >=0){
      float t1 = (-b - Mathf.Sqrt(d))/(2*a); //Optimization: we only need the first collision
      float t2 = (-b + Mathf.Sqrt(d))/(2*a);
      if (t1 >= 0 && t1 < max_dist){
        return t1;
      } else if (t1 < 0 && t2 >= 0){
        return t2;
      }
    }
    return -1.0f;
  }

  public static bool rayCircleIntersect(Vector3 center, Vector3 dir, Vector3 modelPos, float r, float max_dist) {
    float t = rayCircleIntersectTime(center, dir, modelPos, r, max_dist);
    if(t >= 0){
      return true;
    }
    return false;
  }

  public static bool rayObjectListIntersect(Vector3 pos, Vector3 dir, List<Building> models, float max_t) {
    for (int i=0; i<models.Count; i++){
      Vector3 modelPos = models[i].gameObject.transform.position;
      float r = models[i].size;
      bool hit = rayCircleIntersect(modelPos, dir, pos, r, max_t);
      if(hit == true){
        return true;
      }
    }
    return false;
  }
}