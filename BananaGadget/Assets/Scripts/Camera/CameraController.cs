using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Camera mainCam;
    GameObject Player;

    //Offset that will be applied to camera upon movement
    Quaternion Rotoffset = Quaternion.Euler(-5f, 0, 0);
    Vector3 Posoffset = new Vector3(0, 3, -6);
    int rotX = 11;
    int rotY = 0;
    int rotZ = 0;

    //Set to false to prevent camera from following the player
    static bool FollowPlayer;

    //Syncs camera with desired position
    static bool SyncNow;

    //Time camera will wait before resetting to default position
    float ResetTime = 4f;

    //Used to animate to
    GameObject shadowcam;

    GameObject shadowplayer;

    //Used by smoothdamp
    Vector3 Velocity = Vector3.zero;

    //Adjusts camera zoom smoothing
    float smoothTime = 2f;

    //Updated with requested position/rotation from external scripts
    static Vector3 shadowPos;
    static Quaternion shadowRot;

    //Sets minimum distance from target for camera automatic movement
    float mintargetdistance = 5f;

    //Base speed for the auto zoom
    float autozoomspeed = 10f;

    //Set to true if we're animating the camera
    static bool Anim;

    //True when we are in the process of moving towards a target
    static bool movingtotarget;

    float lastMouse;

    //Array of queued RayCast events
    static List<RayEvents> RayQueue = new List<RayEvents>();

    //Mouse sensitivity
    float MouseSens = 2f;

    Coroutine currentCoroutine = null;


    float rotYAxis;
    float rotXAxis;

    Vector3 initialVector;
    float dist;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        FollowPlayer = true;

        //Find our camera
        mainCam = FindObjectOfType<Camera>();

        //Find our player in the scene
        Player = GameObject.FindGameObjectWithTag("Player");


        //Initialise
        shadowcam = new GameObject();
        shadowplayer = new GameObject();

        initialVector = Player.transform.position - Vector3.up;

        rotYAxis = transform.eulerAngles.y;
        rotXAxis = transform.eulerAngles.x;

        dist = Vector3.Magnitude(initialVector);


       
        currentCoroutine = StartCoroutine(CamReset());

        //TESTING
        /*
        Vector3 Test = new Vector3(10, 20, 40);
        Quaternion TestRot = Quaternion.Euler(35, 50, 30);

        MoveCam(Test, TestRot, true);
        */

    }

    public void MoveToTarget(GameObject target)
    {
        //Only move if we are further away than min distance

        if (Vector3.Distance(mainCam.transform.position, target.transform.position) > mintargetdistance)
        {
            mainCam.transform.position = Vector3.SmoothDamp(mainCam.transform.position, target.transform.position, ref Velocity, smoothTime, autozoomspeed, Time.deltaTime);
            mainCam.transform.rotation = Quaternion.Slerp(mainCam.transform.rotation, target.transform.rotation, Time.deltaTime);
        }

        else
        {
            movingtotarget = false;

        }
    }

    //Will move camera to certain position
    static public void MoveCam(Vector3 pos, Quaternion rot, bool Animate)
    {
        FollowPlayer = false;
        //Update these static values with the requested ones
        shadowPos = pos;
        shadowRot = rot;
        Anim = Animate;
        SyncNow = true;

    }

    static public void BackToPlayer(bool Animate)
    {
        movingtotarget = false;
        FollowPlayer = true;

    }


    static public void AddRayEvent(string tag, System.Action function)
    {
        RayEvents currentEvent = new RayEvents(tag, function);
        RayQueue.Add(currentEvent);
    }

    //If player doesn't touch mouse for certain amount of time, reset the camera to default position
    IEnumerator CamReset()
    {
        yield return new WaitForSeconds(ResetTime);
        Quaternion baseRot = Player.transform.rotation;
        Quaternion modRot = Quaternion.Euler(baseRot.x + rotX, baseRot.y + rotY, baseRot.z + rotZ);

        //mainCam.transform.rotation = modRot;


        shadowcam.transform.position = mainCam.transform.position;
        shadowcam.transform.rotation = modRot;
        Anim = true;
        SyncNow = true;


    }

    // Update is called once per frame
    void Update()
    {
        if (movingtotarget)
        {
            MoveToTarget(shadowcam);
        }

        //Sync the position and rotation with the requested data from external script
        if (SyncNow)
        {
            shadowcam.transform.position = shadowPos;
            shadowcam.transform.rotation = shadowRot;
            if (Anim)
            {
                movingtotarget = true;

            }
            else
            {
                mainCam.transform.position = shadowPos;
                mainCam.transform.rotation = shadowRot;

            }
            Anim = false;
            SyncNow = false;

        }

        else if (FollowPlayer)
        {
         


            //Mouse events
            rotYAxis += Input.GetAxis("Mouse X") * MouseSens;
            rotXAxis -= Input.GetAxis("Mouse Y") * MouseSens;

            //Clamp angle

            //Up and down
            rotXAxis = ClampAngle(rotXAxis, -60f, 60f);

            //Side to side
            rotYAxis = ClampAngle(rotYAxis, -60f, 60f);

            Quaternion toRotation = Quaternion.Euler(rotXAxis, rotYAxis, 0);
            Quaternion rotation = toRotation;

            Vector3 negDistance = new Vector3(0, 0, -dist);
            Vector3 position = rotation * negDistance + Vector3.zero;

            transform.rotation = rotation;
            transform.position = position;
             


            transform.position = Player.transform.position;
            transform.position += Posoffset;



            transform.rotation *= Rotoffset;
            //transform.position += Posoffset;


            //If mouse has moved since last update
            if (lastMouse != rotYAxis)
            {
                movingtotarget = false;
                StopCoroutine(currentCoroutine);
                currentCoroutine = StartCoroutine(CamReset());
            }

            lastMouse = rotYAxis;
        }

        //If we left click
        if (Input.GetMouseButtonDown(0))
        {
            //Casts a ray
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                for (int i = 0; i < RayQueue.Count; i++)
                {
                    if (hit.transform.tag == RayQueue[i].Tag)
                    {
                        RayQueue[i].FunctionToRun();
                    }
                }
            }

        }
    }

    //clamp an angle
    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}
