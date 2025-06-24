using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScripts : MonoBehaviour
{   
    public Transform bullet;
    public GameObject Enemytar;

    private float firerate = 5;
    private float currentcooldown = 5;
    private Time now;
    
    // Start is called before the first frame update
    void Start()
    {
        fire();
    }

    // Update is called once per frame
    private void Update()
    {
        //if(currentcooldown > 0) currentcooldown -= Time.deltaTime;
        fire();
    }
    private void fire()
    {
        Vector3 destination = Enemytar.transform.position;

        float distance = Vector3.Distance(bullet.transform.position, destination);
        Vector3 newPos = Vector3.MoveTowards(bullet.transform.position, destination, 2 * Time.deltaTime);
        bullet.transform.position = newPos;
    }
}
