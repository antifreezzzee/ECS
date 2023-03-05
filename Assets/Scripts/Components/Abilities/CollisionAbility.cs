using System.Collections.Generic;
using Components.Interfaces;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Components
{
    public class CollisionAbility : MonoBehaviour, IConvertGameObjectToEntity, ICollisionAbility
    {
        public Collider Collider;
        public List<Collider> Collisions { get; set; }
        
        public void Execute()
        {
            
        }

        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            float3 position = gameObject.transform.position;
            switch (Collider)
            {
                case SphereCollider sphereCollider:
                    sphereCollider.ToWorldSpaceSphere(out var sphereCenter, out var sphereRadius);
                    dstManager.AddComponentData(entity, new ActorColliderData
                    {
                        ColliderType = ColliderType.Sphere,
                        SphereCenter = sphereCenter - position,
                        SphereRadius = sphereRadius,
                        InitialTakeOf = true
                    });
                    break;
                case CapsuleCollider capsuleCollider:
                    capsuleCollider.ToWorldSpaceCapsule(out var capsuleStart, out var capsuleEnd,
                        out var capsuleRadius);
                    dstManager.AddComponentData(entity, new ActorColliderData
                    {
                        ColliderType = ColliderType.Capsule,
                        CapsuleStart = capsuleStart - position,
                        CapsuleEnd = capsuleEnd - position,
                        CapsuleRadius = capsuleRadius,
                        InitialTakeOf = true
                    });
                    break;
                case BoxCollider boxCollider:
                    boxCollider.ToWorldSpaceBox(out var boxCenter, out var boxHalfExtents, out var boxOrientation);
                    dstManager.AddComponentData(entity, new ActorColliderData
                    {
                        ColliderType = ColliderType.Box,
                        BoxCenter = boxCenter - position,
                        BoxHalfExtents = boxHalfExtents,
                        BoxOrientation = boxOrientation,
                        InitialTakeOf = true
                    });
                    break;
            }

            Collider.enabled = false;
        }

    }

    public struct ActorColliderData : IComponentData
    {
        public ColliderType ColliderType;
        public float3 SphereCenter;
        public float SphereRadius;
        public float3 CapsuleStart;
        public float3 CapsuleEnd;
        public float CapsuleRadius;
        public float3 BoxCenter;
        public float3 BoxHalfExtents;
        public quaternion BoxOrientation;
        public bool InitialTakeOf;
    }

    public enum ColliderType
    {
        Sphere = 0,
        Capsule = 1,
        Box = 2
    }
}