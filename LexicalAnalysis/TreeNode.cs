using System.Collections.Generic;

namespace LexicalAnalysis
{
    internal class TreeNode<T>
    {
        private readonly T Item;

        private List<TreeNode<T>> ListNode { set; get; }

        public TreeNode(T item)
        {
            this.Item = item;
        }

        public void AddNode(T item)
        {
            ListNode.Add(item: new TreeNode<T>(item));
        }

        public void AddNode(TreeNode<T> tn)
        {
            ListNode.Add(tn);
        }

    }
}