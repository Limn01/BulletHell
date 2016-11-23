using UnityEngine;
using System.Collections;

public class GunRotation : MonoBehaviour
{
    public int rotationOffset = 180;

    GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    
    void Update()
    {

      
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        difference.Normalize();
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + 0);
        
        //if (mousex > 0)
        //{
        //    transform.rotation = Quaternion.AngleAxis(0, Vector3.up);
        //}
        //
        //if (mousex < 0)
        //{
        //    transform.rotation = Quaternion.AngleAxis(180, Vector3.up);
        //}
        
        
        
        
    }
    
}
