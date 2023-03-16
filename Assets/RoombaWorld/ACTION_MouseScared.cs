using UnityEngine;
using BTs;

public class ACTION_MouseScared : Action
{
    
    /* Declare action parameters here. 
       All public parameters must be of type string. All public parameters must be
       regarded as keys in/for the blackboard context.
       Use prefix "key" for input parameters (information stored in the blackboard that must be retrieved)
       use prefix "keyout" for output parameters (information that must be stored in the blackboard)

       e.g.
       public string keyDistance;
       public string keyoutObject 
     */
    
    // construtor
    public ACTION_MouseScared()  { 
        /* Receive action parameters and set them */
    }
    
    /* Declare private attributes here */

    public override void OnInitialize()
    {
        /* write here the initialization code. Remember that initialization is executed once per ticking cycle */
           
    }

    public override Status OnTick ()
    {
        /* write here the code to be executed every time the action is ticked
           use blackboard.get<type>(keyParam) to retrieve information from the blackboard
           (where keyParam is one of the action parameters or a known key in the blackboard) 
           use blackboard.put(keyoutParam) to store information in the blackboard
           (where keyoutParam is one of the action parameters or a key in the blackboard)
        */
        MOUSE_Blackboard bl = (MOUSE_Blackboard)blackboard;

        gameObject.GetComponent<SpriteRenderer>().color = Color.green;

        bl.maxSpeed *= 2;
        bl.maxAcceleration *= 4;

        return Status.SUCCEEDED;

    }

    public override void OnAbort()
    {
        // write here the code to be executed if the action is aborted while running
    }

}
