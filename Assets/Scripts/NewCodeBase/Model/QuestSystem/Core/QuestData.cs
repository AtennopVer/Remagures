﻿using System;
using Remagures.Tools;

namespace Remagures.Model.QuestSystem
{
    public readonly struct QuestData
    {
        public readonly string Name;
        public readonly string Description;
        public readonly SerializableSprite QuestSprite;

        public QuestData(string name, string description, SerializableSprite questSprite)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Description = description ?? throw new ArgumentNullException(nameof(description));
            QuestSprite = questSprite ?? throw new ArgumentNullException(nameof(questSprite));
        }
    }
}