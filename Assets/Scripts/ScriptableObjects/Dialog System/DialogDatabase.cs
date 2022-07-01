using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Database", menuName = "Dialogs/Database", order = 1)]
public class DialogDatabase : ScriptableObject
{
    [field: SerializeField] public string BaseNodeGUID { get; private set; }
    [field: SerializeField] public DialogContainer Container { get; private set; }

    [HideInInspector] public string CurrentNodeGUID;

    public List<DialogNodeLinkData> Links => Container.NodeLinks.FindAll(x => x.BaseNodeGUID == CurrentNodeGUID);
    public TextAsset CurrentDialog => Container.NodeData.Find(x => x.GUID == CurrentNodeGUID).dialog;
}