using BTs;
using UnityEngine;

[CreateAssetMenu(fileName = "BT_Mouse", menuName = "Behaviour Trees/BT_Mouse", order = 1)]
public class BT_Mouse : BehaviourTree
{
    public GoToTarget goToTarget;
    public BT_Mouse()
    {

    }

    public override void OnConstruction()
    {
        DynamicSelector dyn = new DynamicSelector();

        goToTarget = GetComponent<GoToTarget>();

        dyn.AddChild(new CONDITION_InstanceNear("roombaDetectionRadius", "roombaTag", "true", "roombaKey"),
            new Sequence(
                        new ACTION_MouseScared(),

                        new LambdaAction(() =>
                        {
                            MOUSE_Blackboard bl;
                            bl = (MOUSE_Blackboard)blackboard;
                            goToTarget.target = bl.NearestExitPoint();
                            return Status.SUCCEEDED;
                        }),
                        new RepeatUntilSuccessDecorator(
                            new Selector(
                                new Sequence(
                                    new LambdaCondition(() =>
                                    {
                                        return goToTarget.routeTerminated();
                                    }),
                                    new ACTION_Succeed()
                                ),
                                new Sequence(
                                    new ACTION_Fail()
                                )
                            )
                        ),
                        new ACTION_DebugLog("Asustado"),
                        new LambdaAction(() =>
                        {
                            Destroy(gameObject);
                            return Status.SUCCEEDED;
                        })
                        ));
        dyn.AddChild(new CONDITION_AlwaysTrue(),
            new Sequence(
                        new LambdaAction(() =>
                        {
                            MOUSE_Blackboard bl;
                            bl = (MOUSE_Blackboard)blackboard;
                            GameObject g = new GameObject();
                            g.transform.position = RandomLocationGenerator.RandomWalkableLocationOnScreen();
                            bl.Put("RandomLocation", g);
                            goToTarget.target = g;
                            return Status.SUCCEEDED;
                        }),
                        new RepeatUntilSuccessDecorator(
                            new Selector(
                                new Sequence(
                                    new LambdaCondition(() =>
                                    {
                                        return goToTarget.routeTerminated();
                                    }),
                                    new ACTION_Succeed()
                                ),
                                new Sequence(
                                    new ACTION_Fail()
                                )
                            )
                        ),
                        new LambdaAction(() =>
                        {
                            MOUSE_Blackboard bl;
                            bl = (MOUSE_Blackboard)blackboard;
                            GameObject poo = Instantiate(bl.pooPrefab);
                            poo.transform.position = gameObject.transform.position;
                            return Status.SUCCEEDED;
                        }),

                        new LambdaAction(() =>
                        {
                            MOUSE_Blackboard bl;
                            bl = (MOUSE_Blackboard)blackboard;
                            goToTarget.target = bl.RandomExitPoint();
                            return Status.SUCCEEDED;
                        }),
                        new RepeatUntilSuccessDecorator(
                            new Selector(
                                new Sequence(
                                    new LambdaCondition(() =>
                                    {
                                        return goToTarget.routeTerminated();
                                    }),
                                    new ACTION_Succeed()
                                ),
                                new Sequence(
                                    new ACTION_Fail()
                                )
                            )
                        ),
                        new ACTION_DebugLog("No asustado"),
                        new LambdaAction(() =>
                        {
                            MOUSE_Blackboard bl;
                            bl = (MOUSE_Blackboard)blackboard;
                            Destroy(bl.Get<GameObject>("RandomLocation"));
                            Destroy(gameObject);
                            return Status.SUCCEEDED;
                        })
                        ));

        root = dyn;

    }
}
