#include <bits/stdc++.h>

using namespace std;

string ltrim(const string &);
string rtrim(const string &);
vector<string> split(const string &);

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

long minimumPasses(long m, long w, long p, long n)
{
    long passes = 0;
    long candy = 0;
    long step = 0;
    long run = LLONG_MAX;

    while (candy < n) 
    {
        step = (m > LLONG_MAX / w) ? 0 : (p - candy) / (m * w);

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

        if (step * m > LLONG_MAX / w)
        {
            candy = LLONG_MAX;
        }
        else
        {
            candy += step * m * w;
            run = min(run, long(passes + ceil((n - candy) / (m * w * 1.0))));
        }
    }

    return min(passes, run);
}
