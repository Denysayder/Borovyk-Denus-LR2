// See https://aka.ms/new-console-template for more information
using Newtonsoft.Json;

float real_1, imaginary_1, real_2, imaginary_2;
real_1 = imaginary_1 = real_2 = imaginary_2 = 0;
int n = 0;

Console.WriteLine("Введите действительную часть первого комплексного числа");
real_1 = float.Parse(Console.ReadLine());
Console.WriteLine("Введите мнимую часть первого комплексного числа");
imaginary_1 = float.Parse(Console.ReadLine());
Console.WriteLine("Введите действительную часть второго комплексного числа");
real_2 = float.Parse(Console.ReadLine());
Console.WriteLine("Введите мнимую часть второго комплексного числа");
imaginary_2 = float.Parse(Console.ReadLine());
Console.WriteLine("Первое ведённое комплексное число: z={0}{1}{2}i", real_1, CheckSign(imaginary_1), imaginary_1);
Console.WriteLine("Второе введённое комплексное число: z={0}{1}{2}i", real_2, CheckSign(imaginary_2), imaginary_2);
Complex c1 = new Complex(real_1, imaginary_1);
Complex c2 = new Complex(real_2, imaginary_2);

ComplexSerializer(c1,c2);
Console.WriteLine("c1 + c2 = {0}", c1 + c2);
Console.WriteLine("c1 - c2 = {0}", c1 - c2);
Console.WriteLine("c1 * c2 = {0}", c1 * c2);
Console.WriteLine("c1 / c2 = {0}", c1 / c2);
Console.WriteLine("Введите натуральную степень чтобы возвести первое комплексное число");
n = Convert.ToInt32(Console.ReadLine());
Console.WriteLine("c1 ^ n = {0}", c1 ^ n);
Console.WriteLine("√c1 = {0}", ++c1);
Console.WriteLine("Тригонометрическая форма");
Complex.Trigonometric(c1);
Console.WriteLine("Экспоненциальная форма");
Complex.Exponential(c1);
Console.WriteLine();
Console.WriteLine();

Console.WriteLine("Выполнение программы с объектами из JSON файла");
string path = @"/Users/denisborovik/Projects/laba2_2sem/laba2_2sem/bin/Debug/net6.0/ComplexNumbers — копия.json";
Deserialize(path, out Complex cn1, out Complex cn2);
Console.WriteLine("c1 + c2 = {0}", cn1 + cn2);
Console.WriteLine("c1 - c2 = {0}", cn1 - cn2);
Console.WriteLine("c1 * c2 = {0}", cn1 * cn2);
Console.WriteLine("c1 / c2 = {0}", cn1 / cn2);
Console.WriteLine("Введите натуральную степень чтобы возвести первое комплексное число");
n = Convert.ToInt32(Console.ReadLine());
Console.WriteLine("c1 ^ n = {0}", cn1 ^ n);
Console.WriteLine("√c1 = {0}", ++cn1);
Console.WriteLine("Тригонометрическая форма");
Complex.Trigonometric(cn1);
Console.WriteLine("Экспоненциальная форма");
Complex.Exponential(cn1);




void ComplexSerializer(Complex c1,Complex c2)
{
    string path = @"/Users/denisborovik/Projects/laba2_2sem/laba2_2sem/bin/Debug/net6.0/ComplexNumbers.json";
    List<string> complexSerialized = new List<string>();
    complexSerialized.Add(JsonConvert.SerializeObject(c1));
    complexSerialized.Add(JsonConvert.SerializeObject(c2));
    File.WriteAllLines(path, complexSerialized);
}

void Deserialize(string path, out Complex c1, out Complex c2)
{
    List<Complex> complexes = new List<Complex>
    { };
    complexes = JsonConvert.DeserializeObject<List<Complex>>(File.ReadAllText(path));
    c1 = complexes[0];
    c2 = complexes[1];
}

string CheckSign(float num)
{
    string s = "";
    if (num > 0)
    {
        s = "+";
    }
    return s;
}


[Serializable]
public class Complex
{
    public float real { get; set; }
    //public float Real
    //{
    //    get
    //    {
    //        return real;
    //    }
    //    set
    //    {
    //        real = value;
    //    }
    //}
    public float imaginary { get; set; }
    //public float Imaginary
    //{
    //    get
    //    {
    //        return imaginary;
    //    }
    //    set
    //    {
    //        imaginary = value;
    //    }
    //}

    public Complex(float real, float imaginary)
    {
        this.real = real;
        this.imaginary = imaginary;
    }

    public override string ToString()
    {
        return (String.Format("({0}, {1}i)", real, imaginary));
    }

    public static Complex operator +(Complex c1, Complex c2)
    {
        return (new Complex(c1.real + c2.real, c1.imaginary + c2.imaginary));
    }

    public static Complex operator -(Complex c1, Complex c2)
    {
        return (new Complex(c1.real - c2.real, c1.imaginary - c2.imaginary));
    }

    public static Complex operator *(Complex c1, Complex c2)
    {
        return (new Complex(c1.real * c2.real - c1.imaginary * c2.imaginary,
        c1.real * c2.imaginary + c2.real * c1.imaginary));
    }

    public static Complex operator /(Complex c1, Complex c2)
    {
        if ((c2.real == 0.0f) &&
        (c2.imaginary == 0.0f))
            throw new DivideByZeroException("Can't divide by zero Complex number");

        float newReal =
        (c1.real * c2.real + c1.imaginary * c2.imaginary) /
        (c2.real * c2.real + c2.imaginary * c2.imaginary);
        float newImaginary =
        (c2.real * c1.imaginary - c1.real * c2.imaginary) /
        (c2.real * c2.real + c2.imaginary * c2.imaginary);

        return (new Complex(newReal, newImaginary));
    }

    public static Complex operator ^(Complex c1, int n)
    {
        float newReal =
        Convert.ToSingle(Math.Pow((Math.Sqrt(Math.Pow((c1.real), 2) +
        Math.Pow((c1.imaginary), 2))), n) * (Math.Cos(n * Math.Atan(c1.imaginary / c1.real))));
        float newImaginary =
        Convert.ToSingle(Math.Pow((Math.Sqrt(Math.Pow((c1.real), 2) +
        Math.Pow((c1.imaginary), 2))), n) * (Math.Sin(n * Math.Atan(c1.imaginary / c1.real))));

        return (new Complex(newReal, newImaginary));
    }

    public static Complex operator ++(Complex c1)
    {
        float newReal =
        Convert.ToSingle(Math.Sqrt((Math.Sqrt((Math.Pow(c1.real, 2) +
        Math.Pow(c1.imaginary, 2))) + c1.real) / 2));
        float newImaginary =
        Convert.ToSingle(c1.imaginary / Math.Abs(c1.imaginary) * (Math.Sqrt((Math.Sqrt(Math.Pow(c1.real, 2) +
        Math.Pow(c1.imaginary, 2)) - c1.real) / 2)));

        return (new Complex(newReal, newImaginary));
    }

    public static void Trigonometric(Complex c1)
    {
        float module =
        Convert.ToSingle((Math.Sqrt(Math.Pow((c1.real), 2) +
        Math.Pow((c1.imaginary), 2))));
        float argument =
        Convert.ToSingle(Math.Atan(c1.imaginary / c1.real));
        Console.WriteLine("z = {0} * (cos({1}) + i * sin({1})",module,argument);
    }

    public static void Exponential(Complex c1)
    {
        float module =
        Convert.ToSingle((Math.Sqrt(Math.Pow((c1.real), 2) +
        Math.Pow((c1.imaginary), 2))));
        float argument =
        Convert.ToSingle(Math.Atan(c1.imaginary / c1.real));
        Console.WriteLine("z = {0} * e ^ (i * {1})", module, argument);
    }

}