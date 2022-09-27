using System;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

[GenerateAuthoringComponent]
public struct ECBSingletonComponentTest : IComponentData
{
    public SchedulingTypeTest SchedulingType;
    public int spawnAmount;
    public Entity prefabTospawn;
}


public enum SchedulingTypeTest{
    Run,
    Schedule,
    ScheduleParallel
}