using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EngageScript : MonoBehaviour
{
    public GameObject bullet;
    public GameObject shootparent;


    private int damage;
    private GameObject Enemy;
    public float rate;
    public int bulletspeed;
    private float cooldown;
    private void Start()
    {
        bulletspeed = transform.parent.GetComponent<TroopMenu>().bulletspeed;
        damage = transform.parent.GetComponent<TroopMenu>().bulletdamage;
    }
    void FixedUpdate()
    {
        if(Enemy != null)
        {
            if(cooldown <= 0)
            {
                StartCoroutine(shoot());

            }
            if(cooldown > 0) cooldown -= Time.deltaTime;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(Enemy == null && other.transform.tag == "Enemy" && ((other.gameObject.GetComponent<TroopMenu>() != null && other.gameObject.GetComponent<TroopMenu>().Team != transform.parent.GetComponent<TroopMenu>().Team) || (other.gameObject.GetComponent<BuildingMenu>() != null && other.gameObject.GetComponent<BuildingMenu>().Team != transform.parent.GetComponent<TroopMenu>().Team)))
        {
            Enemy = other.gameObject;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(Enemy == null && other.transform.tag == "Enemy" && ((other.gameObject.GetComponent<TroopMenu>() != null && other.gameObject.GetComponent<TroopMenu>().Team != transform.parent.GetComponent<TroopMenu>().Team) || (other.gameObject.GetComponent<BuildingMenu>() != null && other.gameObject.GetComponent<BuildingMenu>().Team != transform.parent.GetComponent<TroopMenu>().Team)))
        {
            Enemy = other.gameObject;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(Enemy == other.gameObject && other.transform.tag == "Enemy")
        {
            Enemy = null;
        }
    }
    IEnumerator shoot()
    {

        Vector3 targetpos = new Vector3(Enemy.transform.position.x, shootparent.transform.position.y, Enemy.transform.position.z);
        shootparent.transform.LookAt(targetpos);
        GameObject bullet2 = Instantiate(bullet, shootparent.transform.position, shootparent.transform.rotation);
        bullet2.GetComponent<EnemyFireScript>().Enemytar = Enemy;
        bullet2.GetComponent<EnemyFireScript>().bulletspeed = bulletspeed;
        bullet2.GetComponent<EnemyFireScript>().Damage = damage;
        cooldown = rate;
        yield return new WaitForSeconds(1);
    }
}
