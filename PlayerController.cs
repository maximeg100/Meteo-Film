using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    
    [Header("Réglages Mouvement")]
    public float vitesseDeplacement = 5f;
    private float directionX;

    void Start()
    {
    }

    void OnMove(InputValue value)
    {
        directionX = value.Get<float>();
        Debug.Log("Direction reçue : " + directionX); // <-- Ajoute juste cette ligne
    }

    void Update()
    {
        transform.Translate(Vector3.right * directionX * vitesseDeplacement * Time.deltaTime);
    }
}