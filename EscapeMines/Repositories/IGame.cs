namespace EscapeMines.Repositories
{
    public interface IGame
    {
        public void Load(string path);
        public void Move();
        public void ChangeDirection(Movement movement);
        public Status CheckStatus();
        public void Start();

        public bool CheckIntersection();

    }
}
