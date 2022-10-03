using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class ParticleSystem : MonoBehaviour
{
  Vector3 mainObjPos;
  float maxX, minX, maxZ, minZ;
  Vector3 gravity = new Vector3(0, -10, 0);
  float genRate = 2000;
  float COR = 0.7f;
  static int maxParticles = 10000;
  int numParticles = 0; 
  List<Particle> particles = new List<Particle>();
  MainScript script;
  float timer=0;
  float maxTime;
  Vector3 breezeForce;
  Vector3 angle;

  void Start(){
    mainObjPos = this.transform.position;
    Vector3 scale = this.transform.localScale;
    maxX = scale.x/2.0f;
    minX = -scale.x/2.0f;
    maxZ = scale.z/2.0f;
    minZ = -scale.z/2.0f;
    script = GameObject.Find("GroundMain").GetComponent<MainScript>();
    generateBreezeForce();
    angle = transform.forward;
  }

  void generateBreezeForce(){
    maxTime = Random.Range(10.0f,15.0f);
    float x = Random.Range(-10.0f, 10.0f);
    float z = Random.Range(-10.0f, 10.0f);
    breezeForce = new Vector3(x, 0, z);
    timer=0;
  }

  void Update(){
    if(script.getGamePlayReady() == true){

      if(timer>=maxTime){
        generateBreezeForce();
      }

      //--------------------------------------------------------------
      // Generate particles
      float toGen_float = genRate * Time.deltaTime;
      int toGen = (int) toGen_float;
      for (int i = 0; i < toGen; i++) {
        if (numParticles >= maxParticles) break;
        particles.Add(createParticle());
        numParticles += 1;
      }
      //--------------------------------------------------------------

      //--------------------------------------------------------------
      // Remove particles
      for (int i = 0; i < numParticles; i++) {
        Vector3 pos = particles[i].getGameObject().transform.position;
        Collider[] hitColliders = Physics.OverlapSphere(pos, 0.5f);
        // if(pos.y < 1.0f) {
        if(hitColliders.Length > 0){
          foreach(Collider collider in hitColliders){
            if(System.String.Equals(collider.gameObject.name, "Sphere") == false || pos.y < 0.1f){
              particles[i].destroy();
              particles.RemoveAt(i);
              numParticles -= 1;
            }
          }
        }
      }
      //--------------------------------------------------------------

      //--------------------------------------------------------------
      // Momentum
      for (int i = 0; i <  numParticles; i++) {
        Vector3 acc = gravity + breezeForce; //Gravity
        particles[i].getGameObject().transform.position += particles[i].velocity * Time.deltaTime;
        particles[i].velocity += acc * Time.deltaTime;
        angle = ((0.95f*angle)+(0.05f*acc));
        particles[i].getGameObject().transform.rotation = Quaternion.LookRotation(angle, Vector3.up); 

      }
      //--------------------------------------------------------------
      timer+=Time.deltaTime;
    }
  }

  Particle createParticle() {
    Color color = new Color(20, 120, 20);
    Vector3 pos = generateRandomPos();
    Vector3 vel = generateRandomVel();
    return new Particle(pos, vel, color);
  }

  Vector3 generateRandomPos() {
    float x = Random.Range(minX, maxX);
    float y = this.transform.position.y - 1.0f;
    float z = Random.Range(minZ, maxZ);
    return new Vector3(x, y, z);
  }

  Vector3 generateRandomVel() {
    float x = 0.0f;
    // float y = Random.Range(-190.0f, -200.0f);
    float y = Random.Range(-2.0f, -5.0f);
    float z = 0.0f;
    return new Vector3(x, y, z);
  }
}