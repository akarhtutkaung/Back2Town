using UnityEngine;
using System.Collections;
 
public class main_camera : MonoBehaviour {
    float camSens = 0.25f;
    private Vector3 lastMouse = new Vector3(255, 255, 255);
    bool onGround = true;
    Vector3 prevPos;
    MainScript script;
    float yHeight = 2.2f;
    float userMainSpeed = 8.0f; //regular speed
    float userFastSpeed = 12f; //fast speed

    void Start() {
        script = GameObject.Find("GroundMain").GetComponent<MainScript>();
    }
    void Update () {

        lastMouse = Input.mousePosition - lastMouse ;
        lastMouse = new Vector3(-lastMouse.y * camSens, lastMouse.x * camSens, 0 );
        lastMouse = new Vector3(transform.eulerAngles.x + lastMouse.x , transform.eulerAngles.y + lastMouse.y, 0);
        transform.eulerAngles = lastMouse;
        lastMouse =  Input.mousePosition;

        if (Input.GetKeyUp("v")){
            onGround = !onGround;
            changeCameraView();
        }
        if(onGround){

            Vector3 p = GetBaseInput();
            if (p.sqrMagnitude > 0){
            if (Input.GetKey(KeyCode.LeftShift)){
                // Running
                p = p * userFastSpeed;
            } else {
                p = p * userMainSpeed;
            }
            p = p * Time.deltaTime;
            transform.Translate(p);
            Vector3 newPosition = new Vector3(transform.position.x, yHeight, transform.position.z);
            transform.position = newPosition;
            }
        }
    }

    void changeCameraView() {
        if(onGround == false){
            // eagle view
            transform.position = new Vector3(0, 20, 0);
            transform.rotation = Quaternion.LookRotation(new Vector3(45, 0, 0));
        } else {
            transform.position = new Vector3(0, yHeight, 0);
        }
    }

    Vector3 GetBaseInput() { //returns the basic values, if it's 0 than it's not active.
      Vector3 p_Velocity = new Vector3();
      if (Input.GetKey (KeyCode.W)){
          p_Velocity += new Vector3(0, 0 , 1);
      }
      if (Input.GetKey (KeyCode.S)){
          p_Velocity += new Vector3(0, 0, -1);
      }
      if (Input.GetKey (KeyCode.A)){
          p_Velocity += new Vector3(-1, 0, 0);
      }
      if (Input.GetKey (KeyCode.D)){
          p_Velocity += new Vector3(1, 0, 0);
      }
      return p_Velocity;
    }
}