using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public int[] testArray;


    public void AddToThisIndex(int indexToAddTo, int addThis)
    {
        List<int> toSkip = new();
        toSkip.Add(indexToAddTo);
        testArray[indexToAddTo] += addThis;
        DistributeThisAmount(toSkip, addThis/testArray.Length);
    }

    public void DistributeThisAmount(List<int> indexToSkip,int toDistribute)
    {
        for (int i = 0; i < testArray.Length; i++)
        {
            bool shouldSkip = false;
            for (int y = 0; y < indexToSkip.Count; y++)
            {
                if (indexToSkip[y] == i)
                {
                    shouldSkip = true;
                }
            }
            if (!shouldSkip) {
                testArray[i] -= toDistribute;
                if (testArray[i] < 0)
                {
                    int remainder = Mathf.Abs(testArray[i]);
                    testArray[i] = 0;
                    indexToSkip.Add(i);
                    DistributeThisAmount(indexToSkip, remainder / testArray.Length);
                }
            }
        }
    }
}
