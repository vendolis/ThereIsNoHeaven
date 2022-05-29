using System;
using System.Runtime.Serialization;
using UnityEngine;

[Serializable]
public class FieldBounds : ISerializable
{
    public FieldBounds() {}

    public FieldBounds(Vector2 topLeft, Vector2 bottomRight)
    {
        TopLeft = topLeft;
        BottomRight = bottomRight;
    }
    
    protected FieldBounds(SerializationInfo info, StreamingContext context)
    {
        if (info == null)
            throw new System.ArgumentNullException("info");
        
        TopLeft = new Vector2((float)info.GetValue("TopLeftX", typeof(float)),
            (float)info.GetValue("TopLeftY", typeof(float)));
        BottomRight = new Vector2((float)info.GetValue("BottomRightX", typeof(float)),
            (float)info.GetValue("BottomRightY", typeof(float)));
    }

    public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
    {
        if (info == null)
            throw new System.ArgumentNullException("info");
        
        info.AddValue("TopLeftX", TopLeft.x);
        info.AddValue("TopLeftY", TopLeft.y);
        info.AddValue("BottomRightX", BottomRight.x);
        info.AddValue("BottomRightY", BottomRight.y);
    }
    
    public Vector2 TopLeft { get; set; }
    public Vector2 BottomRight { get; set; }
    
    public void SetBounds(Vector2 topLeft, Vector2 bottomRight)
    {
        TopLeft = topLeft;
        BottomRight = bottomRight;
    }
    
}