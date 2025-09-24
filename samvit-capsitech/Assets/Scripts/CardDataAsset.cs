using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardDataAsset", menuName = "Create Card Data")]
public class CardDataAsset : ScriptableObject
{
       public List<CardData> CardsDataElements = new List<CardData>();
}

[Serializable]
public class CardData
{
       public int CardCode;
       public Sprite CardImage;
}
