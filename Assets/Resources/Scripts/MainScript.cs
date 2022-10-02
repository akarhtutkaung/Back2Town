using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

class MainScript : MonoBehaviour
{
    // Models from assets folder
    public GameObject[] buildingModels = new GameObject[10];
    public float[] buildingModelSizes = new float[10];
    GameObject[] npcModels;
    List<Building> buildings;
    List<GameObject> npcs;
    public GameObject building;
    int buildingMaxAmount = 20;
    int npcMaxAmount = 10;
    int nodeMaxAmount = 200;
    float minX = -55.0f;
    float maxX = 55.0f;
    float minZ = -55.0f;
    float maxZ = 55.0f;
    public Dictionary<int, Node> nodes;
    public bool nodeReady = false;
    bool gameplayReady = false;
    bool firstTime = true;
    bool mouseClickPressed = false;
    GameObject camera;
    GameObject newObstacle; 
    float newObstacleOffset = 20.0f;
    LineRenderer laserLineRenderer;
    Material obsHit, obsNohit;

    public bool getGamePlayReady(){
        return gameplayReady;
    }
    public List<Building> getBuildings(){
        return buildings;
    }

    public List<GameObject> getNpcs(){
        return npcs;
    }

	// Use this for initialization
	void Start ()
    {   
        obsHit = Resources.Load<Material>("Textures/LineRender/laserHit");
        obsNohit = Resources.Load<Material>("Textures/LineRender/laserNoHit");
        this.nodes = new Dictionary<int, Node>();
        loadNPCs();
        createRandomBuildings();
        camera = GameObject.Find("MainCamera");
        laserLineRenderer = camera.GetComponent<LineRenderer>();
        laserLineRenderer.enabled = false;
    }
    
    void Update()
    {   
        // drawNodeLines();
        if(gameplayReady == true && firstTime == true){
            createRandomNodes();
            nodes = PRM.connectNeighbors(nodes, buildings);
            createNPCs();     
            nodeReady = true;
            firstTime = false;
        }
        if(gameplayReady == false){
            if (Input.GetKeyUp("p")){
                // Ready to play
                gameplayReady = true;
            }
            if (Input.GetMouseButtonDown(0)){
            // Ready to play
            // Debug.Log("mouse clicked");
            mouseClickPressed = true;
            addObstacle();
            }
            if (Input.GetMouseButtonUp(0)){
            // Ready to play
            // Debug.Log("mouse released");
            mouseClickPressed = false;
            }
            if(mouseClickPressed == true){
                newObstacleOffset += Input.mouseScrollDelta.y * 0.5f;
                Vector3 cameraPos = camera.transform.position;
                Vector3 cameraDir = camera.transform.forward;
                newObstacle.transform.position = new Vector3(cameraPos.x + (cameraDir.x * newObstacleOffset), 0, cameraPos.z + (cameraDir.z * newObstacleOffset));
            }
        } else {
            if (Input.GetMouseButtonDown(0)){
            // Display ray
            mouseClickPressed = true;
            displayRay();
            }
            if (Input.GetMouseButtonUp(0)){
            // Display ray
            mouseClickPressed = false;
            unDisplayRay();
            }
            if(mouseClickPressed == true){
                moveRay();
            }
            if (Input.GetKeyDown("space")){
                spacePressed = true;
                firstPressed = true;
            }
            if (Input.GetKeyUp("space")){
                if(locked == true){
                    createRandomNodes();
                    nodes = PRM.connectNeighbors(nodes, buildings);
                    foreach(GameObject npc in npcs){
                        npc.GetComponent<NPC>().setNodes(nodes);
                        npc.GetComponent<NPC>().buildNewPath();
                        npc.GetComponent<NPC>().resetCounter();
                    }
                }
                displayRay();
                spacePressed = false;
                locked = false;
            }
            
        }
    }
    bool spacePressed = false; 
    bool firstPressed = false;
    bool locked = false;
    GameObject obstacleHitGameObject;

    void moveObstacle() {
        Vector3 cameraPos = camera.transform.position;
        Vector3 cameraDir = camera.transform.forward;
        newObstacleOffset += Input.mouseScrollDelta.y * 0.1f;
        obstacleHitGameObject.transform.position = new Vector3(cameraPos.x + (cameraDir.x * newObstacleOffset), 0, cameraPos.z + (cameraDir.z * newObstacleOffset));
        // obstacleHitGameObject.transform.position = newPos;
    }

    void displayRay(){
        laserLineRenderer.enabled = true;
    }

    void moveRay(){
        Vector3 camDir = camera.transform.forward;
        Vector3 camPos = camera.transform.position;
        Vector3 newPos = new Vector3(camPos.x, camPos.y - 0.15f, camPos.z);
        laserLineRenderer.material = obsNohit;
        laserLineRenderer.SetWidth(0.05f, 0.05f);
        laserLineRenderer.SetPosition(0, camDir * 50);
        laserLineRenderer.SetPosition(1, newPos);
        RaycastHit hit;
        Vector3 startPos = laserLineRenderer.GetPosition(1);
        Vector3 endPos = laserLineRenderer.GetPosition(0);

        // Check if ray hit obstacle
        if (Physics.Raycast(startPos, Vector3.Normalize(endPos - startPos), out hit))
        {
            GameObject hitGameObject = hit.transform.gameObject;
            for(int i=0; i<buildings.Count; i++){
                if(locked == false){
                    if(hitGameObject.GetInstanceID() == buildings[i].gameObject.GetInstanceID()){
                        laserLineRenderer.material = obsHit;
                        if(spacePressed){
                            obstacleHitGameObject = hitGameObject;
                            if(firstPressed){
                                newObstacleOffset = Vector3.Distance(obstacleHitGameObject.transform.position, camera.transform.position);
                                firstPressed = false;
                            }
                            moveObstacle();
                            locked = true;
                        }
                        break;
                    }
                } else {
                    unDisplayRay();
                    moveObstacle();
                }
            }
        }
    }

    void unDisplayRay(){
        laserLineRenderer.enabled = false;
    }

    void addObstacle(){
        Vector3 cameraPos = camera.transform.position;
        Vector3 cameraDir = camera.transform.forward;
        Vector3 newPos = new Vector3(cameraPos.x + (cameraDir.x * newObstacleOffset), 0, cameraPos.z + (cameraDir.z * newObstacleOffset));
        
        int randNum = Random.Range(0, buildingModels.Length);
        
        buildings.Add(new Building(Instantiate(buildingModels[randNum], newPos, buildingModels[randNum].transform.rotation), buildingModelSizes[randNum]));
        newObstacle = buildings[buildings.Count-1].gameObject;
    }

    void drawNodeLines() {
        List<int> keys = new List<int>(nodes.Keys.ToArray());
        foreach(int key in keys){
            Node node = nodes[key];
            for (int i = 0; i < node.neighborsID.Count; i++)
            {
                Vector3 p1Pos = node.position;
                Vector3 p2Pos = nodes[node.neighborsID[i]].position;
                Debug.DrawLine(p1Pos, p2Pos, Color.red);    
            }
        }
    }

    void createRandomBuildings() {
        buildings = new List<Building>();
        for(int i=0; i<buildingMaxAmount; i++){
            int randNum = Random.Range(0, buildingModels.Length);
            Vector3 randPos;
            while(true){
                randPos = randomPos(minX + 10.0f, maxX - 10.0f, minZ + 10.0f, maxZ - 10.0f);
                Collider[] hitColliders = Physics.OverlapSphere(randPos, 10);
                if(hitColliders.Length == 1){
                    if(hitColliders[0].gameObject == this.gameObject){
                        break;
                    }
                }
            }            
            buildings.Add(new Building(Instantiate(buildingModels[randNum], randPos, buildingModels[randNum].transform.rotation), buildingModelSizes[randNum]));
            // Debug.Log(buildingModelSizes[randNum]);
        }
    }

    void loadNPCs() {
        npcModels = Resources.LoadAll<GameObject>("Models/Characters/NPCs");
        // npcModels = Resources.LoadAll<GameObject>("Models/Characters/NPCs/Robot4");
    }

    void createNPCs() {
        npcs = new List<GameObject>();
        for(int i=0; i<npcMaxAmount; i++){
            int randNum = Random.Range(0, npcModels.Length);
            Vector3 randPos;
            while(true){
                randPos = randomPos(minX + 5.0f, maxX - 5.0f, minZ + 5.0f, maxZ - 5.0f);
                Collider[] hitColliders = Physics.OverlapSphere(randPos, 5);
                if(hitColliders.Length == 1){
                    if(hitColliders[0].gameObject == this.gameObject){
                        break;
                    }
                }
            }            
            npcs.Add(Instantiate(npcModels[randNum], randPos, npcModels[randNum].transform.rotation));
            
            // attach script
            npcs[i].AddComponent<NPC>();
        }
    }

    Vector3 randomPos(float minx, float maxX, float minZ, float maxZ){
        Vector3 randPos = new Vector3( Random.Range(minx, maxX), 0, Random.Range(minZ, maxZ));
        return randPos;
    } 

    void createRandomNodes() {
        for (int i = 0; i < nodeMaxAmount; i++){
            Vector3 randPos;
            while(true){
                randPos = randomPos(minX, maxX, minZ, maxZ);
                bool insideAnyObject = pointInObjectList(randPos);
                if(!insideAnyObject){
                    break;
                }
            }
            nodes[i] = new Node(i, randPos);
        }
    }

    bool pointInObjectList(Vector3 pos) {
        Collider[] hitColliders = Physics.OverlapSphere(pos, 3);
        if(hitColliders.Length == 1){
            if(hitColliders[0].gameObject == this.gameObject){
                return false;
            }
        }
        return true;
    }


}
