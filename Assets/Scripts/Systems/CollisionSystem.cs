using System;
using Components;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Systems
{
    public class CollisionSystem : ComponentSystem
    {
        private EntityQuery _collisionQuery;

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
                    Collider[] collidersInCollision = new Collider[50];
                    collisionAbility.CollidersInCollision.Clear();

                    var collisions = 0;

                    switch (actorColliderData.ColliderType)
                    {
                        case ColliderType.Sphere:
                            collisions = Physics.OverlapSphereNonAlloc(actorColliderData.SphereCenter + position,
                                actorColliderData.SphereRadius, collidersInCollision);
                            break;
                        case ColliderType.Capsule:
                            var center = (actorColliderData.CapsuleStart + position + (actorColliderData.CapsuleEnd +
                                position)) / 2;
                            var point1 = actorColliderData.CapsuleStart + position;
                            var point2 = actorColliderData.CapsuleEnd + position;
                            point1 = (float3) (rotation * (point1 - center)) + center;
                            point2 = (float3) (rotation * (point2 - center)) + center;
                            collisions = Physics.OverlapCapsuleNonAlloc(point1, point2, actorColliderData.CapsuleRadius,
                                collidersInCollision);
                            break;
                        case ColliderType.Box:
                            collisions = Physics.OverlapBoxNonAlloc(actorColliderData.BoxCenter + position,
                                actorColliderData.BoxHalfExtents, collidersInCollision,
                                actorColliderData.BoxOrientation * rotation);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    if (collisions > 0)
                    {
                        foreach (var collider in collidersInCollision)
                            collisionAbility.CollidersInCollision?.Add(collider);
                        collisionAbility.Execute();
                    }
                });
        }
    }
}