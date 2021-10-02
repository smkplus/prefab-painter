using System;
using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;
using PrefabPainter;

public static class PaletteCreator
{
	static string currentDirectory;
	static string fileName;
	[UnityEditor.MenuItem("Assets/Create Palette")]
	public static string GetSelectedPathOrFallback()
	{

		string path = "Assets";
		PaintPalette paintPalette = ScriptableObject.CreateInstance<PaintPalette>();

		foreach (UnityEngine.Object obj in Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.Assets))
		{
			path = AssetDatabase.GetAssetPath(obj);

			if ( !string.IsNullOrEmpty(path) && File.Exists(path) ) 
			{
				fileName = Path.GetFileNameWithoutExtension(path);
				
				currentDirectory = Path.GetDirectoryName(path);
				
				PrefabBrush prefabBrush = ScriptableObject.CreateInstance<PrefabBrush>();
				prefabBrush.m_Prefabs = new GameObject[1];
				prefabBrush.m_Prefabs[0] = obj as GameObject;
				
				paintPalette.palette.Add(new PaintObject(prefabBrush));
				
				AssetDatabase.CreateAsset(prefabBrush,$"{currentDirectory}\\{fileName}.asset");
				AssetDatabase.SaveAssets();
				Debug.Log(currentDirectory);

			}

		}

		var directoryInfo = new DirectoryInfo(path).Parent;
		if (directoryInfo != null)
		{
			string folderName = directoryInfo.Name;
			AssetDatabase.CreateAsset(paintPalette,$"{currentDirectory}\\{folderName} palette.asset");
		}
		else
		{
			AssetDatabase.CreateAsset(paintPalette,$"{currentDirectory}\\{new Guid()} palette.asset");
		}


		return path;
	}
}