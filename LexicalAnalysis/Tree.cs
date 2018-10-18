using System;
using System.Collections.Generic;

namespace LexicalAnalysis
{
    public class Tree<T>
    {
        private readonly Tree<T> Instance;
        private TreeNode<T> Root { get; set; }

        public Tree(T item)
        {
            Root = new TreeNode<T>(item);
        }
    }
}
