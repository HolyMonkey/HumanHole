using UnityEngine;

public class FactoryService
{
    public FactoryService()
    {
        
    }
    
    public Wall CreateWall(int id, Transform position)
    {
        return new Wall();
    }

    public Sprite CreateCountour(int id, Transform position)
    {
        return null;
    }
    
    
}
