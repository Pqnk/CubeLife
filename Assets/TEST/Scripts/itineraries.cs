[System.Serializable]

public class itineraries
{
    /*
     * Exemple :
            [
              {
                "id": 0,
                "key": "string",
                "map_key": "string",
                "group_name": "string",
                "title": "string",
                "icon_url": "string",
                "banner_url": "string",
                "size": "XS",
                "distance": 0,
                "checkpoints": [],
                "available": true
              }
            ]
     * */

    public int id;
    public string key;
    public string map_key;
    public string group_name;
    public string title;
    public string icon_url;
    public string banner_url;
    public string size;
    public float distance;
    public float[] checkpoints;
    public bool available;
}
