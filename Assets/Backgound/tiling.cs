using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(SpriteRenderer))] //ensure spriterenderer is attatched to prevent errors 


public class tiling : MonoBehaviour
{
    public int offsetX = 2; //to prevent errors
    public bool hasRightBuddy = false;// checking need to extend mapp 
    public bool hasLeftBuddy = false;

    public bool reverseScale = false; //used if object not tilable 
    private float spriteWidth = 0f; //width of our element 

    private Camera cam;
    private Transform myTransform;

    private void Awake() //use for referencing 
    {
        cam = Camera.main;
        myTransform = transform; 
 
    }

    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer sRenderer = GetComponent<SpriteRenderer>();
        spriteWidth = sRenderer.sprite.bounds.size.x; // width of element no matter the size
        
    }

    // Update is called once per frame
    void Update()
    {
        //does it need buddy? if not do nothing
        if (hasLeftBuddy == false || hasRightBuddy == false) {

            //calc camera's extent = half of width that the camera can see in world coordinates 
            float camHorizontalExtend = cam.orthographicSize * Screen.width / Screen.height;

            //calc x pos where camera can see edge of sprite
            float edgeVisiblePosRight = (myTransform.position.x + spriteWidth / 2) - camHorizontalExtend;

            float edgeVisiblePosLeft = (myTransform.position.x - spriteWidth / 2) + camHorizontalExtend;

            //checking if right entent of camera is >= to the edge of map or if already extended ie edge is visible
            if(cam.transform.position.x >= edgeVisiblePosRight - offsetX && hasRightBuddy == false)
            {
                makeNewBuddy(1);
                hasRightBuddy = true;

            }else if(cam.transform.position.x <= edgeVisiblePosLeft + offsetX && hasLeftBuddy == false)
            {
                makeNewBuddy(-1);
                hasLeftBuddy = true; 
            }



        
        }

        //func to creat buddy on side required
        void makeNewBuddy(int RightOrLeft)
        {
            //calc new position for new buddy 
            Vector3 newPos = new Vector3 
                (myTransform.position.x + spriteWidth * RightOrLeft, myTransform.position.y, myTransform.position.z);

            //instantiate as a transform as place in buddy var
            Transform newBuddy = Instantiate(myTransform, newPos, myTransform.rotation) as Transform; 

            //if not tilable, reverse the x size of the object to get rid of ugly seams 
            if(reverseScale == true)
            {
                //inverts the size of background to perfectly loop 
                newBuddy.localScale = new Vector3(newBuddy.localScale.x * -1, newBuddy.localScale.y, newBuddy.localScale.z);
            }

            newBuddy.parent = myTransform.parent; 

            if( RightOrLeft > 0)
            {
                newBuddy.GetComponent<tiling>().hasLeftBuddy = true;
            }
            else
            {
                newBuddy.GetComponent<tiling>().hasRightBuddy = true;
            }
        }

    }
}
