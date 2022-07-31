using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    private string suit;
    private string rank;
    private bool faceup = false;

    public void SetSuitAndRank(string insuit, string inrank){
        suit = insuit;
        rank = inrank;
        string path = "Free_Playing_Cards/PlayingCards_" + rank + suit;
        GetComponent<MeshFilter>().mesh = Resources.Load<Mesh>(path);
        gameObject.AddComponent<MeshCollider>();
    }
    
    public bool Matches(Card otherCard){
        return (rank == otherCard.rank) && (suit == otherCard.suit);
    }
    
    public void Flip(){
        faceup = !faceup;
        transform.rotation = Quaternion.LookRotation(-transform.forward, Vector3.up);
    }

    public void Hide(){

        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0)){
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit)){
                if(hit.transform == transform){
                    if(!faceup){
                        MemoryGame.instance.Select(this);
                    }
                }
            }
        }
    }
}
