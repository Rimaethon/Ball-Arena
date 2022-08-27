using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    public float speed=5.0f;
    private GameObject focalPoint;
    public GameObject powerupIndicator;
    public bool hasPowerUp;
    private float powerupStrength=15.0f;
    public bool spaceMisuse;
    void Start()
    {
        playerRb=GetComponent<Rigidbody>();
        focalPoint=GameObject.Find("Focal Point");

    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput=Input.GetAxis("Vertical");

        if(transform.position.y<-2)
        {
            MainMenu.instance.LoseScreen();
            Destroy(gameObject);
        }
        if(Input.GetKeyDown(KeyCode.Space) && !spaceMisuse){
            StartCoroutine(StopPlayer());
            spaceMisuse=true;
        }
        playerRb.AddForce(focalPoint.transform.forward * (speed * forwardInput));
        powerupIndicator.transform.position=transform.position+ new Vector3(0,-0.5f,0);
        powerupIndicator.transform.rotation = Quaternion.Euler(0,0,0);
    }
    IEnumerator StopPlayer()
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
        int count=7;
        MainMenu.instance.powerUpTime.gameObject.SetActive(true);
        while(count>0){
            MainMenu.instance.powerUpTime.text="Remaining Power Up Time: "+count.ToString();
            yield return new WaitForSeconds(1);
            count--;
        }
        hasPowerUp=false;
        MainMenu.instance.powerUpTime.gameObject.SetActive(false);
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
