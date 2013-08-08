using System;
using UnityEngine;

namespace JebsToolbox {
  class CSharpRepl : MonoBehaviour {
    int id;
    Rect position = new Rect(200f, 400f, 0, 0);
    string buffer = "";
    string executable_text = "";

    void Awake() {
      id = GUIUtility.GetControlID(FocusType.Keyboard);
    }

    void OnGUI() {
      position = GUILayout.Window( id,
                                   position,
                                   DrawWindow,
                                   "C# Repl!" );
      if( Event.current.isKey && Event.current.keyCode == KeyCode.Return ) {

      }
    }

    void DrawWindow(int id) {
      GUI.enabled = false;
      buffer = GUILayout.TextArea( buffer,
                                   GUILayout.Width( 300f ),
                                   GUILayout.Height( 300f ) );
      GUI.enabled = true;

      executable_text = GUILayout.TextField( executable_text,
                                             GUILayout.Width( 300f ) );
      GUI.DragWindow();
    }
  }
}
