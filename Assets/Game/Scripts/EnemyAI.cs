using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]
    private float _speed = 2.0f;
    [SerializeField]
    private GameObject destroyEnemyAnim;
    
    
    
    // Use this for initialization
    void Start ()
    {
        transform.position = new Vector3(0, 6.5f, 0);
	}
	
	// Update is called once per frame
	void Update ()
    {
        Move();
        
	}
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.Dmg();
            }
            DestroyEnemy();
        }
        else if (other.tag == "Laser")
        {
            Laser laser = other.GetComponent<Laser>();
            if (laser != null)
            {
                laser.DestroyLaser();
            }
            DestroyEnemy(); 
        }
    }

    private void Move()
    {
        transform.Translate(Vector3.down * Time.deltaTime * _speed);
        if (transform.position.y < -6.5f)
        {
            transform.position = (new Vector3(Random.Range(-7.7f, 7.7f), 6.5f, 0));
        }
    }
    private void DestroyEnemy()
    {
        Instantiate(destroyEnemyAnim, this.transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
