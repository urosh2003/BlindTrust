using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeathAnimationScript : MonoBehaviour
{
    public float waitDuration = 3f;
    private float timer = -1f;

    public Sprite[] deathAssaultSprites;
    public Sprite[] deathBricksSprites;
    public Sprite[] deathCarSprites;
    public Sprite[] deathHoleSprites;
    public Sprite[] deathPeopleSprites;
    public Sprite[] deathSquirelSprites;

    public Image m_Image;

    public GameObject gameOverScreen;

    public Sprite[] m_SpriteArray;
    public float m_Speed = 0.02f;

    private int m_IndexSprite;
    Coroutine m_CorotineAnim;
    bool IsDone;

    public Sprite deathScreenGrandpa;
    public Sprite deathScreenPlayer;

    private Sprite deathScreen;

    public GameObject playAgainButton;
    public GameObject gameOverText;

    public AudioSource audio;

    public AudioClip splashSound;
    public AudioClip holeSound;
    public AudioClip squirelSound;
    public AudioClip carSound;
    public AudioClip banditSound;
    public AudioClip groupSound;

    public void Dead(int scenario)
    {
        gameOverScreen.SetActive(true);
        switch (scenario)
        {
            case 0:
                deathScreen = deathScreenGrandpa;
                m_SpriteArray = deathAssaultSprites;
                audio.clip = banditSound;
                break;
            case 1:
                deathScreen = deathScreenGrandpa;
                m_SpriteArray = deathBricksSprites;
                audio.clip = splashSound;
                break;
            case 2:
                deathScreen = deathScreenGrandpa;
                m_SpriteArray = deathCarSprites;
                audio.clip = carSound;
                break;
            case 3:
                deathScreen = deathScreenGrandpa;
                m_SpriteArray = deathHoleSprites;
                audio.clip = holeSound;
                break;
            case 4:
                deathScreen = deathScreenGrandpa;
                m_SpriteArray = deathPeopleSprites;
                audio.clip = groupSound;
                break;
            case 5:
                deathScreen = deathScreenGrandpa;
                m_SpriteArray = deathSquirelSprites;
                audio.clip = squirelSound;
                break;
        }
        timer = 0;
        audio.Play();
        Func_PlayUIAnim();
    }

    public void Func_PlayUIAnim()
    {
        IsDone = false;
        StartCoroutine(Func_PlayAnimUI());
    }

    public void Func_StopUIAnim()
    {
        IsDone = true;
        StopCoroutine(Func_PlayAnimUI());
    }
    IEnumerator Func_PlayAnimUI()
    {
        yield return new WaitForSeconds(m_Speed);
        if (m_IndexSprite >= m_SpriteArray.Length)
        {
            IsDone = true;
            yield break;
        }
        m_Image.sprite = m_SpriteArray[m_IndexSprite];
        m_IndexSprite += 1;
        if (IsDone == false)
            m_CorotineAnim = StartCoroutine(Func_PlayAnimUI());
    }

    private void Update()
    {
        if (timer >= 0)
        {
            timer += Time.deltaTime;
            if(timer >= waitDuration)
            {
                m_Image.sprite = deathScreen;
                Time.timeScale = 0;
                playAgainButton.SetActive(true);
                gameOverText.SetActive(true);
            }
        }
    }
}
