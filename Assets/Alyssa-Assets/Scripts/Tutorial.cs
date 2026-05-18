using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Tutorial : MonoBehaviour
{
    public GameObject instructionObject;
    // public GameObject lineInstruction;
    // public GameObject circleInstruction;
    // public GameObject tutorialCompleteInstruction;
    public EnemyManager enemyManager;

    public TMP_Text instructionText;
    public Animator instructionAnimator;

    public string spellInstructionText = "Hold the trigger button down, aim with the star, and draw with the wand to cast attacks!";
    // public Sprite spellInstructionSprite;

    public string lineInstructionText = "Draw a straight line to shoot a line of projectiles";
    // public Sprite lineInstructionSprite;

    public string circleInstructionText = "Draw a circle to cast a circle of projectiles";
    // public Sprite circleInstructionSprite;

    public string completeInstructionText = "Enemies are coming! Defeat them with your magic powers!";
    // public Sprite completeInstructionSprite;
    public float completeInstructionDuration = 5f;

    public int lineGesturesRequired = 2;
    public int circleGesturesRequired = 2;

    private int lineGesturesDone = 0;
    private int circleGesturesDone = 0;
    private Coroutine completeHideCoroutine;

    private enum TutorialStage
    {
        Spell,
        Line,
        Circle,
        Complete
    }

    private TutorialStage currentStage = TutorialStage.Spell;

    void Start()
    {
        if (enemyManager != null)
        {
            enemyManager.spawnEnabled = false;
        }

        SetStage(TutorialStage.Spell);
    }

    public void OnSpellGestureCompleted()
    {
        if (currentStage != TutorialStage.Spell)
        {
            return;
        }

        Debug.Log("Spell cast instruction completed, moving to line tutorial.");
        SetStage(TutorialStage.Line);
    }

    public void OnLineGestureCompleted()
    {

        if (currentStage != TutorialStage.Line)
        {
            return;
        }

        lineGesturesDone++;
        Debug.Log($"Line gesture completed: {lineGesturesDone}/{lineGesturesRequired}");

        if (lineGesturesDone >= lineGesturesRequired)
        {
            SetStage(TutorialStage.Circle);
        }
    }

    public void OnCircleGestureCompleted()
    {
        if (currentStage != TutorialStage.Circle)
        {
            return;
        }

        circleGesturesDone++;
        Debug.Log($"Circle gesture completed: {circleGesturesDone}/{circleGesturesRequired}");

        if (circleGesturesDone >= circleGesturesRequired)
        {
            SetStage(TutorialStage.Complete);
        }
    }

    private void SetStage(TutorialStage stage)
    {
        currentStage = stage;

        if (instructionObject != null)
        {
            instructionObject.SetActive(true);
        }

        if (stage != TutorialStage.Complete)
        {
            StopCompleteInstructionHide();
        }

        switch (stage)
        {
            case TutorialStage.Spell:
                SetInstructionContent(spellInstructionText, 0);
                break;
            case TutorialStage.Line:
                SetInstructionContent(lineInstructionText, 1);
                break;
            case TutorialStage.Circle:
                SetInstructionContent(circleInstructionText, 2);
                break;
            case TutorialStage.Complete:
                SetInstructionContent(completeInstructionText, 3);
                StartCompleteInstructionHide();
                break;
        }

        if (stage == TutorialStage.Complete)
        {
            if (enemyManager != null)
            {
                enemyManager.spawnEnabled = true;
            }

            Debug.Log("Tutorial complete: enemy spawning enabled.");
        }
    }

    private void StartCompleteInstructionHide()
    {
        StopCompleteInstructionHide();
        completeHideCoroutine = StartCoroutine(HideInstructionAfterDelay(completeInstructionDuration));
    }

    private void StopCompleteInstructionHide()
    {
        if (completeHideCoroutine != null)
        {
            StopCoroutine(completeHideCoroutine);
            completeHideCoroutine = null;
        }
    }

    private IEnumerator HideInstructionAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (instructionObject != null)
        {
            instructionObject.SetActive(false);
        }

        completeHideCoroutine = null;
    }

    private void SetInstructionContent(string text, int spriteNumber)
    {
        if (instructionText != null)
        {
            instructionText.text = text;
        }

        if (instructionAnimator != null)
        {
            instructionAnimator.SetInteger("instructionNumber", spriteNumber);
        }
    }
}

