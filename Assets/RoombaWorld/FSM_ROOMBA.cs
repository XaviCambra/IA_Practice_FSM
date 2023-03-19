using FSMs;
using UnityEngine;
using Steerings;

[CreateAssetMenu(fileName = "FSM_ROOMBA", menuName = "Finite State Machines/FSM_ROOMBA", order = 1)]
public class FSM_ROOMBA : FiniteStateMachine
{
    private ROOMBA_Blackboard blackboard;

    public override void OnEnter()
    {
        
        base.OnEnter(); 
    }

    public override void OnExit()
    {
        
        base.OnExit();
    }

    public override void OnConstruction()
    {
        FiniteStateMachine ROOMBABASE = ScriptableObject.CreateInstance<ROOMBABASE>();
        ROOMBABASE.Name = "Roomba Base";

        State CLEANMODE = new State("Clean mode",
            () => { },
            () => { },
            () => { }
        );

        State CLEANPOO = new State("Clean Poo",
            () => { },
            () => { },
            () => { }
        );

       

        Transition PooDetected = new Transition("Poo Detected",
            () => { return; }, // write the condition checkeing code in {}
            () => { }  // write the on trigger code in {} if any. Remove line if no on trigger action needed
        );

        Transition PooCleaned = new Transition("Poo Cleaned",
           () => { return; }, // write the condition checkeing code in {}
           () => { }  // write the on trigger code in {} if any. Remove line if no on trigger action needed
       );

        Transition DustDetected = new Transition("Dust Detected",
            () => { return ; },
            () => { }
        );

        Transition DustCleaned = new Transition("Dust Cleaned",
            () => { return; },
            () => { }
        );


        AddStates(CLEANMODE, CLEANPOO);

        AddTransition(ROOMBABASE, PooDetected, CLEANPOO);
        AddTransition(CLEANPOO, PooCleaned, ROOMBABASE);
        AddTransition(CLEANMODE, DustCleaned, ROOMBABASE);
        

        initialState = ROOMBABASE; 
        
    }
}
