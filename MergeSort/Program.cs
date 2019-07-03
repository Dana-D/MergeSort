using System;
using System.Collections.Generic;

namespace MergeSort
{
    class Program
    {
        static void Main(string[] args)
        {
            string input1 = @"1
4
4 3 2 1";
            string input = @"3
5
3 5 2 4 1
3
9 15 0
4
2 3 7 4";
            input = input.Replace("\r", "");
            Test[] tests = parseInput(input);

            foreach (Test t in tests)
            {
                LinkedList<int> result = t.mergeSort();
                Console.WriteLine("----ORIGINAL----");
                Test.printList(t.list);
                Console.WriteLine("------NEW-------");
                Test.printList(result);
                Console.WriteLine("----------------");
            }
        }

        static Test[] parseInput(string input)
        {
            string[] inputs = input.Split("\n");

            int num_of_tests = Int32.Parse(inputs[0]);
            int pos = 0;
            Test[] tests = new Test[num_of_tests];

            for (int i = 1; i < inputs.Length; i++)
            {
                if(i%2 == 0)
                {
                    LinkedList<int> newList = new LinkedList<int>();
                    string[] line = inputs[i].Split(" ");
                    foreach (string s in line)
                    {
                        LinkedListNode<int> node = new LinkedListNode<int>(Int32.Parse(s));
                        newList.AddLast(node);
                    }
                    Test t = new Test(newList);
                    tests[pos] = t;
                    pos++;
                }
            }

            return tests;
        }
    }

    class Test
    {
        public LinkedList<int> list;

        public Test()
        {
            list = new LinkedList<int>();
        }

        public Test(LinkedList<int> newList)
        {
            list = newList;
        }

        public static int getMiddle(LinkedList<int> l)
        {
            double d = (double)l.Count / 2;
            int m = (int)Math.Ceiling(d);
            return m;
        }

        public LinkedList<int> mergeLists(LinkedList<int> one, LinkedList<int> two)
        {
            LinkedList<int> newList = new LinkedList<int>();
            LinkedListNode<int> leftNode = one.First;
            LinkedListNode<int> rightNode = two.First;

            while (leftNode != null || rightNode != null)
            {
                if(leftNode != null && leftNode.Value < rightNode.Value)
                {
                    newList.AddLast(leftNode.Value);
                    leftNode = leftNode.Next;
                }
                else if(rightNode != null)
                {
                    newList.AddLast(rightNode.Value);
                    rightNode = rightNode.Next;
                }
            }

            return newList;
        }

        public LinkedList<int> mergeSort()
        {
            return mergeSort(list, getMiddle(list));
        }

        public LinkedList<int> mergeSort(LinkedList<int> aList, int middle)
        {
            LinkedList<int> left = new LinkedList<int>();
            LinkedList<int> right = new LinkedList<int>();

            LinkedListNode<int> node = aList.First;
            for (int i = 0; i < aList.Count; i++)
            {
                if ( i < middle)
                {
                    left.AddLast(node.Value);
                    //Console.WriteLine("Left: " + node.Value);
                }
                else
                {
                    right.AddLast(node.Value);
                    //Console.WriteLine("Right: " + node.Value);
                }
                node = node.Next;
            }

            if (left.First.Next != null)
            {
                left = mergeSort(left, getMiddle(left));
            }

            if (right.First.Next != null)
            {
                right = mergeSort(right, getMiddle(right));
            }

            if (left.First.Value <= right.First.Value)
            {
                left = mergeLists(left, right);
                return left;
            }
            else
            {
                right = mergeLists(right, left);
                return right;
            }
        }

        public static void printList(LinkedList<int> aList)
        {
            LinkedListNode<int> node = aList.First;
            if (node == null)
            {
                return;
            }
            while (node != null)
            {
                if (node.Next == null)
                {
                    Console.Write(node.Value);
                }
                else
                {
                    Console.Write(node.Value + " ");
                }
                node = node.Next;
            }
            Console.WriteLine();
        }
    }
}
