using UnityEngine;
using System.Collections;

public class Shooting : MonoBehaviour
{
    public float fireRate;
    public float Damage = 10;
    public LayerMask notToHit;

    float timeToFire;
    Transform firePoint;
	// Use this for initialization
	void Awake ()
    {
        firePoint = GameObject.FindGameObjectWithTag("FirePoint").transform;

        if (firePoint == null)
        {
            Debug.LogError(("No firepoint"));
        }	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (fireRate == 0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                Shoot();
            }
            
        }
        else
        {
            if (Input.GetButton("Fire1") && Time.time > timeToFire)
            {
                timeToFire = Time.time + 1 / fireRate;
                Shoot();
            }
        }
	}

    void Shoot()
    {
        Vector2 mousePos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        Vector2 firePointPosition = new Vector2(firePoint.position.x, firePoint.position.y);
        RaycastHit2D hit = Physics2D.Raycast(firePointPosition, mousePos - firePointPosition, 100, notToHit);
        Debug.DrawLine(firePointPosition, (mousePos -firePointPosition) * 100, Color.black);
    }
}
