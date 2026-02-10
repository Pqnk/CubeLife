using UnityEngine;

[System.Serializable]

public class PlayerMe
{
    /*
     * Example : 
                    {
                  "coins": 0,
                  "gems": 0,
                  "level": 0,
                  "xp": 0,
                  "total_distance": 0,
                  "icon": {
                    "url": "https://example.com/"
                  },
                  "banner": {
                    "url": "https://example.com/"
                  },
                  "title": {
                    "url": "https://example.com/"
                  }
                }
     */


    public int coins;
    public int gems;
    public int level;
    public int xp;
    public float total_distance;
    public ImageUrl icon = new ImageUrl();
    public ImageUrl banner = new ImageUrl();
    public ImageUrl title = new ImageUrl();

}

public class ImageUrl
{
    public string url = "";
}
