namespace BDSA2019.Lecture03
{
    public class Calculator
    {
        public static int Add(int x, int y) => checked(x + y);

        public static int Negate(int x)
        {
            return -x;
        }
    }
}
