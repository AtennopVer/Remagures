﻿using System;
using UnityEngine;

namespace Remagures.Dialogs.Model.Data
{
    public struct DialogSpeakerInfo
    {
        public string SpeakerName { get; }
        public Sprite SpeakerSprite { get; }
        public DialogLayoutType LayoutType { get; }
        
        public DialogSpeakerInfo(string speakerName, Sprite speakerSprite, DialogLayoutType layoutType)
        {
            SpeakerName = speakerName ?? throw new ArgumentException("SpeakerName can't be null");
            SpeakerSprite = speakerSprite ? speakerSprite : throw new ArgumentException("Sprite can't be null");
            LayoutType = layoutType;
        }
    }
}