using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial class IJobSpawner : SystemBase
{
    protected override void OnCreate()
    {
        RequireSingletonForUpdate<ECBSingletonComponentTest>();
    }

    protected override void OnUpdate()
    {
        Enabled = false;
        var ecb = new EntityCommandBuffer(World.UpdateAllocator.ToAllocator);

        SpawnerJobUnique spawnerJob = new SpawnerJobUnique { deltaTime = Time.DeltaTime, spawnAmount = 500, ecbJob = ecb };
        JobHandle spawnerJobhandle = spawnerJob.Schedule();

        spawnerJobhandle.Complete();
        ecb.Playback(EntityManager);
    }
}


public partial struct SpawnerJobUnique : IJobEntity
{
    public float deltaTime;
    public int spawnAmount;
    public EntityCommandBuffer ecbJob;
    public void Execute(Entity entity, in ECBSingletonComponentTest ecbSingleton)
    {
        for (int i = 0; i < ecbSingleton.spawnAmount; i++)
        {
            for (int j = 0; j < ecbSingleton.spawnAmount; j++)
            {
                var e = ecbJob.Instantiate(ecbSingleton.prefabTospawn);
                ecbJob.SetComponent(e, new Translation { Value = new float3(i * 2, 0, j * 2) });
            }
        }
    }
}