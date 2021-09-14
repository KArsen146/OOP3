namespace c_sharp2
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Triangle A = new Triangle(2, 2, 3);
            A.First = 1;
            A.Square();
        }
    }
}