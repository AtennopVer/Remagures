﻿using Remagures.Model.DialogSystem;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Remagures.Root.DialogSystem
{
    public sealed class CurrentDialogPlayingFactory : SerializedMonoBehaviour, IAdditionalBehaviourFactory
    {
        [SerializeField] private DialogsListFactory _dialogsListFactory;
        [SerializeField] private DialogPlayerFactory _dialogPlayerFactory;
        private IAdditionalBehaviour _builtBehaviour;
        
        public IAdditionalBehaviour Create()
        {
            if (_builtBehaviour != null)
                    return _builtBehaviour;
            
            _builtBehaviour = new NextDialogPlaying(_dialogPlayerFactory.Create(), _dialogsListFactory.Create());
            return _builtBehaviour;
        }
    }
}