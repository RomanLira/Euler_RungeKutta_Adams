namespace MethodLib
{
    public class Vector
    {
        double[] v;
        int size;
        public int Size
        {
            get { return size; }
        }
        public double this[int j]
        {
            get { return v[j]; }
            set { v[j] = value; }
        }

        public Vector(int n)
        {
            size = n;
            v = new double[n];
        }

        public int Length()
        {
            return v.Length;
        }

        public static implicit operator double[] (Vector a)
        {
            double[] temp = new double[a.Length()];
            for (int i = 0; i < a.Length(); i++)
                temp[i] = a[i];
            return temp;
        }

        public static Vector operator +(Vector A, Vector B)
        {
            Vector C = new Vector(A.Length());
            for (int i = 0; i < A.Length(); i++)
            {
                C[i] = A[i] + B[i];
            }
            return C;
        }

        public static Vector operator +(Vector A, double B)
        {
            Vector C = new Vector(A.Length());
            for (int i = 0; i < A.Length(); i++)
            {
                C[i] = A[i] + B;
            }
            return C;
        }

        public static Vector operator +(double B, Vector A)
        {
            Vector C = new Vector(A.Length());
            for (int i = 0; i < A.Length(); i++)
            {
                C[i] = A[i] + B;
            }
            return C;
        }

        public static Vector operator -(Vector A, Vector B)
        {
            Vector C = new Vector(A.Length());
            for (int i = 0; i < A.Length(); i++)
            {
                C[i] = A[i] - B[i];
            }
            return C;
        }

        public static Vector operator *(Vector A, double h)
        {
            Vector temp = new Vector(A.Length());
            for (int i = 0; i < A.Length(); i++)
                temp[i] = A[i] * h;
            return temp;
        }

        public static Vector operator *(double h, Vector A)
        {
            Vector temp = new Vector(A.Length());
            for (int i = 0; i < A.Length(); i++)
                temp[i] = A[i] * h;
            return temp;
        }

        public static Vector operator /(Vector A, double b)
        {
            Vector temp = new Vector(A.Length());
            for (int i = 0; i < A.Length(); i++)
                temp[i] = A[i] / b;
            return temp;
        }
    }
}
