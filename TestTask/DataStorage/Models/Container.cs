using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DataStorage.Constants;
using DataStorage.Exceptions;

namespace DataStorage.Models
{
    public class Container<T> : IEnumerable<Matrix<T>>
    {
        /// <summary>
        /// Gets length of each position in each matrix of this container
        /// </summary>
        public int[][] MatricesPositionSizes
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets point types of each matrix in this container
        /// </summary>
        public Dimensions[] PointTypes
        {
            get;
            private set;
        }
        /// <summary>
        /// Gets number of position in each matrix of this container
        /// </summary>
        public int[] MatricesPositionNumber
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the number of matrices in this container
        /// </summary>
        public int Length
        {
            get;
            private set;
        }

        /// <summary>
        /// Matrices collection
        /// </summary>
        public Matrix<T>[] Matrices
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets indexer for matrices 
        /// </summary>
        /// <param name="index">Matrix index</param>
        /// <returns>Position in specified matrix</returns>
        public Matrix<T> this[int index]
        {
            get
            {
                if (Matrices.Length > index && index >= 0)
                {
                    return Matrices[index];
                }
                else
                {
                    throw new IndexOutOfRangeException();
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="{Container<typeparamref name="T"}" /> class.
        /// </summary>
        internal Container()
        {
            this.PointTypes = null;
            this.Length = 0;
            this.MatricesPositionSizes = null;
            this.MatricesPositionNumber = null;
            this.Matrices = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="{Container<typeparamref name="T"}" /> class.
        /// </summary>
        /// <param name="matrices">Matrices of this container</param>
        internal Container(int length, int[] matrixPositionsNumber, int[][] matricesPositionSizes, Dimensions[] pointTypes)
        {
            this.Length = length;
            
            if (pointTypes.Length == this.Length)
            {
                this.PointTypes = pointTypes;
            }
            else
            {
                throw new WrongPointTypeException();
            }

            if (matrixPositionsNumber.Length == this.Length)
            {
                this.MatricesPositionNumber = matrixPositionsNumber;
            }
            else
            {
                throw new WrongContainerSizeException();
            }

            if (matricesPositionSizes.GetLength(0) == this.Length)
            {
                this.MatricesPositionSizes = matricesPositionSizes;

            }
            else
            {
                throw new WrongContainerSizeException();
            }
            this.InitializeMatrices();
        }


        private void InitializeMatrices()
        {
            this.Matrices = new Matrix<T>[this.Length];
            for (int i = 0; i < this.Length; i++)
            {
                this.Matrices[i] = new Matrix<T>( this.MatricesPositionNumber[i], this.MatricesPositionSizes[i], this.PointTypes[i]);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="{Container<typeparamref name="T"}" /> class.
        /// </summary>
        /// <param name="container">Matrices of this container</param>
        internal Container(Container<T> container)
        {
            this.Length = container.Length;
            this.MatricesPositionSizes = container.MatricesPositionSizes;
            this.PointTypes = this.PointTypes;
            this.MatricesPositionNumber = container.MatricesPositionNumber;
            this.Matrices = container.Matrices;
        }

        /// <summary>
        /// Sets matrix at specified index
        /// </summary>
        /// <param name="index">The index</param>
        /// <param name="matrix">New matrix</param>
        internal void SetMatrix(int index, Matrix<T> matrix)
        {
            if (this.Length > index
                   && index >= 0
                   && matrix.Length == this.MatricesPositionNumber[index]
                   && matrix.PointType == this.PointTypes[index]
                   && matrix.PositionLength.SequenceEqual(this.MatricesPositionSizes[index]))
            {
                this.Matrices[index] = matrix;
            }
            else if (!(this.Length > index
                   && index >= 0))
            {
                throw new IndexOutOfRangeException();
            }
            else if (matrix.Length != this.MatricesPositionNumber[index])
            {
                throw new WrongMatrixSizeException();
            }
            else if (matrix.PointType != this.PointTypes[index])
            {
                throw new WrongPointTypeException();
            }
            else if (!matrix.PositionLength.SequenceEqual(this.MatricesPositionSizes[index]))
            {
                throw new WrongPositionSizeException();
            }
            else
            {
                throw new Exception();
            }
        } 

        /// <summary>
        /// Provides IEnumerator of matrices
        /// </summary>
        /// <returns>IEnumerator of matrices</returns>
        public IEnumerator<Matrix<T>> GetEnumerator()
        {
            foreach (var matrix in Matrices)
            {
                if (matrix == null)
                {
                    break;
                }
                yield return matrix;
            }
        }

        /// <summary>
        /// Provides IEnumerator for container
        /// </summary>
        /// <returns>Enumerator for matrices in container</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
