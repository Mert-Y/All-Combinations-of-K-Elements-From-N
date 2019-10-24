using System;
using System.Collections.Generic;
using System.Linq;


namespace All_Combinations_of_K_Elements_From_N
{

    class Program
    {
        static void combinations(int[] arr, int len, int startPosition, int[] result, List<List<int>> set)
        {
            if (len == 0)
            {
                set.Add(new List<int>(result));
                return;
            }
            for (int i = startPosition; i <= arr.Length - len; i++)
            {
                result[result.Length - len] = arr[i];
                combinations(arr, len - 1, i + 1, result, set);
            }
        }

        static List<List<int>> GetBestCoverage2(List<List<int>> superSet, List<List<int>> subSet2)
        {
            List<List<int>> result;
            List<List<int>> subSet = new List<List<int>>(subSet2);
            if (superSet.Count == subSet.Count)
            {
                result = new List<List<int>>(superSet);
                return result;
            }
            else
            {
                result = new List<List<int>>();
            }


            for (int i = 0; i < superSet.Count; i++)
            {
                List<int> subFoundList = new List<int>();
                for (int j = 0; j < subSet.Count; j++)
                {
                    int cnt = 0;
                    foreach (var item in superSet[i])
                    {
                        foreach (var item2 in subSet[j])
                        {
                            if (item == item2)
                            {
                                cnt++;
                            }
                        }
                    }
                    if (cnt == subSet[j].Count)
                    {
                        subFoundList.Add(j);
                    }
                    if (subFoundList.Count == superSet[i].Count)
                    {
                        for (int k = subFoundList.Count - 1; k >= 0; k--)
                        {
                            subSet.RemoveAt(subFoundList[k]);
                        }
                        result.Add(superSet[i]);
                        break;
                    }
                }
            }
            return result;
        }

        static List<List<int>> GetBestCoverage(List<List<int>> superSet, List<List<int>> subSet)
        {
            List<List<int>> result;
            if (superSet.Count == subSet.Count)
            {
                result = new List<List<int>>(superSet);
                return result;
            }
            else
            {
                result = new List<List<int>>();
            }

            int findMatchCnt = superSet[0].Count;
            while (subSet.Count > 0)
            {


                List<int> superFoundList = new List<int>();
                for (int i = 0; i < superSet.Count; i++)
                {
                    List<int> subFoundList = new List<int>();


                    for (int j = 0; j < subSet.Count; j++)
                    {
                        int cnt = 0;
                        foreach (var item in superSet[i])
                        {
                            foreach (var item2 in subSet[j])
                            {
                                if (item == item2)
                                {
                                    cnt++;
                                }
                            }
                        }
                        if (cnt == subSet[j].Count)
                        {
                            subFoundList.Add(j);
                        }
                        if (subFoundList.Count == findMatchCnt)
                        {
                            for (int k = subFoundList.Count - 1; k >= 0; k--)
                            {
                                subSet.RemoveAt(subFoundList[k]);
                            }
                            superFoundList.Add(i);
                            superSet[i].Add(findMatchCnt);
                            result.Add(superSet[i]);
                            break;
                        }
                    }


                }
                for (int k = superFoundList.Count - 1; k >= 0; k--)
                {
                    superSet.RemoveAt(superFoundList[k]);
                }
                findMatchCnt--;
            }

            if (subSet.Count > 0)
            {
                GetBestCoverage(superSet, subSet);
            }
            return result;
        }
        static void Main(string[] args)
        {
            int superSetRange = 6;
            int subSetRange = 5;

            List<List<int>> superSet = new List<List<int>>();
            List<List<int>> subSet = new List<List<int>>();

            //Range
            int[] arr = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };

            //Create Combinations
            combinations(arr, superSetRange, 0, new int[superSetRange], superSet);
            combinations(arr, subSetRange, 0, new int[subSetRange], subSet);

            List<string> test = new List<string>();
            foreach (var item in subSet)
            {
                test.Add(String.Join(" ", item));
            }
            List<List<int>> result = new List<List<int>>();
            int tmpCnt = subSet.Count;
            int maxFnd = 0;
            for (int i = 0; i < superSet.Count; i++)
            {
                result = GetBestCoverage2(superSet, subSet);
                Console.WriteLine(result.Count);
                if (result.Count>maxFnd)
                {
                    maxFnd = result.Count;
                }
                var item = superSet[superSet.Count-1];
                superSet.RemoveAt(superSet.Count - 1);
                superSet.Insert(0, item);
            }
            
            test = new List<string>();
            foreach (var item in subSet)
            {
                test.Add(String.Join(" ", item));
            }
            //Convert to string
            List<string> resultString = new List<string>();
            foreach (var item in result)
            {
                resultString.Add(String.Join(" ", item));
                Console.WriteLine(resultString.Last());
            }
            Console.WriteLine("Total Superset: " + superSet.Count);
            Console.WriteLine("Total Subset: " + tmpCnt);
            Console.WriteLine("Total Found: " + result.Count);
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
