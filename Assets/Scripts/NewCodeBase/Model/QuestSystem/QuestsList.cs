using System;
using System.Collections.Generic;
using Remagures.View.QuestSystem;

namespace Remagures.Model.QuestSystem
{
    public class QuestsList : IQuestsList
    {
        public IReadOnlyList<IQuest> Quests => _quests;
        
        private readonly List<IQuest> _quests = new();
        private QuestPopups _questPopups;
        
        public QuestsList(QuestPopups questPopups)
            => _questPopups = questPopups ?? throw new ArgumentNullException(nameof(questPopups));

        public void AddQuest(IQuest quest)
        {
            if (!CanAddQuest(quest))
                throw new InvalidOperationException("Can't add this quest to list");
        
            _quests.Add(quest);
            _questPopups.AddTextToQueue("Новый квест: " + quest.Data.Name);
        }

        public bool CanAddQuest(IQuest quest)
        {
            if (quest == null)
                throw new ArgumentNullException(nameof(quest));
            
            return !_quests.Contains(quest) && !quest.IsCompleted;
        }

        public void RemoveQuest(IQuest quest)
        {
            if (!CanRemoveQuest(quest)) 
                throw new InvalidOperationException("Can't remove this quest from list");
        
            _quests.Remove(quest);
        }

        public bool CanRemoveQuest(IQuest quest)
            => quest.IsCompleted;
    }
}