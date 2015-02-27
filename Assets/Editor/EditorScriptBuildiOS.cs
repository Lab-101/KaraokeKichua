using UnityEngine;
using System.Collections;
using UnityEditor;
using System;
using System.Collections.Generic;

class EditorScriptBuildiOS {

		static string[] SCENES = FindEnabledEditorScenes();
		
		static string APP_NAME = "KaraokeKichua-Iphone";
		static string TARGET_DIR = "/Users/Shared/Jenkins/Home/workspace/KaraokeKichua-Iphone";
		
		[MenuItem ("Custom/CI/Build Mac OS X")]
		static void PerformMacIphone()
		{
			string target_dir = APP_NAME + "";
		GenericBuild(SCENES, TARGET_DIR + "/" + target_dir, BuildTarget.iPhone,BuildOptions.None);
		}



		private static string[] FindEnabledEditorScenes() {
			List<string> EditorScenes = new List<string>();
			foreach(EditorBuildSettingsScene scene in EditorBuildSettings.scenes) {
				if (!scene.enabled) continue;
				EditorScenes.Add(scene.path);
			}
			return EditorScenes.ToArray();
		}
		
		static void GenericBuild(string[] scenes, string target_dir, BuildTarget build_target, BuildOptions build_options)
		{
			EditorUserBuildSettings.SwitchActiveBuildTarget(build_target);
			string res = BuildPipeline.BuildPlayer(scenes,target_dir,build_target,build_options);
			if (res.Length > 0) {
				throw new Exception("BuildPlayer failure: " + res);
			}
		}
	}

