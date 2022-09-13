using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Life : MonoBehaviour
{
    [SerializeField] float hp = 10.0f;
    [SerializeField] float maxhp = 10.0f;
    [SerializeField] Image lifeImg;

    RectTransform lifeImgrectTr;
    Vector2 imgInitPos;
    float shakeTime;

    private void Awake()
    {
        lifeImgrectTr = lifeImg.rectTransform;
        imgInitPos = lifeImgrectTr.anchoredPosition;
    }

    public void TakeDamage(float value)
    {
        if (hp <= 0.0f)
            return;

        SFXManager.Inst.SoundOnShot(eSFX.HIT);

        hp -= value;
        if (hp <= 0.0f)
        {        
            GameObject.FindObjectOfType<GameEnding>().CaughtPlayer();
        }
          

        lifeImg.fillAmount = hp / maxhp;
        shakeTime = 0.5f;
    }

    public void Update()
    {
        if (shakeTime > 0.0f)
        {
            shakeTime -= Time.deltaTime;
            lifeImgrectTr.anchoredPosition = 2 * Random.insideUnitCircle + imgInitPos;
        }
        else
            lifeImgrectTr.anchoredPosition = imgInitPos;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Enemies"))
        {
            TakeDamage(100.0f);
        }
    }
}
