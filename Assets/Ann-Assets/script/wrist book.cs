using UnityEngine;

public class WristBookController : MonoBehaviour
{
    [Header("Animator")]
    public Animator bookAnimator;

    [Header("Pages")]
    public GameObject pageItem1;       // "Collect KeyStone" page
    public GameObject pageItem2;       // "Collect AncientCoin" page
    public GameObject pageComplete;    // "Task Complete" page
    public AudioSource openSound;
    public AudioSource flipSound;


    public int collectedCount = 0;     // public so Pocket.cs can read it
    private bool isOpen = false;

    void Start()
    {
        // Hide all pages at start
        pageItem1.SetActive(false);
        pageItem2.SetActive(false);
        pageComplete.SetActive(false);

        // Open book and show item 1 page on start
        StartCoroutine(OpenBookAfterDelay(5f));


    }

    void OpenBook()
    {
        if (isOpen) return;
        isOpen = true;
        bookAnimator.SetTrigger("open");

        // Show first page after open animation finishes
        StartCoroutine(ShowPageAfterDelay(pageItem1, 0.5f));
        openSound.Play();
    }

    // Called by Pocket.cs when a valid item is collected
    public void OnItemCollected()
    {
        if (collectedCount >= 2) return; // guard against extra calls

        collectedCount++;

        if (collectedCount == 1)
        {
            // First item collected — flip to item 2 page
            bookAnimator.SetTrigger("flip");
            StartCoroutine(SwitchPageAfterFlip(pageItem1, pageItem2, 0.5f));
            flipSound.Play();
        }
        else if (collectedCount == 2)
        {
            // Second item collected — flip to complete page
            bookAnimator.SetTrigger("flip");
            StartCoroutine(SwitchPageAfterFlip(pageItem2, pageComplete, 0.5f));
            flipSound.Play();
        }
    }

    System.Collections.IEnumerator ShowPageAfterDelay(GameObject page, float delay)
    {
        yield return new WaitForSeconds(delay);
        page.SetActive(true);
    }
    System.Collections.IEnumerator OpenBookAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        OpenBook();
    }

    System.Collections.IEnumerator SwitchPageAfterFlip(GameObject hidePage, GameObject showPage, float delay)
    {
        yield return new WaitForSeconds(delay);
        hidePage.SetActive(false);
        showPage.SetActive(true);
    }
}
