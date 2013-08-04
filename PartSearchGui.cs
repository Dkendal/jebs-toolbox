using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace part_search {
  [KSPAddon( KSPAddon.Startup.EditorAny, true )]
  public class PartSearchGui : MonoBehaviour {
#region properties
    int id;
    Rect window_position = new Rect();
    List<SearchFilter> search_filters = new List<SearchFilter>();

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
      AddSearchFilters();
    }

    void OnGUI() {
      window_position = GUILayout.Window( id, window_position, DrawPartSearchWindow, "" );
    }
#endregion

    void OnSearchTextChanged() {
      EditorPartList.Instance.Refresh();
    }

    void DrawPartSearchWindow(int window_id) {
      SearchText = GUILayout.TextField( SearchText, GUILayout.MinWidth( 300f ) );
      GUI.DragWindow();
    }

    void AddSearchFilters() {
      var title_search = new SearchFilter(
        (AvailablePart p) => Regex.IsMatch( p.title, "(?i)" + SearchText ) );

      search_filters.Add( title_search );

      search_filters.ForEach( x => x.Enabled = true );
    }
  }
}
