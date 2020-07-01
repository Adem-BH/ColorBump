using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Control : MonoBehaviour
{
    private Rigidbody rb;
    Vector2 LastMousePos = Vector2.zero;
    public float SwipeForce;

    [SerializeField] float WallDistance;
    [SerializeField] float MinCamDistance;

    public float speed;

    public GameObject WinPanel;

    public GameObject Color;

    public Image Fade;

    public int Level;


    IEnumerator Die(float DelayTime)
    {
        print("Dead");
        speed = 0;
        SwipeForce = 0;

        


        yield return new WaitForSeconds(DelayTime);

        SceneManager.LoadScene(Level);

    }


    IEnumerator LevelChange(float DelayTime)
    {


        SwipeForce = 0;
        speed = 0;
        rb.velocity = Vector3.zero;

        Fade.DOFade(1, 1f);

        yield return new WaitForSeconds(DelayTime);
        Level = 1;

        PlayerPrefs.SetInt("Level", Level);
        SceneManager.LoadScene(1);


    }

    IEnumerator End(float DelayTime)
    {


        SwipeForce = 0;
        speed = 0;
        rb.velocity = Vector3.zero;

       

        yield return new WaitForSeconds(DelayTime);

        Level = 0;

        PlayerPrefs.SetInt("Level", Level);

        WinPanel.SetActive(true);


    }

    void Start()
    {
        Level = PlayerPrefs.GetInt("Level");
        rb = GetComponent<Rigidbody>();

    }


    void Update()
    {

        Vector2 DeltaPos = Vector2.zero;

        if (Input.GetMouseButton(0))
        {

            Vector2 CurrentMousePos = Input.mousePosition;

            if (LastMousePos == Vector2.zero) LastMousePos = CurrentMousePos;

            DeltaPos = CurrentMousePos - LastMousePos;

            LastMousePos = CurrentMousePos;

            Vector3 Force = new Vector3(DeltaPos.x, 0, DeltaPos.y) * SwipeForce;
            rb.AddForce(Force);

        }

        else
        {

            LastMousePos = Vector2.zero;

        }

        if (Input.GetKeyDown(KeyCode.Escape) && Color.gameObject.activeSelf == false)
        {

            Color.SetActive(true);
            speed = 0;
            SwipeForce = 0;
            rb.velocity = Vector3.zero;

        }

        else if (Input.GetKeyDown(KeyCode.Escape) && Color.gameObject.activeSelf == true)
        {

            Color.SetActive(false);
            speed = 5;
            SwipeForce = 200;

        }

    }

    void LateUpdate()
    {

        Vector3 Position = transform.position;

        if (Position.z < Camera.main.transform.position.z + MinCamDistance)
        {

            Position.z = Camera.main.transform.position.z + MinCamDistance;

        }



        if (Position.x < -WallDistance)
        {

            Position.x = -WallDistance;

        }

        else if (Position.x > WallDistance)
        {

            Position.x = WallDistance;

        }

        transform.position = Position;
    }

    void FixedUpdate()
    {

        rb.MovePosition(rb.position + Vector3.forward * speed * Time.fixedDeltaTime);


        Camera.main.transform.position += Vector3.forward * speed * Time.fixedDeltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Evil")
        {

            StartCoroutine(Die(2));

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Goal")
        {

            StartCoroutine(LevelChange(2));

        }

        if (other.gameObject.tag == "End")
        {

            StartCoroutine(End(0.5f));

        }
    }
}
