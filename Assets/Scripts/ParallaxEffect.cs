using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    public GameObject newSprite;
    GameObject player;
    Vector3 playerPos;
    Vector3 playerScreenPos;
    readonly float bufferDist = 15;

// sprite master
    SpriteRenderer sprite;
    float spriteOffsetX = 16.5f;

// sprite clone
    GameObject cloneSprite_L;
    GameObject cloneSprite_R;
    SpriteRenderer cloneRend_L;
    SpriteRenderer cloneRend_R;
    Vector3 cloneSpritePos_L;
    Vector3 cloneSpritePos_R;
    Vector2 cloneBounds_L;
    Vector2 cloneBounds_R;

// load bools
    bool requiresLoad_L = false;
    bool requiresLoad_R = false;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        player = GameObject.Find("Player");

        cloneSpritePos_L = this.transform.position;
        cloneSprite_L = Instantiate(newSprite, cloneSpritePos_L, Quaternion.identity);
        cloneSprite_L.name = $"{newSprite.name}_L";
        cloneRend_L = cloneSprite_L.GetComponent<SpriteRenderer>();
        cloneBounds_L = new Vector2(cloneRend_L.bounds.min.x, cloneRend_L.bounds.max.x);

        cloneSpritePos_R = new Vector3(cloneBounds_L.y + spriteOffsetX, cloneSpritePos_L.y, cloneSpritePos_L.z);
        cloneSprite_R = Instantiate(newSprite, cloneSpritePos_R, Quaternion.identity);
        cloneSprite_R.name = $"{newSprite.name}_R";
        cloneRend_R = cloneSprite_R.GetComponent<SpriteRenderer>();
        cloneBounds_R = new Vector2(cloneRend_R.bounds.min.x, cloneRend_R.bounds.max.x);

        sprite.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        playerPos = player.transform.position;
        playerScreenPos = Camera.main.WorldToViewportPoint(playerPos);
        cloneBounds_L = new Vector2(cloneRend_L.bounds.min.x, cloneRend_L.bounds.max.x);
        cloneBounds_R = new Vector2(cloneRend_R.bounds.min.x, cloneRend_R.bounds.max.x);

        if (player.transform.position.x + bufferDist >= cloneBounds_R.y && playerScreenPos.x > 0.5f)
        {
            requiresLoad_R = true;
        }
        else if (player.transform.position.x - bufferDist <= cloneBounds_L.x && playerScreenPos.x < 0.5f)
        {
            requiresLoad_L = true;
        }


        if (requiresLoad_R)
        {
            cloneSpritePos_L = cloneSpritePos_R;
            cloneSprite_L.transform.position = cloneSpritePos_L;
            cloneSpritePos_R = new Vector3(cloneBounds_R.y + spriteOffsetX, cloneSpritePos_R.y, cloneSpritePos_R.z);
            cloneSprite_R.transform.position = cloneSpritePos_R;
            requiresLoad_R = false;
        }
        else if (requiresLoad_L)
        {
            cloneSpritePos_R = cloneSpritePos_L;
            cloneSprite_R.transform.position = cloneSpritePos_R;
            cloneSpritePos_L = new Vector3(cloneBounds_L.x - spriteOffsetX, cloneSpritePos_L.y, cloneSpritePos_L.z);
            cloneSprite_L.transform.position = cloneSpritePos_L;
            requiresLoad_L = false;
        }
    }
}
