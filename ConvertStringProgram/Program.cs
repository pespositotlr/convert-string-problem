using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConvertStringProgram
{
    class Program
    {
        static void Main(string[] args)
        {
            ConvertString.GetConvertedString("(id, name, email, type(id, name, customFields(c1, c2, c3)), externalId)");
        }
    }

    public class ConvertString
    {
        public class ConvertStringNode
        {
            public string Value { get; set; }
            public List<ConvertStringNode> Children { get; set; }
        }

        public static void GetConvertedString(string inputString)
        {
            if (string.IsNullOrEmpty(inputString))
                return;

            //Assume first character is '('
            if (inputString[0] != '(')
                return;

            ConvertStringNode nodeTree = GetNodeTree(inputString.Substring(1));

            PrintNodeTreeDefaultOrder(nodeTree);

            Console.WriteLine();

            PrintNodeTreeSortAlphabetically(nodeTree);

            Console.ReadLine();
        }

        private static ConvertStringNode GetNodeTree(string inputString)
        {
            //Create root node
            ConvertStringNode rootNode = new ConvertStringNode() { Value = "root", Children = null };
            Stack<ConvertStringNode> parentStack = new Stack<ConvertStringNode>();
            parentStack.Push(rootNode);

            ConvertStringNode currentChildNode = null;

            int lastNodeStartIndex = 0;

            //Loop through the characters and create a node when you get to the end of a word
            for (int i = 0; i < inputString.Length; ++i)
            {
                char ch = inputString[i];

                ConvertStringNode currentParent = parentStack.Peek();

                if (ch == '(' || ch == ',' || ch == ')')
                {
                    string currentValue = inputString.Substring(lastNodeStartIndex, i - lastNodeStartIndex).Trim();

                    if (currentValue.Length > 0) //Length of the value will be 0 if there's 2 or more of , or ( or ) in a row
                    {
                        ConvertStringNode node = new ConvertStringNode();

                        node.Value = currentValue;

                        currentChildNode = node;

                        if (currentParent.Children == null)
                            currentParent.Children = new List<ConvertStringNode>();

                        currentParent.Children.Add(node);
                    }

                    lastNodeStartIndex = i + 1; //Increment the last node's start index so you don't start the next word with a , or a ( or )

                    if (ch == '(') //Indicates this node has children, put this child at the top of the parentStack
                    {
                        parentStack.Push(currentChildNode);
                    }
                    else if (ch == ')') //Indicates we're at the end of this parentStack item's children
                    {
                        parentStack.Pop();
                    }
                }

            }

            return rootNode;
        }

        private static void PrintNodeTreeDefaultOrder(ConvertStringNode root)
        {
            string result = PrintNodeTree(root, -1, false);

            Console.Write(result);
        }

        private static void PrintNodeTreeSortAlphabetically(ConvertStringNode root)
        {
            string result = PrintNodeTree(root, -1, true);

            Console.Write(result);
        }

        private static string PrintNodeTree(ConvertStringNode parent, int indentCount, bool sortAlphabetically = false)
        {
            StringBuilder sb = new StringBuilder();

            if (indentCount >= 0)
            {
                for (int i = 0; i < indentCount; i++)
                    sb.Append("  ");

                sb.Append("- ");
                sb.Append(parent.Value);
                sb.Append(Environment.NewLine);
            }

            if (parent.Children != null)
            {
                if (sortAlphabetically)
                    parent.Children = parent.Children.OrderBy(x => x.Value).ToList();

                foreach (ConvertStringNode child in parent.Children)
                {
                    sb.Append(PrintNodeTree(child, indentCount + 1, sortAlphabetically));
                }
            }

            return sb.ToString();
        }
    }
}
