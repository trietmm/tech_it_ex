using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static mti_tech_interview_examination.Models.CommonModel;

namespace mti_tech_interview_examination.Lib.Execute
{
    public static class Extention
    {
        private static Random rng = new Random();
        public static List<int> RandomList(this List<int> list, int Num)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                int value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
            return list.Take(Num).ToList();
        }
    }
}