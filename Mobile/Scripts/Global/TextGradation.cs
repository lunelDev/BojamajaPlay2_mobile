using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class TextGradation : BaseMeshEffect
{
    public Gradient mygradient;

    public override void ModifyMesh(VertexHelper vh)
    {
        List<UIVertex> vertexs = new List<UIVertex>();
        vh.GetUIVertexStream(vertexs);

        float min = vertexs.Min(t => t.position.x);
        float max= vertexs.Max(t => t.position.x);

        for( int i =0; i<vertexs.Count; i++)
        {
            var v = vertexs[i];
            float curXNo = Mathf.InverseLerp(min,max, v.position.x);
            Color c = mygradient.Evaluate(curXNo);
           
            v.color = new Color(c.r ,c.g, c.b,1);
            vertexs[i] = v; 
            
        }
        vh.Clear();
        vh.AddUIVertexTriangleStream(vertexs);
        throw new System.NotImplementedException();
    }
}
