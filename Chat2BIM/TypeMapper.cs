using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Chat2BIM
{
    public class TypeMapper
    {
        public static string mapType(string input)
        {
            input = input.ToLower();
            switch (input)
            {
                case "wand":
                    return "IfcWall";
                case "tür":
                    return "IfcDoor";
                case "platte":
                    return "IfcSlab";
                case "fenster":
                    return "IfcWindow";
                case "abdeckung":
                    return "IfcCovering";
                case "decke":
                    return "IfcRoof";
                case "treppe":
                    return "IfcStair";
            }
            return "";
        }
    }
}