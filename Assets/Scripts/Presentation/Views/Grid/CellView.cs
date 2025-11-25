using Domain;
using UnityEngine;

namespace Presentation.Views.Grid
{
  public class CellView : MonoBehaviour
  {
    private GridView _parentGridView;
    private GridPos _gridPosition;
    
    public void Initialize(GridView parentGridView, GridPos gridPosition)
    {
      _parentGridView = parentGridView;
      _gridPosition = gridPosition;
    }
  }
}

