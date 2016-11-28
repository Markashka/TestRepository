using System;
using DataStorage.Constants;

namespace DataStorage.Models
{
    /// <summary>
    /// Generic class point
    /// </summary>
    /// <typeparam name="T">Point coordinate type</typeparam>
    public class Point<T>
    {

        /// <summary>
        /// Point type of this point
        /// </summary>
        public Dimensions PointType
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets point coordinates
        /// </summary>
        public T[] Coordinates
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets coordinate by index 
        /// </summary>
        /// <param name="index">Coordinate index</param>
        /// <returns>Coordinate at specified index</returns>
        public T this[int index]
        {
            get
            {
                if ((int)this.PointType > index
                    && index >= 0)
                {
                    return this.Coordinates[index];
                }
                else
                {
                    throw new IndexOutOfRangeException();
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="{Point<typeparamref name="T"/>}" /> class.
        /// </summary>
        internal Point()
        {
            this.Coordinates = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="{Point<typeparamref name="T"}" /> class.
        /// </summary>
        /// <param name="dimension">Point's dimension</param>
        /// <param name="coordinates">Point coordinates</param>
        internal Point(Dimensions dimension, params T[] coordinates)
        {
            this.PointType = dimension;
            this.Coordinates = new T[(int)this.PointType];
            for (int i = 0; i < (int)this.PointType; i++)
            {
                this.Coordinates[i] = coordinates[i];
            }
        }
    }
}
