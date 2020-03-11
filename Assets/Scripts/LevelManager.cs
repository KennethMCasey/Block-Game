using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour { 

    bool didExplode = false;

    //dictonary that represents the space on the board and returns the block ID occupying the space, -1 is empty 0 is player any other positive number is an obstruction block
    private Dictionary<int, int> spaceData = new Dictionary<int,int>();

    //the z value that is held constant
    public float zValue;

    //these are the surrounding walls, these are used to restrict movement
    public GameObject leftWall;
    public GameObject upperWall;
    public GameObject bottomWall;
    public GameObject rightUpperWall;
    public GameObject rightBottomWall;

    public GameObject Obstruction1;
    public GameObject Obstruction2;
    public GameObject Obstruction3;
    public GameObject Obstruction4;
    //public GameObject Obstruction5;

    //public Rigidbody _rigidbodyLW;
    //public Rigidbody _rigidbodyUW;
    //public Rigidbody _rigidbodyBW;
    //public Rigidbody _rididbodyRUW;
    //public Rigidbody _rididbodyRBW;

    //the scale size of your blocks. scale/2 should be equal to one position.
    //this would be best if cale is 20 so each position is 10, should work with other values but never tested...
    public float scale = 20;

    //pass the camera in, the camera will move to a different location when paused
    public Camera mainCamera;
    private Vector3 defaultCameraPosition;

    //in game data
    public float time = 30f;
    private bool didWin = false;
    private bool didLose = false;
    private int moveCount = 0;
    private bool isPaused = false;


    // Start is called before the first frame update

    void Start()
    {
        
        defaultCameraPosition = mainCamera.transform.position;
        }

    // Update is called once per frame
    void Update()
    {
        if (time < 5 && !didExplode){
            explodeOnWin();
            didExplode = true;
        }
        time = time - Time.deltaTime;
        Time.timeScale = (isPaused || didLose || didWin) ? 0 : 1;
        mainCamera.transform.position = (isPaused) ? defaultCameraPosition + new Vector3(500, 500, 500) : defaultCameraPosition;
        didLose = (time <= 0);
    }

    public  int GetSpaceData(int space) {
        return (spaceData.ContainsKey(space) ) ?  spaceData[space] : -1;
    }

    public void SetSpaceData(int spaceOne, int spaceTwo, int blockID) {
        if (spaceData.ContainsKey(spaceOne)) spaceData.Remove(spaceOne);
        spaceData.Add(spaceOne, blockID);
        if (spaceData.ContainsKey(spaceTwo)) spaceData.Remove(spaceTwo);
        spaceData.Add(spaceTwo, blockID);
    }

    public void DeleteSpaceData(int spaceOne, int spaceTwo, int blockID) {
        if (spaceData.ContainsKey(spaceOne)) if (spaceData[spaceOne] == blockID) spaceData.Remove(spaceOne);
        if (spaceData.ContainsKey(spaceTwo)) if (spaceData[spaceTwo] == blockID) spaceData.Remove(spaceTwo);
    }

    public float getBoundsFor(string wall) {
        return (wall == "L") ? leftWall.transform.position.x : (wall == "U") ? upperWall.transform.position.y : (wall == "B") ? bottomWall.transform.position.y : (wall == "Ru") ? rightUpperWall.transform.position.x : (wall == "Rb") ? rightBottomWall.transform.position.x : -1;
    }

    public bool getDidWin() { return didWin; }
    public void setDidWin(bool tf) { didWin = tf;}

    public bool getDidLose() { return didLose; }
    public void setDidLose(bool tf) { didLose = tf; }

    public int getMoveCount() { return moveCount; }
    public void incMoveCount() { moveCount++; }

    public bool getIsPaused() { return isPaused; }
    public void setIsPaused(bool tf) { isPaused = tf; }

    public void explodeOnWin(){
        //    _rigidbodyLW = leftWall.GetComponent(typeof(Rigidbody)) as Rigidbody;
        //    _rigidbodyUW = upperWall.GetComponent(typeof(Rigidbody)) as Rigidbody;
        //    _rigidbodyBW = bottomWall.GetComponent(typeof(Rigidbody)) as Rigidbody;
        //    _rididbodyRUW = rightUpperWall.GetComponent(typeof(Rigidbody)) as Rigidbody;
        //    _rididbodyRBW = rightBottomWall.GetComponent(typeof(Rigidbody)) as Rigidbody;

        //    _rigidbodyLW.constraints = RigidbodyConstraints.None;
        //    _rigidbodyUW.constraints = RigidbodyConstraints.None;
        //    _rigidbodyBW.constraints = RigidbodyConstraints.None;
        //    _rididbodyRUW.constraints = RigidbodyConstraints.None;
        //    _rididbodyRBW.constraints = RigidbodyConstraints.None;
        Rigidbody gameObjectRidgeBody1 = leftWall.AddComponent<Rigidbody>();
        gameObjectRidgeBody1.constraints = RigidbodyConstraints.None;
        //gameObjectRidgeBody1.useGravity = false;

        Rigidbody gameObjectRidgeBody2 = upperWall.AddComponent<Rigidbody>();
        gameObjectRidgeBody2.constraints = RigidbodyConstraints.None;
        //gameObjectRidgeBody2.useGravity = false;

        Rigidbody gameObjectRidgeBody3 = bottomWall.AddComponent<Rigidbody>();
        gameObjectRidgeBody3.constraints = RigidbodyConstraints.None;
        //gameObjectRidgeBody3.useGravity = false;

        Rigidbody gameObjectRidgeBody4 = rightUpperWall.AddComponent<Rigidbody>();
        gameObjectRidgeBody4.constraints = RigidbodyConstraints.None;
        //gameObjectRidgeBody4.useGravity = false;

        Rigidbody gameObjectRidgeBody5 = rightBottomWall.AddComponent<Rigidbody>();
        gameObjectRidgeBody5.constraints = RigidbodyConstraints.None;
        //gameObjectRidgeBody5.useGravity = false;

        Rigidbody gameObjectRidgeBody6 = Obstruction1.AddComponent<Rigidbody>();
        gameObjectRidgeBody6.constraints = RigidbodyConstraints.None;
        //gameObjectRidgeBody6.useGravity = false;

        Rigidbody gameObjectRidgeBody7 = Obstruction2.AddComponent<Rigidbody>();
        gameObjectRidgeBody7.constraints = RigidbodyConstraints.None;
        //gameObjectRidgeBody7.useGravity = false;

        Rigidbody gameObjectRidgeBody8 = Obstruction3.AddComponent<Rigidbody>();
        gameObjectRidgeBody8.constraints = RigidbodyConstraints.None;
        //gameObjectRidgeBody8.useGravity = false;

        Rigidbody gameObjectRidgeBody9 = Obstruction4.AddComponent<Rigidbody>();
        gameObjectRidgeBody9.constraints = RigidbodyConstraints.None;
        //gameObjectRidgeBody9.useGravity = false;

        //Rigidbody gameObjectRidgeBody10 = Obstruction5.AddComponent<Rigidbody>();
        //gameObjectRidgeBody5.constraints = RigidbodyConstraints.None;
    }
}
