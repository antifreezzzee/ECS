using Components.Interfaces;
using Photon.Pun;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Components
{
    public class UserInputData : MonoBehaviour, IConvertGameObjectToEntity
    {
        [SerializeField] private MonoBehaviour shootAction;
        [SerializeField] private MonoBehaviour rushAction;

        private PhotonView _photonView;
        public MonoBehaviour ShootAction => shootAction;
        public MonoBehaviour RushAction => rushAction;

        
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            
            _photonView = PhotonView.Get(gameObject);
            if (_photonView.IsMine)
            {
                dstManager.AddComponentData(entity, new InputData());
            }
            
            dstManager.AddComponentData(entity, new MoveData());

            if (shootAction != null && shootAction is IAbility)
                dstManager.AddComponentData(entity, new ShootData());

            if (rushAction != null && rushAction is IAbility)
                dstManager.AddComponentData(entity, new RushData());

            dstManager.AddComponentData(entity, new AnimationData());
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
    }

    public struct ShootData : IComponentData
    {
        public float Shoot;
        public bool Ricochet;
    }

    public struct RushData : IComponentData
    {
        public float Rush;
    }

    public struct AnimationData : IComponentData
    {
    }
}