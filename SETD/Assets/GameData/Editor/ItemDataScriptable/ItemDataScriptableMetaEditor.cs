using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

#if UNITY_EDITOR
[CustomEditor(typeof(ItemDataScriptableMeta))]
public class ItemDataScriptableMetaEditor : Editor
{
    List<bool> foldout = new List<bool>();
    ItemDataScriptableMeta dataScriptable;
    ItemData newItem;
    PlayfabUploadCatalog uploadService = new PlayfabUploadCatalog();

    private void OnEnable()
    {
        dataScriptable = (ItemDataScriptableMeta)target;
        newItem = new ItemData();
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.Space();

        if (!dataScriptable || dataScriptable.items == null)
            return;

        var dataCount = dataScriptable.items.Count;
        var catalogName = dataScriptable.targetTable + " " + dataScriptable.tableVersion.GetVersion();

        EditorGUILayout.LabelField("Catalogs Version", EditorStyles.boldLabel);
        dataScriptable.targetTable = EditorGUILayout.TextField(dataScriptable.targetTable);
        EditorGUILayout.LabelField(catalogName, EditorStyles.boldLabel);
        EditorGUILayout.Space();

        if (dataCount <= 0)
            EditorGUI.BeginDisabledGroup(true);

        if (GUILayout.Button("Upload to Playfab"))
        {
            dataScriptable.tableVersion.subVersion++;
            var catalog = uploadService.SetupCatalogJson(dataScriptable.items);
            uploadService.UploadCatalog(catalogName, catalog);
        }

        EditorGUI.EndDisabledGroup();

        EditorGUILayout.Space();

        DrawCreatePanel();

        if (dataCount <= 0)
            return;

        EditorGUILayout.Space();

        EditorGUILayout.LabelField("Total Item: ", dataCount.ToString());

        EditorGUILayout.Space();
        DrawSortButtons();
        EditorGUILayout.Space();


        while (foldout.Count < dataCount)
        {
            foldout.Add(false);
        }
        while (foldout.Count > dataCount)
        {
            foldout.RemoveAt(foldout.Count - 1);
        }

        for (int i = 0; i < dataCount; i++)
        {
            var item = dataScriptable.items[i];
            foldout[i] = EditorGUILayout.Foldout(foldout[i], item.itemName);

            if (foldout[i])
            {
                EditorGUI.indentLevel++;
                DrawItemData(item);
                EditorGUI.indentLevel--;
            }
        }

    }

    private void DrawCreatePanel()
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.BeginVertical();
        EditorGUILayout.LabelField("Create New Item", EditorStyles.boldLabel);
        EditorGUILayout.Space();
        GUILayout.Label("Item Name");
        newItem.itemName = EditorGUILayout.TextArea(newItem.itemName, GUILayout.Width(300));
        newItem.id = dataScriptable.items.Count;
        GUILayout.Label("Item Price");
        newItem.price = EditorGUILayout.IntField(newItem.price, GUILayout.Width(150));
        newItem.isStackable = EditorGUILayout.ToggleLeft("Stackable?", newItem.isStackable);
        EditorGUI.BeginDisabledGroup(!newItem.isStackable);
        GUILayout.Label("Item Max Capacity");
        newItem.capacity = EditorGUILayout.IntField(newItem.isStackable ? newItem.capacity : 1, GUILayout.Width(150));
        EditorGUI.EndDisabledGroup();
        GUILayout.Label("Item Description");
        newItem.itemDesc = EditorGUILayout.TextArea(newItem.itemDesc, GUILayout.Width(300), GUILayout.Height(100));
        EditorGUILayout.Space();

        if (string.IsNullOrEmpty(newItem.itemName) || (newItem.isStackable && (newItem.capacity <= 0)))
        {
            EditorGUI.BeginDisabledGroup(true);
        }
        if (GUILayout.Button("Create"))
        {
            dataScriptable.AddItem(newItem);
            EditorUtility.SetDirty(dataScriptable);
            newItem = new ItemData();
        }
        EditorGUI.EndDisabledGroup();
        EditorGUILayout.EndVertical();
        EditorGUILayout.EndHorizontal();
    }

    private void DrawSortButtons()
    {
        EditorGUILayout.BeginHorizontal();
        foreach (var method in typeof(ItemData).GetFields())
        {
            if (method.Name == "id")
            {
                if (GUILayout.Button("Sort by Item ID"))
                {
                    dataScriptable.items = dataScriptable.items.OrderBy(method.GetValue).ToList();
                }
            }
            else if (method.Name == "itemName")
            {
                if (GUILayout.Button("Sort by Name"))
                {
                    dataScriptable.items = dataScriptable.items.OrderBy(method.GetValue).ToList();
                }
            }
            else if (method.Name == "price")
            {
                if (GUILayout.Button("Sort by Price"))
                {
                    dataScriptable.items = dataScriptable.items.OrderByDescending(method.GetValue).ToList();
                }
            }
            else if (method.Name == "capacity")
            {
                if (GUILayout.Button(method.Name))
                {
                    dataScriptable.items = dataScriptable.items.OrderByDescending(method.GetValue).ToList();
                }
            }
            else if (method.Name == "isStackable")
            {
                if (GUILayout.Button("Focus stackable item"))
                {
                    dataScriptable.items = dataScriptable.items.OrderByDescending(method.GetValue).ToList();
                }
            }
        }

        EditorGUILayout.EndHorizontal();
    }

    private void DrawItemData(ItemData item)
    {
        EditorGUILayout.BeginVertical("Box");

        EditorGUILayout.Space();

        foreach (var method in item.GetType().GetFields())
        {
            EditorGUILayout.LabelField(method.Name, method.GetValue(item).ToString());
            EditorGUILayout.Space();
        }

        if (GUILayout.Button("Delete"))
        {
            dataScriptable.DeleteItem(item);
            EditorUtility.SetDirty(dataScriptable);
        }

        EditorGUILayout.Space();

        EditorGUILayout.EndVertical();
    }

    private void UploadItemTable()
    {
        // Playfab calling here.
    }
}
#endif