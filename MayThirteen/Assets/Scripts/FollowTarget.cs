using UnityEngine;
using System.Collections;

public class FollowTarget : MonoBehaviour
{

		public Transform targetToFollow;
		void FixedUpdate ()
		{
				Vector3 newPosition = targetToFollow.position;
				newPosition.z = transform.position.z;
				transform.position = newPosition;
		}
}
