using UnityEngine;

[RequireComponent(typeof(CharacterController), typeof(Animator))]
public class PlayerContext : MonoBehaviour
{
    public CharacterController Controller {  get; private set; }
    public Animator Anim { get; private set; }
    [SerializeField] GameObject playerAvata;
    public GameObject PlayerAvata => playerAvata;

    private void Awake()
    {
        Controller = GetComponent<CharacterController>();
        Anim = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        RestartEvent.OnRestart += Respawn;
    }

    private void OnDisable()
    {
        RestartEvent.OnRestart -= Respawn;
    }

    void Respawn()
    {
        Controller.enabled = false;
        transform.position = Vector3.up;
        Controller.enabled = true;
    }
}
