using UnityEngine;
using System.Collections;
using UnityEditor;

public class prefab_creator : MonoBehaviour {

	[MenuItem("Prefab/Create Prefab")]
	public static void prefaFromFbx() {
		foreach(var i in Selection.gameObjects) {
			var name = i.name;
				Object prefab = EditorUtility.CreateEmptyPrefab("Assets/Prefabs/" + name+"_Prefab" + ".prefab");
				EditorUtility.ReplacePrefab(i, prefab);
				AssetDatabase.Refresh();
		}
	}
	
}
