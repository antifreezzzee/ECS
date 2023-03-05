using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

namespace Components
{
    public static class Utils
    {
        public static List<Collider> GetAllColliders(this GameObject gameObject)
        {
            return gameObject == null ? null : gameObject.GetComponents<Collider>().ToList();
        }

        public static void ToWorldSpaceBox(this BoxCollider boxCollider, out float3 center, out float3 halfExtents,
            out quaternion orientation)
        {
            Transform transform = boxCollider.transform;
            orientation = transform.rotation;
            center = transform.TransformPoint(boxCollider.center);
            var lossyScale = transform.lossyScale;
            var scale = Abs(lossyScale);
            halfExtents = Vector3.Scale(scale, boxCollider.size) * 0.5f;
        }

        public static void ToWorldSpaceCapsule(this CapsuleCollider capsuleCollider, out float3 point0,
            out float3 point1, out float radius)
        {
            Transform transform = capsuleCollider.transform;
            var center = (float3) transform.TransformPoint(capsuleCollider.center);
            radius = 0;
            float height = 0;
            float3 lossyScale = transform.lossyScale;
            float3 dir = float3.zero;

            switch (capsuleCollider.direction)
            {
                case 0: //x
                    radius = Mathf.Max(lossyScale.y, lossyScale.z) * capsuleCollider.radius;
                    height = lossyScale.x * capsuleCollider.height;
                    dir = capsuleCollider.transform.TransformDirection(Vector3.right);
                    break;
                case 1: //y
                    radius = Mathf.Max(lossyScale.x, lossyScale.z) * capsuleCollider.radius;
                    height = lossyScale.y * capsuleCollider.height;
                    dir = capsuleCollider.transform.TransformDirection(Vector3.up);
                    break;
                case 2: //z
                    radius = Mathf.Max(lossyScale.x, lossyScale.y) * capsuleCollider.radius;
                    height = lossyScale.z * capsuleCollider.height;
                    dir = capsuleCollider.transform.TransformDirection(Vector3.forward);
                    break;
            }

            if (height < radius * 2)
            {
                dir = Vector3.zero;
            }

            point0 = center + dir * (height * 0.5f - radius);
            point1 = center - dir * (height * 0.5f - radius);
        }

        public static void ToWorldSpaceSphere(this SphereCollider sphereCollider, out float3 center, out float radius)
        {
            Transform transform = sphereCollider.transform;
            center = transform.TransformPoint(sphereCollider.center);
            radius = sphereCollider.radius * Max(Abs(transform.lossyScale));
        }

        private static float3 Abs(float3 v)
        {
            return new float3(Mathf.Abs(v.x), Mathf.Abs(v.y), Mathf.Abs(v.z));
        }

        private static float Max(float3 v)
        {
            return Math.Max(v.x, Mathf.Max(v.y, v.z));
        }
    }
}