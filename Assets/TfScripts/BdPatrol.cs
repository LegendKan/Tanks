using UnityEngine;
using System.Collections;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

    public class BdPatrol : Action
    {
        public SharedFloat arriveDistance = 0.1f;
        public SharedBool randomPatrol = false;
        public SharedTransformList waypoints;

        private NavMeshAgent navMeshAgent;
        // The current index that we are heading towards within the waypoints array
        private int waypointIndex;

    public AIController aictrl;

        public override void OnAwake()
        {
            navMeshAgent = gameObject.GetComponent<NavMeshAgent>();
        }

        public override void OnStart()
        {
        // initially move towards the closest waypoint
            aictrl = this.GetComponent<AIController>();
            float distance = Mathf.Infinity;
            float localDistance;
            for (int i = 0; i < waypoints.Value.Count; ++i)
            {
                if ((localDistance = Vector3.Magnitude(transform.position - waypoints.Value[i].position)) < distance)
                {
                    distance = localDistance;
                    waypointIndex = i;
                }
            }

        // set the speed, angular speed, and destination then enable the agent
            navMeshAgent.speed = aictrl.GetMoveSpeed();
            navMeshAgent.angularSpeed = aictrl.GetBodyRotateSpeed();
            navMeshAgent.enabled = true;
            navMeshAgent.destination = Target();
        }

        // Patrol around the different waypoints specified in the waypoint array. Always return a task status of running. 
        public override TaskStatus OnUpdate()
        {


        //不用跑了，可以打了
        if (aictrl.GetEnemyCurrentShellCount() * aictrl.GetShellDamage() - aictrl.GetCurrentHealth() < 0 && aictrl.GetCurrentShellCount() * aictrl.GetShellDamage() - aictrl.GetEnemyCurrentHealth() >= 0)
        {
            return TaskStatus.Failure;
        }


        if (!navMeshAgent.pathPending)
            {
                var thisPosition = transform.position;
                thisPosition.y = navMeshAgent.destination.y; // ignore y
                if (Vector3.SqrMagnitude(thisPosition - navMeshAgent.destination) < arriveDistance.Value)
                {
                    if (randomPatrol.Value)
                    {
                        waypointIndex = Random.Range(0, waypoints.Value.Count);
                    }
                    else
                    {
                        waypointIndex = (waypointIndex + 1) % waypoints.Value.Count;
                    }
                    navMeshAgent.destination = Target();
                }
            }

            return TaskStatus.Running;
        }

        public override void OnEnd()
        {
            // Disable the nav mesh
            navMeshAgent.enabled = false;
        }

        // Return the current waypoint index position
        private Vector3 Target()
        {
            return waypoints.Value[waypointIndex].position;
        }

        // Reset the public variables
        public override void OnReset()
        {
            arriveDistance = 0.1f;
            waypoints = null;
            randomPatrol = false;
        }

       
    }
