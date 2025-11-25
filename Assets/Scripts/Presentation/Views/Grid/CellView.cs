using Domain;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Presentation.Views.Grid
{
  public class CellView : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
  {
    [SerializeField] private MeshRenderer _meshRenderer;
    [SerializeField] private Color _normalColor = Color.darkGreen;
    [SerializeField] private Color _highlightColor = Color.yellow;
    [SerializeField] private Color _occupiedColor = Color.darkRed;
    
    private GridView _parentGridView;
    private GridPos _gridPosition;
    private MaterialPropertyBlock _propertyBlock;
    
    public void Initialize(GridView parentGridView, GridPos gridPosition)
    {
      _parentGridView = parentGridView;
      _gridPosition = gridPosition;
      
      _propertyBlock = new MaterialPropertyBlock();
      SetColor(_normalColor);
    }
    
    private void SetColor(Color color)
    {
      _meshRenderer.GetPropertyBlock(_propertyBlock);
      
     if (_meshRenderer.sharedMaterial.HasProperty("_BaseColor"))
      {
        _propertyBlock.SetColor("_BaseColor", color);
      }
      else if (_meshRenderer.sharedMaterial.HasProperty("_Color"))
      {
        _propertyBlock.SetColor("_Color", color);
      }

      _meshRenderer.SetPropertyBlock(_propertyBlock);
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
      _parentGridView.NotifyCellPointerEnter(_gridPosition);
      SetColor(_highlightColor);
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
      _parentGridView.NotifyCellPointerExit(_gridPosition);
      SetColor(_normalColor);
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
      _parentGridView.NotifyCellClicked(_gridPosition);
    }
  }
}

