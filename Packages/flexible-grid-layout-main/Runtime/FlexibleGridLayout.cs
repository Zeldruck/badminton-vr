using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlexibleGridLayout : LayoutGroup
{
    public enum EFitType
    {
        Uniform,
        Width,
        Height,
        FixedRows,
        FixedColumns,
    }

    public EFitType fitType;
    public int rows, columns;
    public Vector2 cellSize;
    public Vector2 spacing;

    public bool fitX, fitY;
    
    public override void CalculateLayoutInputHorizontal()
    {
        base.CalculateLayoutInputHorizontal();

        if (fitType is EFitType.Width or EFitType.Height or EFitType.Uniform)
        {
            fitX = true;
            fitY = true;
            
            float sqrt = Mathf.Sqrt(transform.childCount);
            rows = Mathf.CeilToInt(sqrt);
            columns = rows;
        }

        if (fitType is EFitType.Width or EFitType.FixedColumns)
        {
            rows = Mathf.CeilToInt(transform.childCount / (float) columns);
        }
        else if (fitType is EFitType.Height or EFitType.FixedRows)
        {
            columns = Mathf.CeilToInt(transform.childCount / (float) rows);
        }

        float parentWidth = rectTransform.rect.width;
        float parentHeight = rectTransform.rect.height;

        float cellWidth = parentWidth / (float) columns - ((spacing.x / (float)columns) * 2) - (padding.left / (float)columns) - (padding.right / (float)columns);
        float cellHeight = parentHeight / (float) rows - ((spacing.y / (float)rows) * 2) - (padding.top / (float)rows) - (padding.bottom / (float)rows);

        cellSize.x = fitX ? cellWidth : cellSize.x;
        cellSize.y = fitY ? cellHeight : cellSize.y;

        int columnCount = 0, rowCount = 0;

        for (int i = 0; i < rectChildren.Count; i++)
        {
            rowCount = i / columns;
            columnCount = i % columns;

            var item = rectChildren[i];

            float xPos = (cellSize.x * columnCount) + (spacing.x * columnCount) + padding.left;
            float yPos = cellSize.y * rowCount + spacing.y * rowCount + padding.top;
            
            SetChildAlongAxis(item, 0, xPos, cellSize.x);
            SetChildAlongAxis(item, 1, yPos, cellSize.y);
        }
    }

    public override void CalculateLayoutInputVertical()
    {
        
    }

    public override void SetLayoutHorizontal()
    {
        
    }

    public override void SetLayoutVertical()
    {
        
    }
}
