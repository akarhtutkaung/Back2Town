using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
  GameObject gameObject;

  public Vector3 velocity;

  public Color color;

  public Particle(Vector3 position, Vector3 velocity, Color color){
    gameObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);
    gameObject.transform.position = position;
    gameObject.transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);
    this.velocity = velocity;
    this.color = color;
  }

  public void destroy(){
    Destroy(gameObject);
  }

  public GameObject getGameObject(){
    return gameObject;
  }
}