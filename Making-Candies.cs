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

    public static long minimumPasses(long m, long w, long p, long n)
    {
        long passes = 0;
        long candy = 0;
        long step = 0;
        long minTotalPasses = long.MaxValue;

        while (candy < n)
        {
            step = (m > long.MaxValue / w) ? 0 : (p - candy) / (m * w);

            if (step <= 0)
            {
                long mw = candy / p;

                if (m >= w + mw)
                {
                    w += mw;
                }
                else if (w >= m + mw)
                {
                    m += mw;
                }
                else
                {
                    long total = m + w + mw;
                    m = total / 2;
                    w = total - m;
                }

                candy %= p;
                step = 1;
            }

            passes += step;

            if (step * m > long.MaxValue / w)
            {
                candy = long.MaxValue;
            }
            else
            {
                candy += step * m * w;
                minTotalPasses = Math.Min(minTotalPasses,
                    passes + (long)Math.Ceiling((double)(n - candy) / (m * w)));
            }
        }

        return Math.Min(passes, minTotalPasses);
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
