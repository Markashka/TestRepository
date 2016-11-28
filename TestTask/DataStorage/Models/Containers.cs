using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataStorage.Constants;
using DataStorage.Exceptions;

namespace DataStorage.Models
{
    /// <summary>
    /// Container class
    /// </summary>
    /// <typeparam name="T">Type parameter</typeparam>
    public class Containers<T> : IEnumerable<Container<T>>
    {
        /// <summary>
        /// Gets number of containers
        /// </summary>
        public int Length
        {
            get
            {
                if (this.Values != null)
                {
                    return this.Values.Count;
                }
                else
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// Gets all containers
        /// </summary>
        public List<Container<T>> Values
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets point types of each matrix in all containers
        /// </summary>
        public Dimensions[] PointTypes
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets sizes of positions in each matrix of all containers
        /// </summary>
        public int[][] MatricesPositionSizes
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the number of matrices in all containers
        /// </summary>
        public int MatricesNumber
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets number of position in each matrix of all containers
        /// </summary>
        public int[] MartixLengthes
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets indexer for containers 
        /// </summary>
        /// <param name="index">Container index</param>
        /// <returns>Position in specified container</returns>
        public Container<T> this[int index]
        {
            get
            {
                if (this.Length > index && index >= 0)
                {
                    return this.Values[index];
                }
                else
                {
                    throw new IndexOutOfRangeException();
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="{Containers<typeparamref name="T"}" /> class.
        /// </summary>
        internal Containers()
        {
            this.PointTypes = null;
            this.MartixLengthes = null;
            this.MatricesPositionSizes = null;
            this.Values = null;
            this.MatricesNumber = 0;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="{Container<typeparamref name="T"}" /> class.
        /// </summary>
        /// <param name="matricesNumber">Matrices of this container</param>
        /// <param name="matrixLengthes">Number of positions in each indexed matrix</param>
        /// <param name="matrixPositionSizes">Number of points in each position of each indexed matrix</param>
        /// <param name="pointTypes">Type of points in each  indexed matrix</param>
        internal Containers(int matricesNumber, int[] matrixLengthes, int[][] matrixPositionSizes, Dimensions[] pointTypes)
        {
            this.MatricesNumber = matricesNumber;
            if (pointTypes.Length == matricesNumber)
            {
                this.PointTypes = pointTypes;
            }
            else
            {
                throw new WrongContainerSizeException();
            }

            if (matrixLengthes.Length == matricesNumber)
            {
                this.MartixLengthes = matrixLengthes;
            }
            else
            {
                throw new WrongContainerSizeException();
            }

            if (matrixPositionSizes.GetLength(0) == matricesNumber)
            {
                for (int i = 0; i < matricesNumber; i++)
                {
                    if (matrixPositionSizes[i].Length != matrixLengthes[i])
                    {
                        throw new WrongMatrixSizeException();
                    }
                }

                this.MatricesPositionSizes = matrixPositionSizes;
            }
            else
            {
                throw new WrongContainerSizeException();
            }

            this.Values = new List<Container<T>>();
        }

        /// <summary>
        /// Sets container at specified index
        /// </summary>
        /// <param name="container">New container</param>
        public void AddContainer(Container<T> container)
        {
            if (container.Length == this.MatricesNumber
                   && container.PointTypes.SequenceEqual<Dimensions>(this.PointTypes)
                   && container.MatricesPositionNumber.SequenceEqual(this.MartixLengthes)
                   && this.IsMatricesPositionSizesEqual(container))
            {
                this.Values.Add(container);
            }
            else if (!container.PointTypes.SequenceEqual<Dimensions>(this.PointTypes))
            {
                throw new WrongPointTypeException();
            }
            else if (!container.MatricesPositionNumber.SequenceEqual(this.MartixLengthes))
            {
                throw new WrongMatrixSizeException();
            }
            else if (!this.IsMatricesPositionSizesEqual(container))
            {
                throw new WrongPositionSizeException();
            }
            else
            {
                throw new Exception();
            }
        }

        /// <summary>
        /// Defines whether matrices position sizes are equal 
        /// </summary>
        /// <param name="container">Container to check</param>
        /// <returns>Boolean value</returns>
        private bool IsMatricesPositionSizesEqual(Container<T> container)
        {
            if (container.MatricesPositionSizes.GetLength(0) != this.MatricesNumber)
            {
                return false;
            }

            for (int i = 0; i < container.MatricesPositionSizes.GetLength(0); i++)
            {
                if (!container.MatricesPositionSizes[i].SequenceEqual(this.MatricesPositionSizes[i]))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Provides IEnumerator of containers
        /// </summary>
        /// <returns>IEnumerator of containers</returns>
        public IEnumerator<Container<T>> GetEnumerator()
        {
            foreach (var container in this.Values)
            {
                if (container == null)
                {
                    break;
                }

                yield return container;
            }
        }

        /// <summary>
        /// Provides IEnumerator for containers
        /// </summary>
        /// <returns>Enumerator for container in containers</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
