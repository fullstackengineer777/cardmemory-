using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryGame : MonoBehaviour
{

    string[] kCardSuits = new string[]{ "Club" , "Diamond" , "Spades" , "Heart" };
    string[] kCardRanks = new string[]{ "2", "3", "4", "6", "7", "8", "9", "10", "J", "Q", "K", 
                                "A" };
    static public MemoryGame instance;

    private Card[] cards;    
    private Card selectOne;
    private Card selectTwo;
    private double selectTime;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        //Get all cards on GameBoard
        cards = transform.GetComponentsInChildren<Card>();
        //Deal random cards, in pairs
        int n = 0;
        Shuffle(cards);
        for(int m = 0  ; m < cards.Length/2 ; ++m){
            
            string suit = GetRandomFromArray(kCardSuits);
            string rank = GetRandomFromArray(kCardRanks);

            cards[n++].SetSuitAndRank(suit, rank);
            cards[n++].SetSuitAndRank(suit, rank);

        }

    }

    private void Shuffle<T>(T[] array){
        int n = array.Length;
        while(n > 1){
            int k = (int)Mathf.Floor(Random.value * (n--));
            T temp = array[n];
            array[n] = array[k];
            array[k] = temp;
        }
    }

    private T GetRandomFromArray<T>(T[] array)
    {
        return array[(int)Mathf.Floor(Random.value * array.Length)];
    }
    
    public void Select(Card card){

        if(selectTwo == null){
            card.Flip();
            if(selectOne == null){
                selectOne = card;
            }else{
                selectTwo = card;
                selectTime = Time.time;
            }
        }

    }

    private void CheckMatch(){
        if(selectOne.Matches(selectTwo)){
            selectOne.Hide();
            selectTwo.Hide();
        } else {
            selectOne.Flip();
            selectTwo.Flip();
        }
        selectOne = selectTwo = null;
    }
    // Update is called once per frame
    void Update()
    {
        if(selectTwo != null){
            if(Time.time > selectTime + 1.0){
                CheckMatch();
            }
        }
    }
}
