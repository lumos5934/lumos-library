using UnityEngine;

namespace LLib
{
    public class PlacementSystem
    {
        public enum CellPivot
        {
            Center,
            BottomLeft,
            BottomRight,
            TopLeft,
            TopRight
        }
        
        
        private PlaceableGrid _grid;
        private IPlaceable _placeable;
        private PlacementContext _context;
        private CellPivot _pivot = CellPivot.BottomLeft;
        
        
        public IPlaceable Placeable => _placeable;
        public PlaceableGrid Grid => _grid;
        public PlacementContext Context => _context;
        public CellPivot Pivot => _pivot;
        

        public void Begin(PlaceableGrid grid, IPlaceable target, Vector3 position)
        {
            if (target == null)
                return;
            
            if (_placeable != null)
            {
                if (_placeable != target)
                {
                    _placeable.OnCancel();
                }
                else
                {
                    return;
                }
            }
            
            _grid = grid;
            _placeable = target;


            _context.IsReplace = _grid.IsPlaced(_placeable, out var bounds);
            
            if (_context.IsReplace)
            {
                _grid.Release(bounds);
            }
            else
            {
                var startCell = CalculateStartCell(position, _placeable.Size);
                
                bounds = new BoundsInt((Vector3Int)startCell, (Vector3Int)_placeable.Size);
            }
            
            UpdateContext(bounds);
            
            _placeable.OnBegin();
        }


        public void Move(Vector3 position)
        {
            if (_grid == null || _placeable == null)
                return;
            
            var startCell = CalculateStartCell(position, _placeable.Size);
            
            var newBounds = new BoundsInt((Vector3Int)startCell, _context.CellBounds.size);
            
            int clampedMinX = Mathf.Clamp(newBounds.xMin, _grid.CellBounds.xMin, _grid.CellBounds.xMax - newBounds.size.x);
            int clampedMinY = Mathf.Clamp(newBounds.yMin, _grid.CellBounds.yMin, _grid.CellBounds.yMax - newBounds.size.y);
            
            var clampedNewBounds = new BoundsInt(new Vector3Int(clampedMinX, clampedMinY, 0), newBounds.size);
            if (clampedNewBounds != _context.CellBounds)
            {
                UpdateContext(clampedNewBounds);
            }
        }

        
        public bool TryPlace()
        {
            if (_grid == null ||
                _placeable == null)
                return false;


            if (_grid.Place(_placeable, _context.CellBounds))
            {
                _placeable.OnPlace();
                _placeable = null;
                return true;
            }

            return false;
        }

        
        public bool Remove()
        {
            if (_grid == null ||
                _placeable == null)
                return false;

            if (_grid.Remove(_placeable))
            {
                _placeable.OnRemove();
                _placeable = null;
                _grid = null;
                
                return true;
            }
         
            return false;
        }
        
        
        public void Cancel()
        {
            if (_grid == null ||
                _placeable == null)
                return;
            
            if (_context.IsReplace)
            {
                if (_grid.IsPlaced(_placeable, out var bounds))
                {
                    _grid.Occupy(bounds);
                }
                
                _placeable.OnCancel();
            }
            else
            {
                _placeable.OnRemove();
            }

            _grid = null;
            _placeable = null;
        }


        public void SetPivot(CellPivot cellPivot)
        {
            _pivot = cellPivot;
        }

        
        private Vector2Int CalculateStartCell(Vector3 position, Vector2Int size)
        {
            Vector3 offset = Vector3.zero;
            
            if (_pivot == CellPivot.Center)
            {
                if (size.x % 2 == 0) offset.x = 0.5f * _grid.CellSize;
                if (size.y % 2 == 0) offset.y = 0.5f * _grid.CellSize;
            }

            var gridCellBounds = _grid.CellBounds;
            var gridCellSize = _grid.CellSize;
            
            int cellX = Mathf.FloorToInt(position.x / gridCellSize);
            int cellY = Mathf.FloorToInt(position.y / gridCellSize);
            
            cellX = Mathf.Clamp(cellX, gridCellBounds.min.x, gridCellBounds.max.x);
            cellY = Mathf.Clamp(cellY, gridCellBounds.min.y, gridCellBounds.max.y);
            
            Vector2Int startCell = new Vector2Int(cellX, cellY);
            

            switch (_pivot)
            {
                case CellPivot.Center:
                    startCell.x -= size.x / 2;
                    startCell.y -= size.y / 2;
                    break;
                case CellPivot.BottomRight:
                    startCell.x -= (size.x - 1);
                    break;
                case CellPivot.TopLeft:
                    startCell.y -= (size.y - 1);
                    break;
                case CellPivot.TopRight:
                    startCell.x -= (size.x - 1);
                    startCell.y -= (size.y - 1);
                    break;
                case CellPivot.BottomLeft:
                default:
                    break;
            }
            
            return startCell;
        }
        
        
        private void UpdateContext(BoundsInt cellBounds)
        {
            var worldSize = (Vector3)cellBounds.size * _grid.CellSize;
            var worldMin = (Vector3)cellBounds.min * _grid.CellSize;
            var worldCenter = worldMin + (worldSize * 0.5f);
            var worldBounds = new Bounds(worldCenter, worldSize);

            cellBounds.size = new Vector3Int(cellBounds.size.x, cellBounds.size.y, 1);

            _context.WorldBounds = worldBounds;
            _context.CellBounds = cellBounds;

            bool canPlace = true;
            
            foreach (Vector2Int cell in cellBounds.allPositionsWithin)
            {
                if (_grid.IsOccupied(cell))
                {
                    canPlace = false;
                    break;
                }
            }

            _context.CanPlace = canPlace;
        
            _placeable.OnUpdated(_context);
        }
    }
}

