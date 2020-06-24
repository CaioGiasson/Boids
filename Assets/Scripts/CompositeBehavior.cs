using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu ( menuName = "Flock/Behavior/Composite" )]
public class CompositeBehavior : FlockBehavior {

	public FlockBehavior[] behaviors;
	public float[] weights;

	public override Vector2 CalculateMove ( FlockAgent agent, List<Transform> context, Flock flock ) {

		Vector2 move = Vector2.zero;

		for ( int i = 0 ; i < behaviors.Length ; i++ ) {
			Vector2 temp = behaviors[i].CalculateMove ( agent, context, flock ) * weights[i];

			if ( temp != Vector2.zero && temp.sqrMagnitude > weights[i] * weights[i] ) {
				temp.Normalize ( );
				temp *= weights[i];
			}

			move += temp;
		}

		return move;
	}

}
