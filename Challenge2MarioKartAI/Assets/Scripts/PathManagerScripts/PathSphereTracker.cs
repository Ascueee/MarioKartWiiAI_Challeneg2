using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathSphereTracker : MonoBehaviour
{
    [SerializeField] PathManager pm;
    [SerializeField] int sphereIndexTracker;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
       
        CollisionWithWayPoint(other);
    }

    void CollisionWithWayPoint(Collider other)
    {
        //checks if the object is colliding with a waypoint
        if(other.gameObject.tag == "WayPoint")
        {
            //increments the sphere tracker to let the pathmanager know what index its ons
            sphereIndexTracker++;
            //sets the sphere index in the pm
            pm.SetSphereIndex(sphereIndexTracker);
            //resets the percentage complete on each waypoint
            pm.ResetTimer(0, 0);
        }
        if(other.gameObject.tag == "startWayPoint")
        {
            sphereIndexTracker = 0;
            pm.SetSphereIndex(0);
            pm.ResetTimer(0, 0);
        }
    }
}
