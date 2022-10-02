using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Float : MonoBehaviour
{
  float maxY;
  float minY;
  float offSet = 0.2f;
  float speed = 0.2f;
  bool goUp = true;
  void Start(){
    Vector3 pos = this.transform.position;
    maxY = pos.y + offSet;
    minY = pos.y - offSet;
  }

  void Update(){
    Vector3 currentPos = this.transform.position;
    if(goUp == true){
      currentPos.y += speed * Time.deltaTime;
      if(currentPos.y >= maxY){
        goUp = false;
      }
    } else {
      currentPos.y -= speed * Time.deltaTime;
      if(currentPos.y <= minY){
        goUp = true;
      }
    }
    this.transform.position = currentPos;
  }
}