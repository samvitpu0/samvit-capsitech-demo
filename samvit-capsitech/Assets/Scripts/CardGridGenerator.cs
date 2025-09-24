using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardGridGenerator : MonoBehaviour
{

    
    [Header("References")] 
    public GameObject cardPrefab;
    public RectTransform GridContainer;
    
    private List<GameObject> pool = new List<GameObject>();
    private List<Card> cards = new List<Card>();
    
    private int rows = 3;
    private int columns = 4;
    private float gridSpacing = 10f;
    
    public List<Card> GenerateGrid(int _rows, int _columns, float _gridSpacing = 10)
    {
        rows = _rows;
        columns = _columns;
        gridSpacing = _gridSpacing;
        
        cards.Clear();
        
        if (cardPrefab == null || GridContainer == null) return null;
        
        int requiredCount = rows * columns;

        while (pool.Count < requiredCount)
        {
            GameObject obj = Instantiate(cardPrefab, GridContainer);
            obj.SetActive(false);
            pool.Add(obj);
        }

        foreach (GameObject obj in pool)
        {
            obj.SetActive(false);
        }
        
        float containerWidth = GridContainer.rect.width;
        float containerHeight = GridContainer.rect.height;
        
        float cellWidth = (containerWidth - (columns - 1) * gridSpacing) / columns;
        float cellHeight = (containerHeight - (rows - 1) * gridSpacing) / rows;
        
        float size = Mathf.Min(cellWidth, cellHeight);
        
        float gridWidth = (columns * size) + ((columns - 1) * gridSpacing);
        float gridHeight = (rows * size) + ((rows - 1) * gridSpacing);
        
        float startX = -gridWidth / 2 + size / 2;
        float startY = gridHeight / 2 - size / 2;

        int index = 0;

        for (int r = 0; r < rows; r++)
        {
            for (int c = 0; c < columns; c++)
            {
                GameObject newImage = pool[index];
                newImage.SetActive(true);
                cards.Add(newImage.GetComponent<Card>());
                RectTransform rt = newImage.GetComponent<RectTransform>();
                
                rt.sizeDelta = new Vector2(size, size);
                
                float x = startX + c * (size + gridSpacing);
                float y = startY - r * (size + gridSpacing);
                
                rt.localPosition = new Vector3(x, y, 0);
                
                index++;
            }
        }
        var shuffledList = cards.OrderBy( x => Random.value ).ToList( );
        return shuffledList;
    }

}
