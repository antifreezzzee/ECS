using Components;
using Unity.Entities;
using UnityEngine;

namespace Systems
{
    public class CharacterMoveSystem : ComponentSystem
    {
        private EntityQuery _moveQuery;

        protected override void OnCreate()
        {
            _moveQuery = GetEntityQuery(ComponentType.ReadOnly<InputData>(),
                ComponentType.ReadOnly<MoveData>(), ComponentType.ReadOnly<Transform>());
        }

        protected override void OnUpdate()
        {
            Entities.With(_moveQuery).ForEach(
                (Entity entity, Transform transform, ref InputData inputData, ref MoveData moveData) =>
                {
                    var pos = transform.position;
                    var input = new Vector3(inputData.Move.x, 0, inputData.Move.y);
                    Vector3 lookDirection = pos + input;
                    pos += input * moveData.Speed;
                    transform.position = pos;
                    transform.LookAt(lookDirection);
                });
        }
    }
}