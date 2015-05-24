using UnityEngine;
using UnityEditor;
using System.Collections;

[InitializeOnLoad]
public class BuildBatch {
	private static string[] scene = {
		"Assets/Resources/Scene/StartGame.unity",
		"Assets/Resources/Scene/Main.unity"
	};
	
	static bool BuildiOS() {
		Debug.Log ("============= START BUILDING IOS ==============");
		BuildOptions opt = BuildOptions.SymlinkLibraries;

		string errorMessage = BuildPipeline.BuildPlayer(scene, "Exports/iOS", BuildTarget.iOS, opt);

		if (string.IsNullOrEmpty(errorMessage)) {
			Debug.Log ("BUILD SUCCESS");
			return true;
		}

		Debug.LogError(errorMessage);
		return false;
	}

	static bool BuildAndroid() {
		Debug.Log ("============= START BUILDING ANDROID ==============");

		BuildOptions opt = BuildOptions.None | BuildOptions.AcceptExternalModificationsToPlayer;
		string errorMessage = BuildPipeline.BuildPlayer(scene, "Exports/Android", BuildTarget.Android, opt);

		if (string.IsNullOrEmpty(errorMessage)) {
			Debug.Log ("BUILD SUCCESS");
			return true;
		}

		Debug.LogError(errorMessage);
		return false;
	}
}
