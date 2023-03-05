using System.Collections.Generic;
using UnityEngine;

namespace Components.Interfaces
{
    public interface IAbilityTarget : IAbility
    {
        List<GameObject> Targets { get; set; }
    }
}