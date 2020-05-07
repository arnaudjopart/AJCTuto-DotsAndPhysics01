using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Jobs;
using Unity.Physics;
using Unity.Physics.Systems;
using Unity.Transforms;
using UnityEngine;

public class TriggerDetectionJobSystem : JobComponentSystem
{
    private BuildPhysicsWorld m_buildPhysicsWorld;
    private StepPhysicsWorld m_stepPhysicsWorld;

    protected override void OnCreate()
    {
        m_buildPhysicsWorld = World.GetOrCreateSystem<BuildPhysicsWorld>();
        m_stepPhysicsWorld = World.GetOrCreateSystem<StepPhysicsWorld>();
    }
    
    private struct TriggerDetection : ITriggerEventsJob
    {
        public ComponentDataFromEntity<AttractedBody> m_attractedComponentDataFromEntity;
        
        public void Execute(TriggerEvent _triggerEvent)
        {
            if (m_attractedComponentDataFromEntity.HasComponent(_triggerEvent.Entities.EntityA))
            {
                var attractedBody = m_attractedComponentDataFromEntity[_triggerEvent.Entities.EntityA];
                attractedBody.m_isActive = true;
                m_attractedComponentDataFromEntity[_triggerEvent.Entities.EntityA] = attractedBody;
            }
            
            if (m_attractedComponentDataFromEntity.HasComponent(_triggerEvent.Entities.EntityB))
            {
                var attractedBody = m_attractedComponentDataFromEntity[_triggerEvent.Entities.EntityB];
                attractedBody.m_isActive = true;
                m_attractedComponentDataFromEntity[_triggerEvent.Entities.EntityB] = attractedBody;
            }
        }
    }

    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        var job = new TriggerDetection
        {
            m_attractedComponentDataFromEntity = GetComponentDataFromEntity<AttractedBody>()
        };
        return job.Schedule(m_stepPhysicsWorld.Simulation, ref m_buildPhysicsWorld.PhysicsWorld, inputDeps);
    }
}
