using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public struct InputComponent : IComponentData
{
    public Vector2 MousePos;
    public bool PressingLMB;
}
