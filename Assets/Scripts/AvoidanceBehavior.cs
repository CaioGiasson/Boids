using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu ( menuName = "Flock/Behavior/Avoidance" )]
public class AvoidanceBehavior : FilteredFlockBehavior {

	public override Vector2 CalculateMove ( FlockAgent agent, List<Transform> context, Flock flock ) {
		if ( context.Count == 0 ) return agent.transform.up;

		Vector2 avoidanceMove = Vector2.zero;
		List<Transform> filteredContext = ( filter == null ) ? context : filter.Filter ( agent, context );
		int nAvoid = 0;
		foreach ( Transform item in filteredContext ) {
			if ( Vector2.SqrMagnitude ( item.position - agent.transform.position ) < flock.SquareAvoidanceRadius ) {
				nAvoid++;
				avoidanceMove += ( Vector2 ) ( agent.transform.position - item.position );
			}
		}
		if ( nAvoid > 0 ) avoidanceMove /= nAvoid;
		return avoidanceMove;
	}

}
