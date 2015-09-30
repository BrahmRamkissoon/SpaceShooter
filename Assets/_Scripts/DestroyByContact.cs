using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour
{
    // reference to explosion visual effect
    public GameObject explosion;
    public GameObject playerExplosion;
    public int scoreValue;

    // reference to instance of the gamecontroller
    private GameController gameController;

    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot find 'GameController' script");

        }
        
    }
    void OnTriggerEnter(Collider other)
    {
        // ignore collision with boundary
        if (other.tag == "Boundary")
        {
            return;
        }
        // instantiate explosion at exact pos, rotation of asteriod
        Instantiate(explosion, transform.position, transform.rotation);

        // check for player contact
        if (other.tag == "Player")
        {
            Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            gameController.GameOver();
        }
        gameController.AddScore(scoreValue);

        Destroy(other.gameObject);
        Destroy(gameObject);
        
    }
}
