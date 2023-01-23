using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public static class DataEditor
{
#if UNITY_EDITOR
    [MenuItem("Tools/Database")]
    public static void CreateDataBase()
    {
        DataBases bases;

        if (!AssetDatabase.IsValidFolder("Assets/Resources"))
            AssetDatabase.CreateFolder("Assets", "Resources");

        if (!AssetDatabase.IsValidFolder("Assets/Resources/Scriptables"))
            AssetDatabase.CreateFolder("Assets/Resources", "Scriptables");


        bases = AssetDatabase.LoadAssetAtPath<DataBases>("Assets/Resources/Scriptables/DataBases.asset");

        if (bases == null)
        {
            bases = ScriptableObject.CreateInstance<DataBases>();
            AssetDatabase.CreateAsset(bases, "Assets/Resources/Scriptables/DataBases.asset");
        }

        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();
        Selection.activeObject = bases;
    }
#endif
}
