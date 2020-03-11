using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePositionMouse : MonoBehaviour
{
    //grabs level manager on Game Manager object
    private LevelManager levelManager;

    //ID of this block
    //This must be a unique number >= 0
    // 0 is reserved for the player block
    public int blockID;

    //the four positions that this block can occupy
    //Positions must be names "Pos #" where # represents a unique number > 0
    //array is organized such that [0].xy < [1].xy < [2].xy < [3].xy otherwise lrestrictions will be broken
    public GameObject[] permitedPosition = new GameObject[4];
    private float middlePos; //derived from permitedPosition[1] & permitedPosition[2]

    //determins which direction the block will move
    public bool isXconstant;
    
    //used for mouse movement
    private float mZCoord;
    Vector3 mOffset;

    //calculated as needed in script
    private float lowerRestriction;
    private float upperRestriction;

    //used to see if position has changed
    Vector3 oldPos;

    // Start is called before the first frame update
    void Start()
    {
        //calc middle position
        middlePos = (isXconstant) ? ((permitedPosition[1].transform.position + permitedPosition[2].transform.position) / 2).y : ((permitedPosition[1].transform.position + permitedPosition[2].transform.position) / 2).x;

        //get level manager on object Game Manager game object which has LevelManager
        levelManager = GameObject.Find("Game Manager").GetComponent<LevelManager>();


        //sets starting space data
        if ( (isXconstant) ? transform.position.y == middlePos + (levelManager.scale / 2) : transform.position.x == middlePos + (levelManager.scale / 2))
        {
            levelManager.SetSpaceData(GetID(permitedPosition[0]), GetID(permitedPosition[1]), blockID);
        }

        else if ( (isXconstant) ? transform.position.y == middlePos : transform.position.x == middlePos)
        {
            levelManager.SetSpaceData(GetID(permitedPosition[1]), GetID(permitedPosition[2]), blockID);
        }

       else if ( (isXconstant) ?  transform.position.y == middlePos - (levelManager.scale / 2) : transform.position.x == middlePos - (levelManager.scale / 2))
        {
            levelManager.SetSpaceData(GetID(permitedPosition[2]), GetID(permitedPosition[3]), blockID);
        }

        //note old position
        oldPos = transform.position;

    }



    // Update is called once per frame
    void Update()
    {

    }

    void OnMouseDown()
    {
        //for mouse movement
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mOffset = gameObject.transform.position - GetMouseAsWorldPoint();

        //get restrictions
        //note: upper restriction for player is different so game can be won
        upperRestriction = (levelManager.GetSpaceData(GetID(permitedPosition[1]))
            != -1 && levelManager.GetSpaceData(GetID(permitedPosition[1])) != blockID) ?
            middlePos - (levelManager.scale / 2) - 5 :
            (levelManager.GetSpaceData(GetID(permitedPosition[0])) != -1 && levelManager.GetSpaceData(GetID(permitedPosition[0])) != blockID) ?

            middlePos + (levelManager.scale / 2) - 5.5f : (isXconstant) ? levelManager.getBoundsFor("U") - levelManager.scale / 2 : (blockID == 0) ?
            // need to change "Ru" to another wall 
            levelManager.getBoundsFor("Ru") + 10f : levelManager.getBoundsFor("Ru") - levelManager.scale / 2;
            //levelManager.getBoundsFor("L") : levelManager.getBoundsFor("L") - levelManager.scale / 2;


        lowerRestriction = (levelManager.GetSpaceData(GetID(permitedPosition[2])) != -1 && levelManager.GetSpaceData(GetID(permitedPosition[2])) 
            != blockID) ? middlePos + (levelManager.scale/2) - 5 : (levelManager.GetSpaceData(GetID(permitedPosition[3])) != -1 && levelManager.GetSpaceData(GetID(permitedPosition[3]))
            != blockID) ? middlePos -4.5f : (isXconstant) ? levelManager.getBoundsFor("B")+levelManager.scale/2 : levelManager.getBoundsFor("L") + levelManager.scale / 2;

        //note old position
        oldPos = transform.position;
    }

    public void OnMouseDrag()
    {
        //sets transform.position to mouse location
        Vector3 temp = GetMouseAsWorldPoint() + mOffset;
        if (isXconstant) {
            if (temp.y > upperRestriction) temp.y = upperRestriction;
            if (temp.y < lowerRestriction) temp.y = lowerRestriction;
            temp.x = transform.position.x; }
        if (!isXconstant) {
            if (temp.x > upperRestriction) temp.x = upperRestriction;
            if (temp.x < lowerRestriction) temp.x = lowerRestriction;
            temp.y = transform.position.y; }
        transform.position = temp;

       
        //hard coded, player will always start on left side and try to go right
        if (blockID == 0)
        {
            levelManager.setDidWin(transform.position.x >= levelManager.getBoundsFor("Ru") + 5f);
        }
    }

    private void OnMouseUp()
    {
        Vector3 temp = transform.position;
            if ( isXconstant ?  temp.y >= middlePos+(levelManager.scale/2)-5 : temp.x >= middlePos + (levelManager.scale / 2) - 5)
            {
                transform.localPosition = new Vector3((isXconstant) ? transform.position.x : middlePos + (levelManager.scale / 2), (!isXconstant) ? transform.position.y : middlePos + (levelManager.scale/2), levelManager.zValue);
                levelManager.SetSpaceData(GetID(permitedPosition[0]), GetID(permitedPosition[1]), blockID);
                levelManager.DeleteSpaceData(GetID(permitedPosition[2]), GetID(permitedPosition[3]), blockID);
            }
            else if (isXconstant ? temp.y < middlePos + (levelManager.scale / 2) - 5 && temp.y > middlePos  - 5 : temp.x < middlePos + (levelManager.scale / 2) - 5 && temp.x > middlePos - 5)
            {
                transform.localPosition = new Vector3((isXconstant) ? transform.position.x : middlePos, (!isXconstant) ?transform.position.y : middlePos, levelManager.zValue);
                levelManager.SetSpaceData(GetID(permitedPosition[1]), GetID(permitedPosition[2]), blockID);
                levelManager.DeleteSpaceData(GetID(permitedPosition[0]), GetID(permitedPosition[3]), blockID);

            }
            else if ( isXconstant ? temp.y <= middlePos - 5 : temp.x <= middlePos - 5)
            {
                transform.localPosition = new Vector3((isXconstant) ? transform.position.x : middlePos - (levelManager.scale / 2), (!isXconstant) ? transform.position.y : middlePos - (levelManager.scale/2), levelManager.zValue);
                levelManager.SetSpaceData(GetID(permitedPosition[2]), GetID(permitedPosition[3]), blockID);
                levelManager.DeleteSpaceData(GetID(permitedPosition[0]), GetID(permitedPosition[1]), blockID);

            }

            //increment move count
        if (transform.position != oldPos) levelManager.incMoveCount();
    }

    //for mouse movement
    private Vector3 GetMouseAsWorldPoint()

    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mZCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    //gets the id of position, requires that position objects are named correctly
    private int GetID(GameObject pos) {
        string temp = "";

        for (int i = 3; i < pos.name.Length; i++) temp += pos.name[i];

        int theInt = int.Parse(temp);

        return theInt;
    }

    
}
