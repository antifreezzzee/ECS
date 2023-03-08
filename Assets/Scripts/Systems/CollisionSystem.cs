using System;
using System.Collections.Generic;
using Components;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Systems
{
    public class CollisionSystem : ComponentSystem
    {
        private EntityQuery _collisionQuery;
        
        private Collider[] _colliders = new Collider[50];

        protected override void OnCreate()
        {
            _collisionQuery = GetEntityQuery(ComponentType.ReadOnly<ActorColliderData>(),
                ComponentType.ReadOnly<Transform>());
        }

        protected override void OnUpdate()
        {
            Entities.With(_collisionQuery).ForEach(
                (Entity entity, CollisionAbility collisionAbility, ref ActorColliderData actorColliderData) =>
                {
                    var gameObject = collisionAbility.gameObject;
                    float3 position = gameObject.transform.position;
                    var rotation = gameObject.transform.rotation;

                    collisionAbility.Collisions?.Clear();

                    var size = 0;

                    switch (actorColliderData.ColliderType)
                    {
                        case ColliderType.Sphere:
                            size = Physics.OverlapSphereNonAlloc(actorColliderData.SphereCenter + position,
                                actorColliderData.SphereRadius, _colliders);
                            break;
                        case ColliderType.Capsule:
                            var center = (actorColliderData.CapsuleStart + position + (actorColliderData.CapsuleEnd +
                                position)) / 2;
                            var point1 = actorColliderData.CapsuleStart + position;
                            var point2 = actorColliderData.CapsuleEnd + position;
                            point1 = (float3) (rotation * (point1 - center)) + center;
                            point2 = (float3) (rotation * (point2 - center)) + center;
                            size = Physics.OverlapCapsuleNonAlloc(point1, point2, actorColliderData.CapsuleRadius,
                                _colliders);
                            break;
                        case ColliderType.Box:
                            size = Physics.OverlapBoxNonAlloc(actorColliderData.BoxCenter + position,
                                actorColliderData.BoxHalfExtents, _colliders,
                                actorColliderData.BoxOrientation * rotation);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    if (size > 0)
                    {
                        foreach (var result in _colliders)
                        {
                            collisionAbility.Collisions?.Add(result);
                        }
                        
                        collisionAbility.Execute();
                    }
                });
        }
    }
}