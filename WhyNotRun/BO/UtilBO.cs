using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace WhyNotRun.BO
{
    public static class UtilBO
    {
        public const string OBJECT_ID_REGEX = "^[0-9a-fA-F]{24}$";


        public static ObjectId ToObjectId(this string valor)
        {
            if (ValidarRegex(valor, OBJECT_ID_REGEX))
            {
                return new ObjectId(valor);
            }
            throw new InvalidCastException("A string à ser convertida não é um ObjectId válido.");
        }

        public static bool ValidarRegex(string valor, string pattern)
        {
            var match = Regex.Match(valor, pattern, RegexOptions.IgnoreCase);
            return match.Success;
        }




    }
}