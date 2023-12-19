using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;
using System.Windows.Markup;
using System.Xml.Serialization;
public class TreeNode<T> where T : notnull, IComparable
{
    
    public Dictionary<T, TreeNode<T>> root { get; init; }
    public T Value { get;private set; }
    public IEnumerable<TreeNode<T>>? Children { get;set;}

    private TreeNode(T Value, IEnumerable<T> values) 
    {
        this.Value = Value;
        this.Children = values?.Select(x => new TreeNode<T>(x));
    }

    public TreeNode(T Value)
    {
        this.Value = Value;
    }


}
public static class TreeNodehelper
{
    public static IEnumerable<TreeNode<T>> InvertTree<T>(this TreeNode<T> node) where T : notnull, IComparable
    {
        var result = new List<TreeNode<T>>();

        if (node == null)
        {
            return result;
        }
        else
        {
            if (node.Children != null)
            {
                node.Children.Select(child =>
                {
                    if (child?.Children != null && child.Children.Count() > 0)
                    {
                        result.AddRange(InvertTree(child));
                        ((List<TreeNode<T>>)child.Children).Add(node);
                        return null;
                    }
                    else
                    {
                        child.Children = new List<TreeNode<T>> { node };
                        return child;
                    }
                }).Where(x => x != null).ToList().ForEach(x => result.Add(x));
                node.Children = new List<TreeNode<T>>();
            }
            return result;
        }
    }
    public static string PrintTreeforplantUML<T>(this TreeNode<T> node) where T : notnull, IComparable
    {
        var sb = new StringBuilder();
        sb.AppendLine("@startuml");
        //node?.root?.Keys?.ToList().ForEach(x => sb.AppendLine("class no" + x + " {}"));
        node?.root?.Values?.OrderBy(x => x.Value).ToList().ForEach(x => x.Children?.ToList().ForEach(y => sb.AppendLine("(no" + x.Value + ") --> " + "(no" + y.Value+")")));
        sb.AppendLine("@enduml");
        return sb.ToString();
    }

    public static TreeNode<T> BuildTree<T>(Func<T, IEnumerable<T>> Next, T value = default, int Depth = -1, Dictionary<T, TreeNode<T>>? root = null) where T : notnull, IComparable
    {

        TreeNode<T> newNode ;
        if (root == null)
        {
            root = new Dictionary<T, TreeNode<T>>();
        }
       
        if (!root.ContainsKey(value))
        {
            newNode = new TreeNode<T>(value){root = root};
            root.Add(value,newNode);
        }
        else
        {
            return root[value];
        }
    
        if (Depth == 0 )
        {
            return newNode;
        }
        else
        {
            var orderedNextValues = Next?.Invoke(value)?.Order();
            if (orderedNextValues == null || orderedNextValues.Count() == 0)
            {
                return newNode;
            }
            else
            {
                newNode.Children = orderedNextValues?.Select(x => BuildTree(Next, value:x, Depth - 1, root)).ToImmutableList();
                return newNode;
            }
        }
    }

}
