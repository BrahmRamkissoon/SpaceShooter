using UnityEngine;
using System.Collections;

// add a boundary to prevent player from going offscreen
[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{
    public float speed;         // control speed of player object
    public Boundary boundary;   // instantiate boundary class
    public float tilt;          // control tilt of player ship

    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;

    private float nextFire;

    private void Update()
    {
        // get input from a button the player clicks to fire bolt
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            // instantiate a shot
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            GetComponent<AudioSource>().Play();
        }
    }

    // Update is called once per frame
	void FixedUpdate () {
        // get input from player
	    float moveHorizontal = Input.GetAxis("Horizontal");
	    float moveVertical = Input.GetAxis("Vertical");

	    Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        GetComponent<Rigidbody>().velocity = movement * speed;
        
        // constrain object movement
	    GetComponent<Rigidbody>().position = new Vector3
            (Mathf.Clamp (GetComponent<Rigidbody>().position.x, boundary.xMin, boundary.xMax),  
            0.0f,
            Mathf.Clamp(GetComponent<Rigidbody>().position.z, boundary.zMin, boundary.zMax)
            );

        // add tilt to object as it moves along z axis
	    GetComponent<Rigidbody>().rotation = Quaternion.Euler(0.0f, 0.0f, GetComponent<Rigidbody>().velocity.x * -tilt);



	}
}
