using System.Collections.Generic;

namespace ELearning.Common
{
    /// <summary>
    /// 表示一个树的节点信息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Node<T> where T : class,new()
    {
        private T _data;
        private Node<T> _parent;
        private List<Node<T>> _childs;

        public Node(T data)
        {
            _data = data;
        }

        public T Data { get { return _data; } }

        public Node<T> Parent { get { return _parent; } }

        public bool HasChild { get { return Childs.Count > 0; } }

        public Node<T> Add(T data)
        {
            var node = new Node<T>(data);
            node._parent = this;
            if (_childs == null)
                _childs = new List<Node<T>>();

            _childs.Add(node);

            return node;
        }

        public List<Node<T>> Childs { get { return _childs ?? new List<Node<T>>(); } }
    }
}