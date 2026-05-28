using UnityEngine;

public class MeteoManager : MonoBehaviour
{
    public GameObject pluie;
    public GameObject neige;
    
    public GameObject solNeige; 
    public GameObject solPluie; 
    
    public float vitesseNeige = 1f; 
    public float vitessePluie = 1f; 
    public float sliderMax = 500f;

    private ParticleSystem pluieParticles;
    private ParticleSystem neigeParticles; 

    void Start()
    {
        pluieParticles = pluie.GetComponent<ParticleSystem>();
        neigeParticles = neige.GetComponent<ParticleSystem>();
        
        // On force les deux sols à -8f (bien profonds) au démarrage
        solNeige.transform.position = new Vector3(solNeige.transform.position.x, -8f, solNeige.transform.position.z);
        solPluie.transform.position = new Vector3(solPluie.transform.position.x, -8f, solPluie.transform.position.z);
        
        DesactiverToutesLesConditionsMeteo();
    }

    public void DesactiverToutesLesConditionsMeteo()
    {
        pluie.SetActive(false);
        neige.SetActive(false);
    }

    public void ActiverPluie()
    {
        pluie.SetActive(true);
        neige.SetActive(false);
    }

    public void ActiverNeige()
    {
        neige.SetActive(true);
        pluie.SetActive(false);
    }

    public void ChangerIntensitePluie(float intensite)
    {
        var emission = pluieParticles.emission;
        emission.rateOverTime = intensite;
    }

    public void ChangerIntensiteNeige(float intensite)
    {
        var emission = neigeParticles.emission;
        emission.rateOverTime = intensite;
    }

    void Update()
    {
        // --- LOGIQUE POUR LA NEIGE ---
        Vector3 posNeige = solNeige.transform.position;
        float forceNeige = neigeParticles.emission.rateOverTime.constant;
        float cibleYNeige = -8f; // Par défaut, elle reste cachée à -8
        float vNeige = vitesseNeige * 0.1f * Time.deltaTime;

        if (neige.activeSelf && forceNeige > 0)
        {
            cibleYNeige = 4.0f; // Elle monte à 4 si activée
            vNeige = (forceNeige / sliderMax) * vitesseNeige * 0.1f * Time.deltaTime;
        }
        posNeige.y = Mathf.MoveTowards(posNeige.y, cibleYNeige, vNeige);
        solNeige.transform.position = posNeige;

        // --- LOGIQUE POUR LA PLUIE ---
        Vector3 posPluie = solPluie.transform.position;
        float forcePluie = pluieParticles.emission.rateOverTime.constant;
        float cibleYPluie = -8f; // Par défaut, elle reste cachée à -8
        float vPluie = vitessePluie * 0.1f * Time.deltaTime;

        if (pluie.activeSelf && forcePluie > 0)
        {
            cibleYPluie = 4.0f; // Elle monte à 2 (inondation) si activée
            vPluie = (forcePluie / sliderMax) * vitessePluie * 0.1f * Time.deltaTime;
        }
        posPluie.y = Mathf.MoveTowards(posPluie.y, cibleYPluie, vPluie);
        solPluie.transform.position = posPluie;
    }
}