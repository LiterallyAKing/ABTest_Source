using System;
using UnityEngine;

[AddComponentMenu("Custom/Cameras/CameraFollow_2D")]
public class CameraFollow_2D : MonoBehaviour
{
	public Vector3 upperbounds;
	public Vector3 lowerbounds;
	public Vector3 target_offset;
    public Transform target;
    public float damping = 1;
    public float lookAheadFactor = 3;
    public float lookAheadReturnSpeed = 0.5f;
    public float lookAheadMoveThreshold = 0.1f;

    private float m_OffsetZ;
    private Vector3 m_CurrentVelocity;
    private Vector3 m_LookAheadPos;



    // Use this for initialization
    private void Start()
    {

        m_OffsetZ = (transform.position - target.position).z;
        transform.parent = null;
    }


    // Update is called once per frame
    private void Update()
    {
		// only update lookahead pos if accelerating or changed direction
		//     float xMoveDelta = (target.position - m_LastTargetPosition).x;
		//     bool updateLookAheadTarget = Mathf.Abs(xMoveDelta) > lookAheadMoveThreshold;
		//     if (updateLookAheadTarget)
		//     {
		//m_LookAheadPos = lookAheadFactor * Vector3.right; // *Mathf.Sign(xMoveDelta);
		//     }
		//     else
		//     {
		//         m_LookAheadPos = Vector3.MoveTowards(m_LookAheadPos, Vector3.zero, Time.deltaTime*lookAheadReturnSpeed);
		//     }

		//m_LookAheadPos = lookAheadFactor * Vector3.right;
		//m_LookAheadPos = (lookAheadFactor * Vector3.right) * (player.show_velocity.x/10f); 

		 //player.show_velocity.x

		 Vector3 aheadTargetPos = target.position + m_LookAheadPos + Vector3.forward*m_OffsetZ;
        Vector3 newPos = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref m_CurrentVelocity, damping);
		newPos = newPos + target_offset;
		if (newPos.y < lowerbounds.y) {
			newPos.y = lowerbounds.y;
		} else if (newPos.y > upperbounds.y) {
			newPos.y = upperbounds.y;
		}
		if (newPos.x < lowerbounds.x) {
			newPos.x = lowerbounds.x;
		}
		transform.position = newPos;
		
    }
}

