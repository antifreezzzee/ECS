using System.Collections.Generic;
using UnityEngine;

namespace Components.Interfaces
{
    public interface ITargetedAbility : IAbility
    {
        List<GameObject> Targets { get; set; }
    }
}