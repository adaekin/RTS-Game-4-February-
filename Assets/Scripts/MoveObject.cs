using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    public List<GameObject> destinationloc;
    public float speed;
    public static MoveObject Instance;
    public bool clearcheckpoints = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;

        }
    }
    // Update is called once per frame


    // Instantinate ile checkpointler yaptýrýlýcak, checkpointlerden hareket edilicek, checkpointlere gittikten sonra silinecek


    void Update()
    {
        if (clearcheckpoints) removeallcheckpoints(); clearcheckpoints = false;
        if (destinationloc.Count > 0 && destinationloc[0] != null)
        {
            Vector3 destination = destinationloc[0].transform.position;

            float distance = Vector3.Distance(transform.position, destination);
            if (distance >= 1)
            {
                Vector3 newPos = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
                transform.position = newPos;
            }
            else if(distance < 1)
            {
                Destroy(destinationloc[0]);
                destinationloc.Remove(destinationloc[0]);
                
            }
        }


    }
    
    void removeallcheckpoints()
    {
        if(destinationloc.Count > 0)
        {
            int count = 0;
            for(int i = destinationloc.Count - 1; i >= 0; i--)
            {
                Destroy(destinationloc[i]);
                destinationloc.RemoveAt(i);
            }
        }
        


    }
}
