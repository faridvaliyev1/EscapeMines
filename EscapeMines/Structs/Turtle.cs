namespace EscapeMines.Repositories
{
    public struct Turtle
    {
        public Coordinate coordinate { get; set; }
        public Direction direction { get; set; }

        public Turtle(Coordinate coordinate, Direction direction)
        {
            this.coordinate = coordinate;
            this.direction = direction;
        }
    }
}
