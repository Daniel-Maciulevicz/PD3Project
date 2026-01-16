using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static PlayerInput Input { get; private set; }
    [SerializeField]
    private PlayerInput _input;

    private void Awake()
    {
        if (Input == null)
        {
            Input = _input;

            transform.SetParent(null);
            DontDestroyOnLoad(gameObject);

            Debug.Log("Inputs set to " + Input);
        }
        else
            Destroy(gameObject);
    }
}