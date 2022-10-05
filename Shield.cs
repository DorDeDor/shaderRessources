using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    MeshRenderer meshRenderer;
    [SerializeField] AnimationCurve displacementCurve;
    [SerializeField] float displacementMagnitude;
    [SerializeField] float lerpSpeed;

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                HitShield(hit.point);
            }
        }
    }

    public void HitShield(Vector3 hitPos)
    {
        meshRenderer.material.SetVector("_HitPos", hitPos);
        StopAllCoroutines();
        StartCoroutine(Coroutine_HitDisplacement());
        
    }

    IEnumerator Coroutine_HitDisplacement()
    {
        float lerp = 0;
        while (lerp < 100)
        {
            meshRenderer.material.SetFloat("_DisplacementStrength", displacementCurve.Evaluate(lerp) * displacementMagnitude);
            lerp += Time.deltaTime*lerpSpeed;
            yield return null;
        }
        
        
    }
}
