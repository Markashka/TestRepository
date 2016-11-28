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
    /// Indexed collection of points
    /// </summary>
    /// <typeparam name="T">Type parameter</typeparam>
    public class Position<T> : IEnumerable<Point<T>>
    {
        /// <summary>
        /// Gets the point type of this position
        /// </summary>
        public Dimensions PointType
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the number of data points in this position
        /// </summary>
        public int Length
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the point list
        /// </summary>
        public Point<T>[] Points
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or point by index 
        /// </summary>
        /// <param name="index">Point index</param>
        /// <returns>Point in specified position</returns>
        public Point<T> this[int index]
        {
            get
            {
                if (this.Length > index 
                    && index >= 0)
                {
                    return this.Points[index];
                }
                else
                {
                    throw new IndexOutOfRangeException();
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="{Position<typeparamref name="T"}" /> class.
        /// </summary>
        public Position()
        {
            this.PointType = Dimensions.OneDimensional;
            this.Length = 0;
            this.Points = null;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="{Position<typeparamref name="T"}" /> class.
        /// </summary>
        /// <param name="pointType">PointType of this position</param>
        /// <param name="Length">Number of points in this position</param>
        internal Position(int length, Dimensions pointType)
        {
            this.PointType = pointType;
            this.Length = length;
            this.Points = new Point<T>[this.Length];
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="{Position<typeparamref name="T"}" /> class.
        /// </summary>
        /// <param name="position">Position to copy</param>
        internal Position(Position<T> position)
        {
            this.PointType = position.PointType;
            this.Length = position.Length;
            this.Points = position.Points;
        }

        /// <summary>
        /// Sets point at specified position of this 
        /// </summary>
        /// <param name="index">Integer in 0 and this position length</param>
        /// <param name="point">New point</param>
        internal void SetPoint(int index, Point<T> point)
        {
            if (this.Length > index
                    && index >= 0
                    && point.PointType == this.PointType)
            {
                this.Points[index] = new Point<T>(this.PointType, point.Coordinates);
            }
            else if (!(this.Length > index
                    && index >= 0))
            {
                throw new IndexOutOfRangeException();
            }
            else if (point.PointType != this.PointType)
            {
                throw new WrongPointTypeException();
            }
            else
            {
                throw new Exception();
            }
        }

        /// <summary>
        /// Provides IEnumerator of points
        /// </summary>
        /// <returns>IEnumerator of points</returns>
        public IEnumerator<Point<T>> GetEnumerator()
        {
            foreach (var point in this.Points)
            {
                if (point == null)
                {
                    break;
                }

                yield return point;
            }
        }

        /// <summary>
        /// Provides IEnumerator for position
        /// </summary>
        /// <returns>Enumerator for points in position</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
