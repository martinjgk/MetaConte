using UnityEngine;
using UnityEditor;
using GDS;

#if UNITY_EDITOR
namespace GDS {

    [InitializeOnLoad]
    public static class PlayModeStateChanged {

        static PlayModeStateChanged() {
            EditorApplication.playModeStateChanged += onPlayModeChange;
        }

        private static void onPlayModeChange(PlayModeStateChange state) {
            if (state == PlayModeStateChange.ExitingPlayMode) {
                Global.GlobalBus.Publish(new ResetEvent());
            }

        }
    }
}
#endif