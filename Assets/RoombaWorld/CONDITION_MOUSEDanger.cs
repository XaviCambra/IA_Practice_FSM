using UnityEngine;
using BTs;

public class CONDITION_MOUSE_Danger : Condition
{
    public string keyroomba;
    public string keyroombaDetectionRadius;

    /* Declare function parameters here. 
       All public parameters must be of type string. All public parameters must be
       regarded as keys in/for the blackboard context.
       Use prefix "key" for input parameters (information stored in the blackboard that must be retrieved)
       use prefix "keyout" for output parameters (information that must be stored in the blackboard)
    

       e.g.
       public string keyDistance;
       public string keyoutObject 
     */

    // Constructor
    public CONDITION_MOUSE_Danger(string keyexitPoints, string keyroombaDetectionRadius)  {
        /* Receive function parameters and set them */
        this.keyroomba = keyroomba;
        this.keyroombaDetectionRadius = keyroombaDetectionRadius;
    }

    // optional
    public override void OnInitialize()
    {
        roomba = blackboard.Get<GameObject>(keyroomba);
        roombaDetectionRadius = blackboard.Get<GameObject>(keyroombaDetectionRadius);
        lastTick = false;
        /* implement this method for conditions that have state and/or
        for conditions that may be called many times in the same ticking cycle
        (e.g. conditions in dynamic selectors). 
        If in doubt, do not implement it. Do all the work in Check */
    }

    public override bool Check ()
    {
        /* Add your code here */
        if (SensingUtils.DistanceToTarget(gameObject, roomba) <= keyroombaDetectionRadius) { lastTick = true; }
        if (SensingUtils.DistanceToTarget(gameObject, roomba) > keyroombaDetectionRadius) { lastTick = false; }

        return lastTick;
    }

}
