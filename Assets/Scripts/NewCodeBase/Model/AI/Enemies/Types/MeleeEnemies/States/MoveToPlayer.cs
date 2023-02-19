using Remagures.Model.AI.StateMachine;
using UnityEngine;

namespace Remagures.Model.AI.Enemies.MeleeEnemies
{
    public sealed class MoveToPlayer : IState
    {
        private readonly MeleeEnemy _meleeEnemy;
        private static readonly int IS_STAYING_ANIMATOR_NAME = Animator.StringToHash("isStaying");

        public MoveToPlayer(MeleeEnemy meleeEnemy)
        {
            _meleeEnemy = meleeEnemy;
        }
        
        public void OnEnter() { }
        public void OnExit() { }

        public void Update()
        {
            _meleeEnemy.Movement.Move(_meleeEnemy.TargetData.Target);
            _meleeEnemy.Animations.Animator.SetBool(IS_STAYING_ANIMATOR_NAME, false);
        }
    }
}