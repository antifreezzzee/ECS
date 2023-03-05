using Components.Interfaces;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Components
{
    public class UserInputData : MonoBehaviour, IConvertGameObjectToEntity
    {
        public float speed;
        public MonoBehaviour shootAction;
        public MonoBehaviour rushAction;

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            dstManager.AddComponentData(entity, new InputData());
            dstManager.AddComponentData(entity, new MoveData
            {
                Speed = speed / 1000
            });

            if (shootAction != null && shootAction is IAbility)
                dstManager.AddComponentData(entity, new ShootData());

            if (rushAction != null && rushAction is IAbility)
                dstManager.AddComponentData(entity, new RushData());
        }
    }

    public struct InputData : IComponentData
    {
        public float2 Move;
        public float Shoot;
        public float Rush;
    }

    public struct MoveData : IComponentData
    {
        public float Speed;
    }

    public struct ShootData : IComponentData
    {
        public float Shoot;
    }

    public struct RushData : IComponentData
    {
        public float Rush;
    }
}