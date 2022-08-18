using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    public float speed=5.0f;
    private GameObject focalPoint;
    public GameObject powerupIndicator;
    public bool hasPowerUp;
    private float powerupStrength=15.0f;
    public bool spaceMisuse;
    // Start is called before the first frame update
    void Start()
    {
        playerRb=GetComponent<Rigidbody>();
        focalPoint=GameObject.Find("Focal Point");

    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput=Input.GetAxis("Vertical");

        
        if(Input.GetKeyDown(KeyCode.Space) && !spaceMisuse){
            StartCoroutine(stopPlayer());
            spaceMisuse=true;
        }
        playerRb.AddForce(focalPoint.transform.forward*speed*forwardInput);
        powerupIndicator.transform.position=transform.position+ new Vector3(0,-0.5f,0);
        powerupIndicator.transform.rotation = Quaternion.Euler(0,0,0);
    }
    IEnumerator stopPlayer()
    {
        playerRb.isKinematic=true;
        yield return new WaitForSeconds(0.1f);
        playerRb.isKinematic=false;
        yield return new WaitForSeconds(3);
        spaceMisuse=false;
    }
    private void OnTriggerEnter(Collider other) {
        
        if(other.CompareTag("Powerup"))
        {
            hasPowerUp=true;
            Destroy(other.gameObject);
            
            powerupIndicator.gameObject.SetActive(true);
            StartCoroutine(PowerupCountdownRoutine());
        }
    }
    IEnumerator PowerupCountdownRoutine(){
        yield return new WaitForSeconds(7);
        hasPowerUp=false;
        powerupIndicator.gameObject.SetActive(false);
    }
    private void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.CompareTag("Enemy") && hasPowerUp)
        {
            Rigidbody enemyRigidbody=collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer=collision.gameObject.transform.position-transform.position;

            Debug.Log("Player collided with:" + collision.gameObject.name + " with powerup set to"+ hasPowerUp);
            enemyRigidbody.AddForce(awayFromPlayer*powerupStrength,ForceMode.Impulse);
        }
        
    }
}
