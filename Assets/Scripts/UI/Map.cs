using UnityEngine;  
using System.Collections;  
  
public class Map : MonoBehaviour {  
  
    public Texture map1;//小地形图    
    public Texture jueseTexture;//标识角色的图片    
    float juesePosX = 0;  
    float juesePosY = 0;  
    public GameObject player;//角色    
    public GameObject plane;//地形    
    float planeWidth;//地形的宽    
    float planeHeight;//地形的高    
  
    float angle = 0; //人物旋转的角度  
  
    void Start()  
    {  
        //获取地形的宽高    
        planeWidth = plane.GetComponent<MeshFilter>().mesh.bounds.size.x * plane.transform.localScale.x;  
        planeHeight = plane.GetComponent<MeshFilter>().mesh.bounds.size.z * plane.transform.localScale.z;  
        print("width+heith:"+planeWidth + ", " + planeHeight);  
        print("bounds:" + plane.GetComponent<MeshFilter>().mesh.bounds);  
    }  
    void OnGUI()  
    {  
        GUI.DrawTexture(new Rect(Screen.width-map1.width, 0, map1.width, map1.height), map1);  
  
        GUIUtility.RotateAroundPivot(angle, new Vector2((Screen.width - map1.width)+juesePosX + 5, juesePosY + 5));  
  
        GUI.DrawTexture(new Rect((Screen.width - map1.width)+juesePosX, juesePosY, 10, 10), jueseTexture);  
    }  
  
  
    void Update()  
    {  
        print("people:" + player.transform.position.x + "," + player.transform.position.y);  
        print(1);  
        //根据palyer在plane的比例关系，映射到对应地图位置。    
        juesePosX = map1.width * player.transform.position.x / planeWidth + map1.width / 2;  
        juesePosY = map1.height * (-player.transform.position.z) / planeHeight + map1.height / 2;  
  
        print("x:" + juesePosX + "y:" + juesePosY);  
  
        angle = player.transform.eulerAngles.y-90;  
        print("angle:" + angle);  
    }   
} 