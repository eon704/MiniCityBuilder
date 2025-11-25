using Domain;
using R3;

namespace Presentation.Views.Interfaces
{
  public interface IGridView
  {
    public void InitializeMap(int sizeX, int sizeY);
    
    Observable<GridPos> OnCellPointerEnterAsObservable();
    Observable<GridPos> OnCellPointerExitAsObservable();
    Observable<GridPos> OnCellClickedAsObservable();
  }
}

