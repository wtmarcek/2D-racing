using UnityEngine;
using System.Collections;

public class Overtaking : MonoBehaviour {

	/*void Overtake(bool alarm){

		mudPos = predictor.mudPosition;

		Vector3 mudDirection = mudPos - transform.position;
		float angle 		 = Vector3.Angle (transform.position, mudDirection);
		float tan			 = Mathf.Tan (2.0f / predictor.transform.position.z);
		tan					 = Mathf.Rad2Deg;

		Debug.Log (angle);

		if (alarm) {
			Vector3 targetPos = predictor.exitPosition;
			Vector3 relativePos = targetPos - transform.position;

			Debug.DrawLine (transform.position, mudPos, Color.yellow);

			Quaternion toRotation = Quaternion.LookRotation (relativePos);
			transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, aiTurnSpeed/2);
		} 

		if (angle > 120.0f) {
			predictor.mudAlarm = false;
		}
	}*/
}
