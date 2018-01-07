using ECM.Controllers;
using HutongGames.PlayMaker;
using UnityEngine;
using UnityEngine.AI;

namespace MichaelWolfGames.AI.Playmaker.ECM
{
    public class ECM_StateActonNavBehaviour : StateActionBehaviour
    {
        [ActionSection("NavMeshAgent Variables")]
        public float AgentSpeed = 0f;
        public float AgentAcceleration = 0f;
        public bool UseDefaultAgentVariables = true;

        public BaseAgentController agentController { get; protected set; }
        protected float reachedDestinationThreshold = 0.1f;
        protected Vector3 startNavPos = Vector3.zero;
        protected float DefaultAgentSpeed;
        protected float DefaultAgentAcceleration;
        //protected Timer _navigationTimeOutTimer = new Timer(2f);
        public override void Init(FsmState state)
        {
            base.Init(state);

        }

        public override void Awake()
        {
            base.Awake();
            //InitializeNavMeshAgent();
        }

        public override void OnEnter()
        {
            base.OnEnter();
            InitializeNavMeshAgent();
            if (!UseDefaultAgentVariables)
            {
                SetNavMeshAgentVariables();
            }
        }


        public override void OnExit()
        {
            base.OnExit();
            ResetNavMeshAgentVariables();
        }

        protected virtual bool InitializeNavMeshAgent()
        {
            agentController = BehaviourController.CharacterTransform.GetComponent<BaseAgentController>();
            if (!agentController)
            {
                BehaviourController.CharacterTransform.GetComponentInParent<NavMeshAgent>();
            }
            if (!agentController) return false;
            if (agentController)
            {
                DefaultAgentSpeed = agentController.speed;
                DefaultAgentAcceleration = agentController.acceleration;
            }
            InitializeStartPosition(10f);
            return true;
        }

        private void SetNavMeshAgentVariables()
        {
            agentController.speed = AgentSpeed;
            agentController.acceleration = AgentAcceleration;
        }
        protected void ResetNavMeshAgentVariables()
        {
            agentController.speed = DefaultAgentSpeed;
            agentController.acceleration = DefaultAgentAcceleration;
        }

        protected virtual bool InitializeStartPosition(float sampleDistance = 5f)
        {
            NavMeshHit hit;
            bool pointFound = GetClosestPointOnNavMesh(out hit, sampleDistance);
            if (pointFound)
            {
                agentController.enabled = true;
                if (startNavPos == Vector3.zero)
                {
                    startNavPos = hit.position;
                }
                //startNavPos = hit.position;
            }
            else
                startNavPos = agentController.transform.position;
            //navMeshAgent.enabled = pointFound;
            return pointFound;
        }



        #region Common Navigation Methods and Parameters

        public Transform NavTransform
        {
            get { return agentController.transform; }
        }

        public Vector3 ToTargetVector
        {
            get { return (targetTransform.position - agentController.transform.position); }
        }

        public Vector3 ToTargetDirection
        {
            get { return ToTargetVector.normalized; }
        }

        public Vector3 ProjectedToTargetVector
        {
            get
            {
                return (Vector3.ProjectOnPlane(targetTransform.position, Vector3.up) -
                        Vector3.ProjectOnPlane(agentController.transform.position, Vector3.up));
            }
        }

        /// <summary>
        /// The ToTargetVector projected on the XZ-plane and normalized.
        /// </summary>
        public Vector3 ProjectedToTargetDirection
        {
            get { return ProjectedToTargetVector.normalized; }
        }

        public void SmoothLookAtTarget(float speed, float timeDelta = 0f)
        {
            if (timeDelta <= 0f) timeDelta = Time.deltaTime;
            var targetRot = Quaternion.LookRotation(ProjectedToTargetDirection, Vector3.up);
            //navMeshAgent.
            NavTransform.rotation = Quaternion.Slerp(NavTransform.rotation, targetRot, speed * timeDelta);
        }

        public void SmoothLookAtTarget()
        {
            var targetRot = Quaternion.LookRotation(ProjectedToTargetDirection, Vector3.up);
            NavTransform.rotation = Quaternion.Slerp(NavTransform.rotation, targetRot,
                agentController.angularSpeed * Time.deltaTime);
        }

        public Vector3 ProjectedTargetPositionAtSameHeight
        {
            get
            {
                var v = Vector3.ProjectOnPlane(targetTransform.position, Vector3.up);
                v.y = agentController.transform.position.y;
                return v;
            }
        }
        //public Vector3 ProjectedPositionAtSameHeight
        //public float GetRange(Vector3 position, bool isProjected = true)
        //{

        //}
        public bool CheckRange(float range, bool isProjected = true)
        {
            return ((isProjected) ? ProjectedToTargetVector : ToTargetVector).magnitude < range;
        }

        public bool CheckRange(float range, out bool isInRange, bool isProjected = true)
        {
            isInRange = ((isProjected) ? ProjectedToTargetVector : ToTargetVector).magnitude < range;
            return isInRange;
        }

        protected virtual void SetDestination(Vector3 position, float offsetDistance = 0f)
        {
            if (!agentController.enabled) return;
            NavMeshHit hit;
            if (GetClosestPointOnNavMesh(position, out hit, 100f))
            {
                var pos = hit.position - ((position - agentController.transform.position).normalized * offsetDistance);
                agentController.agent.SetDestination(pos);
            }
            else
            {
                agentController.agent.SetDestination(position); //ToDo: Expand this and make it more reliable.
            }
        }
        protected virtual void FollowTransform(Transform target, float offsetDistance = 0f)
        {
            SetDestination(target.position, offsetDistance);
        }

        protected virtual bool HasReachedDestination
        {
            get
            {
                return
                ((Vector3.Distance(agentController.transform.position + Vector3.down * agentController.agent.baseOffset, agentController.agent.destination) < reachedDestinationThreshold
                 || !agentController.agent.hasPath) && !agentController.agent.pathPending);
            }
        }

        protected virtual float DistanceToDestination
        {
            get
            {
                return
                (Vector3.Distance(agentController.transform.position + Vector3.down * agentController.agent.baseOffset,
                    agentController.agent.destination));
            }
        }
        public virtual bool GetClosestPointOnNavMesh(Vector3 worldPos, out NavMeshHit closestHit, float sampleDistance = 5f)
        {
            if (NavMesh.SamplePosition(worldPos, out closestHit, sampleDistance, 1))
            {
                return true;
            }
            return false;
        }
        public virtual bool GetClosestPointOnNavMesh(out NavMeshHit closestHit, float sampleDistance = 5f)
        {
            if (NavMesh.SamplePosition(agentController.transform.position + Vector3.down * agentController.agent.baseOffset, out closestHit,
                sampleDistance, 1))
            {
                //navMeshAgent.enabled = true;
                //if (startNavPos == Vector3.zero)
                //{
                //    startNavPos = closestHit.position;
                //}
                return true;
            }
            return false;
        }

        protected void FindNewRandomDestination(float randomDestinationDistance)
        {
            if (!agentController.agent.isOnNavMesh) return;
            var randVector = Random.insideUnitCircle *
                             Random.Range(randomDestinationDistance / 2f, randomDestinationDistance);
            var newPos = agentController.transform.position +
                         Vector3.down * agentController.agent.baseOffset +
                         new Vector3(randVector.x,
                             agentController.transform.position.y - agentController.agent.baseOffset,
                             randVector.y);
            if (Vector3.Distance(startNavPos, newPos) > randomDestinationDistance)
            {
                newPos = startNavPos;
            }
            NavMeshHit hit;
            if (GetClosestPointOnNavMesh(newPos, out hit))
            {
                newPos = hit.position;
            }
            agentController.agent.SetDestination(newPos);
        }

        /// <summary>
        /// Finds a valid NavMesh position along a defined ray.
        /// </summary>
        protected bool NavigationSampleCast(Vector3 startPosition, Vector3 castDirection, out Vector3 result,
            float minRange, float maxRange, float castDelta)
        {
            var currentDist = minRange;
            var samplePosition = startPosition + (castDirection * currentDist);
            samplePosition = Vector3.ProjectOnPlane(samplePosition, Vector3.up);

            int breakOut = 0;
            result = Vector3.zero;
            while (!SampleNavMeshPosition(samplePosition, out result) && currentDist <= maxRange)
            {
                currentDist += castDelta;
                samplePosition = startPosition + (castDirection * currentDist);
                samplePosition = Vector3.ProjectOnPlane(samplePosition, Vector3.up);
                if (++breakOut > 10)
                    break;
            }
            if (result == Vector3.zero)
            {
                Debug.LogWarning("Could not find Retreat Path.");
                return false;
            }
            return true;
        }

        protected bool SampleNavMeshPosition(Vector3 samplePosition, out Vector3 result, float maxDistance = 50f)
        {
            NavMeshHit hit;
            if (NavMesh.SamplePosition(samplePosition, out hit, maxDistance, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
            result = Vector3.zero; //Maybe use current position instead?
            return false;
        }

        protected bool CheckRaycastLineOfSight(float checkRange, LayerMask layerMask)
        {
            var eyePos = agentController.transform.position +
                         Vector3.up * (((agentController) ? agentController.agent.height : 1) * 0.75f);
            var eye2Target = targetTransform.position - eyePos;
            RaycastHit hit;
            if (Physics.Raycast(eyePos, eye2Target.normalized, out hit, checkRange, layerMask,
                QueryTriggerInteraction.Ignore))
            {
                if (
                ((hit.collider.attachedRigidbody)
                    ? hit.collider.attachedRigidbody.transform
                    : hit.collider.transform).root == targetTransform)
                {
                    //Debug.Log("Saw Target!");
                    return true;
                }
            }
            return false;
        }

        protected void PlaceOnClosestNavMeshPoint()
        {
            Vector3 v;
            if (SampleNavMeshPosition(agentController.transform.position, out v))
            {
                Debug.Log("[StateActionNavBehaviour]: Placing Agent On Closest NavMesh.");
                agentController.transform.position = v;
            }
        }

        #endregion
    }
}