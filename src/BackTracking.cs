using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interview
{
    public class BackTracking
    {
        //39. Combination Sum
        public IList<IList<int>> CombinationSum(int[] candidates, int target)
        {
            Array.Sort(candidates);
            var ret = new List<IList<int>>();
            bk(candidates, 0, target, ret, new List<int>());
            return ret;
        }

        void bk(int[] nums, int st, int target, List<IList<int>> ret, List<int> subSet)
        {
            if (target == subSet.Sum())
            {
                ret.Add(new List<int>(subSet));
                return;
            }
            if (target < subSet.Sum())
                return;
            
            for (int i = st; i < nums.Length; i++)
            {
                if(nums[i] > target)
                    break;
                subSet.Add(nums[i]);
                bk(nums, i, target, ret, subSet);  //note: here is i (not i+1, because number can be repeated)
                subSet.RemoveAt(nums[i]);
            }
        }

        //494. Target Sum
        //You are given a list of non-negative integers, a1, a2, ..., an, and a target, S. Now you have 2 symbols + and -. For each integer, you should choose one from + and - as its new symbol.
        //Find out how many ways to assign symbols to make sum of integers equal to target S
        //Input: nums is [1, 1, 1, 1, 1], S is 3. 
        //Output: 5
        int result_sumWay = 0;
        public int FindTargetSumWays(int[] nums, int S)
        {
            if (nums == null || nums.Length == 0)
                return 0;

            helper(nums, 0, S, 0);
            return result_sumWay;
        }
        void helper(int[] nums, int level, int S, int curSum)
        {
            if (nums.Length == level && curSum == S)
            {
                result_sumWay++;
                return;
            }
            else if (nums.Length <= level)
                return;

            helper(nums, level + 1, S, curSum + nums[level]);
            helper(nums, level + 1, S, curSum - nums[level]);

        }

        //254. Factor Combinations
        //Numbers can be regarded as product of its factors.For example,
        //input: 12        output:
        //[  [2, 6],  [2, 2, 3],  [3, 4] ]

        public IList<IList<int>> GetFactors(int n)
        {
            List<IList<int>> ret = new List<IList<int>>();
            if (n <= 3)
                return ret;

            factorHelper(ret, new List<int>(), n, 2);
            return ret;
        }
        void factorHelper(IList<IList<int>> ret, List<int> curList, int cur, int curIdx)
        {
            if (cur == 1)
            {
                if (curList.Count > 1)
                    ret.Add(new List<int>(curList));
                return;
            }
            for (int i = curIdx; i <= cur; i++)
            {
                if (cur % i == 0)
                {
                    curList.Add(i);
                    factorHelper(ret, curList, cur / i, i);
                    curList.RemoveAt(curList.Count - 1);
                }
            }
        }

        #region General term 

        //78. Subsets
        //If nums = [1,2,3], a solution is:
        //  [
        //[]
        //[3],
        //[1],
        //[2],
        //[1,2,3],
        //[1,3],
        //[2,3],
        //[1,2], 
        //]
        // time complexity is O(2^N) for this bk-tracking approach
        public IList<IList<int>> Subsets(int[] nums)
        {
            var ret = new List<IList<int>>();
            backtracking(0, nums, new List<int>(), ret);
            return ret;
        }
        void backtracking(int curIdx, int[] nums, List<int> list, List<IList<int>> ret)
        {
            ret.Add(new List<int>(list));
            for (int i = curIdx; i < nums.Length; i++)
            {
                list.Add(nums[i]);
                backtracking(i + 1, nums, list, ret);
                list.RemoveAt(list.Count - 1);
            }
        }

        //90. Subsets II
        //Given a collection of integers that might contain duplicates, nums, return all possible subsets.
        //Note: The solution set must not contain duplicate subsets.
        //If nums = [1,2,2], a solution is:
        //[
        //  [2],
        // [1],
        //[1,2,2],
        //[2,2],
        //[1,2],
        //[]
        //]
        public IList<IList<int>> SubsetsWithDup(int[] nums)
        {
            var ret = new List<IList<int>>();
            var hashsets = new HashSet<string>();
            Array.Sort(nums);
            subsetHelperWithoutDuplicate2(0, nums, new List<int>(), ret);
            return ret;
        }
        void subsetHelperWithoutDuplicate2(int curIdx, int[] nums, List<int> list, IList<IList<int>> ret)
        {
            ret.Add(new List<int>(list));
            for (int i = curIdx; i < nums.Length; i++)
            {
                // if (i > curIdx && nums[i] == nums[i - 1])   //key point note: i>curIdx
                //    continue;
                list.Add(nums[i]);
                subsetHelperWithoutDuplicate2(i + 1, nums, list, ret);
                list.Remove(nums[i]);
                while (i + 1 < nums.Length && nums[i] == nums[i + 1])
                    i++;
            }
        }

        //77. Combinations
        //Given two integers n and k, return all possible combinations of k numbers out of 1 ... n.
        //If n = 4 and k = 2, a solution is:
        //[
        //    [2,4],
        //    [3,4],
        //    [2,3],
        //    [1,2],
        //    [1,3],
        //    [1,4],
        //]
        public IList<IList<int>> Combine(int n, int k)
        {
            List<IList<int>> ret = new List<IList<int>>();

            helper(1, new List<int>(), ret, n, k);
            return ret;
        }
        void helper(int curIdx, List<int> curList, List<IList<int>> ret, int n, int k)
        {
            if (curList.Count == k)
            {
                ret.Add(new List<int>(curList));
                return;
            }
            for (int i = curIdx; i <= n; i++)
            {
                curList.Add(i);
                helper(i + 1, curList, ret, n, k);
                curList.Remove(curList.Last());
            }
        }

        void BTHelper(List<int> cur, int[] A, int K, List<List<int>> ret, int stIdx)
        {
            if (cur.Count > 0 && cur.Sum() % K == 0)
            {
                ret.Add(new List<int>(cur));
                //return;
            }

            for (int i = stIdx; i < A.Length; i++)
            {
                if (i > stIdx && A[i] == A[i - 1])
                    continue;
                cur.Add(A[i]);
                BTHelper(cur, A, K, ret, i + 1);
                cur.Remove(cur.Last());
            }

        }

        public IList<IList<int>> CombinationSum3(int[] candidates, int target)
        {
            IList<IList<int>> ret = new List<IList<int>>();
            bk(ret, 0, new List<int>(), candidates, target);
            return ret;
        }
        void bk(IList<IList<int>> ret, int idx, List<int> curList, int[] candidates, int t)
        {
            if (curList.Sum() == t)
            {
                ret.Add(new List<int>(curList));
                return;
            }
            if (curList.Sum() > t)
                return;

            for (int i = idx; i < candidates.Length; i++)
            {
                curList.Add(candidates[i]);
                bk(ret, i + 1, curList, candidates, t);
                curList.Remove(candidates[i]);
            }
        }

        //40. Combination Sum II
        //All numbers (including target) will be positive integers.
        //The solution set must not contain duplicate combinations.
        //For example, given candidate set[10, 1, 2, 7, 6, 1, 5] and target 8,
        //A solution set is: 
        //[
        //  [1, 7],
        //  [1, 2, 5],
        //  [2, 6],
        //  [1, 1, 6]
        //]
        public IList<IList<int>> CombinationSum2(int[] candidates, int target)
        {
            Array.Sort(candidates);
            List<IList<int>> ret = new List<IList<int>>();
            helper(candidates, target, 0, new List<int>(), ret);
            return ret;
        }
        void helper(int[] nums, int target, int idx, List<int> curList, List<IList<int>> ret)
        {
            if (target == 0)
            {
                ret.Add(new List<int>(curList));
                return;
            }

            for (int i = idx; i < nums.Length; i++)
            {
                if (nums[i] > target)
                    return;
                if (i > idx && nums[i] == nums[i - 1])
                    continue;
                curList.Add(nums[i]);
                helper(nums, target - nums[i], i + 1, curList, ret);
                curList.Remove(curList.Last());
            }
        }

        //46. Permutations
        //Given a collection of Distinct numbers, return all possible permutations.
        //      For example,      [1, 2, 3] have the following permutations:
        //[
        //[1,2,3],
        //[1,3,2],
        //[2,1,3],
        //[2,3,1],
        //[3,1,2],
        //[3,2,1]
        //]
        public IList<IList<int>> Permute(int[] nums)
        {
            var ret = new List<IList<int>>();
            PermuBackTrack1(nums, 0, ret);
            return ret;
        }
        void PermuBackTrack1(int[] nums, int idx, List<IList<int>> ret)
        {
            if (idx == nums.Length)
            {
                ret.Add(new List<int>(nums.ToList()));
                return;
            }
            for (int i = idx; i < nums.Length; i++)
            {
                swap(nums, idx, i);
                PermuBackTrack1(nums, idx + 1, ret);
                swap(nums, i, idx);
            }
        }
        void swap(int[] arr, int i, int j)
        {
            if (i == j)
                return;
            int temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;
        }

        //47. Permutations II
        //Given a collection of numbers that might contain duplicates, return all possible unique permutations.
        //For example,[1,1,2] have the following unique permutations:
        //[
        //[1,1,2],
        //[1,2,1],
        //[2,1,1]
        //]
        public IList<IList<int>> PermuteUnique(int[] nums)
        {
            var ret = new List<IList<int>>();
            var ret1 = new HashSet<IList<int>>();
            Array.Sort(nums);
            backtrackPerm(nums, 0, ret1);
            return ret1.ToList();   // ret;
        }
        void backtrackPerm(int[] nums, int idx, HashSet<IList<int>> ret)
        {
            if (idx == nums.Length)
            {
                ret.Add(new List<int>(nums));
                return;
            }
            for (int i = idx; i < nums.Length; i++)
            {
                if (i > idx && nums[i] == nums[idx])
                    continue;

                swap(nums, idx, i);
                backtrackPerm(nums, idx + 1, ret);
                swap(nums, i, idx);
            }
        }


        HashSet<string> hashSet = new HashSet<string>();
        void backtrack(int[] nums, int idx, IList<IList<int>> ret)
        {
            if (idx == nums.Length)
            {
                string temp = string.Join(",", nums.ToList());
                if (!hashSet.Contains(temp))
                {
                    hashSet.Add(temp);
                    ret.Add(new List<int>(nums.ToList()));
                }
                return;
            }
            for (int i = idx; i < nums.Length; i++)
            {
                if (i > idx && nums[i] == nums[i - 1])
                    continue;

                swap(nums, idx, i);
                backtrack(nums, idx + 1, ret);
                swap(nums, i, idx);
            }
        }



        #endregion
    }
}

