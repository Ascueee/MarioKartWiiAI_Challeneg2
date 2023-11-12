using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    [SerializeField] Transform spherePathObject;
    [SerializeField] Transform[] wayPointPoints;
    [SerializeField] Transform[] wayPointHandles;
    [SerializeField] int trackerIndex;

    [SerializeField] float duration;
    float interpolateTime;
    float percentageComplete;

    /// <summary>
    /// PLAN TO CREATE ALL THE BEZEIR CURVES AT THE BEGINNING OF THE SCRIPT SO THAT THE SPHERE CAN THEN FOLLOW THE LINEAR INTERPOLATION
    /// </summary>

    // Start is called before the first frame update
    void Start()
    {

 
    }

    // Update is called once per frame
    void Update()
    {

        interpolateTime += Time.deltaTime;
        percentageComplete = interpolateTime / duration;

        if(percentageComplete >= 1 )
        {
            percentageComplete = 0;
            interpolateTime = 0;
        }

        //UpdateWayPoint();
        UpdateHardCode();

        //spherePathObject.position = BezeirLerp(wayPointPoints[0].position, wayPointHandles[0].position, wayPointHandles[1].position, wayPointPoints[1].position, interpolateTime);
    }


    ///---------------------------------GENERAL METHODS---------------------------------
    void UpdateWayPoint()
    {
        if(trackerIndex <= wayPointPoints.Length) 
        {
         
        }

        spherePathObject.position = BezeirLerp(wayPointPoints[trackerIndex].position, wayPointHandles[trackerIndex].position, wayPointHandles[trackerIndex + 1].position, wayPointPoints[trackerIndex + 1].position, percentageComplete);

    }

    void UpdateHardCode()
    {
        if(trackerIndex == 0)
        {
            LERP(spherePathObject, wayPointPoints[0], wayPointPoints[1], percentageComplete);
            //spherePathObject.position = BezeirLerp(wayPointPoints[0].position, wayPointHandles[0].position, wayPointHandles[1].position, wayPointPoints[1].position, percentageComplete);
        }
        else if(trackerIndex == 1)
        {
            LERP(spherePathObject, wayPointPoints[1], wayPointPoints[2], percentageComplete);
            //spherePathObject.position = BezeirLerp(wayPointPoints[1].position, wayPointHandles[1].position, wayPointHandles[2].position, wayPointPoints[2].position, percentageComplete);
        }
        else if(trackerIndex == 2)
        {
            spherePathObject.position = BezeirLerp(wayPointPoints[2].position, wayPointHandles[2].position, wayPointHandles[3].position, wayPointPoints[3].position, percentageComplete);
        }
        else if(trackerIndex == 3)
        {
            LERP(spherePathObject, wayPointPoints[3], wayPointPoints[4], percentageComplete);
            //spherePathObject.position = BezeirLerp(wayPointPoints[3].position, wayPointHandles[3]. position, wayPointHandles[4].position, wayPointPoints[4].position, percentageComplete);
        }
        else if(trackerIndex == 4)
        {
            spherePathObject.position = BezeirLerp(wayPointPoints[4].position, wayPointHandles[4].position, wayPointHandles[5].position, wayPointPoints[5].position, percentageComplete);
        }
        else if(trackerIndex  == 5)
        {
            LERP(spherePathObject, wayPointPoints[5], wayPointPoints[6], percentageComplete);
        }
        else if(trackerIndex == 6)
        {
            LERP(spherePathObject, wayPointPoints[6], wayPointPoints[7], percentageComplete);
        }
        else if(trackerIndex == 7)
        {
            LERP(spherePathObject, wayPointPoints[7], wayPointPoints[8], percentageComplete);
        }
        else if(trackerIndex == 8)
        {
            spherePathObject.position = BezeirLerp(wayPointPoints[8].position, wayPointHandles[8].position, wayPointHandles[9].position, wayPointPoints[9].position, percentageComplete);
        }
        else if(trackerIndex == 9)
        {
            LERP(spherePathObject, wayPointPoints[9], wayPointPoints[10], percentageComplete);
        }
        else if(trackerIndex == 10)
        {
            spherePathObject.position = BezeirLerp(wayPointPoints[10].position, wayPointHandles[10].position, wayPointHandles[11].position, wayPointPoints[11].position, percentageComplete);
        }
        else if(trackerIndex == 11)
        {
            LERP(spherePathObject, wayPointPoints[11], wayPointHandles[0], percentageComplete);
        }
        
    }

    ///---------------------------------GENERAL METHODS ENDS---------------------------------



    ///---------------------------------LERP METHODS---------------------------------

    //this lerp method linearly interpolates an object between two points
    void LERP(Transform movingObject, Transform pointA, Transform pointB, float t) 
    {
        movingObject.position = Vector3.Lerp(pointA.position,pointB.position, t);
    }


    //performs a cubicLerp on a bezeircurve with multiple handles
    Vector3 BezeirLerp(Vector3 a, Vector3 b, Vector3 c, Vector3 d, float t)
    {
        Vector3 ab_bc = QuadraticLerp(a, b, c, t);
        Vector3 bc_cd = QuadraticLerp(b, c, d, t);

        return Vector3.Lerp(ab_bc, bc_cd, percentageComplete);



    }

    //does a quadratic lerp between three points
    Vector3 QuadraticLerp(Vector3 a, Vector3 b, Vector3 c, float t)
    {
        Vector3 ab = Vector3.Lerp(a, b, t);
        Vector3 bc = Vector3.Lerp(b, c, t);

        return Vector3.Lerp(ab,bc, percentageComplete); 
    }

    ///---------------------------------LERP METHODS ENDS---------------------------------


    ///---------------------------------GETTER SETTER METHODS---------------------------------
    
    public void SetSphereIndex(int sphereIndexTracker)
    {
        trackerIndex = sphereIndexTracker;
    }
    

    /// This gets called in the PathSphereTracker script to reset the timer and percentage complete 
    public void ResetTimer(float percentageCompleteReset, float interpolateTimeReset)
    {
        interpolateTime = interpolateTimeReset;
        percentageComplete = percentageCompleteReset;
    }


    ///---------------------------------GETTER SETTER METHODS ENDS---------------------------------

}
