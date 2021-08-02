using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
public class generateSpriteShapeCollider : MonoBehaviour
{
    public SpriteShapeController spriteShape;
    public EdgeCollider2D edge;
    public float boxOffset;
    public float radius;
    [ContextMenu("Fine, I will do it myself")]
    void generateCollider(){
       for (int i = this.transform.childCount; i > 0; --i){
           if(transform.GetChild(0).gameObject.name == "box")
            DestroyImmediate(this.transform.GetChild(0).gameObject);
       }
            
        // List<Vector2> positions = new List<Vector2>(); 
        // for(int i = 0; i<spriteShape.spline.GetPointCount();i++){
           
        //     positions.Add(spriteShape.spline.GetPosition(i));
            
            
        // }
        // edge.SetPoints(positions);
        // edge.edgeRadius=radius;
        List<Vector2> positions = new List<Vector2>();
        edge.GetPoints(positions);
        Vector2 dir = positions[1]-positions[0];
        edge.edgeRadius=radius*transform.localScale.x;
        spawnBox(positions[0],dir,transform);

        dir = positions[positions.Count-2]-positions[positions.Count-1];
        spawnBox(positions[positions.Count-1],dir,transform);
        
        
    }
    GameObject spawnBox(Vector2 pos, Vector2 dir,Transform parent){
        GameObject box = new GameObject("box");
        BoxCollider2D col = box.AddComponent<BoxCollider2D>();
        col.size = Vector2.one*edge.edgeRadius*2;
        box.transform.right=transform.TransformDirection(dir);
        box.transform.position=((Vector2)transform.TransformPoint(pos)-(Vector2)(box.transform.right.normalized*(radius*boxOffset))*transform.localScale.x);
        
        box.transform.parent=parent;
        box.transform.SetAsFirstSibling();
        
        return box;

    }
}
