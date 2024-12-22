namespace SetriceCenter.Metadata
{
    public class ColumnDefinition
    {
        public string DisplayName { get; set; }   // Человекочитаемое имя
        public string PropertyName { get; set; }  // Название свойства модели
        public bool IsFilterable { get; set; }    // Можно ли фильтровать
        public bool IsEditable { get; set; }      // Можно ли редактировать
        public bool IsPrimaryKey { get; set; }    // Является ли первичным ключом
    }

}
