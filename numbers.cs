using System;

public struct ComplexNumber
{
    public double Real { get; }
    public double Imaginary { get; }

    public ComplexNumber(double real, double imaginary)
    {
        Real = real;
        Imaginary = imaginary;
    }

    public double Magnitude => Math.Sqrt(Real * Real + Imaginary * Imaginary);
    public double Phase => Math.Atan2(Imaginary, Real);

    public static ComplexNumber operator +(ComplexNumber a, ComplexNumber b) => new ComplexNumber(a.Real + b.Real, a.Imaginary + b.Imaginary);
    public static ComplexNumber operator -(ComplexNumber a, ComplexNumber b) => new ComplexNumber(a.Real - b.Real, a.Imaginary - b.Imaginary);
    public static ComplexNumber operator *(ComplexNumber a, ComplexNumber b) => new ComplexNumber(a.Real * b.Real - a.Imaginary * b.Imaginary, a.Real * b.Imaginary + a.Imaginary * b.Real);
    public static ComplexNumber operator /(ComplexNumber a, ComplexNumber b)
    {
        if (b.Magnitude == 0) throw new DivideByZeroException("Cannot divide by zero complex number.");
        double denominator = b.Real * b.Real + b.Imaginary * b.Imaginary;
        return new ComplexNumber((a.Real * b.Real + a.Imaginary * b.Imaginary) / denominator, (a.Imaginary * b.Real - a.Real * b.Imaginary) / denominator);
    }
    public static ComplexNumber operator *(ComplexNumber a, double scalar) => new ComplexNumber(a.Real * scalar, a.Imaginary * scalar);
    public static ComplexNumber operator *(double scalar, ComplexNumber a) => new ComplexNumber(a.Real * scalar, a.Imaginary * scalar);

    public ComplexNumber Conjugate => new ComplexNumber(Real, -Imaginary);

    public override string ToString() => $"{Real}{(Imaginary >= 0 ? "+" : "")}{Imaginary}i";
    public override bool Equals(object obj) => obj is ComplexNumber other && Real == other.Real && Imaginary == other.Imaginary;
    public override int GetHashCode() => HashCode.Combine(Real, Imaginary);

    
    public static ComplexNumber Zero => new ComplexNumber(0, 0);
    public static ComplexNumber One => new ComplexNumber(1, 0);
    public static ComplexNumber I => new ComplexNumber(0, 1);
}
