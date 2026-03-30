using UnityEngine;

public interface ICrumblingWall
{
    public int MaxCrumblingCount {  get; set; }
    public int CurrentCrumblingCount { get; set; } 
    public void Crumbling(int count);
    public void Destroy();
}
