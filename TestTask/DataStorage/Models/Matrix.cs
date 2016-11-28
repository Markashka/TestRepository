using System;
using System.Collections;
using System.Collections.Generic;
using DataStorage.Constants;
using DataStorage.Exceptions;

namespace DataStorage.Models
{
    /// <summary>
    /// Matrix class
    /// </summary>
    /// <typeparam name="T">Type parameter</typeparam>
    public class Matrix<T> : IEnumerable<Position<T>>
    {
        /// <summary>
        /// Gets point type of this matrix
        /// </summary>
        public Dimensions PointType
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets length of each position in this matrix
        /// </summary>
        public int[] PositionLength
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the number of data positions in this matrix
        /// </summary>
        public int Length
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the positions collection
        /// </summary>
        public Position<T>[] Positions
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets positions at certain index
        /// </summary>
        /// <param name="index">Position index</param>
        /// <returns>Position in specified position</returns>
        public Position<T> this[int index]
        {
            get
            {
                if (this.Length > index 
                    && index >= 0)
                {
                    return this.Positions[index];
                }
                else
                {
                    throw new IndexOutOfRangeException();
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="{Matrix<typeparamref name="T"}" /> class.
        /// </summary>
        public Matrix()
        {
            this.PointType = Dimensions.OneDimensional;
            this.PositionLength = null;
            this.Length = 0;
            this.Positions = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="{Matrix<typeparamref name="T"}" /> class.
        /// </summary>
        /// <param name="pointType">Point type of matrix</param>
        /// <param name="length">Number of positions in matrix</param>
        /// <param name="positionLength">Length of each position in matrix</param>
        internal Matrix(int length, int[] positionLength, Dimensions pointType)
        {
            this.PointType = pointType;
            this.Length = length;
            this.PositionLength = positionLength;
            this.Positions = new Position<T>[this.Length];
            for (int i = 0; i < this.Length; i++)
            {
                this.Positions[i] = new Position<T>(this.PositionLength[i], this.PointType);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="{Matrix<typeparamref name="T"}" /> class.
        /// </summary>
        /// <param name="matrix">The matrix to copy</param>
        internal Matrix(Matrix<T> matrix)
        {
            this.PointType = matrix.PointType;
            this.Length = matrix.Length;
            this.PositionLength = matrix.PositionLength;
            this.Positions = matrix.Positions;
        }

        /// <summary>
        /// Sets position of this matrix
        /// </summary>
        /// <param name="index">Position index</param>
        /// <param name="position">New position</param>
        internal void SetPosition(int index, Position<T> position)
        {
            if (this.Length > index
                    && index >= 0
                    && position.PointType == this.PointType
                    && position.Length == this.PositionLength[index])
            {
                this.Positions[index] = position;
            }
            else if (!(this.Length > index
                    && index >= 0))
            {
                throw new IndexOutOfRangeException();
            }
            else if (!(position.PointType != this.PointType))
            {
                throw new WrongPointTypeException();
            }
            else if (!(position.Length != this.PositionLength[index]))
            {
                throw new WrongPositionSizeException();
            }
            else
            {
                throw new Exception();
            }
        }

        /// <summary>
        /// Provides IEnumerator of positions
        /// </summary>
        /// <returns>IEnumerator of positions</returns>
        public IEnumerator<Position<T>> GetEnumerator()
        {
            foreach (var position in this.Positions)
            {
                if (position == null)
                {
                    break;
                }

                yield return position;
            }
        }

        /// <summary>
        /// Provides IEnumerator for matrix
        /// </summary>
        /// <returns>Enumerator for positions in matrix</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
