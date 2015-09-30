using UnityEngine;
using System.Collections;

public class DestroyByTime : MonoBehaviour
{
    // time before object is destroyed
    public float lifetime;

	// Use this for initialization
	void Start () {
	    Destroy(gameObject, lifetime);

	}
	
}
