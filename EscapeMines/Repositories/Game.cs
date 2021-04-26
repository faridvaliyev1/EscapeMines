using EscapeMines.Extensions;
using System;
using System.Collections.Generic;
using System.IO;

namespace EscapeMines.Repositories
{
    public class Game : IGame
    {
        public int GridM { get; set; }
        public int GridN { get; set; }

        public HashSet<Coordinate> Mines = new HashSet<Coordinate>();
        public Turtle Turtle { get; set; }
        public Coordinate Exit { get; set; }

        public List<Movement> Movements = new List<Movement>();
        public void Load(string path)
        {
          using(StreamReader reader=new StreamReader(path))
            {
                while (!reader.EndOfStream)
                {
                    
                    try
                    {
                        //starting reading Grid Size
                        string[] first_line = reader.ReadLine().Split(' ');

                        GridN = Convert.ToInt32(first_line[0]);
                        GridM = Convert.ToInt32(first_line[1]);

                        //end of reading Grid Size

                        //starting reading mines from the second line
                        string[] second_line = reader.ReadLine().Split(' ');

                        for(int i = 0; i < second_line.Length; i++)
                        {

                            string[] line = second_line[i].Split(',');
                            Mines.Add(new Coordinate
                            {
                                X = Convert.ToInt32(line[0]),
                                Y = Convert.ToInt32(line[1])
                            });
                        }

                        //end of the reading mines

                        //starting reading of Exit coordinate
                        string[] third_line = reader.ReadLine().Split(' ');

                        Exit = new Coordinate
                        {
                            X = Convert.ToInt32(third_line[1]),
                            Y = Convert.ToInt32(third_line[0])
                        };
                        //end of reading Exit Coordinate
                        
                        //start of reading Turtle data
                        string[] fourth_line = reader.ReadLine().Split(' ');

                        Turtle = new Turtle
                        {
                            coordinate=new Coordinate
                            {
                                X = Convert.ToInt32(fourth_line[1]),
                                Y = Convert.ToInt32(fourth_line[0]),
                            } ,
                            //converting string value to the enum
                            direction=(Direction)Enum.Parse(typeof(Direction),fourth_line[2],true)
                        };

                        //end of reading Turtle data


                        //start of the reading movements
                        string[] fifth_line = reader.ReadLine().Split(' ');

                        foreach(var movement in fifth_line)
                        {
                            //Converting string value to enum value
                            Movements.Add((Movement)Enum.Parse(typeof(Movement), movement, true));
                        }

                        //end of the reading movements
                    }

                    catch
                    {
                        Console.WriteLine("There was a problem while reading");
                    }
                }
            }
        }
        public void Move()
        {
            Coordinate _coordinate = new Coordinate();
            if (Turtle.direction == Direction.N)
            {
                _coordinate.X = Turtle.coordinate.X - 1;
                _coordinate.Y = Turtle.coordinate.Y;
            }
            else if (Turtle.direction == Direction.E)
            {
                _coordinate.X = Turtle.coordinate.X;
                _coordinate.Y = Turtle.coordinate.Y+1;
            }
            else if (Turtle.direction == Direction.W)
            {
                _coordinate.X = Turtle.coordinate.X;
                _coordinate.Y = Turtle.coordinate.Y - 1;
            }
            else
            {
                _coordinate.X = Turtle.coordinate.X + 1;
                _coordinate.Y = Turtle.coordinate.Y;
            }

            Turtle = new Turtle(_coordinate, Turtle.direction);
        }
        public void ChangeDirection(Movement movement)
        {

            Coordinate _coordinate = new Coordinate()
            {
                X = Turtle.coordinate.X,
                Y = Turtle.coordinate.Y
            };
            //in here I change it with clockwise for enum values
            if (movement == Movement.R)
            {
                //I have written enum extension for incrementing value
                Turtle = new Turtle(_coordinate, Turtle.direction.Next());
            }
            else
            {
                //In here I have created enum extension for getting counter clock wise values
                Turtle = new Turtle(_coordinate, Turtle.direction.Prev());
            }
            
        }

        //For each move I check status before move
        public Status CheckStatus()
        {
            if (CheckIntersection())
            {
                return Status.Crossed_line;
            }
            else if (Mines.Contains(Turtle.coordinate))
            {
                return Status.Failure;
            }
            else if (Exit.Equals(Turtle.coordinate))
            {
                return Status.Success;
            }
            else
            {
                return Status.In_Danger;
            }
        }
        public void Start()
        {
            foreach (var movement in Movements)
            {
                if (movement != Movement.M)
                {
                    ChangeDirection(movement);
                }
                else
                {
                    Move();

                    var status = CheckStatus();

                    Console.WriteLine(status.ToDescription());

                    if (status == Status.Failure || status == Status.Success || status==Status.Crossed_line)
                    {
                        break;
                    }

                }
            }
        }

        public bool CheckIntersection()
        {
            if (Turtle.direction == Direction.N && Turtle.coordinate.X< 0)
                return true;
            else if (Turtle.direction == Direction.E && Turtle.coordinate.Y> GridN)
                return true;
            else if (Turtle.direction == Direction.W && Turtle.coordinate.Y< 0)
                return false;
            else if (Turtle.direction == Direction.S && Turtle.coordinate.X> GridM)
                return true;
            else
                return false;   
        }
    }
}
