using Components;
using Components.Interfaces;
using Unity.Entities;

namespace Systems
{
    public class CharacterRushSystem : ComponentSystem
    {
        private EntityQuery _rushQuery;

        protected override void OnCreate()
        {
            _rushQuery = GetEntityQuery(ComponentType.ReadOnly<InputData>(),
                ComponentType.ReadOnly<RushData>(), ComponentType.ReadOnly<UserInputData>());
        }

        protected override void OnUpdate()
        {
            Entities.With(_rushQuery).ForEach(
                (Entity entity, UserInputData userInputData, ref InputData inputData) =>
                {
                    if (inputData.Rush > 0f && userInputData.RushAction != null &&
                        userInputData.RushAction is IAbility ability)
                        ability.Execute();
                });
        }
    }
}