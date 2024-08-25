using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace SelfCollections
{
    public class PriorityQueue<TElement, TPriority>
    {
        /// <summary>
        /// Represents an implicit heap-ordered complete d-ary tree, stored as an array.
        /// </summary>
        private (TElement Element, TPriority Priority)[] _nodes;

        /// <summary>
        /// Custom comparer used to order the heap.
        /// </summary>
        private readonly IComparer<TPriority>? _comparer;

        /// <summary>
        /// The number of nodes in the heap.
        /// </summary>
        private int _size;

        /// <summary>
        /// Version updated on mutation to help validate enumerators operate on a consistent state.
        /// </summary>
        private int _version;

        /// <summary>
        /// Specifies the arity of the d-ary heap, which here is quaternary.
        /// It is assumed that this value is a power of 2.
        /// </summary>
        private const int Arity = 4;

        /// <summary>
        /// The binary logarithm of <see cref="Arity" />.
        /// </summary>
        private const int Log2Arity = 2;

        #region Initialize
        /// <summary>
        ///  Initializes a new instance of the <see cref="PriorityQueue{TElement, TPriority}"/> class.
        /// </summary>
        public PriorityQueue()
        {
            _nodes = Array.Empty<(TElement Element, TPriority Priority)>();
            _comparer = InitializeComparer(null);
        }

        /// <summary>
        ///  Initializes a new instance of the <see cref="PriorityQueue{TElement, TPriority}"/> class
        ///  with the specified initial capacity.
        /// </summary>
        /// <param name="initialCapacity">Initial capacity to allocate in the underlying heap array.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        ///  The specified <paramref name="initialCapacity"/> was negative.
        /// </exception>
        public PriorityQueue(int initialCapacity)
            : this(initialCapacity, comparer: null)
        {
        }

        /// <summary>
        ///  Initializes a new instance of the <see cref="PriorityQueue{TElement, TPriority}"/> class
        ///  with the specified custom priority comparer.
        /// </summary>
        /// <param name="comparer">
        ///  Custom comparer dictating the ordering of elements.
        ///  Uses <see cref="Comparer{T}.Default" /> if the argument is <see langword="null"/>.
        /// </param>
        public PriorityQueue(IComparer<TPriority> comparer)
        {
            _nodes = Array.Empty<(TElement Element, TPriority Priority)>();
            _comparer = InitializeComparer(comparer);
        }

        /// <summary>
        ///  Initializes a new instance of the <see cref="PriorityQueue{TElement, TPriority}"/> class
        ///  with the specified initial capacity and custom priority comparer.
        /// </summary>
        /// <param name="initialCapacity">Initial capacity to allocate in the underlying heap array.</param>
        /// <param name="comparer">
        ///  Custom comparer dictating the ordering of elements.
        ///  Uses <see cref="Comparer{T}.Default" /> if the argument is <see langword="null"/>.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        ///  The specified <paramref name="initialCapacity"/> was negative.
        /// </exception>
        public PriorityQueue(int initialCapacity, IComparer<TPriority>? comparer)
        {
            if (initialCapacity < 0)
            {
                throw new ArgumentOutOfRangeException("Initial capacity cannot be a negative number.");
            }

            _nodes = new (TElement, TPriority)[initialCapacity];
            _comparer = InitializeComparer(comparer);
        }

        /// <summary>
        /// Initializes the custom comparer to be used internally by the heap.
        /// </summary>
        private static IComparer<TPriority>? InitializeComparer(IComparer<TPriority>? comparer)
        {
            if (typeof(TPriority).IsValueType)
            {
                if (comparer == Comparer<TPriority>.Default)
                {
                    return null;
                }
                return comparer;
            }
            else
            {
                return comparer ?? Comparer<TPriority>.Default;
            }
        }
        #endregion

        /// <summary>
        ///  Gets the number of elements contained in the <see cref="PriorityQueue{TElement, TPriority}"/>.
        /// </summary>
        public int Count => _size;

        /// <summary>
        ///  Gets the priority comparer used by the <see cref="PriorityQueue{TElement, TPriority}"/>.
        /// </summary>
        public IComparer<TPriority> Comparer => _comparer ?? Comparer<TPriority>.Default;

        /// <summary>
        ///  Adds the specified element with associated priority to the <see cref="PriorityQueue{TElement, TPriority}"/>.
        /// </summary>
        /// <param name="element">The element to add to the <see cref="PriorityQueue{TElement, TPriority}"/>.</param>
        /// <param name="priority">The priority with which to associate the new element.</param>
        public void Enqueue(TElement element, TPriority priority)
        {
            int currentSize = _size;
            _version++;

            if (_nodes.Length == currentSize)
            {
                Grow(currentSize + 1);
            }

            _size = currentSize + 1;

            if (_comparer == null)
            {
                MoveUpDefaultComparer((element, priority), currentSize);
            }
            else
            {
                MoveUpCustomComparer((element, priority), currentSize);
            }
        }

        /// <summary>
        ///  Returns the minimal element from the <see cref="PriorityQueue{TElement, TPriority}"/> without removing it.
        /// </summary>
        /// <exception cref="InvalidOperationException">The <see cref="PriorityQueue{TElement, TPriority}"/> is empty.</exception>
        /// <returns>The minimal element of the <see cref="PriorityQueue{TElement, TPriority}"/>.</returns>
        public TElement Peek()
        {
            if (_size == 0)
            {
                throw new InvalidOperationException("The priority queue is empty.");
            }

            return _nodes[0].Element;
        }

        /// <summary>
        ///  Removes and returns the minimal element from the <see cref="PriorityQueue{TElement, TPriority}"/>.
        /// </summary>
        /// <exception cref="InvalidOperationException">The queue is empty.</exception>
        /// <returns>The minimal element of the <see cref="PriorityQueue{TElement, TPriority}"/>.</returns>
        public TElement Dequeue()
        {
            if (_size == 0)
            {
                throw new InvalidOperationException("The priority queue is empty.");
            }

            TElement element = _nodes[0].Element;
            RemoveRootNode();
            return element;
        }

        /// <summary>
        ///  Removes the minimal element and then immediately adds the specified element with associated priority to the <see cref="PriorityQueue{TElement, TPriority}"/>,
        /// </summary>
        /// <param name="element">The element to add to the <see cref="PriorityQueue{TElement, TPriority}"/>.</param>
        /// <param name="priority">The priority with which to associate the new element.</param>
        /// <exception cref="InvalidOperationException">The queue is empty.</exception>
        /// <returns>The minimal element removed before performing the enqueue operation.</returns>
        /// <remarks>
        ///  Implements an extract-then-insert heap operation that is generally more efficient
        ///  than sequencing Dequeue and Enqueue operations: in the worst case scenario only one
        ///  shift-down operation is required.
        /// </remarks>
        public TElement DequeueEnqueue(TElement element, TPriority priority)
        {
            if (_size == 0)
            {
                throw new InvalidOperationException("The priority queue is empty.");
            }

            (TElement Element, TPriority Priority) root = _nodes[0];

            if (_comparer == null)
            {
                if (Comparer<TPriority>.Default.Compare(priority, root.Priority) > 0)
                {
                    MoveDownDefaultComparer((element, priority), 0);
                }
                else
                {
                    _nodes[0] = (element, priority);
                }
            }
            else
            {
                if (_comparer.Compare(priority, root.Priority) > 0)
                {
                    MoveDownCustomComparer((element, priority), 0);
                }
                else
                {
                    _nodes[0] = (element, priority);
                }
            }

            _version++;
            return root.Element;
        }

        /// <summary>
        ///  Removes the minimal element from the <see cref="PriorityQueue{TElement, TPriority}"/>,
        ///  and copies it to the <paramref name="element"/> parameter,
        ///  and its associated priority to the <paramref name="priority"/> parameter.
        /// </summary>
        /// <param name="element">The removed element.</param>
        /// <param name="priority">The priority associated with the removed element.</param>
        /// <returns>
        ///  <see langword="true"/> if the element is successfully removed;
        ///  <see langword="false"/> if the <see cref="PriorityQueue{TElement, TPriority}"/> is empty.
        /// </returns>
        public bool TryDequeue([MaybeNullWhen(false)] out TElement element, [MaybeNullWhen(false)] out TPriority priority)
        {
            if (_size != 0)
            {
                (element, priority) = _nodes[0];
                RemoveRootNode();
                return true;
            }

            element = default;
            priority = default;
            return false;
        }

        /// <summary>
        ///  Returns a value that indicates whether there is a minimal element in the <see cref="PriorityQueue{TElement, TPriority}"/>,
        ///  and if one is present, copies it to the <paramref name="element"/> parameter,
        ///  and its associated priority to the <paramref name="priority"/> parameter.
        ///  The element is not removed from the <see cref="PriorityQueue{TElement, TPriority}"/>.
        /// </summary>
        /// <param name="element">The minimal element in the queue.</param>
        /// <param name="priority">The priority associated with the minimal element.</param>
        /// <returns>
        ///  <see langword="true"/> if there is a minimal element;
        ///  <see langword="false"/> if the <see cref="PriorityQueue{TElement, TPriority}"/> is empty.
        /// </returns>
        public bool TryPeek([MaybeNullWhen(false)] out TElement element, [MaybeNullWhen(false)] out TPriority priority)
        {
            if (_size != 0)
            {
                (element, priority) = _nodes[0];
                return true;
            }

            element = default;
            priority = default;
            return false;
        }

        /// <summary>
        ///  Adds the specified element with associated priority to the <see cref="PriorityQueue{TElement, TPriority}"/>,
        ///  and immediately removes the minimal element, returning the result.
        /// </summary>
        /// <param name="element">The element to add to the <see cref="PriorityQueue{TElement, TPriority}"/>.</param>
        /// <param name="priority">The priority with which to associate the new element.</param>
        /// <returns>The minimal element removed after the enqueue operation.</returns>
        /// <remarks>
        ///  Implements an insert-then-extract heap operation that is generally more efficient
        ///  than sequencing Enqueue and Dequeue operations: in the worst case scenario only one
        ///  shift-down operation is required.
        /// </remarks>
        public TElement EnqueueDequeue(TElement element, TPriority priority)
        {
            if (_size != 0)
            {
                (TElement Element, TPriority Priority) root = _nodes[0];

                if (_comparer == null)
                {
                    if (Comparer<TPriority>.Default.Compare(priority, root.Priority) > 0)
                    {
                        MoveDownDefaultComparer((element, priority), 0);
                        _version++;
                        return root.Element;
                    }
                }
                else
                {
                    if (_comparer.Compare(priority, root.Priority) > 0)
                    {
                        MoveDownCustomComparer((element, priority), 0);
                        _version++;
                        return root.Element;
                    }
                }
            }

            return element;
        }

        /// <summary>
        ///  Enqueues a sequence of element/priority pairs to the <see cref="PriorityQueue{TElement, TPriority}"/>.
        /// </summary>
        /// <param name="items">The pairs of elements and priorities to add to the queue.</param>
        /// <exception cref="ArgumentNullException">
        ///  The specified <paramref name="items"/> argument was <see langword="null"/>.
        /// </exception>
        public void EnqueueRange(IEnumerable<(TElement Element, TPriority Priority)> items)
        {
            if (items == null)
            {
                throw new ArgumentNullException("Item is null");
            }

            int count = 0;
            var collection = items as ICollection<(TElement Element, TPriority Priority)>;
            if (collection is not null && (count = collection.Count) > _nodes.Length - _size)
            {
                Grow(checked(_size + count));
            }

            if (_size == 0)
            {
                // build using Heapify() if the queue is empty.

                if (collection is not null)
                {
                    collection.CopyTo(_nodes, 0);
                    _size = count;
                }
                else
                {
                    int i = 0;
                    (TElement, TPriority)[] nodes = _nodes;
                    foreach ((TElement element, TPriority priority) in items)
                    {
                        if (nodes.Length == i)
                        {
                            Grow(i + 1);
                            nodes = _nodes;
                        }

                        nodes[i++] = (element, priority);
                    }

                    _size = i;
                }

                _version++;

                if (_size > 1)
                {
                    Heapify();
                }
            }
            else
            {
                foreach ((TElement element, TPriority priority) in items)
                {
                    Enqueue(element, priority);
                }
            }
        }

        /// <summary>
        ///  Enqueues a sequence of elements pairs to the <see cref="PriorityQueue{TElement, TPriority}"/>,
        ///  all associated with the specified priority.
        /// </summary>
        /// <param name="elements">The elements to add to the queue.</param>
        /// <param name="priority">The priority to associate with the new elements.</param>
        /// <exception cref="ArgumentNullException">
        ///  The specified <paramref name="elements"/> argument was <see langword="null"/>.
        /// </exception>
        public void EnqueueRange(IEnumerable<TElement> elements, TPriority priority)
        {
            if (elements == null)
            {
                throw new ArgumentNullException("Element is null");
            }

            int count;
            if (elements is ICollection<TElement> collection &&
                (count = collection.Count) > _nodes.Length - _size)
            {
                Grow(checked(_size + count));
            }

            if (_size == 0)
            {
                // build using Heapify() if the queue is empty.

                int i = 0;
                (TElement, TPriority)[] nodes = _nodes;
                foreach (TElement element in elements)
                {
                    if (nodes.Length == i)
                    {
                        Grow(i + 1);
                        nodes = _nodes;
                    }

                    nodes[i++] = (element, priority);
                }

                _size = i;
                _version++;

                if (i > 1)
                {
                    Heapify();
                }
            }
            else
            {
                foreach (TElement element in elements)
                {
                    Enqueue(element, priority);
                }
            }
        }

        /// <summary>
        ///  Removes all items from the <see cref="PriorityQueue{TElement, TPriority}"/>.
        /// </summary>
        public void Clear()
        {
            if (RuntimeHelpers.IsReferenceOrContainsReferences<(TElement, TPriority)>())
            {
                // Clear the elements so that the gc can reclaim the references
                Array.Clear(_nodes, 0, _size);
            }
            _size = 0;
            _version++;
        }

        /// <summary>
        ///  Ensures that the <see cref="PriorityQueue{TElement, TPriority}"/> can hold up to
        ///  <paramref name="capacity"/> items without further expansion of its backing storage.
        /// </summary>
        /// <param name="capacity">The minimum capacity to be used.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        ///  The specified <paramref name="capacity"/> is negative.
        /// </exception>
        /// <returns>The current capacity of the <see cref="PriorityQueue{TElement, TPriority}"/>.</returns>
        public int EnsureCapacity(int capacity)
        {
            if (capacity < 0)
            {
                throw new ArgumentOutOfRangeException("Capacity cannot be a negative number.");
            }

            if (_nodes.Length < capacity)
            {
                Grow(capacity);
                _version++;
            }

            return _nodes.Length;
        }

        /// <summary>
        ///  Sets the capacity to the actual number of items in the <see cref="PriorityQueue{TElement, TPriority}"/>,
        ///  if that is less than 90 percent of current capacity.
        /// </summary>
        /// <remarks>
        ///  This method can be used to minimize a collection's memory overhead
        ///  if no new elements will be added to the collection.
        /// </remarks>
        public void TrimExcess()
        {
            int threshold = (int)(_nodes.Length * 0.9);
            if (_size < threshold)
            {
                Array.Resize(ref _nodes, _size);
                _version++;
            }
        }

        /// <summary>
        /// Grows the priority queue to match the specified min capacity.
        /// </summary>
        private void Grow(int minCapacity)
        {
            const int ArrayMaxLength = Int32.MaxValue - 56;

            // 增长倍数
            const int GrowFactor = 2;

            // 扩容最小值
            const int MinimumGrow = 4;

            // 决定新数组的大小
            int newCapacity = GrowFactor * _nodes.Length;
            if ((uint)newCapacity > ArrayMaxLength)
            {
                newCapacity = ArrayMaxLength;
            }
            newCapacity = Math.Max(newCapacity, _nodes.Length + MinimumGrow);
            if (newCapacity < minCapacity)
            {
                newCapacity = minCapacity;
            }

            // 重新分配内存
            Array.Resize(ref _nodes, newCapacity);
        }

        /// <summary>
        /// Removes the node from the root of the heap
        /// </summary>
        private void RemoveRootNode()
        {
            int lastNodeIndex = --_size;
            _version++;

            if (lastNodeIndex > 0)
            {
                (TElement Element, TPriority Priority) lastNode = _nodes[lastNodeIndex];
                if (_comparer == null)
                {
                    MoveDownDefaultComparer(lastNode, 0);
                }
                else
                {
                    MoveDownCustomComparer(lastNode, 0);
                }
            }

            if (RuntimeHelpers.IsReferenceOrContainsReferences<(TElement, TPriority)>())
            {
                _nodes[lastNodeIndex] = default;
            }
        }

        /// <summary>
        /// Gets the index of an element's parent.
        /// </summary>
        private int GetParentIndex(int index) => (index - 1) >> Log2Arity;

        /// <summary>
        /// Gets the index of the first child of an element.
        /// </summary>
        private int GetFirstChildIndex(int index) => (index << Log2Arity) + 1;

        /// <summary>
        /// Converts an unordered list into a heap.
        /// </summary>
        private void Heapify()
        {
            // Leaves of the tree are in fact 1-element heaps, for which there
            // is no need to correct them. The heap property needs to be restored
            // only for higher nodes, starting from the first node that has children.
            // It is the parent of the very last element in the array.

            (TElement Element, TPriority Priority)[] nodes = _nodes;
            int lastParentWithChildren = GetParentIndex(_size - 1);

            if (_comparer == null)
            {
                for (int index = lastParentWithChildren; index >= 0; --index)
                {
                    MoveDownDefaultComparer(nodes[index], index);
                }
            }
            else
            {
                for (int index = lastParentWithChildren; index >= 0; --index)
                {
                    MoveDownCustomComparer(nodes[index], index);
                }
            }
        }

        /// <summary>
        /// Moves a node up in the tree to restore heap order.
        /// </summary>
        private void MoveUpDefaultComparer((TElement Element, TPriority Priority) node, int nodeIndex)
        {
            // Instead of swapping items all the way to the root, we will perform
            // a similar optimization as in the insertion sort.

            Debug.Assert(_comparer is null);
            Debug.Assert(0 <= nodeIndex && nodeIndex < _size);

            (TElement Element, TPriority Priority)[] nodes = _nodes;

            while (nodeIndex > 0)
            {
                int parentIndex = GetParentIndex(nodeIndex);
                (TElement Element, TPriority Priority) parent = nodes[parentIndex];

                if (Comparer<TPriority>.Default.Compare(node.Priority, parent.Priority) < 0)
                {
                    nodes[nodeIndex] = parent;
                    nodeIndex = parentIndex;
                }
                else
                {
                    break;
                }
            }

            nodes[nodeIndex] = node;
        }

        /// <summary>
        /// Moves a node up in the tree to restore heap order.
        /// </summary>
        private void MoveUpCustomComparer((TElement Element, TPriority Priority) node, int nodeIndex)
        {
            // Instead of swapping items all the way to the root, we will perform
            // a similar optimization as in the insertion sort.

            Debug.Assert(_comparer is not null);
            Debug.Assert(0 <= nodeIndex && nodeIndex < _size);

            IComparer<TPriority> comparer = _comparer;
            (TElement Element, TPriority Priority)[] nodes = _nodes;

            while (nodeIndex > 0)
            {
                int parentIndex = GetParentIndex(nodeIndex);
                (TElement Element, TPriority Priority) parent = nodes[parentIndex];

                if (comparer.Compare(node.Priority, parent.Priority) < 0)
                {
                    nodes[nodeIndex] = parent;
                    nodeIndex = parentIndex;
                }
                else
                {
                    break;
                }
            }

            nodes[nodeIndex] = node;
        }

        /// <summary>
        /// Moves a node down in the tree to restore heap order.
        /// </summary>
        private void MoveDownDefaultComparer((TElement Element, TPriority Priority) node, int nodeIndex)
        {
            // The node to move down will not actually be swapped every time.
            // Rather, values on the affected path will be moved up, thus leaving a free spot
            // for this value to drop in. Similar optimization as in the insertion sort.

            Debug.Assert(_comparer is null);
            Debug.Assert(0 <= nodeIndex && nodeIndex < _size);

            (TElement Element, TPriority Priority)[] nodes = _nodes;
            int size = _size;

            int i;
            while ((i = GetFirstChildIndex(nodeIndex)) < size)
            {
                // Find the child node with the minimal priority
                (TElement Element, TPriority Priority) minChild = nodes[i];
                int minChildIndex = i;

                int childIndexUpperBound = Math.Min(i + Arity, size);
                while (++i < childIndexUpperBound)
                {
                    (TElement Element, TPriority Priority) nextChild = nodes[i];
                    if (Comparer<TPriority>.Default.Compare(nextChild.Priority, minChild.Priority) < 0)
                    {
                        minChild = nextChild;
                        minChildIndex = i;
                    }
                }

                // Heap property is satisfied; insert node in this location.
                if (Comparer<TPriority>.Default.Compare(node.Priority, minChild.Priority) <= 0)
                {
                    break;
                }

                // Move the minimal child up by one node and
                // continue recursively from its location.
                nodes[nodeIndex] = minChild;
                nodeIndex = minChildIndex;
            }

            nodes[nodeIndex] = node;
        }

        /// <summary>
        /// Moves a node down in the tree to restore heap order.
        /// </summary>
        private void MoveDownCustomComparer((TElement Element, TPriority Priority) node, int nodeIndex)
        {
            // The node to move down will not actually be swapped every time.
            // Rather, values on the affected path will be moved up, thus leaving a free spot
            // for this value to drop in. Similar optimization as in the insertion sort.

            Debug.Assert(_comparer is not null);
            Debug.Assert(0 <= nodeIndex && nodeIndex < _size);

            IComparer<TPriority> comparer = _comparer;
            (TElement Element, TPriority Priority)[] nodes = _nodes;
            int size = _size;

            int i;
            while ((i = GetFirstChildIndex(nodeIndex)) < size)
            {
                // Find the child node with the minimal priority
                (TElement Element, TPriority Priority) minChild = nodes[i];
                int minChildIndex = i;

                int childIndexUpperBound = Math.Min(i + Arity, size);
                while (++i < childIndexUpperBound)
                {
                    (TElement Element, TPriority Priority) nextChild = nodes[i];
                    if (comparer.Compare(nextChild.Priority, minChild.Priority) < 0)
                    {
                        minChild = nextChild;
                        minChildIndex = i;
                    }
                }

                // Heap property is satisfied; insert node in this location.
                if (comparer.Compare(node.Priority, minChild.Priority) <= 0)
                {
                    break;
                }

                // Move the minimal child up by one node and continue recursively from its location.
                nodes[nodeIndex] = minChild;
                nodeIndex = minChildIndex;
            }

            nodes[nodeIndex] = node;
        }
    }
}
