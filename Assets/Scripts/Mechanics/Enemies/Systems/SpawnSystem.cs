using Leopotam.Ecs;
using System;
using UnityEngine;

class SpawnSystem : IEcsRunSystem, IEcsInitSystem
{
    private EcsFilter<Enemy> enemyFilter;
    private StaticData staticData;
    private EcsWorld world;
    private PoolSystem pool;

    private int maxTop,
        maxRight;

    public void Init()
    {
        maxTop = staticData.FieldSize.y / 2;
        maxRight = staticData.FieldSize.x / 2;
    }

    public void Run()
    {
        if (enemyFilter.GetEntitiesCount() < staticData.MaxEnemies)
        {
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        EcsEntity enemyEntity = world.NewEntity();
        ref Enemy enemy = ref enemyEntity.Get<Enemy>();
        enemy.EnemyType = (StaticData.EnemyType)
            UnityEngine.Random.Range(0, Enum.GetNames(typeof(StaticData.EnemyType)).Length);

        ref ObjectComponent enemyObj = ref enemyEntity.Get<ObjectComponent>();
        enemyEntity.AddObjectComp(staticData, pool, StaticData.UnitType.Enemy);
        enemyObj.ObTransform.position = GetSpawnPoint(enemy.EnemyType);

        enemyEntity.AddMovable(staticData);

        UnitData unit = staticData.EnemiesData[(int)enemy.EnemyType];

        enemyEntity.AddAttacker(staticData);
    }

    private Vector2 GetSpawnPoint(StaticData.EnemyType enemy)
    {
        bool fromVertical = UnityEngine.Random.value > 0.5f;
        if (fromVertical)
        {
            bool fromTop = UnityEngine.Random.value > 0.5f;
            return new Vector2(
                UnityEngine.Random.Range(-maxRight, maxRight),
                fromTop ? maxTop - 1 : -maxTop
            );
        }
        else
        {
            bool fromRight = UnityEngine.Random.value > 0.5f;
            return new Vector2(
                fromRight ? maxRight - 1 : -maxRight,
                UnityEngine.Random.Range(-maxTop, maxTop)
            );
        }
    }
}
