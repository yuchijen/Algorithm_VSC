using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interview
{
    public class BinarySearch
    {
        //215. Kth Largest Element in an Array
        //use q-sort 核心思想是每次都要先找一个中枢点Pivot，然后遍历其他所有的数字，像这道题从小往大排的话，
        //就把小于中枢点的数字放到左半边，把大于中枢点的放在右半边，这样中枢点是整个数组中第几大的数字就确定了，
        //虽然左右两部分不一定是完全有序的，但是并不影响本题要求的结果，所以我们求出中枢点的位置，如果正好是k-1，
        //那么直接返回该位置上的数字；如果大于k-1，说明要求的数字在左半部分，更新右边界，再求新的中枢点位置；
        //反之则更新右半部分，求中枢点的位置；不得不说，这个思路真的是巧妙啊～
        //quickSort(arr[], low, high)
        //{
        //  if (low<high)
        //  {   /* pi is partitioning index, arr[pi] is now at right place */
        //      pi = partition(arr, low, high);
        //      quickSort(arr, low, pi - 1);  // Before pi
        //      quickSort(arr, pi + 1, high); // After pi
        //  }
        //}
        public int FindKthLargest(int[] nums, int k)
        {
            int left = 0, right = nums.Length - 1;
            while (true)
            {
                int pos = partition(nums, left, right);
                if (pos == k - 1)
                    return nums[pos];
                else if (pos > k - 1)
                    right = pos - 1;
                else
                    left = pos + 1;
            }
        }
        int partition(int[] nums, int left, int right)
        {
            int pivot = nums[left], l = left + 1, r = right;
            while (l <= r)
            {
                if (nums[l] < pivot && nums[r] > pivot)
                {
                    swap(nums, l, r);
                    l++;
                    r--;
                }
                if (nums[l] >= pivot) ++l;
                if (nums[r] <= pivot) --r;
            }
            swap(nums, left, r);
            return r;
        }

        void swap(int[] arr, int i, int j)
        {
            int temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;
        }


        //378. Kth Smallest Element in a Sorted Matrix
        //Given a n x n matrix where each of the rows and columns are sorted in ascending order, find the kth smallest element in the matrix.
        //Note that it is the kth smallest element in the sorted order, not the kth distinct element.
        //Example:
        //matrix = [[ 1,  5,  9],
        //          [10, 11, 13],
        //          [12, 13, 15]],
        // k = 8,  return 13.
        // nlogn * log(max val - min val in matrix)
        public int KthSmallest(int[,] matrix, int k)
        {
            if (matrix == null || matrix.GetLength(0) == 0 || matrix.GetLength(1) == 0)
                return -1;

            int n = matrix.GetLength(0);
            int st = matrix[0, 0];
            int end = matrix[matrix.GetLength(0) - 1, matrix.GetLength(1) - 1];
            //use mid-value as assumption to do b-search, 

            while (st <= end)
            {
                int mid = st + (end - st) / 2;
                //search all elements: b-search each row
                int count = 0;
                for (int i = 0; i < n; i++)
                {
                    count += BSearchFindCountLessThanVal(matrix, i, n, mid);
                }
                if (count < k)
                    st = mid + 1;
                else
                    end = mid - 1;
            }
            return st;

        }
        int BSearchFindCountLessThanVal(int[,] matrix, int row, int n, int val)
        {
            int st = 0;

            while (st <= n)
            {
                int midIdx = st + (n - st) / 2;

                if (matrix[row, midIdx] <= val)
                    st = midIdx + 1;
                else
                    n = midIdx - 1;
            }
            return st;
        }

        //240. Search a 2D Matrix II
        //Write an efficient algorithm that searches for a value in an m x n matrix. This matrix has the following properties:
        //Integers in each row are sorted in ascending from left to right.
        //Integers in each column are sorted in ascending from top to bottom.
        //Example: Consider the following matrix:
        //[
        // [1,   4,  7, 11, 15],
        // [2,   5,  8, 12, 19],
        // [3,   6,  9, 16, 22],
        // [10, 13, 14, 17, 24],
        // [18, 21, 23, 26, 30]
        //]     Given target = 5, return true.
        //O(M+N) ??
        public bool SearchMatrix2D(int[,] matrix, int target)
        {
            int n = matrix.GetLength(0);
            int m = matrix.GetLength(1);
            if (n == 0 || m == 0)
                return false;

            int row = 0;
            int col = m - 1;

            while (row < n && col >= 0)
            {
                if (target > matrix[row, col])
                    row++;
                else if (target < matrix[row, col])
                    col--;
                else
                    return true;
            }
            return false;

        }

        bool bSearch(int[,] matrix, int row, int target, int st, int end)
        {
            while (st <= end)
            {
                int pivol = (st + end) / 2;

                if (target > matrix[row, pivol])
                    st = pivol + 1;
                else if (target < matrix[row, pivol])
                    end = pivol - 1;
                else
                    return true;
            }
            return false;
        }

        //another solution
        public bool SearchMatrix2(int[,] matrix, int target)
        {
            if (matrix == null || matrix.GetLength(0) == 0 || matrix.GetLength(1) == 0)
                return false;
            if (target < matrix[0, 0] || target > matrix[matrix.GetLength(0) - 1, matrix.GetLength(1) - 1])
                return false;

            //b-search 1st column then decide clostest row then search that row's  
            //O(log N x log M)
            int st = 0;
            int end = matrix.GetLength(0) - 1;
            while (st <= end)
            {
                int mid = (st + end) / 2;
                if (matrix[mid, 0] == target)
                    return true;
                if (matrix[mid, 0] < target)
                    st = mid + 1;
                else
                    end = mid - 1;
            }
            //keep closest row number
            int closestRow = end;
            st = 0;
            end = matrix.GetLength(1) - 1;

            while (st <= end)
            {
                int mid = (st + end) / 2;
                if (matrix[closestRow, mid] == target)
                    return true;
                if (matrix[closestRow, mid] < target)
                    st = mid + 1;
                else
                    end = mid - 1;
            }
            return false;
        }
        

        //4. Median of Two Sorted Arrays
        //There are two sorted arrays nums1 and nums2 of size m and n respectively.
        //Find the median of the two sorted arrays.The overall run time complexity should be O(log (m+n)).
        //You may assume nums1 and nums2 cannot be both empty.
        //Example 1:
        //nums1 = [1, 3]
        //nums2 = [2]  The median is 2.0
        //hint: need find k elememts in half, start nums1's half and nums2's half
        public double FindMedianSortedArrays(int[] nums1, int[] nums2)
        {
            //const int n1 = nums1.size();
            //const int n2 = nums2.size();
            //// Make sure n1 <= n2
            //if (n1 > n2) return findMedianSortedArrays(nums2, nums1);

            int k = (nums1.Length + nums2.Length + 1) / 2;

            int st = 0;
            int end = nums1.Length - 1;

            //find where is k-th (mid)  
            while (st < end)
            {
                int m1 = (st + end) / 2;  //index in nums1
                int m2 = k - m1;          //index in nums2

                if (nums1[m1] < nums2[m2 - 1])
                    st = m1 + 1;  //max of m1 still too small , go right 1 more
                else
                    end = m1;
            }

            int seg1 = st;
            int seg2 = k - seg1;
            //consider odd or even in total number)

            int c1 = Math.Max(nums1[seg1 - 1], nums2[seg2 - 1]);
            if ((nums1.Length + nums2.Length) % 2 == 1)
                return c1;

            int c2 = Math.Min(nums1[seg1], nums2[seg2]);
            return (c1 + c2) * 0.5;

            //const int c1 = max(m1 <= 0 ? INT_MIN : nums1[m1 - 1],
            //               m2 <= 0 ? INT_MIN : nums2[m2 - 1]);
            //if ((n1 + n2) % 2 == 1)
            //    return c1;
            //const int c2 = min(m1 >= n1 ? INT_MAX : nums1[m1],
            //                   m2 >= n2 ? INT_MAX : nums2[m2]);
            //return (c1 + c2) * 0.5;
        }


        //33. Search in Rotated Sorted Array
        //Suppose an array sorted in ascending order is rotated at some pivot unknown to you beforehand.
        //(i.e., 0 1 2 4 5 6 7 might become 4 5 6 7 0 1 2).
        //You are given a target value to search.If found in the array return its index, otherwise return -1.
        //You may assume no duplicate exists in the array.
        public int SearchRotatedSortedArray(int[] nums, int target)
        {
            if (nums == null || nums.Length == 0)
                return -1;

            int lo = 0;
            int hi = nums.Length - 1;
            //find min value index, which is rotated index
            while (lo < hi)
            {
                int mid = (lo + hi) / 2;
                if (nums[mid] > nums[hi])
                    lo = mid + 1;
                else
                    hi = mid;
            }
            // lo==hi is the index of the smallest value and also the number of places rotated.
            int rot = lo;
            lo = 0; hi = nums.Length - 1;
            while (lo <= hi)
            {
                int mid = (lo + hi) / 2;
                int realmid = (mid + rot) % nums.Length;
                if (nums[realmid] == target)
                    return realmid;
                if (nums[realmid] < target)
                    lo = mid + 1;
                else
                    hi = mid - 1;
            }
            return -1;
        }
        //anayse 2 cases : 
        // 34567012   start to mid are sorted 
        // 67012345   mid to end are sorted
        public int SearchRotatedSortedArray2(int[] nums, int target)
        {
            if (nums == null || nums.Length == 0)
                return -1;
            int st = 0;
            int end = nums.Length - 1;

            while (st <= end)
            {
                int mid = st + (end - st) / 2;
                if (nums[mid] == target)
                    return mid;
                if (nums[mid] < nums[end]) //right side sorted
                {
                    if (nums[mid] < target && target <= nums[end])
                        st = mid + 1;  //go right
                    else
                        end = mid - 1;
                }
                else //left side sorted
                {
                    if (target >= nums[st] && target < nums[mid])
                        end = mid - 1;  //go left
                    else
                        st = mid + 1;
                }
            }
            return -1;
        }

        //81. Search in Rotated Sorted Array II  with duplicate number
        public bool SearchRotatedSortedArrayII(int[] nums, int target)
        {
            if (nums == null || nums.Length == 0)
                return false;

            int lo = 0;
            int hi = nums.Length - 1;

            while (lo <= hi)
            {
                int piv = (lo + hi) / 2;

                if (nums[piv] == target)
                    return true;
                if (nums[piv] > nums[hi])
                {
                    if (target < nums[piv] && target >= nums[lo])
                        hi = piv - 1;
                    else
                        lo = piv + 1;
                }
                else if (nums[piv] < nums[hi])
                {
                    if (target > nums[piv] && target <= nums[hi])
                        lo = piv + 1;
                    else
                        hi = piv - 1;
                }
                else
                    hi--;
            }
            return false;
        }


        //278. First Bad Version
        //You are a product manager and currently leading a team to develop a new product. Unfortunately, the 
        //latest version of your product fails the quality check. Since each version is developed based on the 
        //previous version, all the versions after a bad version are also bad.
        //Suppose you have n versions[1, 2, ..., n] and you want to find out the first bad one, which causes 
        //all the following ones to be bad.
        //You are given an API bool isBadVersion(version) which will return whether version is bad.Implement a 
        //function to find the first bad version.You should minimize the number of calls to the API.
        public int FirstBadVersion(int n)
        {
            if (IsBadVersion(1))
                return 1;

            int st = 1;
            int end = n;

            while (st <= end)
            {
                int mid = st + (end - st) / 2;
                if (mid > 1 && IsBadVersion(mid) && !IsBadVersion(mid - 1))
                    return mid;
                if (IsBadVersion(mid))
                    end = mid - 1;
                else
                    st = mid + 1;
            }
            return -1;
        }

        bool IsBadVersion(int version)
        {
            return true;
        }

        //374. Guess Number Higher or Lower
        //We are playing the Guess Game. The game is as follows:
        //I pick a number from 1 to n.You have to guess which number I picked.
        //Every time you guess wrong, I'll tell you whether the number is higher or lower.
        //You call a pre-defined API guess(int num) which returns 3 possible results (-1, 1, or 0):
        public int guessNumber(int n)
        {
            if (n == 0)
                return 0;

            int st = 1, end = n;

            while (st <= end)
            {
                int piv = (st + end) / 2;

                if (guess(piv) == 0)
                    return piv;
                else if (guess(piv) > 0)
                {
                    end = piv - 1;
                }
                else
                {
                    st = piv + 1;
                }
            }
            return -1;
        }
        int guess(int num)
        {
            return -1;
        }


        //69. Sqrt(x)
        //Implement int sqrt(int x).
        //Compute and return the square root of x.
        public int MySqrt(int x)
        {
            if (x <= 0)
                return 0;

            int st = 1, end = x;

            while (true)
            {
                int piv = (st + end) / 2;

                if (piv > x / piv)
                    end = piv - 1;
                else
                {
                    if ((piv + 1) > x / (piv + 1))
                        return piv;
                    st = piv + 1;
                }
            }
        }


        //162. Find Peak Element
        //A peak element is an element that is greater than its neighbors.
        //Given an input array where num[i] ≠ num[i + 1], find a peak element and return its index.
        //The array may contain multiple peaks, in that case return the index to any one of the peaks is fine.
        //You may imagine that num[-1] = num[n] = -∞.
        //For example, in array[1, 2, 3, 1], 3 is a peak element and your function should return the index number 2.
        public int FindPeakElement(int[] nums)
        {
            if (nums == null || nums.Length == 0)
                return -1;

            int st = 0, end = nums.Length - 1;

            while (st <= end)
            {
                int piv = (st + end) >> 1;
                if (piv == 0 || piv == nums.Length - 1 || (piv < nums.Length - 1 && piv > 1 && nums[piv] > nums[piv + 1] && nums[piv] > nums[piv - 1]))
                    return piv;
                if (piv < nums.Length - 1 && nums[piv] < nums[piv + 1])
                    st = piv + 1;
                else if (piv > 0 && nums[piv] < nums[piv - 1])
                    end = piv - 1;
            }
            return -1;
        }


        //349. Intersection of Two Arrays
        //Given two arrays, write a function to compute their intersection.
        // Example:Given nums1 = [1, 2, 2, 1], nums2 = [2, 2], return [2].
        public int[] Intersection(int[] nums1, int[] nums2)
        {
            var l1 = nums1.ToList();
            var l2 = nums2.ToList();

            var cc = from item in l1
                     where (nums2.Contains(item))
                     select item;

            HashSet<int> hs = new HashSet<int>();

            foreach (var x in cc)
                hs.Add(x);
            return hs.ToArray();

        }


        //287. Find the Duplicate Number
        //Given an array nums containing n + 1 integers where each integer is between 1 and n (inclusive), prove that at least one duplicate number must exist. Assume that there is only one duplicate number, find the duplicate one.
        //Note: You must not modify the array(assume the array is read only).
        //You must use only constant, O(1) extra space., less than O(n^2) 
        public int FindDuplicate(int[] nums)
        {

            if (nums == null && nums.Length == 0)
                return -1;

            Array.Sort(nums);

            for (int i = 0; i < nums.Length - 1; i++)
            {
                if (nums[i] == nums[i + 1])
                    return nums[i];
            }
            return nums[nums.Length - 1];
        }

        //50. Pow(x, n)
        //Implement pow(x, n). smart solution
        public double MyPow(double x, int n)
        {
            if (n == 0)
                return 1;
            if (n < 0)
            {
                return 1 / x * MyPow(1 / x, -(n + 1));
            }
            else
            {
                if (n % 2 == 0)
                    return MyPow(x * x, n >> 1);
                else
                    return x * MyPow(x * x, n >> 1);
            }
        }

        // 153. Find Minimum in Rotated Sorted Array
        //Suppose an array sorted in ascending order is rotated at some pivot unknown to you beforehand.
        //(i.e., 0 1 2 4 5 6 7 might become 4 5 6 7 0 1 2).Find the minimum element.
        //You may assume no duplicate exists in the array.
        public int FindMin(int[] nums)
        {
            if (nums == null || nums.Length == 0)
                return 0;
            if (nums.Length == 1)
                return nums[0];

            int st = 0; int end = nums.Length - 1;

            while (st < end)
            {
                int mid = (st + end) >> 1;

                if (mid + 1 <= end && nums[mid] > nums[mid + 1])
                    return nums[mid + 1];
                else if (nums[mid] < nums[end])
                    end = mid;
                else if (nums[st] < nums[mid])
                    st = mid;
                else
                    end = mid;
            }
            return nums[0];
        }

        //Amazon: search value in increasing and decreasing array
        public int searchIncreasingDecreasing(int[] nums, int target)
        {
            if (nums == null || nums.Length == 0)
                return -1;

            //int st = 0;
            int end = nums.Length - 1;

            int maxIdx = findMaxValIdx(nums);
            if (maxIdx == -1)
                return -1;
            int section1 = BSearch(nums, 0, maxIdx, target);
            if (section1 != -1)
                return section1;

            int section2 = BInverseSearch(nums, maxIdx + 1, nums.Length - 1, target);
            if (section2 != -1)
                return section2;

            return -1;
        }
        int BSearch(int[] nums, int st, int end, int target)
        {
            while (st <= end)
            {
                int pivol = (st + end) / 2;
                if (target == nums[pivol])
                    return pivol;
                if (nums[pivol] < target)
                    st = pivol + 1;
                else
                    end = pivol - 1;
            }
            return -1;
        }
        int BInverseSearch(int[] nums, int st, int end, int target)
        {
            while (st <= end)
            {
                int pivol = (st + end) / 2;
                if (target == nums[pivol])
                    return pivol;
                if (nums[pivol] < target)
                    end = pivol - 1;
                else
                    st = pivol + 1;
            }
            return -1;

        }
        int findMaxValIdx(int[] nums)
        {
            if (nums == null || nums.Length == 0)
                return -1;

            int st = 0;
            int end = nums.Length - 1;

            while (st <= end)
            {
                int pivol = (st + end) / 2;
                if (pivol == 0 || pivol == nums.Length - 1 || (pivol > 0 && pivol < nums.Length - 1 && nums[pivol] > nums[pivol - 1] && nums[pivol] > nums[pivol + 1]))
                    return pivol;

                if (pivol < nums.Length - 1 && nums[pivol] > nums[pivol + 1])
                {
                    end = pivol - 1;
                }
                if (pivol > 0 && nums[pivol] > nums[pivol - 1])
                {
                    st = pivol + 1;
                }
            }
            return -1;
        }

        //33. Search in Rotated Sorted Array
        //Suppose an array sorted in ascending order is rotated at some pivot unknown to you beforehand.
        //  (i.e., 0 1 2 4 5 6 7 might become 4 5 6 7 0 1 2).
        //You are given a target value to search.If found in the array return its index, otherwise return -1.
        public int SearchRotatedArray(int[] nums, int target)
        {
            if (nums == null || nums.Length == 0)
                return -1;

            int st = 0;
            int end = nums.Length - 1;

            while (st <= end)
            {
                int pivol = (st + end) / 2;
                if (target == nums[pivol])
                    return pivol;

                if (nums[pivol] >= nums[st])
                {
                    if (target < nums[pivol] && target >= nums[st])
                        end = pivol - 1;
                    else
                        st = pivol + 1;
                }
                else
                {
                    if (target > nums[pivol] && target <= nums[end])
                        st = pivol + 1;
                    else
                        end = pivol - 1;
                }
            }
            return -1;
        }


    }
}
