﻿using Remagures.Model.MeatSystem;
using Remagures.View.MeatSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root
{
    public sealed class MeatCookerFactory : SerializedMonoBehaviour, IMeatCookerFactory
    {
        [SerializeField] private MeatCountView _meatCountView;
        [SerializeField] private CookedMeatHeapFactory _cookedMeatHeapFactory;
        private IMeatCooker _builtMeatCooker;

        public IMeatCooker Create()
        {
            if (_builtMeatCooker != null)
                return _builtMeatCooker;
                
            _builtMeatCooker = new MeatCooker(_meatCountView, _cookedMeatHeapFactory.Create());
            return _builtMeatCooker;
        }
    }
}