using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {
    [SerializeField]
    private float _speed = 3.0f;
    [SerializeField]
    private int powerUpID; //0 - triple shot, 1 - speed, 2 - shield
    
	
	// Update is called once per frame
	void Update ()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _speed);

        if (transform.position.y < -6)
        {
            transform.position = new Vector3(Random.Range(-8f, 8f), 6);
        }
	}
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                // triple shot
                if (powerUpID == 0)
                {
                    if (!player.canTripleShot)
                    {
                        player.TripleShotOn();
                        Destroy(this.gameObject);
                    }
                }
                //speed
                else if (powerUpID == 1)
                {
                    if (!player.canSpeed)
                    {
                        player.SpeedOn();
                        Destroy(this.gameObject);
                    }
                }
                //shield
                else if (powerUpID == 2)
                {
                    if (!player.isShieldOn)
                    {
                        player.ShieldOn();
                        Destroy(this.gameObject);
                    }
                }
            }

            
        }
    }
}
