using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Text;
using System;

class Result
{

    /*
     * Complete the 'minimumPasses' function below.
     *
     * The function is expected to return a LONG_INTEGER.
     * The function accepts following parameters:
     *  1. LONG_INTEGER m
     *  2. LONG_INTEGER w
     *  3. LONG_INTEGER p
     *  4. LONG_INTEGER n
     */

    public static long minimumPasses(long machines, long workers, long production, long n)
    {
        long totalPasses = 0;
        long producedCandy = 0;
        long productionPerStep = 0;
        long minTotalPasses = long.MaxValue;

        while (producedCandy < n)
        {
            productionPerStep = (machines > long.MaxValue / workers)
                ? 0 : (production - producedCandy) / (machines * workers);

            if (productionPerStep <= 0)
            {
                long machinesAndWorkers = producedCandy / production;

                if (machines >= workers + machinesAndWorkers)
                {
                    workers += machinesAndWorkers;
                }
                else if (workers >= machines + machinesAndWorkers)
                {
                    machines += machinesAndWorkers;
                }
                else
                {
                    long total = machines + workers + machinesAndWorkers;
                    machines = total / 2;
                    workers = total - machines;
                }

                producedCandy %= production;
                productionPerStep = 1;
            }

            totalPasses += productionPerStep;

            if (productionPerStep * machines > long.MaxValue / workers)
            {
                producedCandy = long.MaxValue;
            }
            else
            {
                producedCandy += productionPerStep * machines * workers;
                minTotalPasses = Math.Min(minTotalPasses, totalPasses + 
                (long)Math.Ceiling((double)(n - producedCandy) / (machines * workers)));
            }
        }

        return Math.Min(totalPasses, minTotalPasses);
    }
}


class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        string[] firstMultipleInput = Console.ReadLine().TrimEnd().Split(' ');

        long m = Convert.ToInt64(firstMultipleInput[0]);

        long w = Convert.ToInt64(firstMultipleInput[1]);

        long p = Convert.ToInt64(firstMultipleInput[2]);

        long n = Convert.ToInt64(firstMultipleInput[3]);

        long result = Result.minimumPasses(m, w, p, n);

        textWriter.WriteLine(result);

        textWriter.Flush();
        textWriter.Close();
    }
}
