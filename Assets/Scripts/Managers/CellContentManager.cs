using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//class for assigning cards to cells
public class CellContentManager : MonoBehaviour
{

    [SerializeField]
    private CardBundleData[] _cardBundleData;

    //list for cards, that were chosed in previous levels
    private List<CardData> _answeredCards = new List<CardData>();

    //selecting random card bundle to use in current level
    private int ChooseRandomCardBundle => Random.Range(0, _cardBundleData.Length);



    //Populating cells with card references in random order
    public void PopulateCells(List<CellController> cellList)
    {
        int cardBundleToUse = ChooseRandomCardBundle;
        int[] randomOrder = GetRandomizedOrderArray(_cardBundleData[cardBundleToUse].CardData.Length);

        for (int i = 0; i < cellList.Count; i++)
        {
            cellList[i].AssignCard(_cardBundleData[cardBundleToUse].CardData[randomOrder[i]]);
        }
    }



    //create sorted int array and shuffles it
    private int[] GetRandomizedOrderArray(int lenght)
    {
        int[] newArray = new int[lenght];
        for (int i=0; i<lenght; i++)
        {
            newArray[i] = i;
        }
        Shuffle(newArray);
        return newArray;
    }




    //method to shuffle received array
    private void Shuffle(int[] arrayToShuffle)
    {
        int n = arrayToShuffle.Length;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n);
            int value = arrayToShuffle[k];
            arrayToShuffle[k] = arrayToShuffle[n];
            arrayToShuffle[n] = value;
        }
    }
}
