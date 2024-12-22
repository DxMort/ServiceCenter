using System.Collections.Generic;

namespace SetriceCenter.Metadata
{
    public class EntityDefinition
    {
        public string EntityName { get; set; } // Например, "Parts"
        public List<ColumnDefinition> Columns { get; set; } = new List<ColumnDefinition>();
    }
}
