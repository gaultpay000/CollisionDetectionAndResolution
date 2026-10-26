using System.Diagnostics;

namespace Vector3D_class
{
    public class Vector3D
    {
        #region Private Variables

        float x;
        float y;
        float z;

        float w = 1;

        #endregion

        #region Constructors

        public Vector3D()
        {
            x = 0; 
            y = 0; 
            z = 0; 
        }

        public Vector3D(Vector3D v)
        {
            x = v.x;
            y = v.y;
            z = v.z;
        }

        public Vector3D(float a, float b)
        {
            x = a;
            y = b;
            z = 0;
        }

        public Vector3D(float a, float b, float c)
        {
            x = a;
            y = b;
            z = c;
        }
        #endregion

        #region Setters
        public void SetRectGivenRect(float a, float b)
        {
            x = a;
            y = b;
            z = 0;
        }

        public void SetRectGivenRect(float a, float b, float c)
        {
            x = a;
            y = b;
            z = c;
        }

        public void SetRectGivenPolar(float m ,float h)
        {
            x = m * MathF.Cos(h);
            y = m * MathF.Sin(h);
        }

        public void SetRectGivenMagHeadPitch(float m, float h, float p)
        {
            x = m * MathF.Cos(p) * MathF.Cos(h);
            y = m * MathF.Cos(p) * MathF.Sin(h);
            z = m * MathF.Sin(p);
        
        }
        #endregion

        #region Printers
        public string PrintRect()
        {
            return($"<{x}, {y}, {z}>");
        }

        public string PrintMag()
        {
            return $"{GetMag()}";
        }

        public string PrintPolar()
        {
            return $" Magnitude = {GetMag()}, Heading = {MathF.Acos(x / MathF.Sqrt((x * x) + (y * y)))}";
        }

        public string PrintMagHeadPitch()
        {
            return $"{PrintPolar()}, Pitch = {MathF.Asin(z / GetMag())}";
        }

        public string PrintDirection()
        {
            string alpha = $"Alpha angle = {MathF.Acos(x / GetMag())} ";
            string beta = $"Beta angle = {MathF.Acos(y / GetMag())} ";
            string gamma = $"Gamma angle = {MathF.Acos(z / GetMag())}";

            return alpha + beta + gamma;
        }

        #endregion

        #region Getters
        public float GetMag()
        {

            return MathF.Sqrt((x * x) + (y * y) + (z * z));
        }

        public float GetMagSq()
        {
            return (x * x) + (y * y) + (z * z);
        }

        public float getX() { return x; }
        public float getY() { return y; }
        public float getZ() { return z; }
        #endregion

        #region Operators
        public static Vector3D operator +(Vector3D first, Vector3D second)
        {
            Vector3D sum = new Vector3D();

            sum.SetRectGivenRect(first.getX() + second.getX(),
                first.getY() + second.getY(), first.getZ() + second.getZ());
            return sum;
        }
        public static Vector3D operator +(Vector3D first, float scalar)
        {
            Vector3D sum = new Vector3D();

            sum.SetRectGivenRect(first.getX() + scalar,
                first.getY() + scalar, first.getZ() + scalar);
            return sum;
        }

        public static Vector3D operator -(Vector3D first, Vector3D second)
        {
            Vector3D difference = new Vector3D();

            difference.SetRectGivenRect(first.getX() - second.getX(),
                first.getY() - second.getY(), first.getZ() - second.getZ());
            return difference;
        }

        public static Vector3D operator *(Vector3D vec, float scalar)
        {
            Vector3D quantity = new Vector3D();

            quantity.SetRectGivenRect(vec.getX() * scalar, 
                vec.getY() * scalar, vec.getZ() * scalar);
            return quantity;
        }

        public static Vector3D operator *(float scalar, Vector3D vec)
        {
            Vector3D quantity = new Vector3D();

            quantity.SetRectGivenRect(vec.getX() * scalar,
                vec.getY() * scalar, vec.getZ() * scalar);
            return quantity;
        }

        public static Vector3D operator /(Vector3D vec, float scalar)
        {
            Vector3D quotient = new Vector3D();

            quotient.SetRectGivenRect(vec.getX() / scalar,
                vec.getY() / scalar, vec.getZ() / scalar);
            return quotient;
        }

        public static Vector3D operator ~(Vector3D vec) 
        {
            Vector3D normal = new Vector3D();

            normal.SetRectGivenRect(vec.getX() / vec.GetMag(),
                vec.getY() / vec.GetMag(), vec.getZ() / vec.GetMag());
            return normal;
        }

        public static float operator *(Vector3D Va, Vector3D Vb)
        {
            float dotProduct = (Va.getX() * Vb.getX()) + (Va.getY() * Vb.getY()) + (Va.getZ() * Vb.getZ());
            return dotProduct;
        }
        
        public static float operator %(Vector3D Va, Vector3D Vb)
        {
            float theta = (Va * Vb) / (Va.GetMag() * Vb.GetMag());
            return theta;
        }
        #endregion

        public static Vector3D ParallelProjection(Vector3D u, Vector3D v)
        {
            Vector3D dotProduct = ((v * u) / u.GetMag()) * u;

            return dotProduct;
        }

        public static Vector3D PerpendicularProjection(Vector3D u, Vector3D v)
        {
            Vector3D proj = u - ParallelProjection(u, v);

            return proj;
        }

        public static Vector3D CrossProduct(Vector3D u, Vector3D v)
        {
            float x = u.getY() * v.getZ() - v.getY() * u.getZ();
            float y = v.getX() * u.getZ() + u.getX() * v.getZ();
            float z = u.getX() * v.getY() - v.getX() * u.getY();

            return new Vector3D(x, y, z);
        }

        public static Vector3D ClosestPointOnLine(Vector3D Q, Vector3D P, Vector3D d)
        {
            Vector3D PQ = Q - P;

            Vector3D proj = ParallelProjection(d, P);

            return P + proj;
        }

        public static Vector3D ClosestPointOnPlane(Vector3D Q,Vector3D P, Vector3D n)
        {
            Vector3D PQ = Q - P;

            Vector3D proj = ParallelProjection(n, PQ);

            return Q - proj;
        }
    }
}
