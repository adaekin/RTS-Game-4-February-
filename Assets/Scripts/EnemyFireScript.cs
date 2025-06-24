using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFireScript : MonoBehaviour
{
    public Transform bullet;
    public GameObject Enemytar;
    public int bulletspeed;
    public int Damage;
    private Time now;
    
    // Start is called before the first frame update

    // Update is called once per frame
    private void Update()
    {
        //if(currentcooldown > 0) currentcooldown -= Time.deltaTime;
        if(Enemytar == null)
        {
            Destroy(transform.gameObject);
        }
        fire();
    }
    private void fire()
    {
        Vector3 destination = Enemytar.transform.position;

        transform.LookAt(Enemytar.transform, Vector3.forward);
        float distance = Vector3.Distance(transform.position, destination);
        Vector3 newPos = Vector3.MoveTowards(transform.position, destination, bulletspeed * Time.deltaTime);
        transform.position = newPos;
        if(distance <= 1)
        {
            Destroy(transform.gameObject);
            if(Enemytar != null)
            {
                if(Enemytar.GetComponent<TroopMenu>() != null)
                {
                    Enemytar.GetComponent<TroopMenu>().Health = Enemytar.GetComponent<TroopMenu>().Health - Damage;

                }
                else if (Enemytar.GetComponent<BuildingMenu>() != null)
                {
                    Enemytar.GetComponent<BuildingMenu>().Health = Enemytar.GetComponent<BuildingMenu>().Health - Damage;

                }
            }
            
        }
    }
}
