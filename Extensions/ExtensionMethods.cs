using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Extensions;

public static class ExtensionMethods
{
    public static string RightOf(this string src, char s, int occurence)
    {
        string output = src;

        while (--occurence >= 0)
        {
            output = output.RightOf(s.ToString());
        }

        return output;
    }

    public static string RightOf(this string src, string s)
    {
        string output = String.Empty;
        int idx = src.IndexOf(s);

        if (idx != -1)
        {
            output = src.Substring(idx, s.Length);
        }

        return output;
    }

    public static string LeftOf(this string src, string s)
    {
        string output = src;
        int idx = src.IndexOf(s);

        if (idx != -1)
        {
            output = src[..idx];
        }

        return output;
    }

    public static string LeftOfRight(this string src, char s, int occurence) {
        string output = src;

        while (occurence-- > 0)
        {
            output = output.LeftOfRight(s.ToString());
        }

        return output;
    }

    public static string LeftOfRight(this string src, string s)
    {
        string output = src;
        int idx = src.IndexOf(s);
        int _idx = idx;

        while (_idx != -1)
        {
            _idx = src.IndexOf(s, idx + s.Length);

            if (_idx != -1)
            {
                idx = _idx;
            }
        }

        output = src[..idx];

        return output;
    }
}
