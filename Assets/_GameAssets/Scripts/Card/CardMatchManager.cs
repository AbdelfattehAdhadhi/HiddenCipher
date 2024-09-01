using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CardMatchManager : Singleton<CardMatchManager>
{
    [SerializeField] private List<CardController> selectedCards = new List<CardController>();
    
    private bool isProcessingMatch = false;
    private int comboCounter = 0;

    public event Action OnMatchFound;
    public event Action OnMoveMade;
    public event Action OnMatchMade;
    public event Action<int> OnComboMade;
    public event Action OnNoMatchMade;

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
        OnMoveMade?.Invoke();

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
                OnMatchMade?.Invoke();
                comboCounter++;

                if (comboCounter > 1)
                {
                    OnComboMade?.Invoke(comboCounter - 1);
                }

                card1.OnMatchFound();
                card2.OnMatchFound();

                selectedCards.Remove(card1);
                selectedCards.Remove(card2);
                i -= 2;
                OnMatchFound?.Invoke();
            }
            else
            {
                comboCounter = 0;
                OnNoMatchMade?.Invoke();
            }
        }

        foreach (var card in selectedCards.ToList())
        {
            card.FlipBack();
        }
        selectedCards.Clear();

        isProcessingMatch = false;
    }
}
