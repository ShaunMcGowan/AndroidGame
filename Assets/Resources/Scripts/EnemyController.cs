using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// The AI controller for the second player object for play during offline mode
/// </summary>
public class EnemyController : MonoBehaviour {

    Transform target;
    NavMeshAgent agent;
	void Start ()
    {
        target = PlayerManager.instance.player.transform;
        agent = this.GetComponent<NavMeshAgent>();
	}
	

	void Update ()
    {
        agent.SetDestination(target.position);
	}
}
