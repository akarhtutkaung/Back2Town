using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
  bool nodeReceived = false;
  Dictionary<int, Node> nodes;
  MainScript script;
  int buildingId;
  public List<Node> path;
  public int counter = 0;
  float moveSpeed = 50;
  public bool pathFound = false;
  Vector3 angle;
  Vector3 velocity;
  public Vector3 acceleration;
  public Vector3 goalPosition;

  public void setNodes(Dictionary<int, Node> nodes){
    this.nodes = nodes;
  }
  public float getMoveSpeed(){ return moveSpeed;}
  public Vector3 getVelocity(){
    return velocity;
  }

  public void setVelocity(Vector3 velocity){
    this.velocity = velocity;
  }
  
  void Start() {
    path = new List<Node>();
    angle = transform.forward;
    velocity = new Vector3(0,0,0);
    script = GameObject.Find("GroundMain").GetComponent<MainScript>();
    if(script.nodeReady){
      nodes = script.nodes;
      nodeReceived = true;
      // Debug.Log("nodeReceived");
      createNewGoalNPath(); // uncomment
    }
  }

  void Update() {
    if(!nodeReceived) {
      if(script.nodeReady){
        nodes = script.nodes;
        nodeReceived = true;
        createNewGoalNPath(); // uncomment
        // createPath();
      }
    } else {
        checkPath();
        if(pathFound){
          computeAcceleration();
        }
    }
  }

  void LateUpdate() {
    if(pathFound){
      move();
    }
  }

  void drawNodeLines(){
    Vector3 p1Pos = this.transform.position;
    Vector3 p2Pos = path[counter].position;
    if(counter == path.Count-1){
      Debug.DrawLine(p1Pos, p2Pos, Color.blue);   
    } else {
      Debug.DrawLine(p1Pos, p2Pos, Color.red);   
    }   
  }

  bool firstTime = true;
  public void checkPath(){
    if(pathFound){
      if(firstTime == true && counter == 0){
        velocity = Vector3.Normalize(path[0].position - transform.position);
        firstTime = false;
      }
      if(counter < path.Count){ // Still has not reached final goal
        // drawNodeLines();
        Vector3 goalPos = path[counter].position;

        // See if the path toward the current goal node has collision with building. If yes, then build new path but to the same final goal point.
        bool intersectCurrentNode = pathIntersectBuilding(counter);
        if(intersectCurrentNode == true){
          buildNewPath();
          resetCounter();
        }

        // If the path cannot be build anymore toward the same final goal point, then choose new final goal point
        if(path is null){
          createNewGoalNPath();
        } else {
          if(counter < path.Count-1){
            // See if the path toward the next goal node has collision with 
            // building. If no, then change the current goal node into next goal node.
            bool intersect = pathIntersectBuilding(counter+1);
            if(intersect == false){
              counter++;
              goalPos = path[counter].position;
            }
          }
        }
      } else { // Reached final goal, so need new goal
        createNewGoalNPath();
        resetCounter();
      }
    } else {
      if(script.nodeReady){
        nodes = script.nodes;
        nodeReceived = true;
        createNewGoalNPath();
      }
    }
  }

  void computeAcceleration(){
    if(pathFound){
    List<GameObject> npcs = script.getNpcs();
    acceleration = TTC.computeAgentForces(this, path[counter].position, npcs, moveSpeed, path[path.Count-1].position);
    }
  }

  bool pathIntersectBuilding(int count) {
    Vector3 dirToNextNode = Vector3.Normalize(path[count].position - transform.position);
    float dist = Vector3.Distance(transform.position, path[count].position);
    bool intersect = CollisionLibrary.rayObjectListIntersect(transform.position, dirToNextNode, script.getBuildings(), dist);
    return intersect;
  }

  void move(){
    velocity = acceleration * Time.deltaTime;
    transform.position += new Vector3(velocity.x, 0, velocity.z) * Time.deltaTime;
    
    // Rotation of the agent
    angle = (0.95f*angle)+(0.05f*acceleration);
    transform.rotation = Quaternion.LookRotation(angle, Vector3.up); 

    Vector3 goalPos = path[counter].position;
    // Reached to goal node, change the goal node into next node
    if(Vector3.Distance(transform.position, goalPos) < 0.9f){
      counter++;
    }
  }

  public void createNewGoalNPath(){
    selectGoal();
    buildNewPath();
  }

  void selectGoal() {
    buildingId = Random.Range(0, script.getBuildings().Count-1);
  }

  public void buildNewPath() {
    path = null;
    path = PRM.planPath(transform.position, script.getBuildings()[buildingId].gameObject.transform.position, nodes);
    if(path is null) {
      pathFound = false;
    } else {
      pathFound = true;
      firstTime = true;
    }
  }

  public void resetCounter() {
    counter=0;
  }
}