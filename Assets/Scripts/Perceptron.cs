using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perceptron : MonoBehaviour
{
    [System.Serializable]
    public class TrainingSet
    {
        public double[] inputs;
        public double output;
    }

    public TrainingSet[] trainSet;
    public double[] weights = { 0, 0 };
    public double bias;
    public double totalError;
    private void Start()
    {
        Train(8);
        Debug.Log("Test 1:" + CalcDotProduct(0, 0));
        Debug.Log("Test 1:" + CalcDotProduct(1, 0));
        Debug.Log("Test 1:" + CalcDotProduct(0, 1));
        Debug.Log("Test 1:" + CalcDotProduct(1, 1));
    }
    private void InitiliazeWeights()
    {
        for (int i = 0; i < weights.Length; i++)
        {
            weights[i] = Random.Range(-1.0f, 1.0f);
        }
        bias = Random.Range(-1.0f,1.0f);
    }
    private double DotProduct(double[] weights,double[] inputs)
    {
        if (weights == null || inputs == null)
            return -1;
        if (weights.Length!=inputs.Length)
            return -1;
        double d = 0;
        for (int i = 0; i < weights.Length; i++)
        {
            d += weights[i] * inputs[i];
        }
        d += bias;
        return d;
    }
    private double CalcDotProduct(int t)
    {
        double d = DotProduct(weights, trainSet[t].inputs);
        if (d > 0)
            return 1;

        return 0;
    }
    private double CalcDotProduct(double i,double j)
    {
        double[] inputs = new double[] { i, j };
        double d = DotProduct(weights, inputs);
        if (d > 0)
            return 1;
        return 0;
    }
    private void UpdateWeights(int t)
    {
        double error = trainSet[t].output - CalcDotProduct(t);
        totalError += Mathf.Abs((float)error);
        for (int i = 0; i < weights.Length; i++)
        {
            weights[i] = weights[i] + error * trainSet[t].inputs[i];
        }
        bias += error;
    }
    void Train(int epochs)
    {
        InitiliazeWeights();
        for (int i = 0; i < epochs; i++)
        {
            totalError = 0;
            for (int t = 0; t < trainSet.Length; t++)
            {
                UpdateWeights(t);
                //Debug.Log("W1: " + (weights[0]) + "W2: " + (weights[1]) + " B: " + bias);
            }
            //Debug.Log("Total Error: " + totalError);
        }
    }

}
