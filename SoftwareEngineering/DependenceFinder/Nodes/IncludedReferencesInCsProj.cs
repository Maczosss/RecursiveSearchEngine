﻿using System.Collections.Generic;

namespace DependenceFinder.Nodes
{
    class IncludedReferencesInCsProj
    {
        public List<string> IncludedReferences { get; set; }
        public string InFile { get; set; }


        public IncludedReferencesInCsProj()
        {

        }
    }
}
