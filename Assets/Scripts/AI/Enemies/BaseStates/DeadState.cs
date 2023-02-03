﻿using UnityEngine;

namespace Remagures.AI.Enemies
{
    public class DeadState : IState
    {
        private readonly IEnemy _enemy;
        private readonly int DEAD_ANIMATOR_NAME = Animator.StringToHash("dead");

        public DeadState(IEnemy enemy)
        {
            _enemy = enemy;
        }

        public void OnEnter()
        {
            _enemy.Animations.Animator.logWarnings = false;
            _enemy.Movement.StopMoving();
            
            if (_enemy.Animations.Animator.GetBool(DEAD_ANIMATOR_NAME))
                _enemy.Animations.Animator.SetBool(DEAD_ANIMATOR_NAME, true);
        }
        
        public void Tick() { }
        public void OnExit() { }
    }
}