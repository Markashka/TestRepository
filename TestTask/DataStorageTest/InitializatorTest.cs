using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataStorage.Models;
using DataStorage.Initializers;
using DataStorage.Exceptions;
using DataStorage.Constants;

namespace DataStorageTest
{
    [TestClass]
    public class InitializatorTest
    {

        private Position<int> SetUpPosition(int dimention, int size)
        {
            int[,] coordinates = new int[size, dimention];
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < dimention; j++)
                {
                    coordinates[i, j] = i + j;
                }
            }

           return DataInitializer<int>.CreatePosition(coordinates);
        }

        private Matrix<int> SetUpTestMatrix(int dimention, int size)
        {
            Position<int>[] positions = new Position<int>[size];
            for (int i = 0; i < size; i++)
            {
                positions[i] = SetUpPosition(dimention, i);
            }

            return DataInitializer<int>.CreateMatrix(positions);
        }

        [TestMethod]
        public void CreateZeroPositionTest()
        {
            ////Arrange
            int size = 0;
            int dimention = 1;

            ////Act
            Position<int> testPosition = this.SetUpPosition(dimention, size);

            ////Assert
            Assert.AreEqual(size, testPosition.Length);
            Assert.AreEqual(dimention, (int)testPosition.PointType);
        }

        [TestMethod]
        public void CreateBigPositionOneDimentionalTest()
        {
            ////Arrange
            int bigSize = 10000;
            int dimention = 1;

            ////Act
            Position<int> testPosition = this.SetUpPosition(dimention, bigSize);

            ////Assert
            Assert.AreEqual(bigSize, testPosition.Length);
            Assert.AreEqual(dimention, (int)testPosition.PointType);
            for (int i = 0; i < bigSize; i++)
            {
                Assert.AreEqual(i, testPosition[i][0]);
            }
        }

        [TestMethod]
        public void CreateBigPositionTwoDimentionalTest()
        {
            ////Arrange
            int bigSize = 10000;
            int dimention = 2;

            ////Act
            Position<int> testPosition = this.SetUpPosition(dimention, bigSize);

            ////Assert
            Assert.AreEqual(bigSize, testPosition.Length);
            Assert.AreEqual(2, (int)testPosition.PointType);
            for (int i = 0; i < bigSize; i++)
            {
                Assert.AreEqual(i, testPosition[i][0]);
                Assert.AreEqual(i + 1, testPosition[i][1]);
            }
        }

        [TestMethod]
        public void CreateBigPositionThreeDimentionalTest()
        {
            ////Arrange
            int bigSize = 10000;
            int dimention = 3;

            ////Act
            Position<int> testPosition = this.SetUpPosition(dimention, bigSize);

            ////Assert
            Assert.AreEqual(bigSize, testPosition.Length);
            Assert.AreEqual(3, (int)testPosition.PointType);
            for (int i = 0; i < bigSize; i++)
            {
                Assert.AreEqual(i, testPosition[i][0]);
                Assert.AreEqual(i + 1, testPosition[i][1]);
                Assert.AreEqual(i + 2, testPosition[i][2]);
            }
        }

        [TestMethod]
        public void CreateBigPositionWrongDimentionalTest()
        {
            ////Arrange
            int bigSize = 10000;
            int dimention = 1; 

            try
            {
                ////Act
                Position<int> testPosition = this.SetUpPosition(dimention, bigSize);
            }
            catch (Exception ex)
            {
                ////Assert
                Assert.IsInstanceOfType(ex, typeof(WrongPointTypeException));
            }
        }

        [TestMethod]
        public void CreateMatrixOfOneDimentionalPointsTest()
        {
            ////Arrange
            int bigSize = 1000;
            int dimention = 1;
            Position<int>[] positions = new Position<int>[bigSize];
            for (int i = 0; i < bigSize; i++)
            {
                positions[i] = SetUpPosition(dimention, i);
            }

            ////Act
            Matrix<int> testMatrix = DataInitializer<int>.CreateMatrix(positions);
 
            ////Assert
            Assert.AreEqual(bigSize, testMatrix.Length);
            Assert.AreEqual(1, (int)testMatrix.PointType);
            for (int i = 0; i < bigSize; i++)
            {
                Assert.AreEqual(i, testMatrix.PositionLength[i]);
                Assert.AreSame(positions[i], testMatrix[i]);
            }
        }

        [TestMethod]
        public void CreateMatrixOfTwoDimentionalPointsTest()
        {
            ////Arrange
            int bigSize = 1000;
            int dimention = 2;
            Position<int>[] positions = new Position<int>[bigSize];
            for (int i = 0; i < bigSize; i++)
            {
                positions[i] = SetUpPosition(dimention, i);
            }

            ////Act
            Matrix<int> testMatrix = DataInitializer<int>.CreateMatrix(positions);

            ////Assert
            Assert.AreEqual(bigSize, testMatrix.Length);
            Assert.AreEqual(2, (int)testMatrix.PointType);
            for (int i = 0; i < bigSize; i++)
            {
                Assert.AreEqual(i, testMatrix.PositionLength[i]);
                Assert.AreSame(positions[i], testMatrix[i]);
            }
        }

        [TestMethod]
        public void CreateMatrixOfThreeDimentionalPointsTest()
        {
            ////Arrange
            int bigSize = 1000;
            int dimention = 3;
            Position<int>[] positions = new Position<int>[bigSize];
            for (int i = 0; i < bigSize; i++)
            {
                positions[i] = SetUpPosition(dimention, i);
            }

            ////Act
            Matrix<int> testMatrix = DataInitializer<int>.CreateMatrix(positions);

            ////Assert
            Assert.AreEqual(bigSize, testMatrix.Length);
            Assert.AreEqual(3, (int)testMatrix.PointType);
            for (int i = 0; i < bigSize; i++)
            {
                Assert.AreEqual(i, testMatrix.PositionLength[i]);
                Assert.AreSame(positions[i], testMatrix[i]);
            }
        }

        [TestMethod]
        public void CreateContainerTest()
        {
            ////Arrange
            int[] dimentions = new int[] { 1, 2, 3 };
            int[] sizes = new int[] { 100, 200, 300 };
            Matrix<int>[] matrices = new Matrix<int>[3];
            for (int i = 0; i < 3; i++)
            {
                matrices[i] = SetUpTestMatrix(dimentions[i], sizes[i]);
            }

            ////Act
            Container<int> testContainer = DataInitializer<int>.CreateContainer(matrices);

            ////Assert
            Assert.AreEqual(3, testContainer.Length);
            Assert.IsTrue(sizes.SequenceEqual(testContainer.MatricesPositionNumber));
            Assert.IsTrue(matrices.SequenceEqual(testContainer.Matrices));
            for (int i = 0; i < 3; i++)
            {
                Assert.AreEqual(dimentions[i], (int)testContainer.PointTypes[i]);
            }
        }

        [TestMethod]
        public void CreateContainerCollectionSuccessTest()
        {
            ////Arrange
            int bigSize = 1000;
            Dimensions[] pointTypes = new Dimensions[bigSize];
            int[] matricesSizes = new int[bigSize];
            int[][] matrixPositionSizes = new int[bigSize][];
            for (int i = 0; i < bigSize; i++)
            {
                matricesSizes[i] = i;
                matrixPositionSizes[i] = new int[i];
                pointTypes[i] = (Dimensions)(i % 3 + 1);
                for (int j = 0; j < i; j++)
                {
                    matrixPositionSizes[i][j] = j;
                }
            }

            ////Act
            Containers<int> storage = DataInitializer<int>.CreateContainerCollection(bigSize, matricesSizes, matrixPositionSizes, pointTypes);

            ////Assert
            Assert.AreEqual(0, storage.Length);
            Assert.IsTrue(storage.MartixLengthes.SequenceEqual(matricesSizes));
            Assert.IsTrue(storage.PointTypes.SequenceEqual(pointTypes));
            Assert.IsTrue(storage.MatricesPositionSizes.SequenceEqual(matrixPositionSizes));
        }

        [TestMethod]
        public void CreateContainerCollectionWrongSizes1Test()
        {
            ////Arrange
            int bigSize = 1000;
            Dimensions[] pointTypes = new Dimensions[bigSize];
            int[] matricesSizes = new int[bigSize + 1];
            int[][] matrixPositionSizes = new int[bigSize][];
            for (int i = 0; i < bigSize; i++)
            {
                matricesSizes[i] = i;
                matrixPositionSizes[i] = new int[i];
                pointTypes[i] = (Dimensions)(i % 3 + 1);
                for (int j = 0; j < i; j++)
                {
                    matrixPositionSizes[i][j] = j;
                }
            }


            try
            {
                ////Act
                Containers<int> storage = DataInitializer<int>.CreateContainerCollection(bigSize, matricesSizes, matrixPositionSizes, pointTypes);
            }
            catch (Exception ex)
            {
                ////Assert
                Assert.IsInstanceOfType(ex, typeof(WrongContainerSizeException));
            }
        }

        [TestMethod]
        public void CreateContainerCollectionWrongSizes2Test()
        {
            ////Arrange
            int bigSize = 1000;
            Dimensions[] pointTypes = new Dimensions[bigSize + 1];
            int[] matricesSizes = new int[bigSize];
            int[][] matrixPositionSizes = new int[bigSize][];
            for (int i = 0; i < bigSize; i++)
            {
                matricesSizes[i] = i;
                matrixPositionSizes[i] = new int[i];
                pointTypes[i] = (Dimensions)(i % 3 + 1);
                for (int j = 0; j < i; j++)
                {
                    matrixPositionSizes[i][j] = j;
                }
            }


            try
            {
                ////Act
                Containers<int> storage = DataInitializer<int>.CreateContainerCollection(bigSize, matricesSizes, matrixPositionSizes, pointTypes);
            }
            catch (Exception ex)
            {
                ////Assert
                Assert.IsInstanceOfType(ex, typeof(WrongContainerSizeException));
            }
        }

        [TestMethod]
        public void CreateContainerCollectionWrongSizes3Test()
        {
            ////Arrange
            int bigSize = 1000;
            Dimensions[] pointTypes = new Dimensions[bigSize];
            int[] matricesSizes = new int[bigSize];
            int[][] matrixPositionSizes = new int[bigSize + 1][];
            for (int i = 0; i < bigSize; i++)
            {
                matricesSizes[i] = i;
                matrixPositionSizes[i] = new int[i];
                pointTypes[i] = (Dimensions)(i % 3 + 1);
                for (int j = 0; j < i; j++)
                {
                    matrixPositionSizes[i][j] = j;
                }
            }


            try
            {
                ////Act
                Containers<int> storage = DataInitializer<int>.CreateContainerCollection(bigSize, matricesSizes, matrixPositionSizes, pointTypes);
            }
            catch (Exception ex)
            {
                ////Assert
                Assert.IsInstanceOfType(ex, typeof(WrongContainerSizeException));
            }
        }

        [TestMethod]
        public void CreateContainerCollectionWrongSizes4Test()
        {
            ////Arrange
            int bigSize = 1000;
            Dimensions[] pointTypes = new Dimensions[bigSize];
            int[] matricesSizes = new int[bigSize];
            int[][] matrixPositionSizes = new int[bigSize][];
            for (int i = 0; i < bigSize; i++)
            {
                matricesSizes[i] = i;
                matrixPositionSizes[i] = new int[i + 1];
                pointTypes[i] = (Dimensions)(i % 3 + 1);
                for (int j = 0; j < i; j++)
                {
                    matrixPositionSizes[i][j] = j;
                }
            }


            try
            {
                ////Act
                Containers<int> storage = DataInitializer<int>.CreateContainerCollection(bigSize, matricesSizes, matrixPositionSizes, pointTypes);
            }
            catch (Exception ex)
            {
                ////Assert
                Assert.IsInstanceOfType(ex, typeof(WrongMatrixSizeException));
            }
        }
    }
}
