using FSMs;
using UnityEngine;
using Steerings;

[CreateAssetMenu(fileName = "ROOMBAEMERGENCY", menuName = "Finite State Machines/ROOMBAEMERGENCY", order = 1)]
public class ROOMBAEMERGENCY : FiniteStateMachine
{
    private ROOMBA_Blackboard blackboard;
    private GoToTarget goToTarget;
    private GameObject chargeStationNear;
    private PathFollowing path;

    public override void OnEnter()
    {
        blackboard = GetComponent<ROOMBA_Blackboard>();
        goToTarget = GetComponent<GoToTarget>();
        path = GetComponent<PathFollowing>();
            
        base.OnEnter(); 

}

public override void OnExit()
    {
        base.DisableAllSteerings();
        base.OnExit();

    }

    public override void OnConstruction()
    {
        FiniteStateMachine PATROL = ScriptableObject.CreateInstance<ROOMBABASE>();
        PATROL.Name = "Patrol";



        State GOCHARGE = new State("Go Charge",
           () => {goToTarget.target = chargeStationNear; goToTarget.enabled = true; path.enabled = true; },
           () => { },
           () => {
               goToTarget.enabled = false; path.enabled = false; }
        );

        State RECHARGE = new State("Recharge",
            () => { },
            () => { blackboard.Recharge(Time.deltaTime); },
            () => { }
        );

        Transition Discharged = new Transition("Discharged",
            () => { return blackboard.currentCharge <= blackboard.minCharge; },
            () => {
                if (SensingUtils.DistanceToTarget(gameObject, blackboard.chargeStations[0]) < SensingUtils.DistanceToTarget(gameObject, blackboard.chargeStations[1]))
                {
                    chargeStationNear = blackboard.chargeStations[0];
                }
                else
                {
                    chargeStationNear = blackboard.chargeStations[1];
                }
            }
        );

        Transition ChargerStationReached = new Transition("Station Reached",
            () => { return SensingUtils.DistanceToTarget(gameObject, chargeStationNear) <= blackboard.chargingStationReachedRadius; }
        );

        Transition Charged = new Transition("Charged",
            () => { return blackboard.currentCharge >= blackboard.maxCharge; }
        );

        AddStates(PATROL, GOCHARGE, RECHARGE);
        AddTransition(PATROL, Discharged, GOCHARGE);
        AddTransition(GOCHARGE, ChargerStationReached, RECHARGE);
        AddTransition(RECHARGE, Charged, PATROL);


        initialState = PATROL;

    }
}
