using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interview
{
    public class RandomListNode
    {
        public int label;
        public RandomListNode next, random;
        public RandomListNode(int x) { this.label = x; }
    }
    public class ListNode
    {
        public int val;
        public ListNode next;
        public ListNode(int x) { val = x; }
    }


    public class DoublyListNode
    {
        public int val;
        public DoublyListNode(int i)
        {
            val = i;
        }
        public DoublyListNode pre;
        public DoublyListNode next;
    }

    public class LinkedList
    {
        //142. Linked List Cycle II
        //Given a linked list, return the node where the cycle begins. If there is no cycle, return null.
        //To represent a cycle in the given linked list, we use an integer pos which represents the position (0-indexed) in the linked list where tail connects to. If pos is -1, then there is no cycle in the linked list.
        //Note: Do not modify the linked list.
        //因为快指针每次走2，慢指针每次走1，快指针走的距离是慢指针的两倍。而快指针又比慢指针多走了一圈。所以head到环的起点+环的起点到他们相遇的点的距离 与 环一圈的距离相等。
        //现在重新开始，head运行到环起点 和 相遇点到环起点 的距离也是相等的，相当于他们同时减掉了 环的起点到他们相遇的点的距离
        // meet position: pt2 = "distanace from head to circle start" + 1 circle + some extra step
        // meet position: pt1 = "distanace from head to circle start" +            some extra step       
        // pt1 pt2 are in the same position, so 1 circle distance = "distanace from head to circle start" + some extra step 
        public ListNode DetectCycle(ListNode head)
        {
            if (head == null || head.next == null)
                return null;

            ListNode ptr = head;
            ListNode ptr2 = head;

            while (ptr2 != null && ptr2.next != null)
            {
                ptr = ptr.next;
                ptr2 = ptr2.next.next;
                //meet at same node, looking for cycle start node, 
                if (ptr == ptr2)
                {
                    ListNode pre = head;
                    while (pre != ptr)
                    {
                        pre = pre.next;
                        ptr = ptr.next;
                    }
                    return ptr;
                }
            }
            return null;

        }

        //19. Remove Nth Node From End of List
        //Given a linked list, remove the n-th node from the end of list and return its head.
        //Example:Given linked list: 1->2->3->4->5, and n = 2.
        //After removing the second node from the end, the linked list becomes 1->2->3->5.
        public ListNode RemoveNthFromEnd(ListNode head, int n)
        {
            if (n < 1)
                return null;
            ListNode pt1 = head;
            ListNode pt2 = head;

            while (pt1 != null && pt1.next != null)
            {
                if (n <= 0)
                    pt2 = pt2.next;
                else
                    n = n - 1;

                pt1 = pt1.next;
            }
            if (n == 1)
                return head.next;
            else if (n > 1)
                return null;
            else
            {
                if (pt2.next != null)
                    pt2.next = pt2.next.next;
                else
                    pt2.next = null;

                return head;
            }
        }

        public ListNode RemoveNthFromEnd2(ListNode head, int n)
        {
            if (head == null)
                return null;

            var ptr = head;
            var ptr2 = head;
            int total = 0;
            while (ptr != null)
            {
                total += 1;
                ptr = ptr.next;
            }
            if (total == n)
                return head.next;

            int step = total - n - 1;
            while (step > 0)
            {
                ptr2 = ptr2.next;
                step -= 1;
            }
            if (ptr2 == null || ptr2.next == null)
                return null;
            ptr2.next = ptr2.next.next;
            return head;
        }

        //FB phone screen: reverse partial linkedlist between 2 duplicated values) input only contains 1 duplicate pair
        //e.g.  1,10,4,7,8,10,5 =>  1,10,8,7,4,10,5   (reverse elements between 2 10s)
        public ListNode PartialReverse(ListNode head)
        {
            if (head == null)
                return null;

            var ptrs = findStEndPtr(head);
            ListNode stPtr = ptrs[0];
            ListNode endPtr = ptrs[1];

            //start reserse 
            var stack = new Stack<int>();
            var midStPtr = stPtr.next;
            while (midStPtr != endPtr)
            {
                stack.Push(midStPtr.val);
                midStPtr = midStPtr.next;
            }

            midStPtr = stPtr.next;
            while (stack.Count > 0)
            {
                midStPtr.val = stack.Pop();
                midStPtr = midStPtr.next;
            }

            return head;
        }

        ListNode[] findStEndPtr(ListNode head)
        {
            var ptr = head;
            var ret = new ListNode[2];
            var map = new Dictionary<int, ListNode>();
            while (ptr != null)
            {
                if (map.ContainsKey(ptr.val))
                {
                    ret[0] = map[ptr.val];
                    ret[1] = ptr;
                }
                else
                    map.Add(ptr.val, ptr);

                ptr = ptr.next;
            }
            return ret;
        }

        //143. Reorder List
        //Given a singly linked list L: L0→L1→…→Ln-1→Ln, reorder it to: L0→Ln→L1→Ln-1→L2→Ln-2→…
        //You may not modify the values in the list's nodes, only nodes itself may be changed.
        //Example 1:  Given 1->2->3->4->5, reorder it to 1->5->2->4->3.
        public void ReorderList(ListNode head)
        {
            if (head == null || head.next == null)
                return;

            ListNode ptr = head;
            ListNode ptr2 = head;
            //find middle ptr
            while (ptr2.next != null && ptr2.next.next != null)
            {
                ptr = ptr.next;
                ptr2 = ptr2.next.next;
            }

            //reverse second half list e.g. 1->2->3->4->5->6 to 1->2->3->6->5->4
            ListNode midPre = ptr;
            ListNode midHead = ptr.next;
            ListNode pivol = ptr.next;
            ListNode front = null;
            while (pivol != null && pivol.next != null)
            {
                front = pivol.next;
                pivol.next = pivol.next.next;
                front.next = midHead;
                midHead = front;
                midPre.next = midHead;
            }

            //Start reorder one by one  1->2->3->6->5->4 to 1->6->2->5->3->4

            ListNode ptr1 = head;
            ListNode ptr22 = midPre.next;

            while (ptr1 != midPre)
            {
                midPre.next = ptr22.next;
                ptr22.next = ptr1.next;
                ptr1.next = ptr22;
                ptr1 = ptr22.next;
                ptr22 = midPre.next;
            }
        }


        //Doubly linkedlist contains 0 and 1 only, sort it in O(n), in-space
        // e.g.  0 <-> 1 <-> 1 <-> 0 <-> 1 <-> 0  
        //return 0 <-> 0 <-> 0 <-> 1 <-> 1 <-> 1
        DoublyListNode SortDoublyList(DoublyListNode head)
        {
            while (head == null)
                return null;

            //get tail ptr 
            var ptrEnd = head;
            var ptrStart = head;

            while (ptrEnd != null && ptrEnd.next != null)
                ptrEnd = ptrEnd.next;

            while (ptrStart != ptrEnd)
            {
                if (ptrStart.val == 1)//swap with end
                {
                    int temp = ptrEnd.val;
                    ptrEnd.val = ptrStart.val;
                    ptrStart.val = temp;
                    ptrEnd = ptrEnd.pre;   // end index left shift one
                }
                else
                    ptrStart = ptrStart.next;
            }
            return head;
        }


        //21 Merge Two Sorted Lists
        public ListNode MergeTwoLists(ListNode l1, ListNode l2)
        {
            if (l1 == null)
                return l2;
            if (l2 == null)
                return l1;
            var preHead = new ListNode(-1);

            var cur = preHead;

            while (l1 != null && l2 != null)
            {
                if (l1.val < l2.val)
                {
                    cur.next = l1;
                    l1 = l1.next;
                }
                else
                {
                    cur.next = l2;
                    l2 = l2.next;
                }
                cur = cur.next;
            }
            if (l1 != null)
                cur.next = l1;
            if (l2 != null)
                cur.next = l2;

            return preHead.next;
        }
        //Sort 2 Sorted LinkedList
        public ListNode SortTwo(ListNode n1, ListNode n2)
        {
            if (n1 == null && n2 == null)
                return null;
            if (n1 == null)
                return n2;
            if (n2 == null)
                return n1;

            var pt1 = n1;
            var pt2 = n2;

            var ret = new ListNode(-1);
            var retPt = ret;
            while (pt1 != null && pt2 != null)
            {
                if (pt1.val < pt2.val)
                {
                    retPt.next = new ListNode(pt1.val);
                    pt1 = pt1.next;
                }
                else
                {
                    retPt.next = new ListNode(pt2.val);
                    pt2 = pt2.next;
                }
                retPt = retPt.next;
            }

            while (pt1 != null)
            {
                retPt.next = new ListNode(pt1.val);
                pt1 = pt1.next;
                retPt = retPt.next;
            }
            while (pt2 != null)
            {
                retPt.next = new ListNode(pt2.val);
                pt2 = pt2.next;
                retPt = retPt.next;
            }
            return ret.next;
        }


        //23. Merge k Sorted Lists
        //Merge k sorted linked lists and return it as one sorted list.Analyze and describe its complexity.
        //Example: Input:
        //[
        //  1->4->5,
        //  1->3->4,
        //  2->6
        //]        Output: 1->1->2->3->4->4->5->6
        // 类似merge sort，每次将所有的list两两之间合并，直到所有list合并成一个。如果用迭代而非递归，则空间复杂度为O(1)。时间复杂度：
        // 2n * k/2 + 4n * k/4 + ... + (2^x)n * k/(2^x) = nk * x
        // k/(2^x) = 1 -> 2^x = k -> x = log2(k)
        // 所以时间复杂度为O(nk log(k))
        public ListNode MergeKLists(ListNode[] lists)
        {
            if (lists == null || lists.Length == 0)
                return null;
            int st = 0;
            int end = lists.Length - 1;

            while (end > 0)
            {
                st = 0;
                while (st < end)
                {
                    lists[st] = MergeTwoLists(lists[st], lists[end]);
                    st++;
                    end--;
                }
            }
            return lists[0];
        }


        //这种解法利用了最小堆这种数据结构，我们首先把k个链表的首元素都加入最小堆中，它们会自动排好序。
        //然后我们每次取出最小的那个元素加入我们最终结果的链表中，然后把取出元素的下一个元素再加入堆中，
        //下次仍从堆中取出最小的元素做相同的操作，以此类推，直到堆中没有元素了，此时k个链表也合并为了一个链表，返回首节点即可
        public ListNode MergeKListsPQ(ListNode[] lists)
        {
            if(lists==null || lists.Length==0)
                return null;
            var pq =new  List<ListNode>();            
            for(int i=0; i< lists.Length; i++){
                pq.Add(lists[i]);
            }
            if(pq.Count==0)
                return null;
            var ptr = new ListNode(-1);
            var cur = ptr;
            pq.Sort((x,y)=>x.val -y.val);

            while(pq.Count>0){
                var node = pq[0];
                pq.RemoveAt(0);    
                if(node==null)
                    continue;                
                cur.next = node;
                cur= cur.next;
                if(cur.next!=null){
                    pq.Add(cur.next);
                    pq.Sort((x,y)=>x.val -y.val);        
                }                
            }
            return ptr.next;
        }

        //328. Odd Even Linked List
        //Given a singly linked list, group all odd nodes together followed by the even nodes. 
        //Please note here we are talking about the node number and not the value in the nodes.
        //You should try to do it in place.The program should run in O(1) space complexity and O(nodes) time complexity.
        //Example: Given 1->2->3->4->5->NULL,
        //return         1->3->5->2->4->NULL.
        //Note:The relative order inside both the even and odd groups should remain as it was in the input. 
        //The first node is considered odd, the second node even and so on...
        public ListNode OddEvenList(ListNode head)
        {
            if (head == null || head.next == null)
                return head;

            var ptOdd = head;
            var ptEven = head.next;
            var ptrEven = ptEven;

            while (ptEven != null && ptEven.next != null)
            {
                ptOdd.next = ptEven.next;
                ptOdd = ptOdd.next;

                ptEven.next = ptOdd.next;
                ptEven = ptEven.next;
            }
            ptOdd.next = ptrEven;
            return head;
        }


        //61. Rotate List
        //Given a list, rotate the list to the right by k places, where k is non-negative.
        // Example: Given 1->2->3->4->5->NULL and k = 2,
        //return 4->5->1->2->3->NULL.        
        public ListNode RotateRight(ListNode head, int k)
        {
            if (head == null || head.next == null || k == 0) return head;

            //获取链表的总长度
            ListNode ptr1 = head; int len = 1;
            while (ptr1.next != null)
            { ptr1 = ptr1.next; len++; }
            //将链表首尾相连形成环
            ptr1.next = head;

            //找到需要截断的位置，因为k可能大于链表总长度。所以这里使用取余操作
            for (int i = 0; i < len - k % len; i++)
            {
                ptr1 = ptr1.next;
            }
            //将该处截断，指向空指针即可
            ListNode result = ptr1.next;
            ptr1.next = null;
            return result;
        }


        //206. Reverse Linked List
        //Reverse a singly linked list.
        public ListNode ReverseList(ListNode head)
        {
            if (head == null)
                return null;

            ListNode ptr = head;
            ListNode ptr2 = head;

            Stack<int> st = new Stack<int>();

            while (ptr != null)
            {
                st.Push(ptr.val);
                ptr = ptr.next;
            }

            while (st.Count != 0)
            {
                ptr2.val = st.Pop();
                ptr2 = ptr2.next;
                //retPtr = retPtr.next;
            }

            return head;
        }

        //space complex O(1);
        public ListNode ReverseList2(ListNode head)
        {
            if (head == null)
                return null;
            var new_h = head;

            while (head != null && head.next != null)
            {
                var cur = head.next;
                head.next = head.next.next;
                cur.next = new_h;
                new_h = cur;
            }
            return new_h;
        }

        //24. Swap Nodes in Pairs
        //Given a linked list, swap every two adjacent nodes and return its head.
        //For example        Given 1->2->3->4, you should return the list as 2->1->4->3.
        public ListNode SwapPairs(ListNode head)
        {
            if (head == null || head.next == null)
                return head;

            ListNode front = head.next;
            head.next = SwapPairs(head.next.next);
            front.next = head;
            return front;
        }


        //141. Linked List Cycle
        public bool HasCycle(ListNode head)
        {
            ListNode ptr1 = head;
            ListNode ptr2 = head;

            while (ptr1 != null && ptr2 != null && ptr2.next != null)
            {
                ptr1 = ptr1.next;
                ptr2 = ptr2.next.next;
                if (ptr1 == ptr2)
                    return true;
            }
            return false;
        }

        //237. Delete Node in a Linked List (given only the node to be deleted)
        public void DeleteNode(ListNode node)
        {
            if (node == null)
                return;
            if (node.next != null)
            {
                node.val = node.next.val;
                node.next = node.next.next;
            }
            else
                node = null;
        }


        //2. add two number
        //You are given two non-empty linked lists representing two non-negative integers.The digits are stored in reverse order and each of their nodes contain a single digit.Add the two numbers and return it as a linked list.
        //You may assume the two numbers do not contain any leading zero, except the number 0 itself.
        //Example: Input: (2 -> 4 -> 3) + (5 -> 6 -> 4)
        //Output: 7 -> 0 -> 8
        //Explanation: 342 + 465 = 807
        public ListNode AddTwoNumbers1(ListNode l1, ListNode l2)
        {
            ListNode ptr = new ListNode(0);
            ListNode pre = ptr;
            //bool addone = false;
            int carry = 0;
            while (l1 != null || l2 != null)
            {
                int l1Val = 0;
                int l2Val = 0;
                if (l1 != null)
                    l1Val = l1.val;
                if (l2 != null)
                    l2Val = l2.val;

                int digit = (l1Val + l2Val + carry) % 10;
                carry = (l1Val + l2Val + carry) / 10;

                ptr.next = new ListNode(digit);
                ptr = ptr.next;

                if (l1 != null)
                    l1 = l1.next;
                if (l2 != null)
                    l2 = l2.next;
            }
            if (carry > 0)
                ptr.next = new ListNode(carry);

            return pre.next;
        }


        //445. Add Two Numbers II
        //Input: (7 -> 2 -> 4 -> 3) + (5 -> 6 -> 4)
        //Output: 7 -> 8 -> 0 -> 7
        public ListNode AddTwoNumbers(ListNode l1, ListNode l2)
        {
            if (l1 == null && l2 == null)
                return null;

            Stack<int> st1 = new Stack<int>();
            Stack<int> st2 = new Stack<int>();
            Stack<int> st3 = new Stack<int>();

            while (l1 != null)
            {
                st1.Push(l1.val);
                l1 = l1.next;
            }

            while (l2 != null)
            {
                st2.Push(l2.val);
                l2 = l2.next;
            }
            int carry = 0;
            while (st1.Count != 0 || st2.Count != 0 || carry != 0)
            {
                int v1 = st1.Count == 0 ? 0 : st1.Pop();
                int v2 = st2.Count == 0 ? 0 : st2.Pop();
                int v3 = (v1 + v2 + carry) % 10;
                carry = (v1 + v2 + carry) / 10;
                st3.Push(v3);
            }
            ListNode ret = new ListNode(-1);
            ListNode ptr = ret;
            while (st3.Count != 0)
            {
                ptr.next = new ListNode(st3.Pop());
                ptr = ptr.next;
            }
            return ret.next;
        }

        //leetcode 138. Copy List with Random Pointer 
        //A linked list is given such that each node contains an additional random pointer which could point to any node in the list or null.
        //Return a Deep copy of the list.
        public RandomListNode CopyRandomList(RandomListNode head)
        {
            if (head == null)
                return null;

            Dictionary<RandomListNode, RandomListNode> map = new Dictionary<RandomListNode, RandomListNode>();
            RandomListNode ptr = head;

            while (ptr != null)
            {
                map.Add(ptr, new RandomListNode(ptr.label));
                ptr = ptr.next;
            }

            ptr = head;
            while (ptr != null)
            {
                map[ptr].next = ptr.next == null ? null : map[ptr.next];
                map[ptr].random = ptr.random == null ? null : map[ptr.random];
                ptr = ptr.next;
            }
            return map[head];
        }
    }
}
