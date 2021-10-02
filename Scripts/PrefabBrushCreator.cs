using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;

public static class PrefabBrushCreator
{
	[UnityEditor.MenuItem("Assets/Create Brush")]
	public static string GetSelectedPathOrFallback()
	{
		string path = "Assets";
		
		foreach (UnityEngine.Object obj in Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.Assets))
		{
			path = AssetDatabase.GetAssetPath(obj);

			if ( !string.IsNullOrEmpty(path) && File.Exists(path) ) 
			{
				string fileName = Path.GetFileNameWithoutExtension(path);
				
				string currentDirectory = Path.GetDirectoryName(path);
				
				PrefabBrush prefabBrush = ScriptableObject.CreateInstance<PrefabBrush>();
				prefabBrush.m_Prefabs = new GameObject[1];
				prefabBrush.m_Prefabs[0] = obj as GameObject;
				AssetDatabase.CreateAsset(prefabBrush,$"{currentDirectory}\\{fileName}.asset");
				AssetDatabase.SaveAssets();
				Debug.Log(currentDirectory);

			}
		}
		return path;
	}
}