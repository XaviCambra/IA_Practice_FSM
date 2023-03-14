using FSMs;
using UnityEngine;
using Steerings;

[CreateAssetMenu(fileName = "FSM_Mouse", menuName = "Finite State Machines/FSM_Mouse", order = 1)]
public class FSM_Mouse : FiniteStateMachine
{
    private MOUSE_Blackboard blackboard;
    private Arrive arrive;

    public override void OnEnter()
    {
        blackboard = new MOUSE_Blackboard();
        arrive = new Arrive();
        base.OnEnter(); // do not remove
    }

    public override void OnExit()
    {
        base.DisableAllSteerings();
        base.OnExit();
    }

    public override void OnConstruction()
    {
        blackboard.timeMouse -= Time.deltaTime;

        /*State appear = new State("Appear",
            () => { blackboard.timeMouse = 0; }, 
            () => { Instantiate(blackboard.mousePrefab, blackboard.RandomExitPoint().transform); }, 
            () => { blackboard.timeMouse = 25; }  
        );*/
        /*State arriveToLocation = new State("Arrive",
            () => { arrive.target = RandomLocationGenerator.RandomWalkableLocation() },
            () => {  },
            () => {  }
        );*/
        State leavePoo = new State("Leave Poo",
            () => { },
            () => { Instantiate(blackboard.pooPrefab); },
            () => { }
        );
        State goToDisappear = new State("Go To Disappear",
            () => { arrive.target = blackboard.RandomExitPoint(); arrive.enabled = true; },
            () => {  },
            () => { arrive.enabled = false; Destroy(gameObject); }
        );




    }
}
