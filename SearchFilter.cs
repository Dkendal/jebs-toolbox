using System;
using System.Text;
using UnityEngine;

namespace JebsToolbox {
  class SearchFilter : EditorPartListFilter{

    bool _enabled = false;
    public bool Enabled {
      set {
        if( Enabled == value ) { return; }

        _enabled = value;
        switch( Enabled) {
          case true:
            EditorPartList.Instance.ExcludeFilters.AddFilter( this );
            break;
          case false:
            EditorPartList.Instance.ExcludeFilters.RemoveFilter( this );
            break;
        }
      }
      get { return _enabled; }
    }

    public SearchFilter(Func<AvailablePart, bool> criteria) :
      base( new Guid().ToString(), criteria ){
    }
  }
}
