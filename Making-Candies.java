import java.io.*;
import java.math.*;
import java.security.*;
import java.text.*;
import java.util.*;
import java.util.concurrent.*;
import java.util.function.*;
import java.util.regex.*;
import java.util.stream.*;
import static java.util.stream.Collectors.joining;
import static java.util.stream.Collectors.toList;

class Result {

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

    public static long minimumPasses(long m, long w, long p, long n) {
        long passes = 0;
        long candy = 0;
        long step = 0;
        long minTotalPasses = Long.MAX_VALUE;

        while (candy < n) {
            step = (m > Long.MAX_VALUE / w) ? 0 : (p - candy) / (m * w);

            if (step <= 0) {
                long mw = candy / p;

                if (m >= w + mw) {
                    w += mw;
                } else if (w >= m + mw) {
                    m += mw;
                } else {
                    long total = m + w + mw;
                    m = total / 2;
                    w = total - m;
                }

                candy %= p;
                step = 1;
            }

            passes += step;

            if (step * m > Long.MAX_VALUE / w) {
                candy = Long.MAX_VALUE;
            } else {
                candy += step * m * w;
                minTotalPasses = Math.min(minTotalPasses, passes +
                    (long)Math.ceil((double)(n - candy) / (m * w)));
            }
        }

        return Math.min(passes, minTotalPasses);
    }
}


public class Solution {
    public static void main(String[] args) throws IOException {
        BufferedReader bufferedReader = new BufferedReader(new InputStreamReader(System.in));
        BufferedWriter bufferedWriter = new BufferedWriter(new FileWriter(System.getenv("OUTPUT_PATH")));

        String[] firstMultipleInput = bufferedReader.readLine().replaceAll("\\s+$", "").split(" ");

        long m = Long.parseLong(firstMultipleInput[0]);

        long w = Long.parseLong(firstMultipleInput[1]);

        long p = Long.parseLong(firstMultipleInput[2]);

        long n = Long.parseLong(firstMultipleInput[3]);

        long result = Result.minimumPasses(m, w, p, n);

        bufferedWriter.write(String.valueOf(result));
        bufferedWriter.newLine();

        bufferedReader.close();
        bufferedWriter.close();
    }
}
