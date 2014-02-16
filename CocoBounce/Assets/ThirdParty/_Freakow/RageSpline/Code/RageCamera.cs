#if !UNITY_FLASH && !UNITY_WEBPLAYER
using System.IO;
#endif
#if UNITY_EDITOR
using UnityEditor;
#endif
using System;
using UnityEngine;

[System.Serializable]
[ExecuteInEditMode]
public partial class RageCamera : MonoBehaviour {

    private static bool _farseerToggleInProgress;
    [SerializeField]private Camera _camera;
	private bool _started;

    public static bool FarseerEnabled {
        get { return RageFarseerAdapter.FarseerEnabled; }
        set { if (value != RageFarseerAdapter.FarseerEnabled && !_farseerToggleInProgress) ToggleFarseer(); }
    }

    public void OnEnable() {
		if (_started) return;
		_camera = camera;
		_camera.transparencySortMode = TransparencySortMode.Orthographic;
		_started = true;
	}

    private static void ToggleFarseer() {
        _farseerToggleInProgress = true;
#if UNITY_EDITOR && !UNITY_WEBPLAYER && !UNITY_FLASH

        Debug.Log("RageSpline Farseer state toggled: Please wait for compilation to finish");

        try {
            bool enablingFarseer = !FarseerEnabled;
            FileUtil.MoveFileOrDirectory("Assets/_Freakow/RageSpline/Code/RageFarseerAdapter.cs", "Assets/_Freakow/RageSpline/Code/temp.cs");
            FileUtil.MoveFileOrDirectory("Assets/_Freakow/RageSpline/Code/RageFarseerAdapter.bak", "Assets/_Freakow/RageSpline/Code/RageFarseerAdapter.cs");
            FileUtil.MoveFileOrDirectory("Assets/_Freakow/RageSpline/Code/temp.cs", "Assets/_Freakow/RageSpline/Code/RageFarseerAdapter.bak");
            var rootFolder = Application.dataPath.Substring(0, Application.dataPath.Length - 6);    // Removing "Assets" from the end
            if (enablingFarseer)
                FileUtil.MoveFileOrDirectory(rootFolder + "RageFarseer", "Assets/_ThirdParty/RageFarseer");
            else
                FileUtil.MoveFileOrDirectory("Assets/_ThirdParty/RageFarseer", rootFolder + "RageFarseer");
            AssetDatabase.Refresh(ImportAssetOptions.ForceUpdate);
            ReimportFarseerAdapter();
        } catch(Exception e) {
            Debug.Log("RageSpline: File or Folder Operation Error. Make sure to close your MonoDevelop/IDE first and check if your folder and file permissions "+
                      "aren't set to read-only in your project folder. To resolve errors, you might need to manually move the RageFarseer folder now.");
            Debug.Log("Error Type: " + e+"");
        }
#else
        Debug.LogWarning("Farseer toggle disabled in Webplayer/Flash modes, please temporarily switch to PC/Mac deploy option.");
#endif
        _farseerToggleInProgress = false;
    }

    private static void ReimportFarseerAdapter() {
#if UNITY_EDITOR && !UNITY_WEBPLAYER && !UNITY_FLASH
        var dataPathDir = new DirectoryInfo(Application.dataPath);
        var dataPathUri = new Uri(Application.dataPath);
        foreach (var file in dataPathDir.GetFiles("RageFarseerAdapter.cs", SearchOption.AllDirectories)) {
            var relUri = dataPathUri.MakeRelativeUri(new Uri(file.FullName));
            var relPath = Uri.UnescapeDataString(relUri.ToString());
            AssetDatabase.ImportAsset(relPath, ImportAssetOptions.ForceUpdate);
        }
#endif
    }

}
