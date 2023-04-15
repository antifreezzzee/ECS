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
                ComponentType.ReadOnly<Transform>(),
                ComponentType.ReadOnly<CharacterData>(),
                ComponentType.ReadOnly<CharacterHealth>());
        }

        protected override void OnUpdate()
        {
            Entities.With(_moveQuery).ForEach(
                (Entity entity, ref InputData inputData, Transform transform, CharacterHealth characterHealth,
                    CharacterData characterData) =>
                {
                    var pos = transform.position;
                    var speed = characterData.MoveSpeed / 1000;
                    if (characterHealth.IsAlive)
                    {
                        var input = new Vector3(inputData.Move.x, 0, inputData.Move.y);
                        var lookDirection = pos + input;
                        pos += input * speed;
                        transform.position = pos;
                        transform.LookAt(lookDirection);
                    }
                });
        }
    }
}