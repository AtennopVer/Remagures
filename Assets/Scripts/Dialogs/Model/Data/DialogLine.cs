﻿using System;
using System.Collections.Generic;

namespace Remagures.Dialogs.Model.Data
{
    public class DialogLine
    {
        public string Line { get; }
        public DialogSpeakerInfo SpeakerInfo { get; }
        
        public IReadOnlyList<DialogChoice> Choices { get; }
        public Action OnLineEnded { get; }
        
        public DialogLine(string line, DialogSpeakerInfo speakerInfo, IReadOnlyList<DialogChoice> choices)
        {
            Line = line ?? throw new ArgumentException("TextLine can't be null");
            SpeakerInfo = speakerInfo;
            
            Choices = choices ?? throw new ArgumentException("ChoicesList can't be null");
            OnLineEnded = () => { };
        }
    }
}