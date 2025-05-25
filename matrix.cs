using System;
using System.Linq;

public class ComplexMatrix
{
    private readonly ComplexNumber[,] _elements;

    public int Rows => _elements.GetLength(0);
    public int Columns => _elements.GetLength(1);

    public ComplexMatrix(int rows, int columns)
    {
        if (rows <= 0 || columns <= 0) throw new ArgumentOutOfRangeException("Rows and columns must be positive.");
        _elements = new ComplexNumber[rows, columns];
        InitializeWithZeros();
    }

    public ComplexMatrix(ComplexNumber[,] elements)
    {
        if (elements == null || elements.GetLength(0) == 0 || elements.GetLength(1) == 0)
            throw new ArgumentNullException(nameof(elements), "Matrix elements cannot be null or empty.");
        _elements = (ComplexNumber[,])elements.Clone();
    }

    public ComplexNumber this[int row, int col]
    {
        get => _elements[row, col];
        set => _elements[row, col] = value;
    }

    private void InitializeWithZeros()
    {
        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Columns; j++)
            {
                _elements[i, j] = ComplexNumber.Zero;
            }
        }
    }

    
    public static ComplexMatrix operator +(ComplexMatrix a, ComplexMatrix b)
    {
        if (a.Rows != b.Rows || a.Columns != b.Columns) throw new ArgumentException("Matrices must have the same dimensions for addition.");
        ComplexMatrix result = new ComplexMatrix(a.Rows, a.Columns);
        for (int i = 0; i < a.Rows; i++)
        {
            for (int j = 0; j < a.Columns; j++)
            {
                result[i, j] = a[i, j] + b[i, j];
            }
        }
        return result;
    }

    
    public static ComplexMatrix operator -(ComplexMatrix a, ComplexMatrix b)
    {
        if (a.Rows != b.Rows || a.Columns != b.Columns) throw new ArgumentException("Matrices must have the same dimensions for subtraction.");
        ComplexMatrix result = new ComplexMatrix(a.Rows, a.Columns);
        for (int i = 0; i < a.Rows; i++)
        {
            for (int j = 0; j < a.Columns; j++)
            {
                result[i, j] = a[i, j] - b[i, j];
            }
        }
        return result;
    }

    
    public static ComplexMatrix operator *(ComplexMatrix a, ComplexMatrix b)
    {
        if (a.Columns != b.Rows) throw new ArgumentException("Number of columns in first matrix must equal number of rows in second matrix for multiplication.");
        ComplexMatrix result = new ComplexMatrix(a.Rows, b.Columns);
        for (int i = 0; i < a.Rows; i++)
        {
            for (int j = 0; j < b.Columns; j++)
            {
                ComplexNumber sum = ComplexNumber.Zero;
                for (int k = 0; k < a.Columns; k++)
                {
                    sum += a[i, k] * b[k, j];
                }
                result[i, j] = sum;
            }
        }
        return result;
    }

    
    public static ComplexVector operator *(ComplexMatrix matrix, ComplexVector vector)
    {
        if (matrix.Columns != vector.Dimension) throw new ArgumentException("Matrix columns must match vector dimension for multiplication.");
        ComplexVector result = new ComplexVector(matrix.Rows);
        for (int i = 0; i < matrix.Rows; i++)
        {
            ComplexNumber sum = ComplexNumber.Zero;
            for (int j = 0; j < matrix.Columns; j++)
            {
                sum += matrix[i, j] * vector[j];
            }
            result[i] = sum;
        }
        return result;
    }

    
    public static ComplexMatrix operator *(ComplexMatrix matrix, ComplexNumber scalar)
    {
        ComplexMatrix result = new ComplexMatrix(matrix.Rows, matrix.Columns);
        for (int i = 0; i < matrix.Rows; i++)
        {
            for (int j = 0; j < matrix.Columns; j++)
            {
                result[i, j] = matrix[i, j] * scalar;
            }
        }
        return result;
    }

    public static ComplexMatrix operator *(ComplexNumber scalar, ComplexMatrix matrix) => matrix * scalar;


    
    public ComplexMatrix ConjugateTranspose()
    {
        ComplexMatrix result = new ComplexMatrix(Columns, Rows);
        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Columns; j++)
            {
                result[j, i] = _elements[i, j].Conjugate;
            }
        }
        return result;
    }

    
    public static ComplexMatrix Identity(int dimension)
    {
        ComplexMatrix identity = new ComplexMatrix(dimension, dimension);
        for (int i = 0; i < dimension; i++)
        {
            identity[i, i] = ComplexNumber.One;
        }
        return identity;
    }

    
    public ComplexMatrix Clone()
    {
        return new ComplexMatrix((ComplexNumber[,])_elements.Clone());
    }

    public override string ToString()
    {
        string matrixString = "";
        for (int i = 0; i < Rows; i++)
        {
            matrixString += "[";
            for (int j = 0; j < Columns; j++)
            {
                matrixString += _elements[i, j].ToString();
                if (j < Columns - 1) matrixString += ", ";
            }
            matrixString += "]\n";
        }
        return matrixString.Trim();
    }
}
