using UnityEngine;
using System.Collections;

public class OriantationGuidance : MonoBehaviour 
{
	
	void FixedUpdate()
    {
    	RaycastHit hit;
		Physics.Raycast (transform.position, -transform.up, out hit, 100.0f);


		transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);

		//transform.localEulerAngles = Vector3.Angle(hit.normal, -transform.up);
		Debug.DrawRay(transform.position, -transform.up);
		Debug.DrawRay(hit.point, hit.normal, Color.red);

    }
}
