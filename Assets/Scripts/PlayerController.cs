using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rigidbody2d;
    private float horizontal;
    private float vertical;
    [SerializeField] private float speed = 5.0f;

    private Animator anim;
    private Vector3 initialScale;

    [SerializeField] private float delayBeforeGameOver = 1.5f;
    private bool isPlayerDead = false;

    public float targetTime = 30.0f;
    public string sceneLoader;

    void Start()
    {
        anim = GetComponent<Animator>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        initialScale = transform.localScale;
    }

    void Update()
    {
        if (isPlayerDead) return;

        HandleInput();
        UpdateTimer();
        UpdateAnimation();
    }

    private void FixedUpdate()
    {
        if (isPlayerDead) return;

        MovePlayer();
    }

    void HandleInput()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
    }

    void UpdateTimer()
    {
        targetTime -= Time.deltaTime;
        if (targetTime <= 0.0f)
        {
            ToggleScene();
        }
    }

    void UpdateAnimation()
    {
        bool isRunning = Mathf.Abs(horizontal) > 0.1f || Mathf.Abs(vertical) > 0.1f;
        anim.SetBool("isRunning", isRunning);
        FlipSprite();
    }

    void MovePlayer()
    {
        Vector2 pos = rigidbody2d.position;
        pos.x += speed * horizontal * Time.deltaTime;
        pos.y += speed * vertical * Time.deltaTime;
        rigidbody2d.MovePosition(pos);
    }

    void FlipSprite()
    {
        if (horizontal < 0)
        {
            transform.localScale = new Vector3(-initialScale.x, initialScale.y, initialScale.z);
        }
        else if (horizontal > 0)
        {
            transform.localScale = initialScale;
        }
    }

    void ToggleScene()
    {
        Vector3 playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        SceneManager.LoadScene(sceneLoader);

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            player.transform.position = playerPosition;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("BolaVermelha") || collision.gameObject.CompareTag("BolaForteVermelha"))
        {
            anim.SetTrigger("isDead");
            StartCoroutine(LoadGameOverSceneWithDelay());
        }
    }

    IEnumerator LoadGameOverSceneWithDelay()
    {
        isPlayerDead = true;
        yield return new WaitForSeconds(delayBeforeGameOver);
        SceneManager.LoadScene("GameOver");
    }
}
