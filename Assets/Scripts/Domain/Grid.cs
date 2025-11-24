namespace Domain
{
  public class Grid
  {
    public int Width { get; private set; }
    public int Height { get; private set; }
    
    public Grid(int width, int height)
    {
      Width = width;
    }
  }
}