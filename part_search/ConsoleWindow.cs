using UnityEngine;
using System;
using System.Collections.Generic;
using Mono.CSharp;
using System.IO;
using System.Text;

namespace CSharpRepl {
  [KSPAddon( KSPAddon.Startup.EveryScene, false )]
  public class ConsoleWindow : MonoBehaviour {

    CommandEvaluator cmdEval;
    Vector2 scrollPos;
    int id;
    Rect position;

    void Awake() {
      cmdEval = new CommandEvaluator();
      cmdEval.ClearEval();
      cmdEval.InitEval();

      position = new Rect( 150f, 0, 300, 400 );

      id = GUIUtility.GetControlID(FocusType.Keyboard);
    }

    void OnGUI() {
      position = GUILayout.Window( id,
                                   position,
                                   DrawWindow,
                                   "Shell",
                                   HighLogic.Skin.window );
    }

    void DrawWindow( int id ) {
      bool didComplete = false;
      bool ranCommand = false;
      if( Event.current.isKey && Event.current.type == EventType.KeyDown ) {
        switch( Event.current.keyCode ) {
          case KeyCode.Return:
            cmdEval.Eval();
            ranCommand = true;
            Event.current.Use();
            break;
          case KeyCode.Tab:
            if( Event.current.control ) {
              didComplete = cmdEval.AutocompleteBuffer();
            }
            break;
          case KeyCode.UpArrow:
            didComplete = cmdEval.UpHistory();
            break;
          case KeyCode.DownArrow:
            didComplete = cmdEval.DownHistory();
            break;
        }
      }
      if( ranCommand ) {
        scrollPos = new Vector2( 0, float.MaxValue );
      }

      GUILayout.BeginVertical();
      scrollPos = GUILayout.BeginScrollView( scrollPos );

      GUILayout.TextArea( cmdEval.consoleText );
      GUILayout.EndScrollView();
      cmdEval.commandText = GUILayout.TextField( cmdEval.commandText );

      if( didComplete ) {
        TextEditor te = (TextEditor)GUIUtility.GetStateObject( typeof( TextEditor ), GUIUtility.keyboardControl );
        if( te != null ) {
          te.MoveCursorToPosition( new Vector2( 5555, 5555 ) );
        }
      }
      GUILayout.EndVertical();
      GUI.DragWindow();
    }
  }
}