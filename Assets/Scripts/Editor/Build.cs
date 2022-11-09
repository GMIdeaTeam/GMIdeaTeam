using UnityEditor;
using System;
using System.Collections.Generic;

namespace Builder.Editor {

    public static class Builder
    {
        public static void BuildStandaloneWindows()
        {
            BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
            buildPlayerOptions.scenes = GetBuildSceneList();
            buildPlayerOptions.target = BuildTarget.StandaloneWindows;
            buildPlayerOptions.options = BuildOptions.None;
            BuildPipeline.BuildPlayer(buildPlayerOptions);
        }

        private static string[] GetBuildSceneList()
        {
            EditorBuildSettingsScene[] scenes = UnityEditor.EditorBuildSettings.scenes;

            List<string> listScenePath = new List<string>();

            for (int i = 0; i < scenes.Length; i++)
            {
                if (scenes[i].enabled)
                    listScenePath.Add(scenes[i].path);
            }

            return listScenePath.ToArray();
        }
    }
}
