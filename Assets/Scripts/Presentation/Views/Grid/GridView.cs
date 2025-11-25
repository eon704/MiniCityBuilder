using Domain;
using Presentation.Views.Interfaces;
using R3;
using UnityEngine;


namespace Presentation.Views.Grid
{
  public class GridView : MonoBehaviour, IGridView
  {
    [SerializeField] private CellView _cellPrefab;
    [SerializeField] private float _cellSize = 1f;
    
    private readonly Subject<GridPos> _onCellPointerEnter = new();
    private readonly Subject<GridPos> _onCellPointerExit = new();
    private readonly Subject<GridPos> _onCellClicked = new();
    
    private CellView[,] _cells;
    private int _sizeX;
    private int _sizeY;
    
    public void InitializeMap(int sizeX, int sizeY)
    {
      _sizeX = sizeX;
      _sizeY = sizeY;
      _cells = new CellView[sizeX, sizeY];
      
      // Create cells
      for (int x = 0; x < sizeX; x++)
      {
        for (int y = 0; y < sizeY; y++)
        {
          CreateCell(x, y);
        }
      }
    }
    
    private void CreateCell(int x, int y)
    {
      var cellView = Instantiate(_cellPrefab, transform);
      var worldPos = GridToWorldPosition(new GridPos(x, y));
      cellView.transform.position = worldPos;
      cellView.name = $"Cell_{x}_{y}";
      cellView.Initialize(this, new GridPos(x, y));
      
      _cells[x, y] = cellView;
    }
    
    private Vector3 GridToWorldPosition(GridPos gridPos)
    {
      float offsetX = (_sizeX - 1) * _cellSize * 0.5f;
      float offsetY = (_sizeY - 1) * _cellSize * 0.5f;
      
      float worldX = gridPos.X * _cellSize - offsetX;
      float worldZ = gridPos.Y * _cellSize - offsetY;
      
      return new Vector3(worldX, 0, worldZ);
    }

    public Observable<GridPos> OnCellPointerEnterAsObservable()
    {
      return _onCellPointerEnter;
    }

    public Observable<GridPos> OnCellPointerExitAsObservable()
    {
      return _onCellPointerExit;
    }

    public Observable<GridPos> OnCellClickedAsObservable()
    {
      return _onCellClicked;
    }
    public void NotifyCellPointerEnter(GridPos pos)
    {
      _onCellPointerEnter.OnNext(pos);
    }
    
    public void NotifyCellPointerExit(GridPos pos)
    {
      _onCellPointerExit.OnNext(pos);
    }
    
    public void NotifyCellClicked(GridPos pos)
    {
      _onCellClicked.OnNext(pos);
    }
  }
}

