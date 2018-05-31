using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class is responsible for moving the player around and interacting with the world
/// </summary>
public class PlayerController : MonoBehaviour {

    /// <summary>
    /// Text object that is used for debugging on built versions of the app
    /// </summary>
    private Text debugLog;
    /// <summary>
    /// Set to true when mouse is down
    /// </summary>
    private bool mouseDown;

    /// <summary>
    /// The player movement speed
    /// </summary>
    private float speed = 0.0f;

    private GameObject player;

    /// <summary>
    /// The force of the Player colliding with another gameobject
    /// </summary>
    public float collisionForce = 100.0f;

    void Start ()
    {
        debugLog = GameObject.Find("DebugText").GetComponent<Text>();
        debugLog.text = "Debugger";
        player = this.gameObject;
	}
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 forward = transform.TransformDirection(Vector3.down) * 10;
        Debug.DrawRay(transform.position, forward, Color.red);
        handleScreenTapped();
        handlePlayerKnockedOff();
    }

    /// <summary>
    /// The functions that controls the player when the screen is touched
    /// </summary>
    private void handleScreenTapped()
    {
        
        if (Input.GetMouseButton(0) && playerAboveStage())
        {
            mouseDown = true;
            Vector3 mousePosition = calculateMousePosition();
            player.transform.position = Vector3.MoveTowards(player.transform.position,new Vector3(mousePosition.x,player.transform.position.y,mousePosition.z),speed);
            rotatePlayer(mousePosition);
            if (speed < 0.3f)
            {
                speed += 0.01f; // accelerate the speed of the player
            }
            print("The player speed is : " + speed);
        }
        else // When the user lets of the mouse we reset the values used for moving the player
        {
            print("Ressetting the speed");
            speed = 0.0f; // Reset the speed when the user untouches the screen
        }
    }
    /// <summary>
    /// Turns the mouse position into usable Vector3 for the player to move towards
    /// </summary>
    /// <returns>Mouse position in world space</returns>
    private Vector3 calculateMousePosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 50.0f))
        {
            return hit.point;
        }
        return Vector3.zero;
    }
    // Turn on gravity when the player gets knocked off the stage;
    private void handlePlayerKnockedOff()
    {
        if (!playerAboveStage())
        {
            this.GetComponent<Rigidbody>().useGravity = true;
            this.GetComponent<Rigidbody>().isKinematic = false;
        }
    }
    /// <summary>
    ///  Returns true if the player is above the stage
    /// </summary>
    private bool playerAboveStage()
    {  
        RaycastHit hit;
        if (Physics.Raycast(player.transform.position,Vector3.down, out hit, 50.0f))
        {
            //draw invisible ray cast/vector
            print(hit.transform.tag);
            if (hit.transform.tag != "Ground")
            {
                print(hit.transform.tag);
                
                return false;
            }

        }
        return true; 
     
    }
    /// <summary>
    /// Rotates player in direction moving
    /// </summary>
    private void rotatePlayer(Vector3 movePostion)
    {
        Vector3 prevRotation = player.transform.localEulerAngles;
        player.transform.rotation = Quaternion.Slerp(
        player.transform.rotation,
        Quaternion.LookRotation(movePostion),
        Time.deltaTime * 10.0f);
        player.transform.localEulerAngles = new Vector3(prevRotation.x, player.transform.localEulerAngles.y, prevRotation.z);
    }

    /// <summary>
    /// The collision enter function is used to add force to the other player
    /// </summary>
    /// <param name="collision"></param>
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            print("Adding the force to the object : " + collision.gameObject.name);
            collision.gameObject.GetComponent<Rigidbody>().AddForce(this.transform.forward * collisionForce);
        }
    }
}
