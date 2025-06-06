using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
public partial class AgentDetectItemWithTagAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Agent;
    [SerializeReference] public BlackboardVariable<GameObject> Item;

    private Sensor _sensor;

    protected override Status OnStart()
    {
        if (Agent.Value == null)
        {
            LogFailure("Agent is not assigned.");
            return Status.Failure;
        }

        _sensor = Agent.Value.GetComponent<Sensor>();
        if (_sensor == null)
        {
            LogFailure("Sensor component not found on Agent.");
            return Status.Failure;
        }

        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        var newItem = _sensor.ScanArea();
        if (newItem == null)
        {
            return Status.Running;
        }

        // Store detected GameObject in the blackboard
        Item.Value = newItem;
        return Status.Success;
    }

    protected override void OnEnd() { }

    protected void OnReset()
    {
        _sensor = null;
    }
}