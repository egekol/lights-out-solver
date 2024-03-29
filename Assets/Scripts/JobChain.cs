using System;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

/*
 *
 * Given a NativeArray<int> of size N filled with positive integers, the class below filters the values
 * which are powers of two.The operation is asynchronous and when the operation is completed and the result is ready,
 * the caller is notified through the callback given.The operation is divided into two steps:
 * 
 * The first step is to generate a mask array of size N.
 * The value at an index is 1 if the corresponding value in the source array is a power of two, otherwise 0.
 *
 * The second step is to filter the values in the source array using the mask generated in the first step.
 * The resulting data should contain the original value if it is a power of two, otherwise 0.
 * The jobs can take as long is they need.
 * The operation should not hang the main thread by waiting for jobs to complete.
 * Burst compilation should be used on the jobs.The operations inside the jobs should be as optimized as possible.
 * Relevant attributes should be used to maximize performance.
 * The original array should not be modified. When the result array is ready,
 * invoke the given callback with the result.
 * Note that there is a dependency between the outputs and inputs of the two jobs.
 * Implement the class below and the described jobs, according to the constraints given above.
 * 
 * 
 */

public class JobChain : MonoBehaviour
{
    private NativeArray<int> sourceArray;
    private Action<NativeArray<int>> onComplete;

    public void FilterPowerOfTwoValues(NativeArray<int> values, Action<NativeArray<int>> onCompleted)
    {
        sourceArray = values;
        onComplete = onCompleted;

        var maskJob = new PowerOfTwoMaskJob
        {
            sourceArray = sourceArray,
            maskArray = new NativeArray<int>(sourceArray.Length, Allocator.TempJob)
        };
        var maskJobHandle = maskJob.Schedule(sourceArray.Length, 64);

        var filterJob = new PowerOfTwoFilterJob
        {
            sourceArray = sourceArray,
            maskArray = maskJob.maskArray,
            resultArray = new NativeArray<int>(sourceArray.Length, Allocator.TempJob)
        };
        var filterJobHandle = filterJob.Schedule(sourceArray.Length, 64, maskJobHandle);

        var onCompleteJob = new OnCompleteJob
        {
            resultArray = filterJob.resultArray,
            onComplete = onCompleted
        };
        onCompleteJob.Schedule(filterJobHandle);
    }

    [BurstCompile]
    private struct PowerOfTwoMaskJob : IJobParallelFor
    {
        [ReadOnly] public NativeArray<int> sourceArray;

        public NativeArray<int> maskArray;

        public void Execute(int index)
        {
            var value = sourceArray[index];
            if ((value & (value - 1)) == 0)
                maskArray[index] = 1;
            else
                maskArray[index] = 0;
        }
    }

    [BurstCompile]
    private struct PowerOfTwoFilterJob : IJobParallelFor
    {
        [ReadOnly] public NativeArray<int> sourceArray;

        [ReadOnly] public NativeArray<int> maskArray;

        public NativeArray<int> resultArray;

        public void Execute(int index)
        {
            // resultArray[index] = maskArray[index] == 0 ? 0 : sourceArray[index];
            resultArray[index] = sourceArray[index] & maskArray[index];
        }
    }


    private struct OnCompleteJob : IJob
    {
        [DeallocateOnJobCompletion] public NativeArray<int> resultArray;

        public Action<NativeArray<int>> onComplete;

        public void Execute()
        {
            onComplete(resultArray);
        }
    }
}