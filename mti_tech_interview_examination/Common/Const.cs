﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mti_tech_interview_examination.Common
{
    /// <summary>
    /// Common constant values
    /// </summary>
    public class Const
    {
        public const int MaxAge = 55;
        public const int MinAge = 18;
    }

    /// <summary>
    /// Session keys
    /// </summary>
    public class SessionKey
    {
        /// <summary>
        /// Candidate
        /// </summary>
        public const string Candidate = "Candidate";

        /// <summary>
        /// User name
        /// </summary>
        public const string UserName = "UserName";
    }

    /// <summary>
    /// Define dynamic values
    /// </summary>
    public static class Generator
    {
        /// <summary>
        /// List of available born yeas
        /// </summary>
        public static List<string> BirthYears
        {
            get
            {
                List<string> result = new List<string>();

                //Get min/max born year
                int currentYear = DateTime.Now.Year;
                int minBornYear = currentYear - Const.MaxAge;
                int bornYear = currentYear - Const.MinAge;

                //Add list from min born year to max born year
                while(minBornYear < bornYear)
                {
                    result.Add(bornYear.ToString());
                    bornYear--;
                }
                return result;
            }
        }
    }
}