using FSMs;
using UnityEngine;
using Steerings;

[CreateAssetMenu(fileName = "ROOMBAEMERGENCY", menuName = "Finite State Machines/ROOMBAEMERGENCY", order = 1)]
public class ROOMBAEMERGENCY : FiniteStateMachine
{
    private ROOMBA_Blackboard blackboard;

    /* Declare here, as attributes, all the variables that need to be shared among
     * states and transitions and/or set in OnEnter or used in OnExit 
     * For instance: steering behaviours, blackboard, ...*/

    public override void OnEnter()
    {
        /* Write here the FSM initialization code. This code is execute every time the FSM is entered.
         * It's equivalent to the on enter action of any state 
         * Usually this code includes .GetComponent<...> invocations */
        base.OnEnter(); // do not remove

}

public override void OnExit()
    {
        /* Write here the FSM exiting code. This code is execute every time the FSM is exited.
         * It's equivalent to the on exit action of any state 
         * Usually this code turns off behaviours that shouldn't be on when one the FSM has
         * been exited. */
        base.OnExit();

    }

    public override void OnConstruction()
    {
        FiniteStateMachine FSM_ROOMBA = ScriptableObject.CreateInstance<FSM_ROOMBA>();
        FSM_ROOMBA.Name = "Fsm Roomba";



        State GOCHARGE = new State("Go Charge",
           () => { },
           () => { },
           () => { }
       );

        /* STAGE 2: create the transitions with their logic(s)
         * ---------------------------------------------------

        Transition varName = new Transition("TransitionName",
            () => { }, // write the condition checkeing code in {}
            () => { }  // write the on trigger code in {} if any. Remove line if no on trigger action needed
        );

        */


        /* STAGE 3: add states and transitions to the FSM 
         * ----------------------------------------------
            
        AddStates(...);

        AddTransition(sourceState, transition, destinationState);

         */

        Transition Discharged = new Transition("Discharged",
         () => { return blackboard.currentCharge <= blackboard.minCharge; }
     );

        Transition Charged = new Transition("Charged",
            () => { return blackboard.currentCharge >= blackboard.maxCharge; },
            () => { }
        );

        AddStates(FSM_ROOMBA, GOCHARGE);
        AddTransition(FSM_ROOMBA, Discharged, GOCHARGE);
        AddTransition(GOCHARGE, Charged, FSM_ROOMBA);



        /* STAGE 4: set the initial state
         
        initialState = ...*/



    }
}
