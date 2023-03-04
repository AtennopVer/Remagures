using System;
using Cysharp.Threading.Tasks;
using Remagures.Tools;
using UnityEngine;

namespace Remagures.Model.InventorySystem
{
    public sealed class WeaponItem : IWeaponItem
    {
        public AnimatorOverrideController AnimatorController { get; }
        public int UsingCooldownInMilliseconds { get; }
        public int Damage { get; }
        
        public string Name { get; }
        public string Description { get; }
        public Sprite Sprite { get; }
        public bool IsStackable { get; }
        
        public bool HasUsed { get; private set; }
        
        public WeaponItem(IItem item, AnimatorOverrideController animatorController, int damage, int usingCooldown)
        {
            Name = item.Name;
            Description = item.Description;
            Sprite = item.Sprite;
            IsStackable = item.IsStackable;

            AnimatorController = animatorController ?? throw new ArgumentNullException(nameof(animatorController));
            Damage = damage.ThrowExceptionIfLessOrEqualsZero();
            UsingCooldownInMilliseconds = usingCooldown.ThrowExceptionIfLessOrEqualsZero();
        }
        
        public async void Use()
        {
            HasUsed = true;
            await UniTask.Yield();
            HasUsed = false;
        }
    }
}
