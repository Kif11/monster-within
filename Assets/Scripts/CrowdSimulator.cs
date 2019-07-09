using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdSimulator : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject policePrefab;
    public GameObject crowdPrefab;
    public int CivilianCount;
    public Color[] Tints;
    public GameObject Char;
    public GameObject HealthHeart;
    private Animator animator;
    private MeshRenderer healthRenderer;

    struct Civilian
    {
        public GameObject gameObject;
        public float status;
        public float stoppingRadius;
        public Quaternion originalRot;
    }
    private Civilian[] Civilians;
    private Vector3 center;
    private int tintCount;
    private float healthPoints;

    void Start()
    {
        center = new Vector3(-0.7f, 0f, -3.95f);
        Civilians = new Civilian[CivilianCount];

        for (int i = 0; i < CivilianCount; i++)
        {
            StartCoroutine("OffsetCivilianEntry", i);
            Civilians[i].status = 1.0f;
        }
        tintCount = Tints.Length;

        animator = Char.GetComponent<Animator>();

        HealthHeart.SetActive(true);
        healthRenderer = HealthHeart.GetComponent<MeshRenderer>();
        healthPoints = 200f;
        StartCoroutine("FadeInHeart");
    }

    IEnumerator FadeInHeart ()
    {
        float heartScale = 0.0f;
        Vector3 originalHeartScale = HealthHeart.transform.localScale;
        HealthHeart.transform.localScale = heartScale * originalHeartScale;

        yield return new WaitForSeconds(6);

        while (heartScale < 1.0f)
        {
            HealthHeart.transform.localScale = heartScale * originalHeartScale;
            heartScale += Time.deltaTime;
            yield return null;
        }
        yield return 0;
    }

    void SetRandomColor(GameObject civilian)
    {
        GameObject body = civilian.transform.Find("body_mesh").gameObject;
        Renderer charRenderer = body.GetComponent<Renderer>();
        int randomInt = Mathf.RoundToInt(Random.Range(0.0f, tintCount-1.0f));
        charRenderer.material.SetColor("_Color", Tints[randomInt]);
    }

    GameObject InitCivilian(int i)
    {
        float angle = i * 2.0f * Mathf.PI / CivilianCount;
        Vector3 pos = new Vector3(Mathf.Cos(angle), 0.0f, Mathf.Sin(angle));
        pos = 15.5f * pos + center;

        Vector3 dir = Vector3.Normalize(pos - center);
        Quaternion _lookRotation = Quaternion.LookRotation(dir);
        _lookRotation *= Quaternion.Euler(Vector3.up * 180f);

        GameObject anotherCivilian;
        if (i%3 == 1)
        {
            anotherCivilian = Instantiate(policePrefab, pos, _lookRotation);
        }
        else
        {
            anotherCivilian = Instantiate(crowdPrefab, pos, _lookRotation);
            SetRandomColor(anotherCivilian);
        }
        Civilians[i].gameObject = anotherCivilian;
        Civilians[i].originalRot = _lookRotation;
        Civilians[i].status = 0.0f;
        Civilians[i].stoppingRadius = 1.6f + Random.Range(-0.5f, 0.5f);

        return anotherCivilian;
    }

    // Update is called once per frame
    void Update()
    {

        for (int i = 0; i < CivilianCount; i++)
        {
            if (Civilians[i].status > 0.9f) 
            {
                continue;
            };

            GameObject civilian = Civilians[i].gameObject;
            Vector3 pos = civilian.transform.position;

            if (Vector3.Distance(pos, center) > Civilians[i].stoppingRadius)
            {
                Vector3 newPos = Vector3.MoveTowards(pos, center, Time.deltaTime);
                civilian.transform.position = newPos;
                Transform root = civilian.transform.GetChild(1);
                Vector3 rootPos = root.position;
                rootPos.y = 0.01828616f + 0.15f * Mathf.Abs(Mathf.Sin(2f * Time.fixedTime + i) + Mathf.Cos(4f * Time.fixedTime + i));
                root.position = rootPos;
            }
            else
            {
                healthPoints -= 0.01f;
                animator.SetFloat("HealthPoints", healthPoints);
                healthRenderer.material.SetFloat("_FillAmount", Mathf.Min(healthPoints / 200f,1f));
            }
            float dot = Vector3.Dot(civilian.transform.up, Vector3.up);
            if(dot < 0.1)
            {
                StartCoroutine("DestroyCivilian", i);
            }
        }
    }

    void LateUpdate()
    {
        for (int i = 0; i < CivilianCount; i++)
        {
            if (Civilians[i].status < 0.9f)
            {
                Civilians[i].gameObject.transform.rotation = Quaternion.Lerp(Civilians[i].gameObject.transform.rotation, Civilians[i].originalRot, 2f * Time.deltaTime);
            }
        }
    }

        IEnumerator DestroyCivilian(int i)
    {
        Civilians[i].status = 1.0f;
        yield return new WaitForSeconds(2);
        GameObject civilian = Civilians[i].gameObject;
        Destroy(civilian);
        InitCivilian(i);
        yield return 0;
    }

    IEnumerator OffsetCivilianEntry(int i)
    {
        float round = 20 * (i % 4) + Random.Range(0.0f, 2.0f);
        yield return new WaitForSeconds(round);
        InitCivilian(i);
        yield return 0;
    }

    void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 100, 20), healthPoints.ToString());
    }
}
