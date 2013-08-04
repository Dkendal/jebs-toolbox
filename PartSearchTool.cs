using System;
using UnityEngine;

[KSPAddon( KSPAddon.Startup.EditorAny, true )]
public class PartSearchTool : MonoBehaviour {

  private int id;
  private Rect window_position = new Rect();
  private string search_text;
  
  void Awake() {
    id = GUIUtility.GetControlID(FocusType.Passive);
    search_text = "search for a part";
    window_position.x = 200;
  }

  void Update(){
  }

  void OnGUI() {
    window_position = GUILayout.Window( id, window_position, DrawPartSearchWindow, "" );
  }

  void DrawPartSearchWindow(int window_id) {
    search_text = GUILayout.TextField( search_text, GUILayout.MinWidth(300f));
    GUI.DragWindow();
  }
} 