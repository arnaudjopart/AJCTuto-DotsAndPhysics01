using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

[GenerateAuthoringComponent]
public struct AttractedBody : IComponentData
{

    public float3 m_attractionPoint;
    public float m_strength;
    public bool m_isActive;
}
