using UnityEngine;
using BTs;

[CreateAssetMenu(fileName = "BT_Mouse", menuName = "Behaviour Trees/BT_Mouse", order = 1)]
public class BT_Mouse : BehaviourTree
{
    
    public BT_Mouse()  { 
        
    }
    
    public override void OnConstruction()
    {
        DynamicSelector dyn = new DynamicSelector();

        dyn.AddChild(new CONDITION_InstanceNear("roombaDetectionRadius", "roombaTag", "true", "roombaKey"),
            new Sequence(
                        new ACTION_MouseScared(),
                        new LambdaAction(() =>
                        {
                            MOUSE_Blackboard bl;
                            bl = (MOUSE_Blackboard)blackboard;
                            bl.Put("NearestExit", bl.NearestExitPoint());
                            return Status.SUCCEEDED;
                        }),//("NearestExitPoint"),
                        new ACTION_Arrive("NearestExit"),
                        new ACTION_Deactivate("MOUSE")
                        ));
        dyn.AddChild(new CONDITION_AlwaysTrue(),
            new Sequence(
                        new LambdaAction(() =>
                        {
                            MOUSE_Blackboard bl;
                            bl = (MOUSE_Blackboard)blackboard;
                            // NO SÉ COMO METER EL RANDOMWALKABLELOCATION AQUÍ bl.Put("RandomLocation", bl.);
                            return Status.SUCCEEDED;
                        }),
                        new ACTION_Arrive("RandomLocation"),
                        new ACTION_Activate("pooPrefab"),
                        new ACTION_Arrive("RandomExitPoints")
                        )); 

    }
}
