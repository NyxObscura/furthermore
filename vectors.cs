using System;
using System.Linq;

public class ComplexVector
{
    private readonly ComplexNumber[] _elements;

    public int Dimension => _elements.Length;

    public ComplexVector(int dimension)
    {
        if (dimension <= 0) throw new ArgumentOutOfRangeException(nameof(dimension), "Dimension must be positive.");
        _elements = new ComplexNumber[dimension];
    }

    public ComplexVector(params ComplexNumber[] elements)
    {
        if (elements == null || elements.Length == 0) throw new ArgumentNullException(nameof(elements), "Elements cannot be null or empty.");
        _elements = elements;
    }

    public ComplexNumber this[int index]
    {
        get => _elements[index];
        set => _elements[index] = value;
    }

    
    public void Normalize()
    {
        double magnitudeSquared = _elements.Sum(e => e.Magnitude * e.Magnitude);
        if (magnitudeSquared == 0) return; 
        double magnitude = Math.Sqrt(magnitudeSquared);
        for (int i = 0; i < Dimension; i++)
        {
            _elements[i] /= magnitude;
        }
    }

    public ComplexVector ConjugateTranspose()
    {
        ComplexNumber[] transposedElements = new ComplexNumber[Dimension];
        for (int i = 0; i < Dimension; i++)
        {
            transposedElements[i] = _elements[i].Conjugate;
        }
        return new ComplexVector(transposedElements);
    }

    
    public static ComplexVector operator +(ComplexVector a, ComplexVector b)
    {
        if (a.Dimension != b.Dimension) throw new ArgumentException("Vectors must have the same dimension for addition.");
        ComplexVector result = new ComplexVector(a.Dimension);
        for (int i = 0; i < a.Dimension; i++)
        {
            result[i] = a[i] + b[i];
        }
        return result;
    }

    
    public static ComplexVector operator -(ComplexVector a, ComplexVector b)
    {
        if (a.Dimension != b.Dimension) throw new ArgumentException("Vectors must have the same dimension for subtraction.");
        ComplexVector result = new ComplexVector(a.Dimension);
        for (int i = 0; i < a.Dimension; i++)
        {
            result[i] = a[i] - b[i];
        }
        return result;
    }

    
    public static ComplexVector operator *(ComplexVector v, ComplexNumber scalar)
    {
        ComplexVector result = new ComplexVector(v.Dimension);
        for (int i = 0; i < v.Dimension; i++)
        {
            result[i] = v[i] * scalar;
        }
        return result;
    }

    public static ComplexVector operator *(ComplexNumber scalar, ComplexVector v) => v * scalar;


    
    public ComplexNumber Dot(ComplexVector other)
    {
        if (Dimension != other.Dimension) throw new ArgumentException("Vectors must have the same dimension for dot product.");
        ComplexNumber sum = ComplexNumber.Zero;
        for (int i = 0; i < Dimension; i++)
        {
            sum += this[i].Conjugate * other[i];
        }
        return sum;
    }

    
    public ComplexMatrix OuterProduct(ComplexVector other)
    {
        ComplexMatrix result = new ComplexMatrix(Dimension, other.Dimension);
        for (int i = 0; i < Dimension; i++)
        {
            for (int j = 0; j < other.Dimension; j++)
            {
                result[i, j] = this[i] * other[j].Conjugate;
            }
        }
        return result;
    }


    public override string ToString() => $"[{string.Join(", ", _elements.Select(e => e.ToString()))}]";
    public override bool Equals(object obj)
    {
        if (!(obj is ComplexVector other) || Dimension != other.Dimension) return false;
        for (int i = 0; i < Dimension; i++)
        {
            if (!this[i].Equals(other[i])) return false;
        }
        return true;
    }
    public override int GetHashCode() => _elements.Aggregate(0, (hash, element) => HashCode.Combine(hash, element.GetHashCode()));
}
