using Components;
using Components.Interfaces;
using Unity.Entities;
using UnityEngine;

namespace Systems
{
    public class AIEvaluateSystem : ComponentSystem
    {
        private EntityQuery _evaluateQuery;

        protected override void OnCreate()
        {
            _evaluateQuery = GetEntityQuery(ComponentType.ReadOnly<AIAgent>());
        }

        protected override void OnUpdate()
        {
            Entities.With(_evaluateQuery).ForEach(
                (Entity entity, BehaviourManager behaviourManager) =>
                {
                    IBehaviour bestBehaviour;
                    float highScore = float.MinValue;

                    behaviourManager.ActiveBehaviour = null;
                    
                    foreach (var behaviour in behaviourManager.Behaviours)
                    {
                        if (behaviour is IBehaviour ai)
                        {
                            var currentScore = ai.Evaluate();
                            if (currentScore > highScore)
                            {
                                highScore = currentScore;
                                behaviourManager.ActiveBehaviour = ai;

                            }
                        }
                    }
                    
                    Debug.Log(behaviourManager.ActiveBehaviour);
                });
        }
    }
}