using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interview
{
  public class Node
  {
    public Node next;
    public int property;

    public Node(int i)
    {
      property = i;
      next = null;
    }


    public void AppendNode(int data)
    {

      Node ll = new Node(data);
      Node ptr = this;
      while (ptr.next!=null)
      {
        ptr = ptr.next;
      }
      ptr.next = ll;

    }

    public Node DeleteNode(Node node, int data)
    {
      Node head = node;
      if (node.property == data)
        return head.next;

      while (node.next != null)
      {
        if (node.next.property == data)
        {
          node.next = node.next.next;
          break; 
        }
        node = node.next;
      }
      return head;
    }

    public void RemoveDuplicate(Node node)
    {
      HashSet<int> hs = new HashSet<int>();
      Node head = node;
      if(node==null){throw new Exception();}

      hs.Add(node.property);
      while (node.next != null)
      {
        if (hs.Add(node.next.property))
        {
          node = node.next;
        }
        else
        {
          node.next = node.next.next;
        }

      }

      //print list
      printLinkedList(head);
    }

    void printLinkedList(Node head)
    {

      //print list
      while (head.next != null)
      {
        Console.WriteLine(head.property);
        head = head.next;
      }
      Console.WriteLine(head.property);
      
    }
  }
}
