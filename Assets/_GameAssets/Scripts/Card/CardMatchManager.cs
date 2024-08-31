using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardMatchManager : Singleton<CardMatchManager>
{
    [SerializeField] private List<CardController> selectedCards = new List<CardController>();
    private int matchesFound = 0;
    private bool isProcessingMatch = false;

    public void CardSelected(CardController card)
    {
        Debug.Log("CardSelected");
        if (selectedCards.Contains(card) || isProcessingMatch)
            return;

        selectedCards.Add(card);

        if (selectedCards.Count % 2 == 0)
        {
            StartCoroutine(CheckMatches());
        }
    }

    private IEnumerator CheckMatches()
    {
        isProcessingMatch = true;

        yield return new WaitForSeconds(0.1f);

        
        while (selectedCards.Any(card => !card.IsAnimationComplete()))
        {
            yield return null;
        }

        
        for (int i = 0; i < selectedCards.Count; i += 2)
        {
            CardController card1 = selectedCards[i];
            CardController card2 = selectedCards[i + 1];

            if (card1.Id == card2.Id)
            {
                matchesFound++;
                card1.OnMatchFound();
                card2.OnMatchFound();

                selectedCards.Remove(card1);
                selectedCards.Remove(card2);
                i -= 2;
            }
        }
        selectedCards.Clear();

        isProcessingMatch = false;
    }
}
