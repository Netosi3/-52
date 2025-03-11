using UnityEngine;

public class UnitStamina : MonoBehaviour {
    public float maxStamina = 100f;
    public float stamina;
    public float staminaDrain = 5f;
    public float staminaRegen = 3f;

    private bool isMoving;

    void Start() {
        stamina = maxStamina;
    }

    void Update() {
        isMoving = GetComponent<Unit>().IsMoving(); // Проверяем, движется ли юнит

        if (isMoving) {
            stamina -= staminaDrain * Time.deltaTime;
        } else {
            stamina += staminaRegen * Time.deltaTime;
        }

        stamina = Mathf.Clamp(stamina, 0, maxStamina);
    }
}
