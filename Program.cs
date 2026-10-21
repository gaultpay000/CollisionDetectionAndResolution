namespace CollisionDetectionAndResolution
{
    using Vector3D_class;
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("B for box/cube collision \nC for circle/Sphere collision \nL for line/plane collision");
            string userInput = Console.ReadLine();

            switch (userInput.ToUpper())
            {
                case "B":
                    Vector3D boxA = new Vector3D(
                        TryToParse("box A X value"),
                        TryToParse("box A Y value"),
                        TryToParse("box A Z value"));

                    float axExtent = TryToParse("box A X extent");
                    float ayExtent = TryToParse("box A Y extent");
                    float azExtent = TryToParse("box A z extent");

                    Vector3D boxB = new Vector3D(
                        TryToParse("box B X value"),
                        TryToParse("box B Y value"),
                        TryToParse("box B Z value"));

                    float bxExtent = TryToParse("box B X extent");
                    float byExtent = TryToParse("box B Y extent");
                    float bzExtent = TryToParse("box B Z extent");

                    Vector3D vi = new Vector3D(
                        TryToParse("initial velocity X"),
                        TryToParse("initial velocity Y"),
                        TryToParse("initial velocity Z"));

                    AABB(boxA, axExtent, ayExtent, azExtent,
                         boxB, bxExtent, byExtent, bzExtent, 
                         vi);

                    break;
                case "C":
                    Vector3D xiA = new Vector3D(
                        TryToParse("circleA X initial position"),
                        TryToParse("circleA Y initial position"),
                        TryToParse("circleA Z initial position"));
                    float rA = TryToParse("circleA radius");

                    Vector3D xiB = new Vector3D(
                        TryToParse("circleB X initial position"),
                        TryToParse("circleB Y initial position"),
                        TryToParse("circleB Z initial position"));
                    float rB = TryToParse("circleB radius");

                    CirclesAndSpheresCollision(xiA, rA, xiB, rB);

                    break;
                case "L":
                    Vector3D xi = new Vector3D(
                        TryToParse("circle X initial position"),
                        TryToParse("circle Y initial position"),
                        TryToParse("circle Z initial position"));
                    float r = TryToParse("circle radius");

                    Vector3D d = new Vector3D(
                        TryToParse("amount line travels in the X"),
                        TryToParse("amount line travels in the Y"),
                        TryToParse("amount line travels in the Z"));
                    Vector3D P = new Vector3D(
                        TryToParse("X value of point on the line"),
                        TryToParse("Y value of point on the line"),
                        TryToParse("Z value of point on the line"));

                    LineCircleCollision(xi, r, P, d);

                    break;

            }
        }

        public static void AABB(Vector3D boxA, float AXextent, float AYextent, float AZextent,
            Vector3D boxB, float BXextent, float BYextent, float BZextent, Vector3D v)
        {
            float Axmin = boxA.getX();
            float Axmax = boxA.getX() + AXextent;

            float Aymin = boxA.getY();
            float Aymax = boxA.getY() + AYextent;

            float Azmin = boxA.getZ();
            float Azmax = boxA.getZ() + AZextent;


            float Bxmin = boxB.getX();
            float Bxmax = boxB.getX() + BXextent;

            float Bymin = boxB.getY();
            float Bymax = boxB.getY() + BYextent;

            float Bzmin = boxB.getZ();
            float Bzmax = boxB.getZ() + BZextent;

            if(Axmin > Bxmax || Bxmin > Axmax)
            {
                //no collision
            }
            else if(Aymin > Bymax || Bymin > Aymax)
            {
                //no collision
            }
            else if(Azmin > Bzmax || Bzmin > Azmax && Azmax != Azmin || Bzmax != Bzmin)
            {
                //no collision 
            }
            else
            {
                // there was a collision
            }
        }

        public static void CirclesAndSpheresCollision(Vector3D circleA, float rA, Vector3D circleB, float rB)
        {
            float distance = Distance(circleA, circleB);

            float rSquared = (rA + rB) * (rA + rB);

            if(distance > rSquared)
            {
                //no collision
            }
            else
            {
                //collision
            }
        }

        public static void LineCircleCollision(Vector3D circlePos, float r, Vector3D P, Vector3D d)
        {

        }

        public static void CollisionResolution()
        {

        }

        static float TryToParse(string message)
        {
            Console.WriteLine(message);

            float value;
            if (float.TryParse(Console.ReadLine(), out value))
            {
                return value;
            }
            else throw new Exception(value + "is not a valid value");
        }

        static float Distance(Vector3D a, Vector3D b)
        {
            float final = ((b.getX() - a.getX()) * (b.getX() - a.getX())) + 
                          ((b.getY() - a.getY()) * (b.getY() - a.getY())) + 
                          ((b.getZ() - a.getZ()) * (b.getZ() - a.getZ()));
            return final;
        }
    }
}
