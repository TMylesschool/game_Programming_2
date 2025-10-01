using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static Unity.Collections.Unicode;

public class SlotMachine : MonoBehaviour
{
    public float spinDuration = 2.0f;

    [Header("Slot Settings")]
    public int numberOfSym = 10;               // total symbols
    public int[] symbolWeights;                // weights for each symbol (size = numberOfSym)
    [Range(0f, 1f)] public float nearMissChance = 0.25f; // 25% chance to trigger near miss

    [Header("UI References")]
    public Text firstReel;
    public Text secondReel;
    public Text thirdReel;
    public Text betResult;
    public Text totalCredits;
    public InputField inputBet;
    public Button btnPullLever;

    private bool startSpin = false;
    private bool firstReelSpinned = false;
    private bool secondReelSpinned = false;
    private bool thirdReelSpinned = false;
    private int betAmount;
    private int credits = 1000;
    private int firstReelResult = 0;
    private int secondReelResult = 0;
    private int thirdReelResult = 0;
    private float elapsedTime = 0.0f;

    public void Spin()
    {
        if (credits <= 0)
        {
            betResult.text = "GAME OVER!";
            return;
        }

        if (betAmount > 0)
        {
            startSpin = true;
        }
        else
        {
            betResult.text = "Insert a valid bet!";
        }
    }

    private void OnGUI()
    {
        try
        {
            betAmount = int.Parse(inputBet.text);
        }
        catch
        {
            betAmount = 0;
        }
        totalCredits.text = credits.ToString();
    }

    void checkBet()
    {
        if (firstReelResult == secondReelResult && secondReelResult == thirdReelResult)
        {
            betResult.text = "YOU WIN!";
            credits += 500 * betAmount;
        }
        else
        {
            betResult.text = "YOU LOSE!";
            credits -= betAmount;
        }

        if (credits <= 0)
        {
            betResult.text = "GAME OVER!";
            btnPullLever.interactable = false;
        }
    }

    void FixedUpdate()
    {
        if (startSpin)
        {
            elapsedTime += Time.deltaTime;
            int randomSpinResult = WeightedSpin();

            if (!firstReelSpinned)
            {
                firstReel.text = randomSpinResult.ToString();
                if (elapsedTime >= spinDuration)
                {
                    firstReelResult = randomSpinResult;
                    firstReelSpinned = true;
                    elapsedTime = 0;
                }
            }
            else if (!secondReelSpinned)
            {
                secondReel.text = randomSpinResult.ToString();
                if (elapsedTime >= spinDuration)
                {
                    secondReelResult = randomSpinResult;
                    secondReelSpinned = true;
                    elapsedTime = 0;
                }
            }
            else if (!thirdReelSpinned)
            {
                thirdReel.text = randomSpinResult.ToString();
                if (elapsedTime >= spinDuration)
                {
                    // Normal spin result
                    thirdReelResult = randomSpinResult;

                    // --- NEAR MISS LOGIC ---
                    if (Random.value < nearMissChance)
                    {
                        if (firstReelResult == secondReelResult)
                        {
                            thirdReelResult = NearMiss(firstReelResult);
                        }
                        else if (secondReelResult == thirdReelResult)
                        {
                            firstReelResult = NearMiss(secondReelResult);
                        }
                        else if (firstReelResult == thirdReelResult)
                        {
                            secondReelResult = NearMiss(firstReelResult);
                        }
                    }
                    // -----------------------

                    startSpin = false;
                    elapsedTime = 0;
                    firstReelSpinned = false;
                    secondReelSpinned = false;
                    checkBet();
                }
            }
        }
    }

    // --- Weighted Random Selection (like Loaded Dice) ---
    int WeightedSpin()
    {
        if (symbolWeights == null || symbolWeights.Length != numberOfSym)
        {
            // fallback: flat distribution
            return Random.Range(0, numberOfSym);
        }

        int totalWeight = 0;
        foreach (int w in symbolWeights) totalWeight += w;

        int roll = Random.Range(0, totalWeight);
        int cumulative = 0;

        for (int i = 0; i < symbolWeights.Length; i++)
        {
            cumulative += symbolWeights[i];
            if (roll < cumulative)
                return i;
        }

        return numberOfSym - 1; // fallback
    }

    // --- Near Miss helper ---
    int NearMiss(int winningSymbol)
    {
        // Pick symbol just before or after winning symbol
        int offset = Random.value < 0.5f ? -1 : 1;
        int result = (winningSymbol + offset + numberOfSym) % numberOfSym;
        Debug.Log($"Near Miss Triggered! Almost hit {winningSymbol}, got {result} instead.");
        return result;
    }
}
