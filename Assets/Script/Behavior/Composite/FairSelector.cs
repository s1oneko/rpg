using BehaviorDesigner.Runtime.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Metadata;

public class FairSelector : Composite
{
    // The index of the child that is currently running or is about to run.
    private int currentChildIndex = 0;
    private List<int> executionOrder = new List<int>();
    private System.Random random = new System.Random();

    private bool isShuffled = false;

    public override void OnStart()
    {
        // Only shuffle if all tasks have been executed.
        if (!isShuffled)
        {
            // Generate the execution order based on the children size
            for (int i = 0; i < children.Count; i++)
            {
                executionOrder.Add(i);
            }

            // Fisher-Yates shuffle
            for (int i = 0; i < executionOrder.Count; i++)
            {
                int temp = executionOrder[i];
                int randomIndex = i + random.Next(executionOrder.Count - i);
                executionOrder[i] = executionOrder[randomIndex];
                executionOrder[randomIndex] = temp;
            }

            isShuffled = true;
        }
    }

    public override int CurrentChildIndex()
    {
        return executionOrder[currentChildIndex];
    }

    public override bool CanExecute()
    {
        // Return true as long as there are more children that haven't been executed.
        return currentChildIndex < children.Count;
    }

    public override void OnChildExecuted(TaskStatus childStatus)
    {
        // Move on to the next child.
        currentChildIndex++;
        if (currentChildIndex == children.Count)
        {
            // All tasks have been executed, prepare for the next shuffle.
            isShuffled = false;
        }
    }

    public override void OnEnd()
    {
        // Reset the variables back to their starting values.
        if (isShuffled)
        {
            // Only clear the execution order if we have finished executing all tasks.
            executionOrder.Clear();
            currentChildIndex = 0;
        }
    }
}



