using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{

    private Player player;
    public Image[] Hearts;
    private Image HeartImage;
    public GameObject ParentObject;

    private ArrayList HeartList = new ArrayList();

    private float spacingX;

    void Start()
    {
        player = PlayerManager.instance.player.GetComponent<Player>();
        //player = PlayerManager.instance.player.GetComponent<Player>();
        spacingX = 100;

        AddHearts(player.startingHealth / 2);
    }

    public void AddHearts(int n)
    {
        for(int i = 0; i < n; i++)
        {
            // Instantiate new heart image
            Image newHeart = Instantiate(Hearts[2]);
            // Set new heart as parent of empty gameobject
            newHeart.transform.SetParent(ParentObject.transform, false);
             
            // Change position of each heart by i * spacing between each heart
            newHeart.rectTransform.localPosition = new Vector3(i * spacingX, 0, 0);
            // Add heart to array list
            HeartList.Add(newHeart);
        }
    }

    public void UpdateHearts()
    {
        //Players current health to decide heart sprites
        int i = player.currentHealth;
        foreach(Image h in HeartList)
        {
            if(i >= 2)
            {
                //Full heart
                h.sprite = Hearts[2].sprite;
                //Substract "full heart"
                i -= 2;
            }
            else if(i == 1)
            {
                //Half heart
                h.sprite = Hearts[1].sprite;
                //i = 0, rest of heart should be empty
                i = 0;
            }
            else
            {
                //Empty heart
                h.sprite = Hearts[0].sprite;
            }
        }
    }
}
