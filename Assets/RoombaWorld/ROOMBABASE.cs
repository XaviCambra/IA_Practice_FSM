using FSMs;
using UnityEngine;
using Steerings;

[CreateAssetMenu(fileName = "ROOMBABASE", menuName = "Finite State Machines/ROOMBABASE", order = 1)]
public class ROOMBABASE : FiniteStateMachine
{
    private ROOMBA_Blackboard blackboard;
    private RandomLocationGenerator locationGenerator;
    private GameObject currentPoint;
    private GoToTarget goToTarget;
    private GameObject pooTarget;
    private GameObject newPoo;
    private GameObject dustTarget;
    private PathFollowing path;
    private SteeringContext steering;

    public override void OnEnter()
    {
        blackboard = GetComponent<ROOMBA_Blackboard>();
        goToTarget = GetComponent<GoToTarget>();
        base.OnEnter(); // do not remove
    }

    public override void OnExit()
    {
        
        base.OnExit();
    }

    public override void OnConstruction()
    {
     
         State PATROL = new State("Patrol",
            () => { currentPoint = blackboard.GetRandomPatrolPoint(); goToTarget.target = currentPoint; goToTarget.enabled = true; }, 
            () => { }, 
            () => { }    
        );


        State DUSTFOUND = new State("Dust Found",
           () => {
               if (blackboard.somethingInMemory())
               {
                   dustTarget = blackboard.RetrieveFromMemory();
               }
               goToTarget.target = dustTarget;
               goToTarget.enabled = true;
               path.enabled = true;
           },
           () => { },
           () => { }
       );

        State POOFOUND = new State("Poo Found",
            () => {
                steering.maxSpeed *= 1.3f;
                steering.maxAcceleration *= 2.6f;
                goToTarget.target = pooTarget;
                goToTarget.enabled = true;
                path.enabled = true;
            },
            () => { },
            () => { }
        );

        Transition PointReached = new Transition("Point Reached",
           () => { return SensingUtils.DistanceToTarget(gameObject, currentPoint) <= blackboard.pointReachedRadius; }
        );

        Transition DustDetected = new Transition("Dust Detected",
           () => { return dustTarget = SensingUtils.FindInstanceWithinRadius(gameObject, "DUST", blackboard.dustDetectionRadius); }
        );

        Transition DustReached = new Transition("Dust Reached",
          () => { return SensingUtils.DistanceToTarget(gameObject, dustTarget) <= blackboard.dustReachedRadius; },
          () => { Destroy(dustTarget); }
       );
        Transition DustMemory = new Transition("Dust In Memory",
           () => { return blackboard.somethingInMemory(); }
        );

        Transition PooDetected = new Transition("Poop Detected",
           () => { return pooTarget = SensingUtils.FindInstanceWithinRadius(gameObject, "POO", blackboard.pooDetectionRadius); },
           () => {
               if (dustTarget != null)
               {
                   blackboard.AddToMemory(dustTarget);
               }
           }
        );

        Transition PooReached = new Transition("Poop Reached",
           () => { return SensingUtils.DistanceToTarget(gameObject, pooTarget) <= blackboard.pooReachedRadius; },
           () => { Destroy(pooTarget); }
        );

        Transition CloserPooDetected = new Transition("CloserPoop Detected",
           () => { newPoo = SensingUtils.FindInstanceWithinRadius(gameObject, "POO", blackboard.pooDetectionRadius);
               if (pooTarget != null && newPoo != pooTarget)
               {
                   return SensingUtils.DistanceToTarget(gameObject, pooTarget) > SensingUtils.DistanceToTarget(gameObject, newPoo);
               }
               else
               {
                   return false;
               }
           },
           () => { pooTarget = newPoo; }
        );

        AddStates(PATROL, DUSTFOUND, POOFOUND);

        AddTransition(PATROL, PointReached, PATROL);
        AddTransition(PATROL, PooDetected, POOFOUND);
        AddTransition(POOFOUND, PooReached, PATROL);
        AddTransition(POOFOUND, CloserPooDetected, POOFOUND);
        AddTransition(PATROL, DustDetected, DUSTFOUND);
        AddTransition(DUSTFOUND, PooDetected, POOFOUND);
        AddTransition(DUSTFOUND, DustReached, PATROL);
        AddTransition(PATROL, DustMemory, DUSTFOUND);

        initialState = PATROL;
    }
}
