using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement2D : MonoBehaviour
{
    [SerializeField] private float velocidade = 5f;
    [SerializeField] private float forcaDoPulo = 8f;
    [SerializeField] private Transform checagemDoChao;
    [SerializeField] private LayerMask camadaDoChao;

    private Rigidbody2D rb;
    private float movimentoHorizontal;
    private bool pediuPulo;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Keyboard.current == null) return;

        movimentoHorizontal = 0f;

        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
            movimentoHorizontal = -1f;

        if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
            movimentoHorizontal = 1f;

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
            pediuPulo = true;
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(
            movimentoHorizontal * velocidade,
            rb.linearVelocity.y
        );

        bool noChao = checagemDoChao != null &&
            Physics2D.OverlapCircle(checagemDoChao.position, 0.15f, camadaDoChao);

        if (pediuPulo && noChao)
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, forcaDoPulo);

        pediuPulo = false;
    }
}
