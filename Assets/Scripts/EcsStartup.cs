using Leopotam.Ecs;
using UnityEngine;

public class EcsStartup : MonoBehaviour
{
    EcsWorld world;
    EcsSystems systems;

    private StaticData staticData;
    private PoolSystem pool;

    public void StartGame(StaticData staticData, PoolSystem pool)
    {
        this.staticData = staticData;
        this.pool = pool;
        world = new EcsWorld();
        systems = new EcsSystems(world);
        systems.Init();
    }

    void Update()
    {
        // ��������� ��� ������������ �������.
        systems.Run();
    }

    void OnDestroy()
    {
        // ���������� ������������ �������.
        systems.Destroy();
        // ������� ���������.
        world.Destroy();
    }
}
