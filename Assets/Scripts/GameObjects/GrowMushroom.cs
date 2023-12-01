using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowMushroom : MonoBehaviour
{
    public int seedCost;
    public float growTime;
    public GameObject rootObj;
    public AnimationCurve growthRate;

    public float newScale;
    public BouncePad bouncePad;
    public float newPower = 30;
    Vector3 originalScale;
    void Start()
    {
        originalScale = rootObj.transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void FeedMushroom()
    {
        PlayerData player = FindObjectOfType<PlayerData>();

        if (player && player.seedPoints >= seedCost)
        {
            player.seedPoints -= seedCost;
            StartCoroutine(Grow());
        }
        else
        {
            EffectsManager.instance.MakeFloatingText(transform.position, "need more seeds!");
        }
    }
    private IEnumerator Grow()
    {
        float time = 0;
        float extraScale = newScale - 1;
        GetComponent<InteractableObject>().DisableObject();
        while (time < growTime)
        {
            time += Time.deltaTime;

            float growth = 1 + (growthRate.Evaluate(time / growTime) * extraScale);
            rootObj.transform.localScale = originalScale * growth;

            yield return null;
        }
        bouncePad.power = newPower;
    }
}
