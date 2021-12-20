using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallax : MonoBehaviour
{
    public Transform[] backgrounds; // list of all bg/fg to be parallaxed 
    private float[] parallaxScales; //porportion of the cameras movement to move the backgrounds by 
    public float smoothing = 1f; //parallaxing amount

    private Transform cam; //reference to the main camera's transform 
    private Vector3 prevCamPosition; //position of camera in previous frame

    //called before start but after gameobjects are setup
    //great for references 
    void Awake()
    {
        //setup camera reference
        cam = Camera.main.transform; 
         
    }


    // Start is called before the first frame update
    void Start()
    {
        //the previous frame had the current frame's cam position
        prevCamPosition = cam.position;

        //Assigning parallax scales
        parallaxScales = new float[backgrounds.Length]; 
        for (int i = 0; i < backgrounds.Length; i++)
        {
            parallaxScales[i] = backgrounds[i].position.z * -1; 
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        //foreach Bg 
        for(int i = 0; i< backgrounds.Length; i++)
        {
            //parallax is the opposite of the cam movement becuz the previous frame multiplied by the scale
            float parallax = (prevCamPosition.x - cam.position.x) * parallaxScales[i];

            //set a target x, which is the current pos + the parallax 
            float BgTargetPositionX = backgrounds[i].position.x + parallax;

            //create a target pos which is the bg current pos with its target x pos 
            Vector3 BgtargetPos = new Vector3(BgTargetPositionX, backgrounds[i].position.y, backgrounds[i].position.z);

            //fade between current pos and target pos using lerp 
            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, BgtargetPos, smoothing * Time.deltaTime); 
        }

        //set the prev cam pos to the camera's pos at the end of the frame

        prevCamPosition = cam.position; 


        
    }
}
