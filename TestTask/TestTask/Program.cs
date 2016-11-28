using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataStorage.Initializers;
using DataStorage.Models;
using DataStorage.Constants;

namespace TestTask
{
    class Program
    {
        static void Main(string[] args)
        {
            decimal[,] coordinates1 = new decimal[50, 2];
            for (int i = 0; i < 50; i++)
            {
                coordinates1[i, 0] = i;
                coordinates1[i, 1] = i + 1;
            }
            decimal[,] coordinates2 = new decimal[200, 2];
            for (int i = 0; i < 200; i++)
            {
                coordinates2[i, 0] = i;
                coordinates2[i, 1] = i + 1;
            }
            decimal[,] otherCoordinates = new decimal[0, 2];
            Position<decimal>[] firstMatrixPositions = new Position<decimal>[100];
            firstMatrixPositions[0] = DataInitializer<decimal>.CreatePosition(coordinates1);
            firstMatrixPositions[1] = DataInitializer<decimal>.CreatePosition(coordinates2);
            for (int i = 2; i < 100; i++)
            {
                firstMatrixPositions[i] = DataInitializer<decimal>.CreatePosition(otherCoordinates);
            }


            Matrix<decimal>[] matrices = new Matrix<decimal>[2];
            matrices[0] = DataInitializer<decimal>.CreateMatrix(firstMatrixPositions);

            decimal[,] coordinates12 = new decimal[1, 1];
            coordinates12[0, 0] = 1;
            decimal[,] coordinates22 = new decimal[1, 1];
            coordinates22[0, 0] = 1;
            otherCoordinates = new decimal[0, 1];
            Position<decimal>[] secondMatrixPositions = new Position<decimal>[100];
            secondMatrixPositions[0] = DataInitializer<decimal>.CreatePosition(coordinates12);
            secondMatrixPositions[1] = DataInitializer<decimal>.CreatePosition(coordinates22);
            for (int i = 2; i < 100; i++)
            {
                secondMatrixPositions[i] = DataInitializer<decimal>.CreatePosition(otherCoordinates);
            }
            matrices[1] = DataInitializer<decimal>.CreateMatrix(secondMatrixPositions);

            Container<decimal> myContainer = DataInitializer<decimal>.CreateContainer(matrices);

            ////SetUp container collection
            int[] firstMatrix = new int[100];
            firstMatrix[0] = 50;
            firstMatrix[1] = 200;
            int[] secondMatrix = new int[100];
            secondMatrix[0] = 1;
            secondMatrix[1] = 1;
            Containers <decimal> dataSet = DataInitializer<decimal>
                .CreateContainerCollection(2, new[] { 100, 100 }, new int[][] { firstMatrix, secondMatrix }, new[] { Dimensions.TwoDimensional, Dimensions.OneDimensional});
            dataSet.AddContainer(myContainer);
            dataSet.AddContainer(myContainer);
            dataSet.AddContainer(myContainer);

            foreach (var container in dataSet)
            {
                Console.WriteLine("{0} matrices", container.Length);
                foreach (var matrix in container)
                {
                    Console.WriteLine("{0} positions", matrix.Length);
                    foreach (var position in matrix)
                    {
                        Console.WriteLine("{0} points of {1} dimention", position.Length, (int)position.PointType);
                        foreach (var point in position)
                        {
                            Console.Write("Point : (");
                            for (int i = 0; i < (int)point.PointType; i++)
                            {
                                Console.Write("{0} ", point[i]);
                            }
                            Console.Write(")");
                            Console.WriteLine();
                        }
                    }
                }
            }
        }
    }
}
