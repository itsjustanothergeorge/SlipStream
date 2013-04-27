using UnityEngine;
using System.Collections;

public class AntiGravityEngine : MonoBehaviour {

	[SerializeField]
    private Rigidbody myRigidbody;
    [SerializeField]
    private float vertialForwardSpeed;
    [SerializeField]
    private float horizontalSpeed;
    [SerializeField]
    private float hoverHeight;

    private Vector3 idealPosition;
    private Vector3 lastPosition;

    void FixedUpdate()
    {
    	float v = vertialForwardSpeed * Input.GetAxis("Vertical");
        float h = horizontalSpeed * Input.GetAxis("Horizontal");

        Vector3 movement = new Vector3(v,0,-h);
        myRigidbody.AddRelativeForce(movement);

		RaycastHit hit;
		if (Physics.Raycast (transform.position, -transform.up, out hit, 100.0f)) {
	    	idealPosition = transform.position + ((hoverHeight - hit.distance) * transform.up);
		}

		if(hoverHeight > hit.distance)
		{

		}
	 
		Vector3 pull = (idealPosition - transform.position);
		myRigidbody.AddForce( pull );

		Debug.Log(hit.normal);
		if(hit.point != lastPosition)
		{
			//transform.LookAt(hit.point);
		}
		transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);

		//transform.localEulerAngles = Vector3.Angle(hit.normal, -transform.up);
		Debug.DrawRay(transform.position, -transform.up);
		Debug.DrawRay(hit.point, hit.normal, Color.red);
	}
}
