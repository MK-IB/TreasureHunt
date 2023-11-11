using UnityEditor;
using UnityEngine;

namespace _TreasureHunt._Scripts.GameplayRelated
{
    public class MazeGenerator : EditorWindow
    {
        private string widthText = "10";
        private string heightText = "10";

        [MenuItem("Window/Maze Generator")]
        public static void ShowWindow()
        {
            GetWindow<MazeGenerator>("Custom Editor Window");
        }

        void OnGUI()
        {
            GUILayout.Label("Enter Width and Height:");

            widthText = EditorGUILayout.TextField("Width:", widthText);
            heightText = EditorGUILayout.TextField("Height:", heightText);

            if (GUILayout.Button("Generate Maze"))
            {
                // Call a method from another script here.
                
                FindObjectOfType<GenerateMaze>().MazeGenerator(int.Parse(widthText), int.Parse(heightText));
            }
        }
    }
}