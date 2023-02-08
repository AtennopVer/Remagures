﻿using Remagures.Components;
using Remagures.Model.Health;
using SM = Remagures.AI.StateMachine.StateMachine;

namespace Remagures.AI.Enemies
{
    public interface IEnemy
    {
        IEnemyMovement Movement { get; }
        Health Health { get; }
        
        EnemyAnimations Animations { get; }
        SM StateMachine { get; }
    }
}