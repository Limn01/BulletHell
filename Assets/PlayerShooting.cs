using UnityEngine;
using System.Collections;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bullet;
    public float bulletSpeed;
    public Transform gunEnd;
    public float timeBetweenShots;
    public float range = 100f;

    bool isShooting;
    Ray shootRay;
    int shootabeMask;
    RaycastHit hit;
    float timer;
    Vector3 playerDirection;
	// Use this for initialization
	void Start ()
    {
        shootabeMask = LayerMask.GetMask("Shootable");
	}
	
	// Update is called once per frame
	void Update ()
    {
        timer += Time.deltaTime;

        if (Input.GetButton("Fire1") && timer > timeBetweenShots)
        {
            Shoot();
        }

        Turning();
	}

    void Shoot()
    {
        timer = 0;

    }

    void Turning()
    {
       
    }
}
