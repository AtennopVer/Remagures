﻿using System;
using UnityEngine;

namespace Remagures.Model
{
    public sealed class DisableObjectButton : IButton
    {
        private readonly GameObject _gameObject;

        public DisableObjectButton(GameObject gameObject) 
            => _gameObject = gameObject ?? throw new ArgumentNullException(nameof(gameObject));

        public void Press()
            => _gameObject.SetActive(false);
    }
}