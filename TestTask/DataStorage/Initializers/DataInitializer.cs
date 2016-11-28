using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataStorage.Models;
using DataStorage.Constants;
using DataStorage.Exceptions;

namespace DataStorage.Initializers
{
    public static class DataInitializer<T>
    {
        public static Containers<T> CreateContainerCollection(int matricesNumber, int[] matrixLengthes, int[][] matrixPositionSizes, Dimensions[] pointTypes)
        {
            return new Containers<T>(matricesNumber, matrixLengthes, matrixPositionSizes, pointTypes);
        }

        public static Position<T> CreatePosition(T[,] coordinates)
        {
            if (coordinates.GetLength(1) >= 1
                && coordinates.GetLength(1) <= 3)
            {
                int length = coordinates.GetLength(0);
                Dimensions pointsType = (Dimensions)coordinates.GetLength(1);
                Position<T> result = new Position<T>(length, pointsType);
                for (int i = 0; i < length; i++)
                {
                    T[] coord = new T[(int)pointsType];
                    for (int j = 0; j < (int)pointsType; j++)
                    {
                        coord[j] = coordinates[i, j];
                    }
                    result.SetPoint(i, new Point<T>(pointsType, coord));
                }
                return result;
            }
            else
            {
                throw new WrongPointTypeException();
            }
        }

        public static Matrix<T> CreateMatrix(Position<T>[] positions)
        {
            int[] positionSizes = new int[positions.Length];
            for (int i = 0; i < positions.Length; i++)
            {
                positionSizes[i] = positions[i].Length;
            }
            Dimensions pointsType = positions[0].PointType;
            Matrix<T> result = new Matrix<T>(positions.Length, positionSizes, pointsType);
            for (int i = 0; i < positions.Length; i++)
            {
                result.SetPosition(i, positions[i]);
            }
            return result;
        }

        public static Container<T> CreateContainer(Matrix<T>[] matrices)
        {
            int[] matricesPositionNumber = new int[matrices.Length];
            int[][] matricesPositionSizes = new int[matrices.Length][];
            Dimensions[] pointTypes = new Dimensions[matrices.Length];
            for (int i = 0; i < matrices.Length; i++)
            {
                matricesPositionNumber[i] = matrices[i].Length;
                matricesPositionSizes[i] = matrices[i].PositionLength;
                pointTypes[i] = matrices[i].PointType;
            }
            Container<T> result = new Container<T>(matrices.Length, matricesPositionNumber, matricesPositionSizes, pointTypes);
            for (int i = 0; i < matrices.Length; i++)
            {
                result.SetMatrix(i, matrices[i]);
            }
            return result;
        }
    }
}
