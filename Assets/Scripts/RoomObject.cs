using UnityEngine;

enum CardinalDirections
{
    Up,
    Down,
    Left,
    Right
}

public class RoomObject : MonoBehaviour
{
    public bool hasCardinalNeighbors;
    public LayerMask roomLayerMask;
    
    // x - up | y - down | z - left | w - right
    private Vector4Int _cardinalNeighbors = Vector4Int.Zero();
    private bool hasVerticalNeighbors = false;
    private bool hasHorizontalNeighbors = false;

    public bool shouldbedeleted = false;
    
    


    private void Start()
    {
        if (Physics2D.Raycast(transform.position, Vector2.up, 5.0f, roomLayerMask))
        {
            _cardinalNeighbors.x = 1;
        }
        if (Physics2D.Raycast(transform.position, Vector2.down, 5.0f, roomLayerMask))
        {
            _cardinalNeighbors.y = 1;
        }
        if (Physics2D.Raycast(transform.position, Vector2.left, 5.0f, roomLayerMask))
        {
            _cardinalNeighbors.z = 1;
        }
        if (Physics2D.Raycast(transform.position, Vector2.right, 5.0f, roomLayerMask))
        {
            _cardinalNeighbors.w = 1;
        }


        CheckNeighbors();


    }

    private void Update()
    {
    }

    public void CheckNeighbors()
    {
        if (_cardinalNeighbors.z == 0 && _cardinalNeighbors.w == 0)
        {
            hasHorizontalNeighbors = false;
        }
        else
        {
            hasHorizontalNeighbors = true;
        }

        if (_cardinalNeighbors.x == 0 && _cardinalNeighbors.y == 0)
        {
            hasVerticalNeighbors = false;
        }
        else
        {
            hasVerticalNeighbors = true;
        }


        if (!hasVerticalNeighbors && !hasHorizontalNeighbors)
        {
            shouldbedeleted = true;
        }
    }
}

public class Vector4Int
{
    public int x;
    public int y;
    public int z;
    public int w;

    public Vector4Int(int x, int y, int z, int w)
    {
        this.x = x;
        this.y = y;
        this.z = z;
        this.w = w;
    }

    public static Vector4Int Zero()
    {
        return new Vector4Int(0, 0, 0, 0);
    }
}
