﻿using System;
using BehaviorDesigner.Runtime;

namespace Remagures.Model.Character.BehaviourTree
{
    [Serializable]
    public sealed class SharedMagicApplier : SharedVariable<CharacterMagicApplier>
    {
        public static implicit operator SharedMagicApplier(CharacterMagicApplier value) 
            => new() { Value = value };
    }
}