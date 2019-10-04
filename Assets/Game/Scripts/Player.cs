using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public bool canTripleShot = false;
    public bool canSpeed = false;
    public bool isShieldOn = false;
    public int lives = 3;

    [SerializeField]
    private float _speed = 5.0f;
    [SerializeField]
    private float _speedBoost = 1.5f;
    [SerializeField]
    private float _fireRate = 0.3f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _TripleShotPrefab;
    private float _canFire = 0.0f;
    [SerializeField]
    private GameObject _playerDestroyAnimPrefab;
    [SerializeField]
    private GameObject _ShieldGameObj;

    
    
	void Start ()
    {
        transform.position = new Vector3(0, 0, 0);
	}
	
	
	void Update ()
    {
        Movement();

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }
    

    private void DestroyPlayer()
    {
        Destroy(this.gameObject);
    }

    private void Shoot()
    {
        if (Time.time > _canFire)
        {
            if (canTripleShot)
            {
                Instantiate(_TripleShotPrefab, transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.65f, 0), Quaternion.identity);
            }
            _canFire = Time.time + _fireRate;
        }
    }

    private void Movement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        
        if (canSpeed)
        {
            transform.Translate(Vector3.right * Time.deltaTime * _speed * horizontalInput * _speedBoost);
            transform.Translate(Vector3.up * _speed * verticalInput * Time.deltaTime * _speedBoost);
        }
        else
        {
            transform.Translate(Vector3.right * Time.deltaTime * _speed * horizontalInput);
            transform.Translate(Vector3.up * _speed * verticalInput * Time.deltaTime);
        }
        
        
        if (transform.position.y > 0)
        {
            transform.position = new Vector3(transform.position.x, 0, 0);
        }
        else if (transform.position.y < -4.2f)
        {
            transform.position = new Vector3(transform.position.x, -4.2f, 0);
        }

        if (transform.position.x > 9.5f)
        {
            transform.position = new Vector3(-9.5f, transform.position.y, 0);
        }
        else if (transform.position.x < -9.5f)
        {
            transform.position = new Vector3(9.5f, transform.position.y, 0);
        }
    }

    public void Dmg()
    {
        if (isShieldOn)
        {
            isShieldOn = false;
            _ShieldGameObj.SetActive(false);
        }
        else
        {
            lives--;
        }
        if (lives < 1)
        {
            Instantiate(_playerDestroyAnimPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    public void TripleShotOn()
    {
        canTripleShot = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    public void SpeedOn()
    {
        canSpeed = true;
        StartCoroutine(SpeedPowerDown());
    }

    public void ShieldOn()
    {
        isShieldOn = true;
        _ShieldGameObj.SetActive(true);
    }
    
    public IEnumerator SpeedPowerDown()
    {
        yield return new WaitForSeconds(5.0f);

        canSpeed = false;
    }

    public IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);

        canTripleShot = false;
    }

    
}
