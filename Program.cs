namespace CollisionDetectionAndResolution
{
    using System.Diagnostics;
    using Vector3D_class;
    internal class Program
    {
        static bool detectedCollision = false;
        static readonly float maxTime = 5f;
        static readonly float deltaT = .02f;
        static float elapsedTime = 0f;
        static readonly Vector3D acceleration = new Vector3D();

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

                    Vector3D vAi = new Vector3D(
                        TryToParse("A initial velocity X"),
                        TryToParse("A initial velocity Y"),
                        TryToParse("A initial velocity Z"));

                    Vector3D vBi = new Vector3D(
                        TryToParse("B initial velocity X"),
                        TryToParse("B initial velocity Y"),
                        TryToParse("B initial velocity Z"));

                    AABB(boxA, axExtent, ayExtent, azExtent,
                         boxB, bxExtent, byExtent, bzExtent, 
                         vAi, vBi);

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

                    Vector3D vA = new Vector3D(
                        TryToParse("circleA X initial Velocity"),
                        TryToParse("circleA Y initial Velocity"),
                        TryToParse("circleA Z initial Velocity"));
                    Vector3D vB = new Vector3D(
                        TryToParse("circleB X initial Velocity"),
                        TryToParse("circleB Y initial Velocity"),
                        TryToParse("circleB Z initial Velocity"));

                    CirclesAndSpheresCollision(xiA, rA, xiB, rB, vA, vB);

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

                    Vector3D v = new Vector3D(
                        TryToParse("X velocity of ball"),
                        TryToParse("X velocity of ball"),
                        TryToParse("X velocity of ball"));

                    float epsilon = TryToParse("value of epsilon");

                    LineCircleCollision(xi, r, P, d, v, epsilon);

                    break;

                case "P":
                    Vector3D initialPos = new Vector3D(
                        TryToParse("circle X initial position"),
                        TryToParse("circle Y initial position"),
                        TryToParse("circle Z initial position"));
                    float radius = TryToParse("circle radius");

                    Vector3D n = new Vector3D(
                        TryToParse("amount line travels in the X"),
                        TryToParse("amount line travels in the Y"),
                        TryToParse("amount line travels in the Z"));
                    Vector3D Point = new Vector3D(
                        TryToParse("X value of point on the line"),
                        TryToParse("Y value of point on the line"),
                        TryToParse("Z value of point on the line"));

                    Vector3D velocity = new Vector3D(
                        TryToParse("X velocity of ball"),
                        TryToParse("X velocity of ball"),
                        TryToParse("X velocity of ball"));

                    float e = TryToParse("value of epsilon");

                    LineCircleCollision(initialPos, radius, Point, n, velocity, e);

                    break;
            }
        }

        public static void AABB(Vector3D boxA, float AXextent, float AYextent, float AZextent,
            Vector3D boxB, float BXextent, float BYextent, float BZextent, Vector3D vA, Vector3D vB)
        {

            Vector3D posA = new Vector3D(boxA);
            Vector3D vecA = new Vector3D(vA);

            Vector3D posB = new Vector3D(boxB);
            Vector3D vecB = new Vector3D(vB);

            while (!detectedCollision && elapsedTime < maxTime)
            {
                float Axmin = posA.getX();
                float Axmax = posA.getX() + AXextent;

                float Aymin = posA.getY();
                float Aymax = posA.getY() + AYextent;

                float Azmin = posA.getZ();
                float Azmax = posA.getZ() + AZextent;


                float Bxmin = posB.getX();
                float Bxmax = posB.getX() + BXextent;

                float Bymin = posB.getY();
                float Bymax = posB.getY() + BYextent;

                float Bzmin = posB.getZ();
                float Bzmax = posB.getZ() + BZextent;

                if (Axmin > Bxmax || Bxmin > Axmax)
                {
                    //no collision
                }
                else if (Aymin > Bymax || Bymin > Aymax)
                {
                    //no collision
                }
                else if (Azmin > Bzmax || Bzmin > Azmax && Azmax != Azmin || Bzmax != Bzmin)
                {
                    //no collision 
                }
                else
                {
                    detectedCollision = true;
                    Console.WriteLine("Collision detected");
                    return;
                }

                ForwardPropigation(posA, vecA, out posA, out vecA);
                ForwardPropigation(posB, vecB, out posB, out vecB);
            }
        }

        public static void CirclesAndSpheresCollision(Vector3D circleA, float rA, Vector3D circleB, float rB, Vector3D vA, Vector3D vB)
        {
            Vector3D posA = new Vector3D(circleA);
            Vector3D vecA = new Vector3D(vA);

            Vector3D posB = new Vector3D(circleB);
            Vector3D vecB = new Vector3D(vB);

            float rSquared = (rA + rB) * (rA + rB);

            while (!detectedCollision && elapsedTime < maxTime)
            {
                float distance = Distance(posA, posB);

                if (distance > rSquared)
                {
                    //no collision
                }
                else
                {
                    detectedCollision = true;
                    Console.WriteLine("Collision detected");
                    return;
                }
                ForwardPropigation(posA, vecA, out posA, out vecA);
                ForwardPropigation(posB, vecB, out posB, out vecB);
            }
        }

        public static void LineCircleCollision(Vector3D circlePos, float r, Vector3D P, Vector3D d, Vector3D v, float epsilon)
        {
            Vector3D posA = new Vector3D(circlePos);
            Vector3D vecA = new Vector3D(v);

            while (!detectedCollision && elapsedTime < maxTime)
            {
                if(Vector3D.ClosestPointOnLine(circlePos, P, d).GetMagSq() <= r)
                {
                    detectedCollision = true;
                    Console.WriteLine("Collision Detected");
                    CollisionResolution(vecA, d, epsilon);
                }

                ForwardPropigation(posA, vecA, out posA, out vecA);
            }
        }

        public static void PlaneSphereCollision(Vector3D circlePos, float r, Vector3D P, Vector3D n, Vector3D v, float epsilon)
        {
            Vector3D posA = new Vector3D(circlePos);
            Vector3D vecA = new Vector3D(v);

            while (!detectedCollision && elapsedTime < maxTime)
            {
                if (Vector3D.ClosestPointOnLine(circlePos, P, n).GetMagSq() <= r)
                {
                    detectedCollision = true;
                    CollisionResolution(vecA, n, epsilon);
                    Console.WriteLine("Collision Detected");
                    CollisionResolution(vecA, n, epsilon);
                }

                ForwardPropigation(posA, vecA, out posA, out vecA);
            }
        }

        public static void CollisionResolution(Vector3D vi, Vector3D n, float epsilon)
        {
            float dotProduct = vi * (~n);

            Vector3D vf = vi - (1 + epsilon) * dotProduct * n;
            Console.WriteLine($"Velocity after collision: {vf.PrintRect()}");
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

        static void ForwardPropigation(Vector3D r, Vector3D v, out Vector3D rf, out Vector3D vf)
        {
            rf = r + v * deltaT;// new poisition = old position + old velocity * change in time
            vf = v + acceleration * deltaT; // new velocity = old velocity + acceleration * change in time

            elapsedTime += deltaT;//tracks time

            Console.WriteLine($"position {r.PrintRect()} m");
            Console.WriteLine($"Velocity {v.PrintRect()} m/s");
            Console.WriteLine();
        }
    }
}
