using Components;
using Components.Interfaces;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Systems
{
    public class CharacterAnimationSystem : ComponentSystem
    {
        private EntityQuery _animationQuery;

        protected override void OnCreate()
        {
            _animationQuery = GetEntityQuery(ComponentType.ReadOnly<AnimationData>(), 
                ComponentType.ReadOnly<Animator>());
        }

        protected override void OnUpdate()
        {
            Entities.With(_animationQuery).ForEach(
                (Entity entity, ref InputData inputData, Animator animator, UserInputData userInputData) =>
                {
                    animator.SetBool("onWalk", 
                        Mathf.Abs(inputData.Move.x)  > 0.05 || Mathf.Abs(inputData.Move.y) > 0.05);
                    animator.SetFloat("WalkSpeed", 
                        userInputData.Speed * Vector2.Distance(Vector2.zero, new Vector2(inputData.Move.x,inputData.Move.y)));
                });
        }
    }
}