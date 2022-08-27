using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed=3.0f;
    private Rigidbody enemyRb;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        enemyRb=GetComponent<Rigidbody>();
        player=GameObject.Find("Player");

    }

    // Update is called once per frame
    void Update()
    {
        if(player==null){
            return;
        }
        Vector3 lookDirection= (player.transform.position-transform.position).normalized;
        enemyRb.AddForce(lookDirection*speed);
        if(transform.position.y<-10)
        {
            MainMenu.instance.score++;
            MainMenu.instance.scoreText.text="Score: "+MainMenu.instance.score;
            Destroy(gameObject);
        }
    }
}
