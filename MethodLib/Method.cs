using System;

namespace MethodLib
{
    public class Method
    {
        public Method ()
        {

        }

        public static Vector Function(Vector coefficients, Vector y)
        {
            int n = y.Length();
            Vector dy = new Vector(n);
            switch (y.Length())
            {
                case (2):
                    {
                        for (int i = 0; i < n; i++)
                            dy[i] = coefficients[i * n] * y[0] + coefficients[i * n + 1] * y[1];
                        break;
                    }
                case (3):
                    {
                        for (int i = 0; i < n; i++)
                            dy[i] = coefficients[i * n] * y[0] + coefficients[i * n + 1] * y[1] + coefficients[i * n + 2] * y[2];
                        break;
                    }
                case (4):
                    {
                        for (int i = 0; i < n; i++)
                            dy[i] = coefficients[i * n] * y[0] + coefficients[i * n + 1] * y[1] + coefficients[i * n + 2] * y[2] + coefficients[i * n + 3] * y[3];
                        break;
                    }
            }
            return dy;
        }

        private static void AddToMatrix(ref double[,] result, Vector y, int j, double x)
        {
            for (int i = 1; i <= y.Length(); i++)
            {
                result[j, i] = y[i - 1];
            }
            result[j, 0] = x;
        }

        public static double[,] Euler (Vector coefficients, double x0, double xn, double h, Vector y, int n)
        {
            var result = new double[n, y.Length() + 1];
            var i = 0;
            for (var x = x0; Math.Round(x, 5) <= xn; x += h)
            {
                var dy = Function(coefficients, y);
                y += h * dy;
                AddToMatrix(ref result, y, i, x);
                i++;
            }
            return result;
        }

        public static double[,] RungeKutta (Vector coefficients, double x0, double xn, double h, Vector y, int n)
        {
            var result = new double[n, y.Length() + 1];
            var k1 = new Vector(y.Length());
            var k2 = new Vector(y.Length());
            var k3 = new Vector(y.Length());
            var k4 = new Vector(y.Length());
            int i = 0;
            for (double x = x0; Math.Round(x, 5) <= xn; x += h)
            {
                k1 = Function(coefficients, y);
                k2 = Function(coefficients + h * 0.5, y + k1 * h * 0.5);
                k3 = Function(coefficients + h * 0.5, y + k2 * h * 0.5);
                k4 = Function(coefficients + h, y + k3 * h);
                y = y + (k1 + k2 * 2 + k3 * 2 + k4) * h / 6.0;
                AddToMatrix(ref result, y, i, x);
                i++;
            }
            return result;
        }

        public static double[,] Adams (Vector coefficients, double x0, double xn, double h, Vector y, int n)
        {
            var result = new double[n, y.Length() + 1];
            var k1 = new Vector(y.Length());
            var k2 = new Vector(y.Length());
            var k3 = new Vector(y.Length());
            var k4 = new Vector(y.Length());
            int i = 0;
            for (double x = x0; Math.Round(x, 5) <= xn; x += h)
            {
                k4 = k3;
                k3 = k2;
                k2 = k1;
                k1 = Function(coefficients, y);
                if (i >= 3)
                {
                    y = y + (55.0 * k1 - 59.0 * k2 + 37.0 * k3 - 9.0 * k4) / 24.0 * h;
                }
                else
                {
                    y += h * Function(coefficients, y);
                }
                AddToMatrix(ref result, y, i, x);
                i++;
            }
            return result;
        }
    }
}
