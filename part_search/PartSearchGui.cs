using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace JebsToolbox {
  [KSPAddon( KSPAddon.Startup.EditorAny, true )]
  public class PartSearchGui : MonoBehaviour {
#region properties
    int id;
    Rect window_position = new Rect( EditorPanels.Instance.partsPanelWidth + 5f, 100f, 0, 0 );

    List<SearchFilter> search_filters = new List<SearchFilter>();
    const string search_text_box = "full_text_part_search_textbox";

    string _search_text = "";
    string SearchText {
      get { return _search_text; }
      set {
        var old_val = SearchText;
        _search_text = value;
        if( old_val.Equals( value ) == false ) {
          OnSearchTextChanged();
        }
      }
    }

#endregion

#region unity_event_handlers
    void Awake() {
      id = GUIUtility.GetControlID( FocusType.Passive );

      Func<AvailablePart, bool> full_text_search = part => {
        var part_members = new string[] {
          part.title,
          part.description,
          part.author,
          part.manufacturer,
          part.resourceInfo
        };

        var part_text = String.Join( " ", part_members );
        return Regex.IsMatch( part_text, "(?i)" + SearchText );
      };

      var full_text_search_filter = new SearchFilter( full_text_search );
      full_text_search_filter.Enabled = true;
    }

    void OnGUI() {
      if( Event.current.isKey && Event.current.type == EventType.KeyDown ) 
        OnGUIKeyDownEvent( Event.current );

      if( EditorLogic.fetch.editorScreen == EditorLogic.EditorScreen.Parts ) 
        window_position = GUILayout.Window( id,
                                            window_position,
                                            DrawPartSearchWindow,
                                            "",
                                            HighLogic.Skin.window );
    }
#endregion

    void OnGUIKeyDownEvent(Event e) {
      if( e.keyCode == KeyCode.F && e.control && e.type == EventType.KeyDown ) {
        GUI.FocusControl( search_text_box );
      }
    }

    void DrawPartSearchWindow(int window_id) {
      GUI.SetNextControlName( search_text_box );
      SearchText = GUILayout.TextField( SearchText,
                                        HighLogic.Skin.textField,
                                        GUILayout.Width( 300f ));

      GUI.DragWindow();
    }

    void OnSearchTextChanged() {
      EditorPartList.Instance.Refresh();
    }
  }
}
