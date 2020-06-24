using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamante : MonoBehaviour {

	public float velox;

	void Update ( ) {

		if ( Input.GetKey ( KeyCode.W ) && transform.position.y < 9 ) transform.position += new Vector3 ( 0, velox );
		if ( Input.GetKey ( KeyCode.A ) && transform.position.x > -15 ) transform.position += new Vector3 ( -velox, 0 );
		if ( Input.GetKey ( KeyCode.S ) && transform.position.y > -9 ) transform.position += new Vector3 ( 0, -velox );
		if ( Input.GetKey ( KeyCode.D ) && transform.position.x < 15 ) transform.position += new Vector3 ( velox, 0 );

	}
}
