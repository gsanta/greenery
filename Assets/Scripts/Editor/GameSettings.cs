
using UnityEditor;
using UnityEngine;

public class GameSettings : EditorWindow
{
    private bool showEnemyFieldOfView;
    private bool oldShowEnemyFieldOfView;


    [MenuItem("Window/Game Settings")]
    public static void ShowWindow()
    {
        GetWindow<GameSettings>("Game Settings");
    }

    private void OnGUI()
    {

        showEnemyFieldOfView = GUILayout.Toggle(showEnemyFieldOfView, "Enemy Field of View");
    
        if (showEnemyFieldOfView != oldShowEnemyFieldOfView)
        {
            if (showEnemyFieldOfView)
            {

            } else
            {

            }

            oldShowEnemyFieldOfView = showEnemyFieldOfView;
        }
    }
}
