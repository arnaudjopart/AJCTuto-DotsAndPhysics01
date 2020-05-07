using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;
using UnityEngine;

public class AttractionSystem : ComponentSystem
{
   

    protected override void OnUpdate()
    {
        Entities.WithAll(typeof(AttractedBody)).ForEach((ref PhysicsVelocity _velocity, ref AttractedBody
            _attractedBody, ref Translation _translation) =>
        {
            if (_attractedBody.m_isActive)
            {
                var diff = _attractedBody.m_attractionPoint - _translation.Value;
                float dist = math.length(diff);
                _velocity.Linear += _attractedBody.m_strength * (diff / dist);
            }
        });
    }
}
