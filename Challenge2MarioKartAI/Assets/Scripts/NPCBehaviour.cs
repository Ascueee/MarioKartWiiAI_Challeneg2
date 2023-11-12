using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCBehaviour : MonoBehaviour
{
    [Header("Kart Movement Vars")]
    [SerializeField] Rigidbody rb;
    [SerializeField] float movementForce;
    [SerializeField] float maxMovementSpeed;

    //seek variables
    Vector3 _currentVelocity;
    Vector3 _desiredVelocity;
    Vector3 _steeringVelocity;
    float _maxForce = 5f;

    //flee variables
    Vector3 distanceFromItem;


    [Header("Other Game Objects")]
    [SerializeField] Transform enemyItem;
    [SerializeField] Transform trackSphere;

    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(enemyItem == null)
        {
            Seek();
        }
        else if(enemyItem != null)
        {

            Flee();
        }

       
    }

    private void FixedUpdate()
    {
        //ConstantForwardForce();
        //SpeedCheck();
    }

    void Flee()
    {

        if(distanceFromItem.magnitude > 5)
        {
            enemyItem = null;
        }
        else
        {
            
            _desiredVelocity = ((enemyItem.position + new Vector3(-12, 0, 0)) - transform.position).normalized;
            _desiredVelocity *= maxMovementSpeed;

            _steeringVelocity = _desiredVelocity - _currentVelocity;
            _steeringVelocity = _steeringVelocity = Vector3.ClampMagnitude(_steeringVelocity, _maxForce);
            _steeringVelocity /= rb.mass;

            _currentVelocity += _steeringVelocity;
            _currentVelocity = Vector3.ClampMagnitude(_currentVelocity, _maxForce);

            transform.position += _currentVelocity * Time.deltaTime;
            transform.forward += _currentVelocity * Time.deltaTime;

            Debug.DrawLine(transform.position, enemyItem.position + new Vector3(-12, 0, 0), Color.magenta);
        }

    }

    /// <summary>
    /// This method controls the seek behaviour which allows the NPC to "seek" or follow an object with a set velocity and maxVelocity
    /// </summary>
    void Seek()
    {
        _desiredVelocity = (trackSphere.transform.position - transform.position).normalized;
        _desiredVelocity *= maxMovementSpeed;

        _steeringVelocity = _desiredVelocity - _currentVelocity;
        _steeringVelocity = Vector3.ClampMagnitude(_steeringVelocity, _maxForce);
        _steeringVelocity /= rb.mass;

        _currentVelocity += _steeringVelocity;
        _currentVelocity = Vector3.ClampMagnitude(_currentVelocity, _maxForce);

        transform.position += _currentVelocity * Time.deltaTime;
        transform.forward += _currentVelocity * Time.deltaTime;
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "WayPoint" || other.gameObject.tag == "startWayPoint")
        {

        }
        else if(other.gameObject.tag == "enemyItem")
        {
            distanceFromItem = other.gameObject.transform.position - transform.position;
            
            if(distanceFromItem.magnitude < 2)
            {
               
                enemyItem = other.gameObject.transform;
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        //objectsInCollider.Remove(other.gameObject);
    }
}
