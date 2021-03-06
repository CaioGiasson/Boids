﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flock : MonoBehaviour {
	public FlockAgent agentPrefab;
	List<FlockAgent> agents = new List<FlockAgent> ( );
	public FlockBehavior behavior;

	public KeyCode teclaAumentar;
	public KeyCode teclaDiminuir;
	public Text textoQuantidade;
	public string nome;

	[Range ( 10, 500 )]
	public int startingCount = 250;
	const float AgentDensity = 0.08f;

	[Range ( 1f, 100f )]
	public float driveFactor = 10f;

	[Range ( 1f, 100f )]
	public float maxSpeed = 5f;

	[Range ( 1f, 10f )]
	public float neighborRadius = 1.5f;

	[Range ( 0f, 1f )]
	public float avoidanceRadiusMultiplier = 0.5f;

	float squareMaxSpeed;
	float squareNeighborRadius;
	float squareAvoidanceRadius;
	public float SquareAvoidanceRadius { get { return squareAvoidanceRadius; } }

	void Start ( ) {
		squareMaxSpeed = maxSpeed * maxSpeed;
		squareNeighborRadius = neighborRadius * neighborRadius;
		squareAvoidanceRadius = squareNeighborRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;

		ReloadAgents ( );
	}

	void ReloadAgents ( ) {
		if ( agents.Count > 0 ) {
			foreach ( FlockAgent a in agents ) Destroy ( a.gameObject );
			agents.Clear ( );
		}

		for ( int i = 0 ; i < startingCount ; i++ ) {
			FlockAgent newAgent = Instantiate (
				agentPrefab,
				Random.insideUnitCircle * startingCount * AgentDensity,
				Quaternion.Euler ( Vector3.forward * Random.Range ( 0f, 360f ) ),
				transform
			);
			newAgent.name = "Agent " + i;
			newAgent.Initialize ( this );
			agents.Add ( newAgent );
		}
	}

	void Update ( ) {

		textoQuantidade.text = nome + ": " + startingCount;

		if ( Input.GetKey ( teclaAumentar ) ) {
			startingCount++;
		}
		else if ( Input.GetKey ( teclaDiminuir ) ) {
			startingCount--;
		}
		if ( Input.GetKeyUp ( teclaAumentar ) || Input.GetKeyUp ( teclaDiminuir ) ) ReloadAgents ( );

		foreach ( FlockAgent agent in agents ) {
			List<Transform> context = GetNearbyObjects ( agent );
			Vector2 move = behavior.CalculateMove ( agent, context, this );
			move *= driveFactor;
			if ( move.sqrMagnitude > squareMaxSpeed ) move = move.normalized * maxSpeed;
			agent.Move ( move );
		}

	}

	List<Transform> GetNearbyObjects ( FlockAgent agent ) {
		List<Transform> context = new List<Transform> ( );

		Collider2D[] contextColliders = Physics2D.OverlapCircleAll ( agent.transform.position, neighborRadius );
		foreach ( Collider2D c in contextColliders ) {
			if ( c != agent.AgentCollider ) {
				context.Add ( c.transform );
			}
		}

		return context;
	}
}
